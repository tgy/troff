using Microsoft.Xna.Framework;
using TROFF.Play;

namespace TROFF.GameStates
{
    public class PlayState : GameState
    {
        private Map _map;
        private Player _current, _enemy;

        public override void Initialize()
        {
            _map = new Map();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _map.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(Textures.GameBackground, new Rectangle(0, 0, 801, 601), Color.White);
        }
    }
}
