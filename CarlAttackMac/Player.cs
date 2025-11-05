using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac
{
    public class Player
    {
         /// <summary>
        /// Teexture du joueur
        /// </summary>
        private Texture2D _tex;
        
        /// <summary>
        /// Position du joueur
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Vitesse de déplacement du joueur
        /// </summary>
        private float _speed;

        /// <summary>
        /// Getter de _pos
        /// </summary>
        public Vector2 pos 
        {  
            get { return _pos; } 
        }

        /// <summary>
        /// Getter de _tex
        /// </summary>
        public Texture2D tex
        {
            get { return _tex; }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="startPos"></param>
        public Player(Texture2D texture, Vector2 startPos) 
        {
            _tex = texture;
            _pos = startPos;
            _speed = 1200f;
        }

        /// <summary>
        /// Déplacement du joueur
        /// </summary>
        /// <param name="gameTime"></param>
        public void Move(GameTime gameTime)
        {
            // état du clavier
            KeyboardState keyboard = Keyboard.GetState();

            // temp passé depuis le dernier update de Game1
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // 
            Keys[] pressKey = keyboard.GetPressedKeys();

            foreach(Keys key in pressKey)
            {
                switch (key)
                {
                    // Aller à gauche
                    case Keys.A:
                        _pos.X -= _speed * time;
                        break;

                    // Aller à droite
                    case Keys.D:
                        _pos.X += _speed * time;
                        break;
                }
            }

            // si le joueur sort d'un côtés il apparait de l'autre 
            if (_pos.X >= 2000)
            {
                _pos.X = 0;  
            }
            else if (_pos.X <= -300)
            {
                _pos.X = 1670;
            }
        }

        /// <summary>
        /// Affichage du joueur
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