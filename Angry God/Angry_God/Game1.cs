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
        List<Worshiper> worshipers = new List<Worshiper>();
        int worshiperCount = 0;

        Texture2D lighting;
        bool showLightning;
        TimeSpan lightingTime = TimeSpan.FromMilliseconds(250);
        TimeSpan lastLightning; 

        TileEngine tileEngine = new TileEngine();

        SpriteFont font;
        int score = 0;
        int faith = 100;
        Texture2D faithBar; 
        Random randRotation = new Random(); 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true; 
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
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

            if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {

                worshipers.Add(new Worshiper());
                worshipers[worshiperCount].LoadContent(Content, mousePosition);
                worshiperCount++;
                Console.WriteLine(worshiperCount);
                

            }

            if(keyboardState.IsKeyDown(Keys.D1) && oldKeyboardState.IsKeyUp(Keys.D1))
            {
                showLightning = true;
                faith -= 10; 
            }
            if (lastLightning + lightingTime < gameTime.TotalGameTime)
            {
                showLightning = false;
                lastLightning = gameTime.TotalGameTime;
                faith--; 
            }


            oldKeyboardState = keyboardState; 
            oldMouseState = mouseState;


            foreach(Worshiper worship in worshipers)
            {
                worship.Update(gameTime);
                
            }
            
            for(int i = 0; i < worshipers.Count; i++)
            {
                if (worshipers[i].destroy)
                {
                    
                    worshipers.RemoveAt(i);
                    worshiperCount--;
                    Console.WriteLine("Removed worshiper " + i);
                    score = score + 10;
                    faith = faith + (score / 3); 
                }
            }
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            tileEngine.Draw(spriteBatch); 
            foreach (Worshiper worship in worshipers)
            {
                worship.Draw(spriteBatch); 
            }

            if (showLightning)
            {
                spriteBatch.Draw(lighting, new Vector2(oldMouseState.X - (lighting.Width / 2 - 25), oldMouseState.Y - lighting.Height),null, Color.White, randRotation.Next(0, 5), new Vector2(0,0), 1f, SpriteEffects.None, 1f); 
            }
            spriteBatch.DrawString(font, "Score " + score, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Faith ", new Vector2(10, 40), Color.White);
            spriteBatch.Draw(faithBar, new Rectangle(96, 50, faith, 23), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
