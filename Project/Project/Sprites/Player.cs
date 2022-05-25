using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Sprites
{
    class Player : Sprite
    {
        private KeyboardManager _km;

        private float _pullForce = 0f;
        private float _timer = 0f;
        private float _jumpHold = 0f;

        private bool _hasJumped = false;
        private bool _isRight = false;
        private bool _isLeft = false;

        public Player(Texture2D texture, KeyboardManager km) : base(texture, km)
        {
            Speed = 3f;
            _km = km;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _jumpHold = MathHelper.Clamp(_jumpHold, 0, 50);

            Movement();

            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;



            if (_timer > 0.1)
            {
                _pullForce += 1f;
                _timer = 0;
            }

            Jump();


            this.Velocity.Y += Gravity + _pullForce;


            Collision(sprites);


            Position += Velocity;

            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - _texture.Width);

            Velocity = Vector2.Zero;
        }

        private void Jump()
        {
            if (_km.IsKeyUp(Input.Jump) && !_hasJumped)
            {
                _hasJumped = true;

                if (_km.IsKeyHeld(Input.Right))
                {
                    _isRight = true;
                    _isLeft = false;
                }


                if (_km.IsKeyHeld(Input.Left))
                {
                    _isLeft = true;
                    _isRight = false;
                }

            }


            if (_km.IsKeyHeld(Input.Jump) && !_hasJumped)
            {
                _jumpHold += 1f;
            }

            if (_hasJumped)
            {
                if (_isRight)
                    this.Velocity.X = Speed;

                if (_isLeft)
                    this.Velocity.X = -Speed;
            }

            if (_hasJumped && _jumpHold > 0)
            {
                this.Velocity.Y = -JumpPower;

                _pullForce = 0;
                _jumpHold--;
                
            }

            if (_hasJumped && _jumpHold == 0)
            {

                this.Velocity.Y = -JumpPower;
            }
        }

        private void Collision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                {
                    if (_hasJumped)
                    {
                        _isRight = false;
                        _isLeft = true;
                    }                        
                    else
                        this.Velocity.X = 0;
                }

                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    if (_hasJumped)
                    {
                        _isRight = true;
                        _isLeft = false;
                    }     
                    else
                        this.Velocity.X = 0;
                }

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) || this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                    _pullForce = 0f;
                    _hasJumped = false;
                    _isLeft = false;
                    _isRight = false;
                }
            }
        }

        private void Movement()
        {
            if (Input == null)
                throw new Exception("Please give a value to 'Input'");
   

            if (_km.IsKeyHeld(Input.Right) && _km.IsKeyHeld(Input.Left) && !_hasJumped && !_km.IsKeyHeld(Input.Jump))
                Velocity.X = 0;
            else if (_km.IsKeyHeld(Input.Right) && !_hasJumped && !_km.IsKeyHeld(Input.Jump))
                Velocity.X = Speed;
            else if (_km.IsKeyHeld(Input.Left) && !_hasJumped && !_km.IsKeyHeld(Input.Jump))
                Velocity.X = -Speed;
            
            if (_km.IsKeyHeld(Input.Down))
                Velocity.Y = Speed;
            
            if (_km.IsKeyHeld(Input.Up))
                Velocity.Y = -Speed;

            _km.Update();
        }
    }
}
