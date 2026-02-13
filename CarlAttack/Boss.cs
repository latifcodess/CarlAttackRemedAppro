using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttack
{
    public class Boss
    {
        public Vector2 Pos;
        private Texture2D _tex;
        private float _speed = 50f;
        private int _health = 50;

        public Boss(Texture2D tex, Vector2 pos)
        {
            _tex = tex;
            Pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Pos.Y < 100)
                Pos.Y += _speed * time;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tex, Pos, null, Color.Red, 0f,
                Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
        }

        public void TakeDamage()
        {
            _health--;

            if (_health <= 0)
            {
                // boss mort (à gérer plus tard)
            }
        }
    }
}