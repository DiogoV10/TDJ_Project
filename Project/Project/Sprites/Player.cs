using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Sprites
{
    class Player : Sprite
    {
        private KeyboardManager _km;
        private GameState _gm;


        public Player(Texture2D texture, KeyboardManager km) : base(texture, km)
        {
            Speed = 3f;
            _km = km;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            

            this.JumpHold = MathHelper.Clamp(this.JumpHold, 0, 50);


            Movement();

            this.Timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (this.Timer > 0.1)
            {
                this.PullForce += 1f;
                this.Timer = 0;
            }

            Jump();


            this.Velocity.Y += Gravity + this.PullForce;



            Collision(sprites);

            

            if (Position.Y < 0)
            {
                Game1.ChangeLevel(1, this.Position, this.Velocity, this.PullForce, this.JumpHold, this.HasJumped, this.IsRight, this.IsLeft, this.InAir);
            }

            if (Position.Y > Game1.ScreenHeight)
            {
                Game1.ChangeLevel(0, this.Position, this.Velocity, this.PullForce, this.JumpHold, this.HasJumped, this.IsRight, this.IsLeft, this.InAir);
            }



            Position += Velocity;

            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - _texture.Width);

            Velocity = Vector2.Zero;
        }

        private void Jump()
        {
            if (_km.IsKeyUp(Input.Jump) && !this.HasJumped && this.InAir == false)
            {
                
                if (_km.IsKeyHeld(Input.Right))
                {
                    this.IsRight = true;
                    this.IsLeft = false;
                }


                if (_km.IsKeyHeld(Input.Left))
                {
                    this.IsLeft = true;
                    this.IsRight = false;
                }

                this.HasJumped = true;

            }

            if (_km.IsKeyHeld(Input.Jump) && !this.HasJumped)
            {
                this.JumpHold += 1f;
            }


            if (this.HasJumped)
            {
                if (this.IsRight)
                    this.Velocity.X = Speed;

                if (this.IsLeft)
                    this.Velocity.X = -Speed;
            }

            if (this.HasJumped && this.JumpHold > 0)
            {
                this.Velocity.Y -= JumpPower;

                this.PullForce = 0;
                this.JumpHold--;
                
            }

            if (this.HasJumped && this.JumpHold == 0)
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
                    if (this.HasJumped)
                    {
                        this.IsRight = false;
                        this.IsLeft = true;
                    }                        
                    else
                        this.Velocity.X = 0;
                }

                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    if (this.HasJumped)
                    {
                        this.IsRight = true;
                        this.IsLeft = false;
                    }     
                    else
                        this.Velocity.X = 0;
                }

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) || this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                    this.PullForce = 0f;
                    this.HasJumped = false;
                    this.IsLeft = false;
                    this.IsRight = false;
                    this.InAir = false;
                }

                if (this.Velocity.Y > 0 && !this.IsTouchingTop(sprite) && !this.IsTouchingBottom(sprite) && !this.IsTouchingRight(sprite) && !this.IsTouchingLeft(sprite))
                    this.InAir = true;
            }
        }

        private void Movement()
        {
            
            if (Input == null)
                throw new Exception("Please give a value to 'Input'");


            if (_km.IsKeyHeld(Input.Right) && _km.IsKeyHeld(Input.Left) && !this.HasJumped && !_km.IsKeyHeld(Input.Jump) && !this.InAir)
                Velocity.X = 0;
            else if (_km.IsKeyHeld(Input.Right) && !this.HasJumped && !_km.IsKeyHeld(Input.Jump) && !this.InAir)
            {
                this.IsRight = true;
                this.IsLeft = false;
            }
                
            else if (_km.IsKeyHeld(Input.Left) && !this.HasJumped && !_km.IsKeyHeld(Input.Jump) && !this.InAir)
            {
                this.IsLeft = true;
                this.IsRight = false;
            }

            if (this.IsRight)
            {
                Velocity.X = Speed;
            }

            if (this.IsLeft)
            {
                Velocity.X = -Speed;
            }

            

            _km.Update();
        }
    }
}
