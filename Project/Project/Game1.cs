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

        //Textures
        private Texture2D backgroundTexture;

        //Screen Measures
        public static int ScreenWidth;
        public static int ScreenHeight;

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
            IsMouseVisible= true;

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
            
            //Load Background texture
            backgroundTexture = Content.Load<Texture2D>("Jump-King-Web-Banner-1000w");
        }


        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime, Content);

            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, ScreenWidth, ScreenHeight), new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

            _currentState.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
                
            base.Draw(gameTime);
        }

        public static void ChangeLevel(int level, int JumpCount, Vector2 Position, Vector2 Velocity, float PullForce, float JumpHold, bool HasJumped, bool IsRight, bool IsLeft, bool InAir)
        {
            GameState.ChangeLevel(level, JumpCount, Position, Velocity, PullForce, JumpHold, HasJumped, IsRight, IsLeft, InAir);
        }
    }
}
