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
        public bool alive = true;

        MouseState mouseState;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        Rectangle hitBox;
        Point mousePosition;
        float fadeOut = 1;
        public bool destroy;
        public bool onFire;
        TimeSpan time = TimeSpan.FromMilliseconds(5000);
        TimeSpan lastTime;
        string saying;
        SpriteFont font;
        List<string> phrases = new List<string>();
        List<string> deathPhrases = new List<string>();
        string deathText; 
        float fadeText = 1f;
        public int index;
        public Rectangle worshiperRect; 
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
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive4)"));
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive5)"));
            aliveTextures.Add(content.Load<Texture2D>("orangeGuy(Alive6)"));

            texture2 = content.Load<Texture2D>("orangeGuy(Dead)");
            random = new Random(); 
            index = random.Next(0, aliveTextures.Count); 
            finalTexture = aliveTextures[index];
            worshiperRect = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);
            hitBox = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);
            random = new Random();
            font = content.Load<SpriteFont>("worshiperFont");
            saying = " ";
            // newPosition = new Vector2(0, 0);
            time = TimeSpan.FromMilliseconds(random.Next(1000, 20000));
            phrases.Add("please dont kill us tornado god");
            phrases.Add("i have sinned, please forgive my soul");
            phrases.Add("i sure do i hope i dont get smitted");
            phrases.Add("whats even the point in life");
            phrases.Add("am i really in control?");

            deathPhrases.Add("AUUGGHHH");
            deathPhrases.Add("CRAP");
            deathPhrases.Add("OWWWW");
            deathPhrases.Add("MY PANCREAS");
            deathPhrases.Add("MY LEG");
            deathPhrases.Add("WHY ME");
            deathPhrases.Add("TELL MY WIFE I LOVE HER");
            deathPhrases.Add("dangit GOPI");
            deathPhrases.Add("I NEVER CARED ANYWAYS");
            deathPhrases.Add("My name is Clyde");
            deathPhrases.Add("SCREW YOU");
            deathPhrases.Add("I wanted to burn");
            deathPhrases.Add("Seemed like a day to die");
            deathPhrases.Add("OH MY GOSH WHY");
            deathText = deathPhrases[random.Next(0, deathPhrases.Count)];
        }
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            worshiperRect = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);

            mousePosition = new Point(mouseState.X, mouseState.Y);

            
            xPos = random.Next((int)position.X - spazAmmount, (int)position.X + spazAmmount);
            yPos = random.Next((int)position.Y - spazAmmount, (int)position.Y + spazAmmount);
            newPosition = new Vector2(xPos, yPos);
            position = Vector2.Lerp(position, newPosition, .01f);

            hitBox = new Rectangle((int)position.X, (int)position.Y, finalTexture.Width, finalTexture.Height);

            if (hitBox.Contains(mousePosition) && (keyboardState.IsKeyDown(Keys.D1) && oldKeyboardState.IsKeyUp(Keys.D1)))
            {
                alive = false;
               // Console.WriteLine("smited"); 
            }

            

            oldKeyboardState = keyboardState;
            Collision();
            if (onFire)
            {
                spazAmmount = 800;

                deathPhrases.Clear();
                deathPhrases.Add("i wanted to burn");
                deathPhrases.Add("it burns");
                deathPhrases.Add("how did fire get here?");
                deathPhrases.Add("crap burns");
                deathPhrases.Add("fammin' hot");
                deathText = deathPhrases[random.Next(0, deathPhrases.Count)];               

                alive = false; 
            }

            if(lastTime + time < gameTime.TotalGameTime)
            {
                fadeText = 1; 
                saying = phrases[random.Next(0, phrases.Count)];
                time = TimeSpan.FromMilliseconds(random.Next(1000, 20000)); 
                lastTime = gameTime.TotalGameTime; 
            }
            else
            {
                fadeText -= .01f; 
               // saying = " "; 
            }
            
        }
        void Collision()
        {
            if(position.X + 25 > 800)
            {
                position.X -= 25; 
            }
            if(position.X < 0)
            {
                position.X = 0; 
            }
            if(position.Y < 0)
            {
                position.Y = 0;
            }
            if(position.Y + 25 > 480)
            {
                position.Y -= 25; 
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(finalTexture, position, Color.White);
                spriteBatch.DrawString(font, saying, new Vector2(position.X + 25, position.Y - 25), Color.White * fadeText); 

            }
            else
            {
                spriteBatch.Draw(texture2, position, Color.White * fadeOut);
                spriteBatch.DrawString(font, deathText.ToUpper(), new Vector2(position.X + 25, position.Y - 25), Color.Red * fadeOut);
                fadeOut -= .01f;
                if(fadeOut < .01f)
                {
                    destroy = true; 
                }
            }
        }
    }
}
