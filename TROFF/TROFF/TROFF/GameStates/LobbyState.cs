using System;
using System.Collections.Generic;
using System.Linq;
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

        public LobbyState(string currentName, bool initializer)
        {
            _readyButton = new MenuButton(Textures.Ready)
                {
                    Focused = true,
                    Click = Ready,
                    Area = new Rectangle((Data.Ww - Textures.Ready.Base.Width)/2, 400, Textures.Ready.Base.Width,
                                         Textures.Ready.Base.Height)
                };

            _current = new Player(currentName, (byte) (initializer ? 1 : 2));
            _enemy = new Player(null, (byte)(initializer ? 2 : 1));
        }

        private static void Ready(MenuState m)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Data.Ks.IsKeyUp(Keys.Enter) && Data.PKs.IsKeyDown(Keys.Enter))
                _currentRdy = true;

            // if !isset enemy and isset new client who sends a string, define it as the enemyplayer

            // if isset enemy, listen to a ready packet

            if (_enemyRdy && _currentRdy)
            {
                // create new playstate, start the game
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.LobbyBackground, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(Fonts.Trebuchet16, _current.Name,
                                   new Vector2(145, 290), _current.Id == 2 ? new Color(255, 216, 0) : new Color(0, 255, 210));

            spriteBatch.DrawString(Fonts.Trebuchet16Italic, _enemy.Name ?? "Waiting...",
                                new Vector2(485, 290), _enemy.Id == 2 ? new Color(255, 216, 0) : new Color(0, 255, 210));

            _readyButton.Draw(spriteBatch);
        }
    }
}
