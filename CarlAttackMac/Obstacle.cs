using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac
{
    public class Obstacle
    {
                /// <summary>
        /// Position de l'obstacle
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Texture de l'obstacle
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// État de l'obstacle
        /// </summary>
        private bool _active = true;

        /// <summary>
        /// Durée de vie de l'obstacle
        /// </summary>
        private float _lifetime = 5f; // durée de vie en secondes

        /// <summary>
        /// Getter de _active
        /// </summary>
        public bool Active 
        {  
            get { return _active; } 
        }

        /// <summary>
        /// Getter de _tex
        /// </summary>
        public Texture2D Tex
        {
            get {  return _tex; }
        }

        /// <summary>
        /// Getter de _pos
        /// </summary>
        public Vector2 Pos
        {
            get { return _pos; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture">Texture de l'obstacle</param>
        /// <param name="pos">Position de l'obstacle</param>
        public Obstacle(Texture2D texture, Vector2 pos)
        {
            _tex = texture;
            _pos = pos;
        }

        /// <summary>
        /// Mise à jour de l'obstacle
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Décrementer le temp passer dans le jeu en seconde
            _lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            // si la durée de vie est égal à 0 l'obstacle devient inactif
            if (_lifetime <= 0)
                _active = false;
        }

        /// <summary>
        /// Affichage de l'obstacle
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // si l'obstacle est actif on l'affiche
            if (_active)
                spriteBatch.Draw(texture: _tex,
                 position: _pos,
                 sourceRectangle: null,
                 color: Color.White,
                 rotation: 0f,
                 origin: Vector2.Zero,
                 scale: 0.25f,
                 effects: SpriteEffects.None,
                 layerDepth: 0f);
        }
    }
}