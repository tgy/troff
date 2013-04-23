using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TROFF.Play
{
    public class Player
    {
        public string Name;
        public byte Id { get; private set; }
        public bool Dead { get; private set; }
        private readonly bool _current;
        private int _timer;

        public Point Position;
        public Direction Direction;

        public Player(string name, byte id, bool current)
        {
            _current = current;
            Name = name;
            Id = id;
            Direction = Id == 1 ? Direction.East : Direction.West;
            Position = Id == 1 ? new Point(5, 75) : new Point(194, 75);
            _timer = 0;
        }

        public void Update(GameTime gameTime, NetworkStream stream)
        {
            CheckDeath();
            _timer++;
            if (Data.GameFocus && _current)
            {
                if (Data.PKs.IsKeyDown(Keys.Left) && Data.Ks.IsKeyUp(Keys.Left))
                {
                    Direction = (int) (Direction - 1) < 0 ? (Direction) 3 : (Direction) ((int) (Direction - 1));
                    SendNewDirection(stream);
                }

                else if (Data.PKs.IsKeyDown(Keys.Right) && Data.Ks.IsKeyUp(Keys.Right))
                {
                    Direction = (Direction) (((int) Direction + 1)%4);
                    SendNewDirection(stream);
                }
            }

            if (_timer == 3)
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
                _timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Id == 1 ? Textures.Light1 : Textures.Light2, new Rectangle(Position.X * 4 - 6, Position.Y * 4 - 6, 16, 16), Color.White);
        }

        public void CheckDeath()
        {
            Dead = Position.X < 0 || Position.X >= Map.Cells.GetLength(0) || Position.Y < 0 ||
                   Position.Y >= Map.Cells.GetLength(1) ||
                   Map.Cells[Position.X, Position.Y] != 0;
        }

        public void SendNewDirection(NetworkStream stream)
        {
            stream.WriteByte(1);
            stream.WriteByte((byte) Direction);
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
