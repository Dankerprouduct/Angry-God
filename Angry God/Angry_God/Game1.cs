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

        MouseState mouseState;
        Vector2 mousePosition; 
        MouseState oldMouseState;
        List<Worshiper> worshipers = new List<Worshiper>();
        int worshiperCount = 0;

        TileEngine tileEngine = new TileEngine();
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

            oldMouseState = mouseState;

            foreach(Worshiper worship in worshipers)
            {
                worship.Update(gameTime); 
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
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
