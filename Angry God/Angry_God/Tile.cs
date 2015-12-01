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
        public Tile()
        {

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
                                                                 
            }
        }
        
        
    }
}
