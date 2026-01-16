using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttack
{
    public class BulletManager
    {
                /// <summary>
        /// Texture des projectiles
        /// </summary>
        private Texture2D _bulletTex;

        /// <summary>
        /// Liste des projectiles
        /// </summary>
        private List<Bullet> _bullets = new List<Bullet>();

        /// <summary>
        /// Getter de _bullets
        /// </summary>
        public List<Bullet> Bullets
        {
            get { return _bullets; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Tex"></param>
        public BulletManager(Texture2D Tex) 
        {
            _bulletTex = Tex;
        }

        /// <summary>
        /// mise à jour des projectiles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // parcours la liste a l'envers 
            for (int i = _bullets.Count -1; i >= 0; i--)
            {
                // met à jour les projectiles
                _bullets[i].Update(gameTime);

                // si les projectiles sortent de l'ecran on les supprime
                if (_bullets[i].Pos.Y < -20 - _bullets[i].Tex.Height)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Tir de projectile
        /// </summary>
        /// <param name="playerPos"></param>
        public void Shoot(Vector2 playerPos)
        {
            // instance d'un projectile
            Bullet projectile = new Bullet(_bulletTex, playerPos);
            _bullets.Add(projectile);
        }

        /// <summary>
        /// Affichage des projectiles
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // afficher chaque projectile dans la liste bullets
            foreach(Bullet bullet in _bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}