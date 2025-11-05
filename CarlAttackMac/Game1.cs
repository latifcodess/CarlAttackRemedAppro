using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CarlAttackMac;

    public class Game1 : Game
    {
        enum GameState {Playing, GameOver }
        GameState State = GameState.Playing;

        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        Texture2D playerTex;
        Texture2D bgTex;
        Texture2D bulletTex;
        Texture2D enemyTex;
        float shootTimer = 0f;
        double shootInterval = 0.25f;
        EnemyManager enemyManager;
        Player carl;
        BulletManager bulletManager;
        Texture2D gameOverTex;
        ObstacleManager obstacleManager;
        Texture2D obstacleTex;
        int score = 0;
        SpriteFont font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            // taille de la fenètre
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTex = Content.Load<Texture2D>("carl");
            bgTex = Content.Load<Texture2D>("mcd-store");
            bulletTex = Content.Load<Texture2D>("fork");
            enemyTex = Content.Load<Texture2D>("burger");
            gameOverTex = Content.Load<Texture2D>("gameover");
            obstacleTex = Content.Load<Texture2D>("dumbbell");

            // instance du joueur
            carl = new Player(texture: playerTex, startPos: new Vector2((_graphics.PreferredBackBufferWidth - (playerTex.Width * 0.2f)) / 2, 700));

            // instance de la logique des ennemis
            enemyManager = new EnemyManager(texture: enemyTex);

            // instance de la logique des projectiles
            bulletManager = new BulletManager(Tex: bulletTex);

            // instance de la logique des obstacles
            obstacleManager = new ObstacleManager(texture: obstacleTex);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // appelle de la méthode Move de la classe Player pour pouvoir se deplacer
            carl.Move(gameTime);

            // mise à jour des ennemis
            enemyManager.Update(gameTime);

            // état de la souris
            KeyboardState keyboard = Keyboard.GetState();
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            shootTimer += time;

            // si la touche espace est pressée et que le timer est plus grand ou égal à l'interval
            // on instancie un objet de type Bullet et on l'ajoute dans la liste Bullets
            if (keyboard.IsKeyDown(Keys.Space) && shootTimer >= shootInterval) 
            {
                bulletManager.Shoot(carl.pos);

                // timer remit à 0
                shootTimer = 0f;
            }

            // met à jour les projectiles
            bulletManager.Update(gameTime);

            // si la touche B est pressée et que le timer est plus grand ou égal à l'interval
            // on place un obstacle
            if (keyboard.IsKeyDown(Keys.B) && shootTimer >= shootInterval)
            {
                Vector2 spawnPos = carl.pos;
                float distance = 400f;

                spawnPos.Y -= distance;

                obstacleManager.PlaceObstacle(spawnPos);

                shootTimer = 0f;
            }

            // met à jour les obstacles
            obstacleManager.Update(gameTime);

            // instance d'un rectangle pour le joueur
            Rectangle playerRect = new Rectangle(
                (int)carl.pos.X,
                (int)carl.pos.Y,
                (int)(carl.tex.Width * 0.2f),
                (int)(carl.tex.Height * 0.2f)
            );

            // instance d'un rectangle pour chaques ennemis
            foreach(Enemy enemy in enemyManager.Enemies)
            {
                Rectangle enemyRect = new Rectangle(
                    (int)enemy.Pos.X,
                    (int)enemy.Pos.Y,
                    (int)(enemy.Tex.Width * 0.1f),
                    (int)(enemy.Tex.Height * 0.1f)
                );

                // Game Over si collisions entre joueur et ennemis
                if (playerRect.Intersects(enemyRect))
                {
                    State = GameState.GameOver;
                    break;
                }
            }
            // instance d'un rectangle pour chaques projectile
            for (int i = bulletManager.Bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bulletManager.Bullets[i];

                Rectangle bulletRect = new Rectangle(
                    (int)bullet.Pos.X,
                    (int)bullet.Pos.Y,
                    (int)(bullet.Tex.Width * 1f),
                    (int)(bullet.Tex.Height * 1f)
                    );

                // instance d'un rectangle pour chaques ennemis
                for (int j = enemyManager.Enemies.Count -1; j >= 0; j--)
                {
                    Enemy enemy = enemyManager.Enemies[j];

                    Rectangle enemyRect = new Rectangle(
                        (int)enemy.Pos.X,
                        (int)enemy.Pos.Y,
                        (int)(enemy.Tex.Width * 0.1f),
                        (int)(enemy.Tex.Height * 0.1f)
                    );
                   
                    // supprimer ennemis et projectiles si collisions
                    if (enemyRect.Intersects(bulletRect))
                    {
                        bulletManager.Bullets.Remove(bullet);
                        enemyManager.Enemies.Remove(enemy);

                        // incrementation du score
                        score += 1;
                    }
                }
            }

            // instance d'un rectangle pour chaques ennemis
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                Rectangle enemyRect = new Rectangle(
                    (int)enemy.Pos.X,
                    (int)enemy.Pos.Y,
                    (int)(enemy.Tex.Width * 0.1f),
                    (int)(enemy.Tex.Height * 0.1f)
                );

                // instance d'un rectangle pour chaques obstacles
                foreach (Obstacle obstacle in obstacleManager.Obstacles)
                {
                    Rectangle obstacleRect = new Rectangle(
                        (int)obstacle.Pos.X,
                        (int)obstacle.Pos.Y,
                        (int)(obstacle.Tex.Width * 0.25f),
                        (int)(obstacle.Tex.Height * 0.25f)
                    );

                    // si les ennemis rentre en collision avec un obstacle : ils s'arretent
                    if(enemyRect.Intersects(obstacleRect))
                    {
                        enemy.Speed = 0;
                    }
                }
            }

            // instance d'un rectangle pour chaques projectiles
            for (int i = bulletManager.Bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bulletManager.Bullets[i];

                Rectangle bulletRect = new Rectangle(
                    (int)bullet.Pos.X,
                    (int)bullet.Pos.Y,
                    (int)(bullet.Tex.Width * 1f),
                    (int)(bullet.Tex.Height * 1f)
                    );

                // instance d'un rectangle pour chaques obstacles
                for (int j = obstacleManager.Obstacles.Count - 1; j >= 0; j--)
                {
                    Obstacle obstacle = obstacleManager.Obstacles[j];

                    Rectangle obstacleRect = new Rectangle(
                        (int)obstacle.Pos.X,
                        (int)obstacle.Pos.Y,
                        (int)(obstacle.Tex.Width * 0.25f),
                        (int)(obstacle.Tex.Height * 0.25f)
                    );

                    // Si un projectile rentre en collision avec un obstacle : il disparrait
                    if(obstacleRect.Intersects(bulletRect))
                    {
                        bulletManager.Bullets.Remove(bullet);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
             
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            // image d'arriere plan
            _spriteBatch.Draw(texture: bgTex,
                        destinationRectangle: new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
                        color: Color.White);

            // image du joueur
            carl.Draw(spriteBatch: _spriteBatch);

            // image des ennemis
            enemyManager.Draw(spriteBatch: _spriteBatch);

            // image des projectiles
            bulletManager.Draw(spriteBatch: _spriteBatch);

            obstacleManager.Draw(spriteBatch: _spriteBatch);

            // image de gameover
            if (State == GameState.GameOver)
            {
                _spriteBatch.Draw(gameOverTex, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

