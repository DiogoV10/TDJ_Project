using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Controls2;
using Project.Models;
using Project.Sprites;
using Project.States;
using System;
using System.Collections.Generic;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Screen Measures
        public static int ScreenWidth;
        public static int ScreenHeight;

        //KeyboardManager
        private KeyboardManager km;

        //Sprites
        private List<Sprite> _sprites;

        //States
        private State _currentState;
        private State _nextState;
        public void ChangeState(State state)
        {
            _nextState = state;
        }


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

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            var playerTexture = Content.Load<Texture2D>("Jump Knight Idle");


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
            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentState.Draw(gameTime, _spriteBatch);


            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);


            _spriteBatch.End();
                
            base.Draw(gameTime);
        }
    }
}
