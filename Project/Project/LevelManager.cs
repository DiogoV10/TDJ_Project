using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Models;
using Project.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class LevelManager
    {
        int currentLevel=1;

        

        bool next = false;
        bool prev = false;
        bool lvlChanged = false;

        public void NextLevel(KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 Position, ref Vector2 Velocity, ref float PullForce, ref float JumpHold, ref bool HasJumped, ref bool IsRight, ref bool IsLeft, ref bool InAir)
        {
            currentLevel = MathHelper.Clamp(currentLevel, 1, 2);
            next = true;
            prev = false;
            lvlChanged = true;
            currentLevel++;
            LoadLevel(km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref Position, ref Velocity, ref PullForce, ref JumpHold, ref HasJumped, ref IsRight, ref IsLeft, ref InAir);
        }

        public void PreviousLevel(KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 Position, ref Vector2 Velocity, ref float PullForce, ref float JumpHold, ref bool HasJumped, ref bool IsRight, ref bool IsLeft, ref bool InAir)
        {
            currentLevel = MathHelper.Clamp(currentLevel, 1, 2);
            prev = true;
            next = false;
            lvlChanged = true;
            currentLevel--;
            LoadLevel(km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref Position, ref Velocity, ref PullForce, ref JumpHold, ref HasJumped, ref IsRight, ref IsLeft, ref InAir);
        }

        public void LoadLevel(KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 _position, ref Vector2 _velocity, ref float _pullForce, ref float _jumpHold, ref bool _hasJumped, ref bool _isRight, ref bool _isLeft, ref bool _inAir)
        {
            if (currentLevel == 1)
                _sprites = Level1(km, ScreenWidth, ScreenHeight, Content, ref _position);

            if (currentLevel == 2)
                _sprites = Level2(km, ScreenWidth, ScreenHeight, Content, ref _position);


            if (next)
            {
                var playerTexture = Content.Load<Texture2D>("Jump Knight Idle");

                _sprites.Add(new Player(playerTexture, km)
                {
                    Position = new Vector2(_position.X, ScreenHeight - (playerTexture.Height / 2) - 100),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 5f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                });

                next = false;
            }

            if (prev)
            {
                var playerTexture = Content.Load<Texture2D>("Jump Knight Idle");

                _sprites.Add(new Player(playerTexture, km)
                {
                    Position = new Vector2(_position.X, 10),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 5f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                });

                prev = false;
            }

            if (!prev && !next && !lvlChanged)
            {
                var playerTexture = Content.Load<Texture2D>("Jump Knight Idle");

                _sprites.Add(new Player(playerTexture, km)
                {
                    Position = new Vector2(100,100),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 5f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                });
            }

        }

        private static List<Sprite> Level1(KeyboardManager km, int ScreenWidth, int ScreenHeight, ContentManager Content,ref Vector2 Position)
        {

            List<Sprite> _sprites;


            _sprites = new List<Sprite>()
                {
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(0,400)},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(600,200)},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-600,200),},
                    
                };
            return _sprites;
        }

        private static List<Sprite> Level2(KeyboardManager km, int ScreenWidth, int ScreenHeight, ContentManager Content,ref Vector2 Position)
        {
            List<Sprite> _sprites;

            _sprites = new List<Sprite>()
                {
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(0,400)},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(700,250)},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-700,250),},

                };
            return _sprites;
        }
    }
}
