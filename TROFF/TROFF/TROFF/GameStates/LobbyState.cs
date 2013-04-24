using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TROFF.Menu;
using TROFF.Play;

namespace TROFF.GameStates
{
    internal class LobbyState : GameState
    {
        private readonly MenuButton _readyButton;

        private readonly Player _current;
        private readonly Player _enemy;
        private bool _currentRdy;
        private bool _enemyRdy;
        private bool _showOnce;

        private readonly bool _initializer;
        private readonly TcpListener _listener;
        private TcpClient _client;
        private NetworkStream _stream;

        public LobbyState(string currentName, string ipAddress)
        {
            _readyButton = new MenuButton(Textures.Ready)
                {
                    Focused = true,
                    Click = Ready,
                    Area = new Rectangle((Data.Ww - Textures.Ready.Base.Width)/2, 400, Textures.Ready.Base.Width,
                                         Textures.Ready.Base.Height)
                };

            _initializer = ipAddress == null;

            _current = new Player(currentName, (byte) (_initializer ? 1 : 2), true);
            _enemy = new Player(null, (byte) (_initializer ? 2 : 1), false);

            if (_initializer)
            {
                try
                {
                    _listener = new TcpListener(IPAddress.Parse("0.0.0.0"), 4242);
                    _listener.Start();
                }
                catch (Exception)
                {
                    Data.PopToEnd();
                }
            }

            else
            {
                try
                {
                    _client = new TcpClient(ipAddress, 4242);
                    _stream = _client.GetStream();
                    SendString(_stream, _current.Name);
                    if (_stream.ReadByte() == 1)
                    {
                        int lenght = _stream.ReadByte();
                        byte[] data = new byte[256];
                        _stream.Read(data, 0, lenght);
                        _enemy.Name = Encoding.ASCII.GetString(data, 0, lenght);
                    }
                }
                catch (Exception)
                {
                    Data.PopToEnd();
                }
            }

            _showOnce = false;
        }

        private static void Ready(MenuState m)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!_showOnce)
            {
                _showOnce = true;
                return;
            }
            if (Data.Ks.IsKeyUp(Keys.Enter) && Data.PKs.IsKeyDown(Keys.Enter))
            {
                _currentRdy = true;
                _stream.WriteByte(2);
            }

            if (_enemyRdy && _currentRdy)
            {
                PlayState p = new PlayState(_current, _enemy, _stream);
                Data.GameStates.Push(p);
                Data.GameStates.Peek().Initialize();
            }

            if (_initializer && _enemy.Name == null)
            {
                
                _client = _listener.AcceptTcpClient();
                _stream = _client.GetStream();
                if (_stream.ReadByte() == 1)
                {
                    int lenght = _stream.ReadByte();
                    byte[] data = new byte[256];
                    _stream.Read(data, 0, lenght);
                    _enemy.Name = Encoding.ASCII.GetString(data, 0, lenght);
                }
                SendString(_stream, _current.Name);
            }

            if (_enemy.Name != null)
            {
                if (_stream.DataAvailable && _stream.ReadByte() == 2)
                    _enemyRdy = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.LobbyBackground, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(Fonts.Trebuchet16, _current.Name,
                                   new Vector2(145, 290),
                                   _current.Id == 2 ? new Color(255, 216, 0) : new Color(0, 255, 210));

            spriteBatch.DrawString(Fonts.Trebuchet16Italic, _enemy.Name ?? "Waiting...",
                                   new Vector2(485, 290),
                                   _enemy.Id == 2 ? new Color(255, 216, 0) : new Color(0, 255, 210));

            spriteBatch.DrawString(Fonts.Trebuchet16, "127.0.0.1",
                                   new Vector2(10, 10), new Color(0, 255, 210));

            _readyButton.Draw(spriteBatch);
        }

        private static void SendString(NetworkStream stream, string str)
        {
            byte[] data = Encoding.ASCII.GetBytes(str);

            stream.WriteByte(1);
            stream.WriteByte((byte) data.Length);
            stream.Write(data, 0, data.Length);
        }
    }
}
