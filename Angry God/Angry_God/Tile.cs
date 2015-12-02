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
    class Tile
    {
        public bool fire;
        public List<Texture2D> tiles = new List<Texture2D>();
        public Vector2 position;
        public int tileID = 0;
        TileEngine tEngine;

        TimeSpan time = TimeSpan.FromMilliseconds(250);
        TimeSpan lastTime;
        Game1 game1;
        MouseState mouseState;
        Point mousePosition;
        KeyboardState keyboardState;
        Point worshiperPosition;
        Rectangle hitBox; 
        public Tile(TileEngine tileEngine, Game1 game)
        {
            tEngine = tileEngine;
            game1 = game; 
        }

        public void LoadContent(ContentManager content)
        {
            
            tiles.Add(content.Load<Texture2D>("Tile2"));
            tiles.Add(content.Load<Texture2D>("Tile3")); 
        }

        public void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, 50, 50);



            if (fire)
            {
                WorshiperCollisions();
            }
            if (lastTime + time < gameTime.TotalGameTime)
            {
                if (fire)
                {
                    
                    for (int i = 0; i < tEngine.tiles.Count; i++)
                    {


                        Vector2 checkedPos = new Vector2(position.X - 50, position.Y);
                        if (checkedPos == tEngine.tiles[i].position)
                        {
                            tEngine.tiles[i].tileID = 1;
                        }
                        checkedPos = new Vector2(position.X + 50, position.Y);
                        if (checkedPos == tEngine.tiles[i].position)
                        {
                            tEngine.tiles[i].tileID = 1;
                        }
                        checkedPos = new Vector2(position.X, position.Y + 50);
                        if (checkedPos == tEngine.tiles[i].position)
                        {
                            tEngine.tiles[i].tileID = 1;
                        }
                        checkedPos = new Vector2(position.X, position.Y - 50);
                        if (checkedPos == tEngine.tiles[i].position)
                        {
                            tEngine.tiles[i].tileID = 1;
                        }



                    }


                }

                lastTime = gameTime.TotalGameTime;
            }

            if (tileID == 1)
            {
                fire = true;
            }
            else
            {
                fire = false; 
            }

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePosition = new Point((int)mouseState.X, (int)mouseState.Y);

            if(hitBox.Contains(mousePosition) && keyboardState.IsKeyDown(Keys.D2))
            {
                tileID = 1; 
            }


        }
        void WorshiperCollisions()
        {
            for(int i = 0; i < game1.worshipers.Count; i++)
            {
                worshiperPosition = new Point((int)game1.worshipers[i].position.X, (int)game1.worshipers[i].position.Y);

                if (hitBox.Contains(worshiperPosition))
                {
                    game1.worshipers[i].onFire = true; 
                }
                
            }

        }
        
    }
}
