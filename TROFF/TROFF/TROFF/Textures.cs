using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TROFF
{
    public static class Textures
    {
        public static Texture2D GameBackground,
                                MenuBackground,
                                GameOverBackground,
                                HelpBackground;

        public static Texture2D Color1, Color2;
        public static Texture2D Light1, Light2;

        public static ButtonTextures Create, Join, Quit, Help, ComeBack;

        public static void Initialize(ContentManager content)
        {
            GameBackground = content.Load<Texture2D>("GameBackground");
            MenuBackground = content.Load<Texture2D>("MenuBackground");
            GameOverBackground = content.Load<Texture2D>("GameOverBackground");
            HelpBackground = content.Load<Texture2D>("HelpBackground");

            Create = new ButtonTextures("Create", content);
            Join = new ButtonTextures("Join", content);
            Quit = new ButtonTextures("Quit", content);
            Help = new ButtonTextures("Help", content);
            ComeBack = new ButtonTextures("ComeBack", content);

            Color1 = content.Load<Texture2D>("Color1");
            Color2 = content.Load<Texture2D>("Color2");
            Light1 = content.Load<Texture2D>("Light1");
            Light2 = content.Load<Texture2D>("Light2");
        }
    }

    public class ButtonTextures
    {
        public Texture2D Base, Focus;

        public ButtonTextures(string p, ContentManager content)
        {
            Base = content.Load<Texture2D>(p);
            Focus = content.Load<Texture2D>(p + "Hover");
        }
    }
}
