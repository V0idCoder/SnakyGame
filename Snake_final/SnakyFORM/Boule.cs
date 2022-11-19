using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakyFORM
{
    public class Boule
    {
        public virtual bool isHit(int x, int y)
        {
            return x == this.x && y == this.y;
        }

        public int x { get; set; }
        public int y { get; set; }
        public Brush color { get; set; }



        public virtual void Draw(Graphics g) 
        {
            
            g.FillRectangle(color, x*Form1.gridSize, y*Form1.gridSize , Form1.gridSize, Form1.gridSize);
        }
    }

    
}
