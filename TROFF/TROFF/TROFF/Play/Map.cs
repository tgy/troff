using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TROFF.Play
{
    public static class Map
    {
        public static byte[,] Cells;

        public static void Initialize()
        {
            Cells = new byte[200, 150];
        }
        
        public static void Update(GameTime gameTime)
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 200; x++)
                for (int y = 0; y < 150; y++)
                    switch (Cells[x, y])
                    {
                        case 1:
                            spriteBatch.Draw(Textures.Color1, new Rectangle(x*4, y*4, 4, 4), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(Textures.Color2, new Rectangle(x*4, y*4, 4, 4), Color.White);
                            break;
                    }
        }
    }
}
