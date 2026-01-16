using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttack
{
    public class Enemy
    {
                /// <summary>
        /// Texture de l'ennemi
        /// </summary>
        private Texture2D _tex;

        /// <summary>
        /// Position de l'ennemi
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Vitesse de l'ennemi
        /// </summary>
        private float _speed;

        /// <summary>
        /// Getter de _pos
        /// </summary>
        public Vector2 Pos
        {
            get { return _pos; }
        }

        /// <summary>
        /// Getter de _pos
        /// </summary>
        public Texture2D Tex
        {
            get { return _tex; }
        }

        /// <summary>
        /// Getter Setter de _speed
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tex">Texture</param>
        /// <param name="pos">Position</param>
        public Enemy(Texture2D tex, Vector2 pos, float speed)
        {
            _tex = tex;
            _pos = pos;
            _speed = speed;
        }

        /// <summary>
        /// Déplacement de l'ennemi
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // temp passer depuis le dernier update de Game1
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // incremente la vitesse * time à la position verticale pour que l'ennemi se deplace vers le bas à une certaine vitesse
            _pos.Y += _speed * time;
        }

        /// <summary>
        /// Affichage de l'ennemis
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: _tex,
                             position: _pos,
                             sourceRectangle: null,
                             color: Color.White,
                             rotation: 0f,
                             origin: Vector2.Zero,
                             scale: 0.2f,
                             effects: SpriteEffects.None,
                             layerDepth: 0f);
        }
    }
}