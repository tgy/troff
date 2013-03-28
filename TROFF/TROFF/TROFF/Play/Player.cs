using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TROFF.Play
{
    public class Player
    {
        public string Name;
        public byte Number; // 1 or 2

        public Point Position;
        public Direction Direction;

        public void Update(GameTime gameTime)
        {
            if (!Data.GameFocus) return;

            if (Data.PKs.IsKeyDown(Keys.Left) && Data.Ks.IsKeyUp(Keys.Left))
            {
                Direction newDirection = new Direction();
                switch (Direction)
                {
                    case Direction.North:
                        newDirection = Direction.West;
                        break;
                    case Direction.East:
                        newDirection = Direction.North;
                        break;
                    case Direction.South:
                        newDirection = Direction.East;
                        break;
                    case Direction.West:
                        newDirection = Direction.South;
                        break;
                }
                Direction = newDirection;
            }

            else if (Data.PKs.IsKeyDown(Keys.Right) && Data.Ks.IsKeyUp(Keys.Right))
            {
                Direction newDirection = new Direction();
                switch (Direction)
                {
                    case Direction.North:
                        newDirection = Direction.East;
                        break;
                    case Direction.East:
                        newDirection = Direction.South;
                        break;
                    case Direction.South:
                        newDirection = Direction.West;
                        break;
                    case Direction.West:
                        newDirection = Direction.North;
                        break;
                }
                Direction = newDirection;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Number == 1 ? Textures.Light1 : Textures.Light2, new Rectangle(Position.X * 4 - 6, Position.Y * 4 - 6, 16, 16), Color.White);
        }
    }

    public enum Direction
    {
        North,
        East,
        West,
        South
    }
}
