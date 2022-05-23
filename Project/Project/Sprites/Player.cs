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

        public Player(Texture2D texture, KeyboardManager km) : base(texture, km)
        {
            Speed = 3f;
            _km = km;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            Movement();

            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (_timer > 0.1)
            {
                _pullForce += 0.03f;
                _timer = 0;
            }

            this.Velocity.Y += Gravity + _pullForce;
 

            Collision(sprites);



            Position += Velocity;

            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - _texture.Width);

            Velocity = Vector2.Zero;
        }

        private void Collision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite) || this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    this.Velocity.X = 0;
                }


                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) || this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                    _pullForce = 0f;
                }
                
            }
        }

        private void Movement()
        {
            if (Input == null)
                throw new Exception("Please give a value to 'Input'");

            if (_km.IsKeyHeld(Input.Right) && _km.IsKeyHeld(Input.Left))
                Velocity.X = 0;
            else if (_km.IsKeyHeld(Input.Right))
                Velocity.X = Speed;
            else if (_km.IsKeyHeld(Input.Left))
                Velocity.X = -Speed;
            
            if (_km.IsKeyHeld(Input.Down))
                Velocity.Y = Speed;
            
            if (_km.IsKeyHeld(Input.Up))
                Velocity.Y = -Speed;

            _km.Update();
        }
    }
}
