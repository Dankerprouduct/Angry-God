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

        TimeSpan time = TimeSpan.FromMilliseconds(9000);
        TimeSpan lastTime;

        public Tile(TileEngine tileEngine)
        {
            tEngine = tileEngine; 
        }

        public void LoadContent(ContentManager content)
        {
            
            tiles.Add(content.Load<Texture2D>("Tile2"));
            tiles.Add(content.Load<Texture2D>("Tile3")); 
        }

        public void Update(GameTime gameTime)
        {

            if (fire)
            {
                if (lastTime + time < gameTime.TotalGameTime)
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

                    lastTime = gameTime.TotalGameTime;
                }
                
            }
            if(tileID == 1)
            {
                fire = true; 
            }

        }
        
        
    }
}
