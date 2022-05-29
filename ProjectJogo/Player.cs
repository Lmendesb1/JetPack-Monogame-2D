using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace ProjectJogo
{
    public class Player
    {
        private Texture2D t2d;
        private Texture2D win;
        private Texture2D perdeu;
        private Texture2D vida;
        public Rectangle luigiRectangle;
        private Rectangle winRectangle;
        private Rectangle perdeuRectangle;
        private Rectangle vidaRectangle;
        bool isground = true;
        public int CountPoints=0;
        public bool ganhou = false;
        Game1 game;
        int gameOver = 0;
        List<EnemySpawn> enemies;
        KeyboardManager km;
        public Player(Game1 game, List<EnemySpawn> enemies, KeyboardManager km)
        {
            this.game = game;
            this.enemies = enemies;
            this.km = km;
           
             t2d = game.Content.Load<Texture2D>("Luigi");
            vida = game.Content.Load<Texture2D>("vidas");

            //Posição Inicial do Personagem
            luigiRectangle.X = 120;
            luigiRectangle.Y = 390;

            //Tamanho do Personagem
            luigiRectangle.Width = 100;
            luigiRectangle.Height = 90;

            winRectangle.Width = 800;
            winRectangle.Height = 480;

            perdeuRectangle.Width = 800;
            perdeuRectangle.Height = 480;

            vidaRectangle.Width = 120;
            vidaRectangle.Height = 70;

            vidaRectangle.X = 690;
            vidaRectangle.Y = 0;
            
        }


        public void Update(GameTime gameTime, SoundEffect effect, Song song)
        {

            //Funções para dar movimento ao personagem    

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                luigiRectangle.X += 4;
                t2d = game.Content.Load<Texture2D>("Luigi");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                luigiRectangle.X -= 4;
                t2d = game.Content.Load<Texture2D>("LuigiEsq");
            }

            if (km.IsKeyPressed(Keys.Space))
            {
                
                
                isground = false;
                effect.Play();                            
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                t2d = game.Content.Load<Texture2D>("LuigiFire");
                luigiRectangle.Y -= 7;
            }
                         
            if (isground == false)
            {
                luigiRectangle.Y += 3;
            }
            if (luigiRectangle.Y >= 390)
            {
                luigiRectangle.Y = 390;
                isground = true;
            }
            if(luigiRectangle.Y < 0)
            {
                luigiRectangle.Y = 0;
            }
            if(luigiRectangle.X > 700)
            {
                luigiRectangle.X = 700;
            }
            if(luigiRectangle.X < 0)
            {
                luigiRectangle.X = 0;
            }

            //Collider
            foreach (EnemySpawn i in enemies.ToArray())
            {
                if (luigiRectangle.Intersects(i.textureRectangle))
                {
                    enemies.Remove(i);
                    CountPoints++;
                }
                if (i.textureRectangle.X <= 0)
                { 
                    gameOver ++;
                    enemies.Remove(i);
                }
                if (gameOver == 1)
                {
                    vida = game.Content.Load<Texture2D>("vidas2");
                }
                if(gameOver == 2)
                {
                    vida = game.Content.Load<Texture2D>("vidas1");
                }
              
            }
            if (gameOver == 3)
                MediaPlayer.Stop();

        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(vida, vidaRectangle, Color.White);
            
            spriteBatch.Draw(t2d, luigiRectangle, Color.White);

            if (CountPoints >= 30)
            {
                MediaPlayer.Stop();
                ganhou = true;   
                win = game.Content.Load<Texture2D>("win");
                spriteBatch.Draw(win, winRectangle, Color.White);
            }
            if (gameOver >= 3)
            {

                perdeu = game.Content.Load<Texture2D>("gameOver");
                spriteBatch.Draw(perdeu, perdeuRectangle, Color.White);
            }
        }
    }
}
