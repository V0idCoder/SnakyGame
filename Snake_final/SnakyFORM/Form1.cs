using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Timer = System.Windows.Forms.Timer;

namespace SnakyFORM
{
    public partial class Form1 : Form
    {
        public const int gridSize = 20;
        public const String Filename = "HighScore.txt";
        Pen gridColor = Pens.PaleGreen;
        Snake snake = new Snake();
        Timer timer = new Timer();
        List<Apple> apples = new List<Apple>();
        Random rnd = new Random();
        int score;





        public Form1()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            timer.Interval = 100;

            AddApple();

            timer.Start();
            Score.Text = score.ToString();


          

            
            
                using var sr = new StreamReader(Filename);
                HighScore.Text = sr.ReadToEnd();
            
       
           


        }

        //public void Load()
        //{
        //        string? s;
        //        using var sr = new StreamReader(Filename);
        //        HighScore.Text = sr.ReadToEnd();
        //}


        private void Timer_Tick(object? sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            snake.Update();
            
            
            if (snake.vie == false)
            {
                timer.Stop();
                var message = MessageBox.Show("Voulez-vous recommencer ?","Perdu",MessageBoxButtons.YesNo);
                if (message == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
                
            }

            Apple? a = null;
            foreach (Apple apple in apples)
            {
                if (apple.isHit(snake.x, snake.y))
                {
                    a = apple;
                    break;
                }
            }
            if (a != null)
            {
                apples.Remove(a);
                AddApple();
                snake.growCount++;
                score++;
                Score.Text = score.ToString();
                string text = File.ReadAllText("HighScore.txt");
                int.TryParse(text, out int highScore);

                if(score > highScore)
                {
                    highScore += 1;
                    using var sw = new StreamWriter(Filename);
                    sw.WriteLine(highScore.ToString());

                    HighScore.Text = highScore.ToString();
                }

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void AddApple()
        {
            while (true)
            {
            StartPosition:
                int x = rnd.Next(23);
                int y = rnd.Next(22);
                if (snake.isHit(x, y))
                {
                    continue;
                }

                foreach (Apple apple in apples)
                {
                    if (apple.isHit(x, y))
                    {
                        goto StartPosition;
                    }
                }
                apples.Add(new Apple() { x = x, y = y });

                break;                
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            foreach (Apple apple in apples)
            {
                apple.Draw(e.Graphics);
            }
            snake.Draw(e.Graphics);     
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W or Keys.Up:
                    {
                        snake.direction = Snake.Direction.Up;
                        break;
                    }
                case Keys.A or Keys.Left:
                    {
                        if (!snake.direction.Equals(Snake.Direction.None))
                        snake.direction = Snake.Direction.Left;
                        break;
                    }
                case Keys.S or Keys.Down:
                    {
                        snake.direction = Snake.Direction.Down;
                        break;
                    }
                case Keys.D or Keys.Right:
                    {
                        snake.direction = Snake.Direction.Right;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void DrawGrid(Graphics g)
        {
            
            int x = 0;
            while (x <= 23 * gridSize)
            {
                g.DrawLine(gridColor, x, 0, x, 22* gridSize);
                x += gridSize;
            }

            
            int y = 0;
            while (y <= 22 * gridSize)
            {
                g.DrawLine(gridColor, 0, y, 23 * gridSize, y);
                y += gridSize;
            }

        }

    }
}