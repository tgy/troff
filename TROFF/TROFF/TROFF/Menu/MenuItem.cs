using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TROFF.GameStates;

namespace TROFF.Menu
{
    public abstract class MenuItem
    {
        public delegate void OnClick(MenuState m);
        public OnClick Click;

        public MenuState SubMenu = new MenuState(new List<MenuItem>(), Textures.MenuBackground);

        public Rectangle Area;

        public ButtonTextures Tex;

        public bool Focused;

        public bool NoClick, FocusOnClick;

        public virtual void LoadContent(ContentManager content) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void SetPosition(int x, int y) { }
    }
}
