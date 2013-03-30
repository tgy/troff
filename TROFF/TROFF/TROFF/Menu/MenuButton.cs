using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TROFF.Menu
{
    class MenuButton : MenuItem
    {
        public MenuButton(ButtonTextures tex)
        {
            NoClick = false;
            Tex = tex;
        }

        public override void SetPosition(int x, int y)
        {
            Area = new Rectangle(x, y, Tex.Base.Width, Tex.Base.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Focused ? Tex.Focus : Tex.Base, Area, Color.White);
        }
    }
}
