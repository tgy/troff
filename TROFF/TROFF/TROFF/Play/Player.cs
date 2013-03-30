using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TROFF.Play
{
    public class Player
    {
        public string Name;
        public byte Id;
        public bool Dead;
        public int Timer;

        public Point Position;
        public Direction Direction;

        public Player(string name, byte id, Point position, Direction direction)
        {
            Name = name;
            Id = id;
            Position = position;
            Direction = direction;
            Timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            Timer++;
            if (Data.GameFocus && Id == 2)
            {
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
                    Timer = 0;
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
                    Timer = 0;
                }
            }

            if (Timer == 3)
            {
                Map.Cells[Position.X, Position.Y] = Id;
                switch (Direction)
                {
                    case Direction.North:
                        Position.Y--;
                        break;
                    case Direction.East:
                        Position.X++;
                        break;
                    case Direction.South:
                        Position.Y++;
                        break;
                    case Direction.West:
                        Position.X--;
                        break;
                }
                Timer = 0;
            }

            CheckDeath();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Id == 1 ? Textures.Color1 : Textures.Color2, new Rectangle(Position.X * 4, Position.Y * 4, 4, 4), Color.White);
            spriteBatch.Draw(Id == 1 ? Textures.Light1 : Textures.Light2, new Rectangle(Position.X * 4 - 6, Position.Y * 4 - 6, 16, 16), Color.White);
        }

        public void CheckDeath()
        {
            Dead = Position.X < 0 || Position.X > Map.Cells.GetLength(0) || Position.Y < 0 ||
                   Position.Y > Map.Cells.GetLength(1) ||
                   Map.Cells[Position.X, Position.Y] != 0;
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
