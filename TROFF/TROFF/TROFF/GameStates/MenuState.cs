using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TROFF.Menu;

namespace TROFF.GameStates
{
    public class MenuState : GameState
    {
        public List<MenuItem> Items;
        private int _selectedItemIndex;

        private int _menuPeriod;

        private readonly Texture2D _background;

        public MenuState(List<MenuItem> items, Texture2D background)
        {
            Items = items;
            _menuPeriod = 0;
            _selectedItemIndex = 0;

            _background = background;

            if (items.Count > 0)
                Items[0].Focused = true;
        }

        public MenuState(List<MenuItem> items, Texture2D background, int indexBegin)
        {
            Items = items;
            _menuPeriod = 0;
            _selectedItemIndex = indexBegin;

            _background = background;

            if (items.Count > 0)
                Items[indexBegin].Focused = true;
        }

        public void SetPositions(int startY)
        {
            foreach (var menuItem in Items)
                menuItem.SetPosition(0, 0);

            Items[0].SetPosition((Data.Ww - Items[0].Area.Width)/2, startY);

            for (var i = 1; i < Items.Count; i++)
            {
                var x = Items[i - 1].Area.X;
                if (Items[i - 1].Area.Width != Items[i].Area.Width)
                    x = x + (Items[i - 1].Area.Width - Items[i].Area.Width)/2;
                int z = Items[i - 1] is MenuTextBox && Items[i] is MenuTextBox ? 25 : 0;
                Items[i].SetPosition(x, Items[i - 1].Area.Y + Items[i - 1].Area.Height + 10 + z);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Data.GameFocus)
                return;

            Items[_selectedItemIndex].Focused = false;

            if (Data.PKs.IsKeyDown(Keys.Up) && (Data.Ks.IsKeyUp(Keys.Up) || _menuPeriod == Data.MenuPeriod))
            {
                if (_selectedItemIndex - 1 >= 0 && Items[_selectedItemIndex - 1].NoClick)
                    _selectedItemIndex = _selectedItemIndex - 2;
                else
                    _selectedItemIndex--;
                _menuPeriod = 0;
            }

            if (Data.PKs.IsKeyDown(Keys.Down) && (Data.Ks.IsKeyUp(Keys.Down) || _menuPeriod == Data.MenuPeriod))
            {
                if (_selectedItemIndex + 1 < Items.Count && Items[_selectedItemIndex + 1].NoClick)
                    _selectedItemIndex = _selectedItemIndex + 2;
                else
                    _selectedItemIndex++;
                _menuPeriod = 0;
            }

            if (Data.PKs.IsKeyDown(Keys.Enter) && Data.Ks.IsKeyUp(Keys.Enter) && !Items[_selectedItemIndex].NoClick)
            {
                if (Items[_selectedItemIndex].Click != null)
                    Items[_selectedItemIndex].Click(Items[_selectedItemIndex].SubMenu);
            }

            if (_selectedItemIndex < 0)
                _selectedItemIndex = Items.Count - 1;
            else if (_selectedItemIndex >= Items.Count)
                _selectedItemIndex = Items.IndexOf(Items.Find(x => x.NoClick == false));

            else
            {
                if (_selectedItemIndex < 0)
                    _selectedItemIndex = Items.IndexOf(Items.Find(x => x.NoClick == false));
                else if (_selectedItemIndex >= Items.Count)
                    _selectedItemIndex = Items.Count - 1;
            }

            Items[_selectedItemIndex].Focused = true;

            foreach (var menuItem in Items)
                menuItem.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            foreach (var menuItem in Items)
                menuItem.Draw(spriteBatch);
        }
    }
}
