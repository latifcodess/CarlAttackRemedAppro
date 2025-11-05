using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac
{
    public class EnemyManager
    {
        /// <summary>
        /// Liste des ennemis
        /// </summary>
        private List<Enemy> _enemies = new List<Enemy>();

        /// <summary>
        /// Texture des ennemis
        /// </summary>
        private Texture2D _enemyTex;

        /// <summary>
        /// Instance aléatoire
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// Timer
        /// </summary>
        private float _spawnTimer = 0f;

        /// <summary>
        /// Intervale de spawn
        /// </summary>
        private float _spawnInterval = 0.5f;

        /// <summary>
        /// Getter de _enemies
        /// </summary>
        public List<Enemy> Enemies
        {
            get { return _enemies; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture">Texture de l'ennemi</param>
        public EnemyManager(Texture2D texture) 
        {
            _enemyTex = texture;
        }

        /// <summary>
        /// Apparition des ennemis
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // temp passer depuis le dernier update de Game1
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Incremente le temp passer depuis le dernier spawn d'ennemi
            _spawnTimer += time;

            // si le timer est plus grand que l'interval : on fait un spawn un ennemi et on met le timer à 0
            if (_spawnTimer >= _spawnInterval)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }

            // parcourir la liste _enemies a l'envers
            for (int i = _enemies.Count - 1; i>= 0; i--)
            {
                // mettre à jour les ennemis
                _enemies[i].Update(gameTime);

                // si l'ennemi sort de l'ecran on le supprime
                if(_enemies[i].Pos.Y > 1000)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Logique de l'apparition des ennemis
        /// </summary>
        public void SpawnEnemy()
        {
            int maxX = (int)(1920 - (_enemyTex.Width * 0.2f));

            // Position aléatoire en haut de la fenètre
            float x = _random.Next(0, maxX);
            Vector2 startPos = new Vector2(x, -_enemyTex.Height * 0.2f);

            // Vitesse entre et 10 et 150
            float speed = 100f + (float)_random.NextDouble() * 100f;

            // instance d'un nouvel ennemi dans la liste Enemis
            _enemies.Add(new Enemy(tex: _enemyTex, pos: startPos, speed: speed));
        }

        /// <summary>
        /// Affichage des ennemis
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // pour chaque ennemi dans la liste : l'afficher
            foreach (Enemy enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}