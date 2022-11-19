using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakyFORM
{
    public class Snake : Boule
    {
        public bool vie = true;
        private Direction dir;

        public enum Direction
        {
            None, Up, Left, Right, Down
        }
        public List<Boule> Tail { get; set; }

        public Direction direction
        {
            get => dir; 
            
            set
            {
                switch (value)
                {
                    case Direction.None:
                        break;
                    case Direction.Up:
                        if (dir == Direction.Down)
                        {
                            return;
                        }
                        break;
                    case Direction.Left:
                        if (dir == Direction.Right)
                        {
                            return;
                        }
                        break;
                    case Direction.Right:
                        if (dir == Direction.Left)
                        {
                            return;
                        }
                        break;
                    case Direction.Down:
                        if (dir == Direction.Up)
                        {
                            return;
                        }
                        break;
                    default:
                        break;
                }
                dir = value;
            }
        }
        public int growCount { get; set; }

        public Snake()
        {
            Tail = new List<Boule>();
            x = 10;
            y = 10;
            color = Brushes.DarkGreen;

            for (int i = 1; i < 5; i++)
            {
                Tail.Add(new Boule() { x = x - i, y = y, color = Brushes.Green });
            }
        }

        public void Update()
        {


            if (direction == Direction.None)
            {
                return;
            }

            var last = Tail[Tail.Count - 1];
            if (growCount > 0)
            {
                last = new Boule() { color = Brushes.Green };
                growCount--;
            }
            else
            {
                Tail.Remove(last);
            }

            last.x = x;
            last.y = y;
            Tail.Insert(0, last);

            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Down:
                    y++;
                    break;
                default:
                    break;
            }
            if (TailIsHit(x,y))
            {
                vie = false;
            }

            if (x == 23 || x == -1 || y == 22 || y == -1)
            {
                vie = false;

            }
        }



        public override void Draw(Graphics g)
        {
            foreach (var item in Tail)
            {
                item.Draw(g);
            }
            g.FillEllipse(color, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize, Form1.gridSize);

            switch (direction)
            {
                case Direction.None:
                    g.FillRectangle(color, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize / 2, Form1.gridSize);
                    break;
                case Direction.Up:
                    g.FillRectangle(color, x * Form1.gridSize, y * Form1.gridSize + Form1.gridSize / 2, Form1.gridSize, Form1.gridSize / 2);
                    break;
                case Direction.Left:
                    g.FillRectangle(color, x * Form1.gridSize + Form1.gridSize / 2, y * Form1.gridSize, Form1.gridSize / 2, Form1.gridSize);
                    break;
                case Direction.Right:
                    g.FillRectangle(color, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize / 2, Form1.gridSize);
                    break;
                case Direction.Down:
                    g.FillRectangle(color, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize, Form1.gridSize / 2);
                    break;

            }
        }
        public bool TailIsHit(int x, int y)
        {
            foreach (var item in Tail)
            {
                if (item.isHit(x, y))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool isHit(int x, int y)
        {
            if (TailIsHit(x,y))
            {
                return true;
            }
            return base.isHit(x, y);
        }
    }
}
