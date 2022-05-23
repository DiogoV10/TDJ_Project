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
        private KeyboardManager km;

        public Player(Texture2D texture, KeyboardManager _km) : base(texture, _km)
        {
            Speed = 3f;
            km = _km;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            Movement();

            Position += Velocity;

            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - _texture.Width);

            Velocity = Vector2.Zero;
        }

        private void Movement()
        {
            if (Input == null)
                throw new Exception("Please give a value to 'Input'");

            if (km.IsKeyHeld(Input.Right))
                Velocity.X = Speed;
            else if (km.IsKeyHeld(Input.Left))
                Velocity.X = -Speed;

            km.Update();
        }
    }
}
