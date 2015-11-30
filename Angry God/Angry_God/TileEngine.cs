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
    class TileEngine
    {
        int size = 20; 
        Texture2D tile; 
        public TileEngine()
        {

        }

        public void LoadContent(ContentManager content)
        {
            tile = content.Load<Texture2D>("Tile2"); 
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < size; x++)
            {
                for(int y = 0; y < size; y++)
                {
                    spriteBatch.Draw(tile, new Vector2(50 * x, 50 * y), Color.White);
                }
            }
        }
    }
}
