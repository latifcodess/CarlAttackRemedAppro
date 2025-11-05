using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac
{
    public class ObstacleManager
    {
                /// <summary>
        /// Texture de chaque projectiles
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// Liste contenant des obstacles
        /// </summary>
        private List<Obstacle> _obstacles = new List<Obstacle>();

        /// <summary>
        /// Getter de _obstacles
        /// </summary>
        public List<Obstacle> Obstacles
        {
            get { return _obstacles; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture"></param>
        public ObstacleManager(Texture2D texture)
        {
            _tex = texture;
        }

        /// <summary>
        /// Mise à jour des obstacles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Mettre à jour chaque obstacles 
            foreach (var obstacle in _obstacles)
                obstacle.Update(gameTime);

            // Supprime les obstacles expirés
            _obstacles.RemoveAll(o => !o.Active);
        }

        /// <summary>
        /// Affichage des obstacles
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Afficher chaque obstacles
            foreach (var obstacle in _obstacles)
                obstacle.Draw(spriteBatch);
        }

        /// <summary>
        /// Placement des obstacles
        /// </summary>
        /// <param name="position"></param>
        public void PlaceObstacle(Vector2 position)
        {
            // instance des obstacles
            _obstacles.Add(new Obstacle(_tex, position));
        }
    }
}