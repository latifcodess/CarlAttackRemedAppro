using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac
{
    public class Bullet
    {
                /// <summary>
        /// Texture d'un projectile
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// Position d'un projectile
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Vitesse d'un projectile
        /// </summary>
        private float _speed;

        /// <summary>
        /// état d'un projectileew
        /// </summary>
        private bool _active = true;

        /// <summary>
        /// Getter de _pos
        /// </summary>
        public Vector2 Pos
        {
            get { return _pos;  }
        }

        /// <summary>
        /// Getter de _tex
        /// </summary>
        public Texture2D Tex
        {
            get { return _tex; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tex">Texture du projectile</param>
        /// <param name="pos">position du projectile</param>
        public Bullet (Texture2D tex, Vector2 pos)
        {
            _tex = tex;
            _pos = pos;
            _speed = 1000;
        }

        /// <summary>
        /// Déplacement des projectiles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _pos.Y -= _speed * time;

            // si le projectile sort il devient inactif
            if (_pos.Y > 1000)
            {
                _active = false;
            }
        }

        /// <summary>
        /// Affichage des projectiles
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active) 
            {
                spriteBatch.Draw(texture: _tex,
                                 position: _pos,
                                 sourceRectangle: null,
                                 color: Color.White,
                                 rotation: 0f,
                                 origin: Vector2.Zero,
                                 scale: 1f,
                                 effects: SpriteEffects.None,
                                 layerDepth: 0f);
            }
        }
    }
}