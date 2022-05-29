using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;



namespace ProjectJogo
{
     public class EnemySpawn
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public bool isVisible = true;
        public Rectangle textureRectangle;
        public Texture2D gameover;
        public Rectangle overRectangle;

 

        Random random = new Random();
        int randX, randY;


        public EnemySpawn(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            //Velocidade em que os inimigos percorrem o Mapa
            randY = random.Next(-4 , 4);
            randX = random.Next(-4, -1);

            velocity = new Vector2(randX, randY);

            textureRectangle.Height = 50;
            textureRectangle.Width = 50;

            overRectangle.Height = 200;
            overRectangle.Width = 200;

            textureRectangle.Location = newPosition.ToPoint();

        

        }

        //Auxilio do youtube
        //https://www.youtube.com/watch?v=_TlnUM-uhSI

        public void Update(GraphicsDevice graphics)
        {
            textureRectangle.Location += velocity.ToPoint();

            if (textureRectangle.Y <= 0 || textureRectangle.Y >= graphics.Viewport.Height - texture.Height)
            {
                velocity.Y = -velocity.Y;
            }
            if (textureRectangle.X < 0 - texture.Width)
            {
                isVisible = false;
            }

           

        }
        public void Draw(SpriteBatch spriteBatch)
        {       
            spriteBatch.Draw(texture, textureRectangle, Color.White);

        }
    }

}

