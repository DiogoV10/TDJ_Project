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

        private static int _level = -1;

        private static Vector2 _position;
        private static Vector2 _velocity;

        private static float _pullForce;
        private static float _jumpHold;

        private static bool _isRight;
        private static bool _isLeft;
        private static bool _hasJumped;
        private static bool _inAir;


        //KeyboardManager
        private KeyboardManager km;
        private LevelManager lm;

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
            lm = new LevelManager();
            _sprites = new List<Sprite>();


            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

            

            lm.LoadLevel(km,ref _sprites,ref ScreenWidth,ref ScreenHeight, Content,ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir);

        }


        protected override void Update(GameTime gameTime)
        {
            if (_level == 1)
            {
                lm.NextLevel(km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir);
                _level = -1;
            }

            if (_level == 0)
            {
                lm.PreviousLevel(km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir);
                _level = -1;
            }

            if (_nextState != null)
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

        public static void ChangeLevel(int level, Vector2 Position, Vector2 Velocity, float PullForce, float JumpHold, bool HasJumped, bool IsRight, bool IsLeft, bool InAir)
        {
            _level = level;

            _position = Position;
            _velocity = Velocity;
            _pullForce = PullForce;
            _jumpHold = JumpHold;
            _hasJumped = HasJumped;
            _isRight = IsRight;
            _isLeft = IsLeft;
            _inAir = InAir;
        }
    }
}
