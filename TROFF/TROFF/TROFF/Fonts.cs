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
        public static SpriteFont Trebuchet12, Trebuchet16, Trebuchet16Italic, Trebuchet60Bold;

        public static void Initialize(ContentManager content)
        {
            Trebuchet12 = content.Load<SpriteFont>("Trebuchet12");
            Trebuchet16 = content.Load<SpriteFont>("Trebuchet16");
            Trebuchet16Italic = content.Load<SpriteFont>("Trebuchet16Italic");
            Trebuchet60Bold = content.Load<SpriteFont>("Trebuchet60Bold");
        }
    }
}
