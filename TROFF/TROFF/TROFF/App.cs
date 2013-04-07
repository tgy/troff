using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TROFF.GameStates;
using TROFF.Menu;
using TROFF.Play;

namespace TROFF
{
    public class App : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public App()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Data.GameFocus = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.Initialize(Content);
            Fonts.Initialize(Content);
            _graphics.PreferredBackBufferWidth = 801;
            _graphics.PreferredBackBufferHeight = 601;
            _graphics.ApplyChanges();
            Data.Ww = Window.ClientBounds.Width;
            Data.Wh = Window.ClientBounds.Height;

            Data.PKs = Keyboard.GetState();
            Data.Ks = Keyboard.GetState();

            #region menu-creation

            MenuButton comeBackButton = new MenuButton(Textures.ComeBack);
            comeBackButton.Click = ComeBack;

            MenuButton homeMenuHelpButton = new MenuButton(Textures.Help);
            MenuButton homeMenuCreateButton = new MenuButton(Textures.Create);
            MenuButton homeMenuJoinButton = new MenuButton(Textures.Join);
            MenuButton homeMenuQuitButton = new MenuButton(Textures.Quit);
            MenuState homeMenu = new MenuState(new List<MenuItem> { homeMenuHelpButton, homeMenuCreateButton, homeMenuJoinButton, homeMenuQuitButton }, Textures.MenuBackground, 1);
            homeMenu.SetPositions(230);

            MenuTextBox createMenuNameTextBox = new MenuTextBox("Give me your name or I kill you");
            MenuButton createMenuCreate = new MenuButton(Textures.Create);
            MenuState createMenu = new MenuState(new List<MenuItem> { createMenuNameTextBox, createMenuCreate }, Textures.MenuBackground);
            createMenu.SetPositions(280);

            MenuTextBox joinMenuNameTextBox = new MenuTextBox("Give me your name or I kill you");
            MenuTextBox joinMenuIpTextBox = new MenuTextBox("IP Adress of your friend");
            MenuButton joinMenuJoin = new MenuButton(Textures.Join);
            MenuState joinMenu = new MenuState(new List<MenuItem> { joinMenuNameTextBox, joinMenuIpTextBox, joinMenuJoin }, Textures.MenuBackground);
            joinMenu.SetPositions(250);

            MenuState helpMenu = new MenuState(new List<MenuItem> { comeBackButton }, Textures.HelpBackground);
            helpMenu.SetPositions(410);

            homeMenuHelpButton.SubMenu = helpMenu;
            homeMenuHelpButton.Click = RollOut;
            homeMenuCreateButton.SubMenu = createMenu;
            homeMenuCreateButton.Click = RollOut;
            homeMenuJoinButton.SubMenu = joinMenu;
            homeMenuJoinButton.Click = RollOut;
            homeMenuQuitButton.Click = Quit;

            createMenuCreate.Click = Create;
            joinMenuJoin.Click = Join;

            #endregion

            Data.GameStates = new Stack<GameState>();
            Data.GameStates.Push(homeMenu);
            Data.GameStates.Peek().Initialize();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Data.GameFocus = IsActive;
            Data.Ks = Keyboard.GetState();

            if (Data.PKs.IsKeyDown(Keys.Escape) && Data.Ks.IsKeyUp(Keys.Escape) && Data.GameStates.Count > 1 && Data.GameStates.Peek() is MenuState)
                Data.GameStates.Pop();

            Data.GameStates.Peek().Update(gameTime);

            Data.PKs = Data.Ks;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Data.GameStates.Peek().Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        // Button onClick functions

        private static void ComeBack(MenuState m)
        {
            if (Data.GameStates.Count > 1)
                Data.GameStates.Pop();
        }

        private static void Quit(MenuState m)
        {
            Environment.Exit(0);
        }

        private static void RollOut(MenuState m)
        {
            Data.GameStates.Push(m);
            Data.GameStates.Peek().Initialize();
        }

        private static void Create(MenuState m)
        {
            Map.Initialize();
            string name = ((MenuTextBox) ((MenuState) Data.GameStates.Peek()).Items[0]).Value;
            if (name == "")
                return;
            var l = new LobbyState(name, true);
            l.Initialize();
            Data.GameStates.Push(l);
        }

        private static void Join(MenuState m)
        {
            Map.Initialize();
            string name = ((MenuTextBox)((MenuState)Data.GameStates.Peek()).Items[0]).Value;
            if (name == "")
                return;
            var l = new LobbyState(name, false);
            l.Initialize();
            Data.GameStates.Push(l);
        }
    }
}
