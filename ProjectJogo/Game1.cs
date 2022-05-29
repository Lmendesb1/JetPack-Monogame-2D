using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;




//ALIEN STORM
namespace ProjectJogo
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch level2;
        private KeyboardManager km;
        Player Luigi;
        Player  CountPoints;
        private Texture2D fundo;
        private SpriteFont font;
        private Rectangle screenRectangle;
        private Rectangle fundoRectangle;
        private Vector2 posText;


        //enemy
        List<EnemySpawn> enemies = new List<EnemySpawn>();
        Random random = new Random();

        
        //Sound
        SoundEffect effect;
        Song song;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            km = new KeyboardManager();
            screenRectangle = new Rectangle(0, 0, 800, 480);
            Luigi = new Player(this, enemies,km);
            CountPoints = new Player(this, enemies,km);
                       
            //Posição do Fundo
            fundoRectangle.X = 0;
            fundoRectangle.Y = 0;

            //Tamanho do Fundo
            fundoRectangle.Width = 800;
            fundoRectangle.Height = 480;

            posText.X = 100;
            posText.Y = 100;

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            level2 = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            fundo = Content.Load<Texture2D>("Background");
            effect = Content.Load<SoundEffect>("Sound");
            song = Content.Load<Song>("Song");
            font = Content.Load<SpriteFont>("Arial16"); 
 

            MediaPlayer.Play(song);
            //Tocar a música em loop
            MediaPlayer.IsRepeating = true;

        }


        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            km.Update();
            Luigi.Update(gameTime,  effect, song);
            CountPoints.Update(gameTime, effect, song);

            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (EnemySpawn enemy in enemies)
                enemy.Update(_graphics.GraphicsDevice);

            LoadEnemies();

           
            

            // TODO: Add your update logic here
            base.Update(gameTime);
        }


        public void LoadEnemies()
        {
            int randY = random.Next(100, 400);

            //Spawnar 1 inimigo a cada 1 segundo
            if (spawn >= 1)
            {
                spawn = 0;
                if (enemies.Count < 4)
                {
                    enemies.Add(new EnemySpawn(Content.Load<Texture2D>("Enemy"), new Vector2(1100, randY)));
                }
            }
            for (int i =0; i<enemies.Count; i++)
            {
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
 
        }

        protected override void Draw(GameTime gameTime)
        {
            //Cor do Fundo
            GraphicsDevice.Clear(Color.CadetBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.DrawString(font, "oioi", posText, Color.Red);
            _spriteBatch.Draw(fundo, fundoRectangle, Color.White);
            foreach (EnemySpawn enemy in enemies)
                enemy.Draw(_spriteBatch);
            Luigi.Draw(_spriteBatch);
            _spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
