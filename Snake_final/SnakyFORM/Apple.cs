using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakyFORM
{
    public class Apple : Boule
    {
        Image? pomme = null;

        public Apple()
        {
            color = Brushes.Red;
            string img_path = @"R:\2ème année\1er semestre\Pratique\C#\Snake\SnakyFORM\Images\apple.png";

            //Create image
            pomme = Image.FromFile(img_path);

        }

        public override void Draw(Graphics g)
        {
            if (pomme != null)
                g.DrawImage(pomme, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize, Form1.gridSize);
            else
                g.FillEllipse(color, x * Form1.gridSize, y * Form1.gridSize, Form1.gridSize, Form1.gridSize);


        }
    }
}
