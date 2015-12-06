using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Angry_God
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState; 

        MouseState mouseState;
        Vector2 mousePosition; 
        MouseState oldMouseState;
        public List<Worshiper> worshipers = new List<Worshiper>();
        public List<Cop> cops = new List<Cop>();
        int worshiperCount = 0;
        int copCount = 0; 
        Texture2D lighting;
        bool showLightning;
        TimeSpan lightingTime = TimeSpan.FromMilliseconds(250);
        TimeSpan lastLightning;

        TileEngine tileEngine; 

        SpriteFont font;
        int score = 0;
        public int faith = 100;
        Texture2D faithBar; 
        Random randRotation = new Random();
        bool gameOver; 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true; 
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800; 

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            tileEngine = new TileEngine(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileEngine.LoadContent(Content);


            lighting = Content.Load<Texture2D>("Lightning");
            font = Content.Load<SpriteFont>("ScoreFont");
            faithBar = Content.Load<Texture2D>("Tile2");
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
             
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit(); 
            }


            

            if (!gameOver)
            {
                Lightning(gameTime);
                FaithChecks();
                DestroyEntities();
                UpdateEntities(gameTime);
                tileEngine.Update(gameTime);
                Spawning();
            }
            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        // TODO: move to own class
        void Lightning(GameTime gameTime2)
        {
            if (keyboardState.IsKeyDown(Keys.D1) && oldKeyboardState.IsKeyUp(Keys.D1))
            {
                showLightning = true;
                //faith -= 10; 
            }
            if (lastLightning + lightingTime < gameTime2.TotalGameTime)
            {
                showLightning = false;
                lastLightning = gameTime2.TotalGameTime;
                faith--;
            }
        }

        Random random; 
        void Spawning()
        {
            if(worshiperCount < 10)
            {
                random = new Random(); 
                worshipers.Add(new Worshiper());
                worshipers[worshiperCount].LoadContent(Content, new Vector2(random.Next(0, 800), random.Next(0, 600)));
                worshiperCount++; 
            }


            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {

                worshipers.Add(new Worshiper());
                worshipers[worshiperCount].LoadContent(Content, mousePosition);
                worshiperCount++;
                //  Console.WriteLine(worshiperCount);


            }
            if (keyboardState.IsKeyDown(Keys.C) && oldKeyboardState.IsKeyUp(Keys.C))
            {

                cops.Add(new Cop(this));
                cops[copCount].LoadContent(Content, mousePosition);
                copCount++;
                //  Console.WriteLine(worshiperCount);


            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                worshipers.Add(new Worshiper());
                worshipers[worshiperCount].LoadContent(Content, mousePosition);
                worshiperCount++;
                //  Console.WriteLine(worshiperCount);
            }
        }


        void DestroyEntities()
        {
            for (int i = 0; i < worshipers.Count; i++)
            {
                if (worshipers[i].destroy)
                {

                    worshipers.RemoveAt(i);
                    worshiperCount--;
                    // Console.WriteLine("Removed worshiper " + i);
                    score = score + 10;
                    faith = faith + (score / 3);
                }
            }
            for (int i = 0; i < cops.Count; i++)
            {

                if (cops[i].destroy)
                {
                    cops.RemoveAt(i);
                    copCount--;
                }
            }
        }
        void UpdateEntities(GameTime gameTime2)
        {
            foreach (Worshiper worship in worshipers)
            {
                worship.Update(gameTime2);

            }
            foreach (Cop cop in cops)
            {
                cop.Update(gameTime2);

            }
        }
        void Reset()
        {
            score = 0;
            faith = 100; 
        }
        void FaithChecks()
        {
            if (faith >= 700)
            {
                faith = 700;
            }


            if (faith <= 0)
            {
                faith = 0; 
                //  Console.WriteLine("You loose"); 
                gameOver = true; 

            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            if (!gameOver)
            {
                tileEngine.Draw(spriteBatch);
                foreach (Worshiper worship in worshipers)
                {
                    worship.Draw(spriteBatch);
                }
                foreach (Cop cop in cops)
                {
                    cop.Draw(spriteBatch);

                }
                if (showLightning)
                {
                    spriteBatch.Draw(lighting, new Vector2(oldMouseState.X - (lighting.Width / 2 - 25), oldMouseState.Y - lighting.Height), null, Color.White, randRotation.Next(0, 1), new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
                }
                spriteBatch.DrawString(font, "Score " + score, new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(font, "Faith ", new Vector2(10, 40), Color.White);
                spriteBatch.DrawString(font, "Members " + worshiperCount, new Vector2(10, 70), Color.White);
                spriteBatch.Draw(faithBar, new Rectangle(96, 50, faith, 23), Color.Red);

            }
            else
            {
                spriteBatch.DrawString(font, "GAME OVER SCORE: " + score, new Vector2(800 / 3, 600 / 3), Color.White); 
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
