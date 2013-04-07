using System;
using Microsoft.Xna.Framework;
using TROFF.Play;

namespace TROFF.GameStates
{
    class PlayState : GameState
    {
        private readonly Player _current;
        private readonly Player _enemy;

        public PlayState(Player current, Player enemey)
        {
            _current = current;
            _enemy = enemey;
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

            _current.Update(gameTime);
            _enemy.Update(gameTime);

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
