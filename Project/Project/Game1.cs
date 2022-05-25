using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Models;
using Project.Sprites;
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

            // TODO: use this.Content to load your game content here

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

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);


            _spriteBatch.End();
                
            base.Draw(gameTime);
        }
    }
}
