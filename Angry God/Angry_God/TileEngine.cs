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
        public int[,] tileMap;
        List<Tile> tiles = new List<Tile>();

        MouseState moustState;
        Vector2 mousePosition;
        int x;
        int y;
        SpriteFont font;
        List<Vector2> nTilePositon = new List<Vector2>(); 
        public TileEngine()
        {
            tileMap = new int[16, 10]; 
            foreach(int i in tileMap)
            {
                tiles.Add(new Tile()); 
            }
            

        }

        public void LoadContent(ContentManager content)
        {
            foreach(Tile t in tiles)
            {
                t.LoadContent(content); 
            }
            font = content.Load<SpriteFont>("ScoreFont");
            // Console.WriteLine(tiles[tileMap[x, y]]);
            tiles[1].tileID = 0;

            
            for(int x = 0; x < 16; x++)
            {
                for(int y = 0; y < 10; y++)
                {
                    VectorToList(x, y); 
                }
            }

            for(int i = 0; i < tiles.Count; i++)
            {
                tiles[i].position = nTilePositon[i]; 
            }


            tiles[85].tileID = 1; 

        }

        void VectorToList(int x, int y)
        {
            nTilePositon.Add(new Vector2(50 * x, 50 * y)); 
        }

        public void Update(GameTime gameTime)
        {

            moustState = Mouse.GetState();
            mousePosition = new Vector2(moustState.X, moustState.Y); 


            foreach(Tile t in tiles)
            {
                t.Update(gameTime); 
            }

            x = Convert.ToInt32(mousePosition.X) / 50;
            y = Convert.ToInt32(mousePosition.Y) / 50;
            if(moustState.RightButton == ButtonState.Pressed)
            {
               tiles[tileMap[x,y]].tileID = 0; 
            }

        }


        

        public void Draw(SpriteBatch spriteBatch)
        {
            
            for(int i = 0; i < tiles.Count; i++)
            {
                spriteBatch.Draw(tiles[i].tiles[tiles[i].tileID], tiles[i].position, Color.White);
                spriteBatch.DrawString(font, i.ToString(), tiles[i].position, Color.Black);
            }            

        }


    }
}
