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
        private int _timer;

        private Point _position;
        private Direction _direction;

        public Player(string name, byte id)
        {
            Name = name;
            Id = id;
            _direction = Id == 1 ? Direction.East : Direction.West;
            _position = Id == 1 ? new Point(5, 75) : new Point(194, 75);
            _timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            CheckDeath();
            _timer++;
            if (Data.GameFocus)
            {
                if (Data.PKs.IsKeyDown(Keys.Left) && Data.Ks.IsKeyUp(Keys.Left))
                    _direction = (int)(_direction - 1) < 0 ? (Direction)3 : (Direction)((int)(_direction - 1));

                else if (Data.PKs.IsKeyDown(Keys.Right) && Data.Ks.IsKeyUp(Keys.Right))
                    _direction = (Direction)(((int)_direction + 1) % 4);
            }

            if (_timer == 3)
            {
                Map.Cells[_position.X, _position.Y] = Id;
                switch (_direction)
                {
                    case Direction.North:
                        _position.Y--;
                        break;
                    case Direction.East:
                        _position.X++;
                        break;
                    case Direction.South:
                        _position.Y++;
                        break;
                    case Direction.West:
                        _position.X--;
                        break;
                }
                _timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Id == 1 ? Textures.Light1 : Textures.Light2, new Rectangle(_position.X * 4 - 6, _position.Y * 4 - 6, 16, 16), Color.White);
        }

        public void CheckDeath()
        {
            Dead = _position.X < 0 || _position.X >= Map.Cells.GetLength(0) || _position.Y < 0 ||
                   _position.Y >= Map.Cells.GetLength(1) ||
                   Map.Cells[_position.X, _position.Y] != 0;
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
