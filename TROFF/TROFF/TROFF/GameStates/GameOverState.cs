using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TROFF.Play;

namespace TROFF.GameStates
{
    class GameOverState : GameState
    {
        private readonly string _textDisplayed;
        private readonly Color _textColor;

        public GameOverState(string winner, byte id)
        {
            _textDisplayed = winner.ToUpper();
            if (_textDisplayed != "TIE")
                _textDisplayed += " wins";

            _textColor = id == 2 ? new Color(255, 216, 0) : new Color(0, 255, 210);
        }

        public override void Update(GameTime gameTime)
        {
            if (Data.PKs.IsKeyDown(Keys.Enter) && Data.Ks.IsKeyUp(Keys.Enter) || Data.PKs.IsKeyDown(Keys.Escape) && Data.Ks.IsKeyUp(Keys.Escape))
                while (Data.GameStates.Count > 1)
                    Data.GameStates.Pop();
            
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.GameOverBackground, new Rectangle(0, 0, Textures.GameOverBackground.Width, Textures.GameOverBackground.Height), Color.White);

            Vector2 stringMeasure = Fonts.Trebuchet60Bold.MeasureString(_textDisplayed);
            spriteBatch.DrawString(Fonts.Trebuchet60Bold, _textDisplayed, new Vector2((int)((Data.Ww - stringMeasure.X) / 2), 440 - (int)(stringMeasure.Y/2)), _textColor);

            base.Draw(spriteBatch);
        }
    }
}
