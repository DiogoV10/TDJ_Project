using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.States
{
    class GameState : State
    {
        private Texture2D backgroundTexture;
        private Texture2D playerTexture;
        private List<Sprite> _sprites;
        private SpriteFont font;

        //Screen Measures
        public static int ScreenWidth;
        public static int ScreenHeight;

        //Variables
        private static int _level = -1;

        private static Vector2 _position;
        private static Vector2 _velocity;

        private static float _pullForce;
        private static float _jumpHold;

        private static bool _isRight;
        private static bool _isLeft;
        private static bool _hasJumped;
        private static bool _inAir;

        private static int _jumpCount = 0;

        private SoundEffect jump;

        //KeyboardManager
        private KeyboardManager km = new KeyboardManager();

        //LevelManager
        private LevelManager lm = new LevelManager();


        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            ScreenWidth=800;
            ScreenHeight=480;

            //Load Textures
            backgroundTexture = content.Load<Texture2D>("Forest");
            playerTexture = content.Load<Texture2D>("Jump Knight Idle");
            font = _content.Load<SpriteFont>("Fonts/Font");

            jump = content.Load<SoundEffect>("Jump Retro");

            //Sprites
            _sprites = new List<Sprite>();

            lm.LoadLevel(_game, km, ref _sprites, ref ScreenWidth, ref ScreenHeight, content, ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir, ref _jumpCount);

        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime, ContentManager Content)
        {

            if (_level == 1)
            {
                lm.NextLevel(_game, km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir, ref _jumpCount);
                _level = -1;
            }

            if (_level == 0)
            {
                lm.PreviousLevel(_game, km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref _position, ref _velocity, ref _pullForce, ref _jumpHold, ref _hasJumped, ref _isRight, ref _isLeft, ref _inAir, ref _jumpCount);
                _level = -1;
            }

            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites, jump);
            }
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, ScreenWidth, ScreenHeight), new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);


            spriteBatch.DrawString(font, " "+_jumpCount, new Vector2(ScreenWidth / 2, 10), Color.White);

        }

        public static void ChangeLevel(int level, int JumpCount, Vector2 Position, Vector2 Velocity, float PullForce, float JumpHold, bool HasJumped, bool IsRight, bool IsLeft, bool InAir)
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
            _jumpCount = JumpCount;
        }
    }
}
