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
    public class Cop
    {
        public bool alive = true;
        public bool destroy = false; 
        float fadeOut = 1; 
        public Vector2 position;
        public Texture2D texture;
        public Game1 game;
        Worshiper worshiper;
        Random random;
        bool targering = false;
        public Rectangle copRect;
        int index = 0; 
        public Cop(Game1 game1)
        {
            game = game1; 
        }

        public void LoadContent(ContentManager content, Vector2 mp)
        {
            texture = content.Load<Texture2D>("Cop");
            position = mp;

            copRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); 
        }

        public void Update(GameTime gameTime)
        {

            if (game.worshipers.Count != 0)
            {
                Attack();
            }


            copRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (!targering)
            {
                foreach (Worshiper worshi in game.worshipers)
                {
                    if (worshi.alive)
                    {

                        targering = true;

                        index = worshi.index;
                    }
                    else
                    {
                        targering = false;
                    }

                }
            }

        }

        void Attack()
        {
            if (targering)
            {
                if (index < game.worshipers.Count - 1)
                {
                    position = Vector2.Lerp(position, game.worshipers[index].position, .1f);

                    if (copRect.Intersects(game.worshipers[index].worshiperRect))
                    {
                        game.worshipers[index].onFire = true;
                    }
                }


            }

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(texture, position, Color.White); 
            }
            else
            {
                spriteBatch.Draw(texture, position, Color.Black * fadeOut);
                fadeOut -= .01f; 
                if(fadeOut < .01f)
                {
                    destroy = true; 
                }
            }
            
        }

    }
}
