using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake_game
{
    public partial class MainWindow : Window
    {
        //все списки
        public List<Point> snakePoints = new List<Point>();
        
        //инты
        private int direction = 0; //сейчас
        private int previousDirection = 0; //прошлое
        private int length = 100; //нарисованы на холсте
        private Point startPosition = new Point(210, 320);
        private Point currentPosition = new Point();
        private Random rnd = new Random();

        //енамы
        private enum SnakeSize
        {
            Thin = 4,
            Normal = 6,
            Thick = 8
        }; //размеры змеи
        private enum SpeedOfSnake
        {
            High = 100,
            Middle = 10000,
            Slow = 50000,
        }; // скорость змеи
        private enum DirectionToGo //направление
        {
            goUp = 8,
            goDown = 2,
            goLeft = 4,
            goRight = 6
        };




        private void paintSnake(Point currentposition)
        {
            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Black, //цвет змеи
                Width = (int)SnakeSize.Thick, //размер шарика
                Height = (int)SnakeSize.Thick 
            };

            Canvas.SetTop(newEllipse, currentposition.Y);
            Canvas.SetLeft(newEllipse, currentposition.X);

            int count = paintCanvas.Children.Count;
            paintCanvas.Children.Add(newEllipse);
            snakePoints.Add(currentposition);

            if (count > length)
            {
                paintCanvas.Children.RemoveAt(count - length + 9);
                snakePoints.RemoveAt(count - length);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            
            timer.Interval = new TimeSpan((int)SpeedOfSnake.Middle);//скорость змеи
            timer.Start();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startPosition);
            currentPosition = startPosition;

            // Instantiate Food Objects
            for (int n = 0; n < 10; n++)
            {
                GetNewApple(n);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // Expand the body of the snake to the direction of movement

            switch (direction)
            {
                case (int)MOVINGDIRECTION.DOWNWARDS:
                    currentPosition.Y += 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.UPWARDS:
                    currentPosition.Y -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TOLEFT:
                    currentPosition.X -= 1;
                    paintSnake(currentPosition);
                    break;
                case (int)MOVINGDIRECTION.TORIGHT:
                    currentPosition.X += 1;
                    paintSnake(currentPosition);
                    break;
            }

            // Restrict to boundaries of the Canvas
            if ((currentPosition.X < 5) || (currentPosition.X > 620) ||
                (currentPosition.Y < 5) || (currentPosition.Y > 380))
                GameOver();

            // Hitting a bonus Point causes the lengthen-Snake Effect
            int n = 0;
            foreach (Point point in bonusPoints)
            {

                if ((Math.Abs(point.X - currentPosition.X) < headSize) &&
                    (Math.Abs(point.Y - currentPosition.Y) < headSize))
                {
                    length += 10;
                    score += 10;

                    // In the case of food consumption, erase the food object
                    // from the list of bonuses as well as from the canvas
                    bonusPoints.RemoveAt(n);
                    paintCanvas.Children.RemoveAt(n);
                    paintBonus(n);
                    break;
                }
                n++;
            }

            // Restrict hits to body of Snake

            for (int q = 0; q < (snakePoints.Count - headSize * 2); q++)
            {
                Point point = new Point(snakePoints[q].X, snakePoints[q].Y);
                if ((Math.Abs(point.X - currentPosition.X) < (headSize)) &&
                     (Math.Abs(point.Y - currentPosition.Y) < (headSize)))
                {
                    GameOver();
                    break;
                }
            }
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    if (previousDirection != (int)DirectionToGo.goUp)
                        direction = (int)DirectionToGo.goDown;
                    break;
                case Key.Up:
                case Key.W:
                    if (previousDirection != (int)DirectionToGo.goDown)
                        direction = (int)DirectionToGo.goUp;
                    break;
                case Key.Left:
                case Key.A:
                    if (previousDirection != (int)DirectionToGo.goRight)
                        direction = (int)DirectionToGo.goLeft;
                    break;
                case Key.Right:
                case Key.D:
                    if (previousDirection != (int)DirectionToGo.goLeft)
                        direction = (int)DirectionToGo.goRight;
                    break;

            }
            previousDirection = direction;

        }
        private void GetNewApple(int index)
        {
            Point apple = new Point(rnd.Next(5, 780), rnd.Next(5, 480));

            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Red,
                Width = _headSize,
                Height = _headSize
            };

            Canvas.SetTop(newEllipse, apple.Y);
            Canvas.SetLeft(newEllipse, apple.X);
            PaintCanvas.Children.Insert(index, newEllipse);
            _bonusPoints.Insert(index, apple);

        }
    }
}
