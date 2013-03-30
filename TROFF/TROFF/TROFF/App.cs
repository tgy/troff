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

            PlayState p = new PlayState(true, "John");

            Data.GameStates = new Stack<GameState>();
            Data.GameStates.Push(p);
            Data.GameStates.Peek().Initialize();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Data.GameFocus = IsActive;
            Data.Ks = Keyboard.GetState();

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
    }
}
