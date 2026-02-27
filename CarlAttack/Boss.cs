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
        /// <summary>
        /// état mort ou pas
        /// </summary>
        public bool isDead = false;

        /// <summary>
        /// Position
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Texture
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// Vitesse
        /// </summary>
        private float _speed = 50f;

        /// <summary>
        /// Vie
        /// </summary>
        private int _health = 50;

        /// <summary>
        /// Getter de _tex
        /// </summary>
        public Texture2D Tex
        {
            get { return _tex; }
        }

        /// <summary>
        /// Getter Setter de _pos
        /// </summary>
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="tex">Texture</param>
        /// <param name="pos">Position</param>
        public Boss(Texture2D tex, Vector2 pos)
        {
            _tex = tex;
            _pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // deplacement progressive jusqu'a sa position finale
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

            // retire 5 point de vie
            _health -= 5;

            // si la vie est plus petite ou égal à 0
            if (_health <= 0)
            {
                // l'état mort ou pas est true
                isDead = true;
            }
        }
    }
}