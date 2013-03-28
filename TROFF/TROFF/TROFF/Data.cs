using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TROFF
{
    public static class Data
    {
        public static bool GameFocus;

        public static Stack<GameState> GameStates;

        public static KeyboardState PKs, Ks; // Previous and current keyboardstates

        public static int Ww, Wh; // Windows width and height
    }
}
