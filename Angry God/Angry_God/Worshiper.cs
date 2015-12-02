using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Angry_God
{
    public class Worshiper
    {
        int health;
        int speed;
        Texture2D texture;
        Texture2D textureblue;
        Texture2D texturePurple; 
        Texture2D texture2;
        Texture2D finalTexture;
        List<Texture2D> aliveTextures;

        public Vector2 position;

        Vector2 newPosition;
        Random random;
        
        bool move = true;
        int xPos;
        int yPos;
        int spazAmmount = 100;
        bool alive = true;

        MouseState mouseState;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        Rectangle hitBox;
        Point mousePosition;
        float fadeOut = 1;
        public bool destroy;
        public bool onFire; 
        public Worshiper()
        {
            health = 100;
            speed = 5;
        }

        public void LoadContent(ContentManager content, Vector2 mPos)
        {
            position = mPos;
            aliveTextures = new List<Texture2D>();
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive1)"));
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive2)"));
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive3)"));
            texture2 = content.Load<Texture2D>("orangeGuy(Dead)");
            random = new Random(); 
            int index = random.Next(0, aliveTextures.Count); 
            finalTexture = aliveTextures[index]; 

            hitBox = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);
            random = new Random();
            
           // newPosition = new Vector2(0, 0);
        }
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            

            mousePosition = new Point(mouseState.X, mouseState.Y);

            
            xPos = random.Next((int)position.X - spazAmmount, (int)position.X + spazAmmount);
            yPos = random.Next((int)position.Y - spazAmmount, (int)position.Y + spazAmmount);
            newPosition = new Vector2(xPos, yPos);
            position = Vector2.Lerp(position, newPosition, .01f);

            hitBox = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);

            if (hitBox.Contains(mousePosition) && (keyboardState.IsKeyDown(Keys.D1) && oldKeyboardState.IsKeyUp(Keys.D1)))
            {
                alive = false;
                Console.WriteLine("smited"); 
            }

            

            oldKeyboardState = keyboardState;

            if (onFire)
            {
                spazAmmount = 500;
                alive = false; 
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(finalTexture, position, Color.White);

            }
            else
            {
                spriteBatch.Draw(texture2, position, Color.White * fadeOut);
                fadeOut -= .01f;
                if(fadeOut < .01f)
                {
                    destroy = true; 
                }
            }
        }
    }
}
