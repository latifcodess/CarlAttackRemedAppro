using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace CarlAttack
{
    public class Boss
    {
        public bool isDead = false;
        private Vector2 _pos;
        private Texture2D _tex;
        private float _speed = 50f;
        private int _health = 50;

        public Texture2D Tex
        {
            get { return _tex; }
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Boss(Texture2D tex, Vector2 pos)
        {
            _tex = tex;
            _pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_pos.Y < -50)
                _pos.Y += _speed * time;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _tex,
                _pos,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                scale: 1.2f,
                SpriteEffects.None,
                0f);
        }

        public void TakeDamage()
        {
            if (isDead)
            {
                return;
            }

            _health -= 5;

            if (_health <= 0)
            {
                isDead = true;
            }
        }
    }
}