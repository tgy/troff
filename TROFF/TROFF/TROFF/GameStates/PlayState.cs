using System;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using TROFF.Play;

namespace TROFF.GameStates
{
    class PlayState : GameState
    {
        private readonly Player _current;
        private readonly Player _enemy;
        private readonly NetworkStream _stream;

        public PlayState(Player current, Player enemey, NetworkStream stream)
        {
            _current = current;
            _enemy = enemey;
            _stream = stream;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void CheckOver()
        {
            if (!_current.Dead && !_enemy.Dead)
                return;

            string winner = (_current.Dead && _enemy.Dead) ? "Tie" : _current.Dead ? _enemy.Name : _current.Name;
            byte id = (_current.Dead && _enemy.Dead) ? (byte)0 : _current.Dead ? _enemy.Id : _current.Id;

            System.Threading.Thread.Sleep(500);
            Data.GameStates.Pop();
            Data.GameStates.Push(new GameOverState(winner, id));
        }

        public override void Update(GameTime gameTime)
        {
            Map.Update(gameTime);

            if (_stream.DataAvailable && _stream.ReadByte() == 1)
                _enemy.Direction = (Direction) _stream.ReadByte();

            _current.Update(gameTime, _stream);
            _enemy.Update(gameTime, _stream);

            CheckOver();

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(Textures.GameBackground, new Rectangle(0, 0, 801, 601), Color.White);
            _current.Draw(spriteBatch);
            _enemy.Draw(spriteBatch);

            Map.Draw(spriteBatch);
        }
    }
}
