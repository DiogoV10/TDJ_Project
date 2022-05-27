using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Controls2;
using Project.Models;
using Project.Sprites;
using System;
using System.Collections.Generic;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private KeyboardManager km;

        private List<Sprite> _sprites;

        //Button
        private Color _backgroundColour = Color.CornflowerBlue;

        private List<Component> _gameComponents;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            km = new KeyboardManager();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Buttons
            var randomButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 200),
                Text="Random",
            };

            randomButton.Click += RandomButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 250),
                Text = "Quit",
            };

            quitButton.Click += QuitButton_Click;

            _gameComponents = new List<Component>
            {
                randomButton,
                quitButton,
            };
            #endregion

            var playerTexture = Content.Load<Texture2D>("Ball");


            _sprites = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(0,400)},
                new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(600,200)},
                new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-600,200),},
                new Player(playerTexture,km)
                {
                    Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width / 2), (ScreenHeight / 2) - (playerTexture.Height / 2)),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Down = Keys.S,
                        Up = Keys.W,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 5f,
                },
                new Player(playerTexture,km)
                {
                    Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width / 2), (ScreenHeight / 2) - (playerTexture.Height / 2)-70),
                    Input = new Input()
                    {
                        Right = Keys.Right,
                        Left = Keys.Left,
                        Down = Keys.Down,
                        Up = Keys.Up,
                    },
                    Gravity = 1f,
                },
            };
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();

            _backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        protected override void Update(GameTime gameTime)
        {
            foreach(var component in _gameComponents)
            {
                component.Update(gameTime);
            }
            // TODO: Add your update logic here

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColour);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            foreach (var component in _gameComponents)
                component.Draw(gameTime, _spriteBatch);

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);


            _spriteBatch.End();
                
            base.Draw(gameTime);
        }
    }
}
