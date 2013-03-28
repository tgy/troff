using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TROFF
{
    public static class Fonts
    {
        public static SpriteFont Trebuchet12;

        public static void Initialize(ContentManager content)
        {
            Trebuchet12 = content.Load<SpriteFont>("Trebuchet12");
        }
    }
}
