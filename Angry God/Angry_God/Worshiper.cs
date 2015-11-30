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
    class Worshiper
    {
        int health;
        int speed;
        Texture2D texture;
        Texture2D texture2; 
        Vector2 position;

        Vector2 newPosition;
        Random random;
        
        bool move = true;
        int xPos;
        int yPos;
        int spazAmmount = 100;
        bool alive = true; 
        public Worshiper()
        {
            health = 100;
            speed = 5;
        }

        public void LoadContent(ContentManager content, Vector2 mPos)
        {
            position = mPos; 
            texture = content.Load<Texture2D>("orangeGuy(Alive)");
            texture2 = content.Load<Texture2D>("orangeGuy(Dead)"); 
            random = new Random();
            
           // newPosition = new Vector2(0, 0);
        }
        public void Update(GameTime gameTime)
        {

            xPos = random.Next((int)position.X - spazAmmount, (int)position.X + spazAmmount);
            yPos = random.Next((int)position.Y - spazAmmount, (int)position.Y + spazAmmount);
            newPosition = new Vector2(xPos, yPos);

            position = Vector2.Lerp(position, newPosition, .01f);
            move = false; 
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(texture, position, Color.White);

            }
            else
            {
                spriteBatch.Draw(texture2, position, Color.White); 
            }
        }
    }
}
