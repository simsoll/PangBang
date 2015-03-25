#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PangBang.Collision;
using PangBang.Configuration;
using PangBang.Draw;
using PangBang.Entities;
using PangBang.Input;
using PangBang.Input.Keyboard;
using PangBang.Level;
using PangBang.Messaging.Caliburn.Micro;
using PangBang.Screen;
using PangBang.Text;

#endregion

namespace PangBang
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PangBang : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IEventAggregator _eventAggregator;
        private ScreenManager _screenManager;
        private KeyboardManager _keyboardManager;
        private PlayerInputManager _playerInputManager;
        private readonly IScreenConfiguration _screenConfiguration;
        private readonly ILevelConfiguration _levelConfiguration;

        public PangBang(IScreenConfiguration screenConfiguration, ILevelConfiguration levelConfiguration)
            : base()
        {
            _screenConfiguration = screenConfiguration;
            _levelConfiguration = levelConfiguration;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = _screenConfiguration.Width,
                PreferredBackBufferHeight = _screenConfiguration.Height
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _eventAggregator = new EventAggregator();

            var textTexture = GetPlain2DTexture(2);
            var texture = GetPlain2DTexture(1);
            var textDrawer = new Drawer(_spriteBatch, textTexture);
            var drawer = new Drawer(_spriteBatch ,texture);
            var pixelTextDrawer = new PixelTextDrawer(textDrawer);

            var levelFactory = new LevelFactory(_eventAggregator, _screenConfiguration, _levelConfiguration);
            var collisionManager = new CollisionManager(_eventAggregator);
            var levelManager = new LevelManager(_eventAggregator, levelFactory, drawer, collisionManager);

            var screenFactory = new ScreenFactory(_eventAggregator, pixelTextDrawer, levelManager);
            _screenManager = new ScreenManager(_eventAggregator, screenFactory);
            _screenManager.Load();

            _keyboardManager = new KeyboardManager(_eventAggregator, TimeSpan.FromMilliseconds(500));
            _keyboardManager.Load();

            _playerInputManager = new PlayerInputManager(_eventAggregator);
            _playerInputManager.Load();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            _playerInputManager.Unload();
            _keyboardManager.Unload();
            _screenManager.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _screenManager.Update(gameTime);
            _keyboardManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            _spriteBatch.Begin();

            _screenManager.Draw();

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private Texture2D GetPlain2DTexture(int textureSize)
        {
            var texture = new Texture2D(GraphicsDevice, textureSize, textureSize);
            var color = new Color[textureSize * textureSize];
            for (var i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }
            texture.SetData(color);

            return texture;
        }
    }
}
