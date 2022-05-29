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

        public void NextLevel(Game1 game, KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 Position, ref Vector2 Velocity, ref float PullForce, ref float JumpHold, ref bool HasJumped, ref bool IsRight, ref bool IsLeft, ref bool InAir, ref int JumpCount)
        {
            next = true;
            prev = false;
            lvlChanged = true;
            currentLevel++;
            LoadLevel(game, km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref Position, ref Velocity, ref PullForce, ref JumpHold, ref HasJumped, ref IsRight, ref IsLeft, ref InAir, ref JumpCount);
        }

        public void PreviousLevel(Game1 game, KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 Position, ref Vector2 Velocity, ref float PullForce, ref float JumpHold, ref bool HasJumped, ref bool IsRight, ref bool IsLeft, ref bool InAir, ref int JumpCount)
        {
            prev = true;
            next = false;
            lvlChanged = true;
            currentLevel--;
            LoadLevel(game, km, ref _sprites, ref ScreenWidth, ref ScreenHeight, Content, ref Position, ref Velocity, ref PullForce, ref JumpHold, ref HasJumped, ref IsRight, ref IsLeft, ref InAir, ref JumpCount);
        }

        public void LoadLevel(Game1 game, KeyboardManager km, ref List<Sprite> _sprites, ref int ScreenWidth, ref int ScreenHeight, ContentManager Content, ref Vector2 _position, ref Vector2 _velocity, ref float _pullForce, ref float _jumpHold, ref bool _hasJumped, ref bool _isRight, ref bool _isLeft, ref bool _inAir, ref int _jumpCount)
        {
            var playerTexture = Content.Load<Texture2D>("Jump Knight Idle");


            if (currentLevel == 1)
                _sprites = Level1(km, ScreenWidth, ScreenHeight, Content, ref _position);

            if (currentLevel == 2)
                _sprites = Level2(km, ScreenWidth, ScreenHeight, Content, ref _position);

            if (currentLevel == 3)
                _sprites = Level3(km, ScreenWidth, ScreenHeight, Content, ref _position);


            if (next)
            {
                _sprites.Add(new Player(playerTexture, km, game)
                {
                    Position = new Vector2(_position.X, ScreenHeight - (playerTexture.Height / 2) - 100),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 4f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                    JumpCount = _jumpCount,
                });

                next = false;
            }

            if (prev)
            {
                _sprites.Add(new Player(playerTexture, km, game)
                {
                    Position = new Vector2(_position.X, 10),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 4f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                    JumpCount = _jumpCount,
                });

                prev = false;
            }

            if (!prev && !next && !lvlChanged)
            {
                _sprites.Add(new Player(playerTexture, km, game)
                {
                    Position = new Vector2(ScreenWidth-200,ScreenHeight-55),
                    Input = new Input()
                    {
                        Right = Keys.D,
                        Left = Keys.A,
                        Jump = Keys.Space,
                    },
                    Gravity = 1f,
                    JumpPower = 4f,
                    Velocity = _velocity,
                    PullForce = _pullForce,
                    JumpHold = _jumpHold,
                    HasJumped = _hasJumped,
                    IsRight = _isRight,
                    IsLeft = _isLeft,
                    InAir = _inAir,
                    JumpCount = _jumpCount,
                });
            }

        }


        //Level 1
        private static List<Sprite> Level1(KeyboardManager km, int ScreenWidth, int ScreenHeight, ContentManager Content,ref Vector2 Position)
        {
            List<Sprite> _sprites;


            _sprites = new List<Sprite>()
                {
                    new Sprite(Content.Load<Texture2D>("Ground"),km){ Position = new Vector2(0,ScreenHeight-20)},
                    new Sprite(Content.Load<Texture2D>("Platform1"),km){ Position = new Vector2(ScreenWidth-350,225)},
                    new Sprite(Content.Load<Texture2D>("Platform2"),km){ Position = new Vector2(-20,ScreenHeight-100),},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(50,ScreenHeight-300),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(ScreenWidth,0),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-ScreenWidth,0),},
                };
            return _sprites;
        }

        //Level 2
        private static List<Sprite> Level2(KeyboardManager km, int ScreenWidth, int ScreenHeight, ContentManager Content,ref Vector2 Position)
        {
            List<Sprite> _sprites;

            _sprites = new List<Sprite>()
                {
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(400,ScreenHeight-40)},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(700,300)},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(300,300),},
                    new Sprite(Content.Load<Texture2D>("Platform1"),km){ Position = new Vector2(-20,200),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(ScreenWidth,0),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-ScreenWidth,0),},

                };
            return _sprites;
        }

        //Level 3
        private static List<Sprite> Level3(KeyboardManager km, int ScreenWidth, int ScreenHeight, ContentManager Content, ref Vector2 Position)
        {
            List<Sprite> _sprites;

            _sprites = new List<Sprite>()
                {
                    //new Sprite(Content.Load<Texture2D>("Platform2"),km){ Position = new Vector2(ScreenWidth-100,ScreenHeight-400)},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(300,ScreenHeight-30)},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(500,ScreenHeight-30),},
                    new Sprite(Content.Load<Texture2D>("Platform1"),km){ Position = new Vector2(ScreenWidth-100,ScreenHeight-200),},
                    new Sprite(Content.Load<Texture2D>("Platform3"),km){ Position = new Vector2(ScreenWidth/2,ScreenHeight/2),},
                    new Sprite(Content.Load<Texture2D>("Platform1"),km){ Position = new Vector2(-10,200), Finish = true,},
                    //new Sprite(Content.Load<Texture2D>("Platform1"),km){ Position = new Vector2(-20,100),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(ScreenWidth,0),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(-ScreenWidth,0),},
                    new Sprite(Content.Load<Texture2D>("Background"),km){ Position = new Vector2(0,-ScreenHeight),},
                };
            return _sprites;
        }
    }
}
