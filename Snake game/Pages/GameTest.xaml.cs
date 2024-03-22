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

namespace Snake_game.Pages
{
    /// <summary>
    /// Логика взаимодействия для GameTest.xaml
    /// </summary>
    public partial class GameTest : Page
    {
        public GameTest()
        {
            InitializeComponent();
            this.Focus();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(perSec);
            timer.Interval = new TimeSpan((int)SpeedOfSnake.Middle);//скорость змеи
            timer.Start();


            this.KeyDown += new KeyEventHandler(GetButtonDown);
            paintSnake(startPosition);
            nowPosition = startPosition;


            // яблоки рандом
            for (int n = 0; n < 10; n++)
            {
                PaintNewApple(n);
            }
        }
        //канвас 640 на 420

        //все списки
        public List<Point> snakePoints = new List<Point>();
        private List<Point> applePoints = new List<Point>();
        public int page = 0;

        //инты
        private int direction = 0; //сейчас
        private int previousDirection = 0; //прошлое
        private int length = 50; //нарисованы на холсте
        private Point startPosition = new Point(210, 320);
        private Point nowPosition = new Point();
        private Random rnd = new Random();
        private int score = 0; //счёт
        int sizeL = (int)SnakeSize.Large;

        //енамы
        private enum SnakeSize
        {
            Small = 5,
            Medium = 8,
            Large = 10
        };
        private enum SpeedOfSnake
        {
            High = 100,
            Middle = 10000,
            Slow = 50000,
        };
        private enum DirectionToGo
        {
            Up = 8,
            Down = 2,
            Left = 4,
            Right = 6
        };



        private void paintSnake(Point currentposition)
        {
            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Black, //цвет змеи
                Width = (int)SnakeSize.Large, //размер шарика
                Height = (int)SnakeSize.Large
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

        private void perSec(object sender, EventArgs e)
        {
            // изменение координат спавна точек

            switch (direction)
            {
                case (int)DirectionToGo.Up:
                    nowPosition.Y -= 1;
                    paintSnake(nowPosition);
                    break;
                case (int)DirectionToGo.Down:
                    nowPosition.Y += 1;
                    paintSnake(nowPosition);
                    break;
                case (int)DirectionToGo.Left:
                    nowPosition.X -= 1;
                    paintSnake(nowPosition);
                    break;
                case (int)DirectionToGo.Right:
                    nowPosition.X += 1;
                    paintSnake(nowPosition);
                    break;
            }

            // границы канваса 640 на 420 
            if ((nowPosition.X < 0) || (nowPosition.X > 613) ||
                (nowPosition.Y < 5) || (nowPosition.Y > 650))
                GameOver();

            // рост и очки
            int n = 0;
            foreach (Point point in applePoints)
            {

                if ((Math.Abs(point.X - nowPosition.X) < sizeL) &&
                    (Math.Abs(point.Y - nowPosition.Y) < sizeL))
                {
                    length += 10;
                    score += 10;
                    Score.Text = Convert.ToString(score);

                    //удаление яблок
                    applePoints.RemoveAt(n);
                    paintCanvas.Children.RemoveAt(n);
                    PaintNewApple(n);
                    break;
                }
                n++;
            }



            for (int q = 0; q < (snakePoints.Count - sizeL * 2); q++)
            {
                Point point = new Point(snakePoints[q].X, snakePoints[q].Y);

                if ((Math.Abs(point.X - nowPosition.X) < (sizeL)) &&
                     (Math.Abs(point.Y - nowPosition.Y) < (sizeL)))
                {
                    GameOver();
                    break;
                }
            }
        }
        
        public void GetButtonDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.S:
                    if (previousDirection != (int)DirectionToGo.Up)
                        direction = (int)DirectionToGo.Down;
                    break;
                case Key.Up:
                case Key.W:
                    if (previousDirection != (int)DirectionToGo.Down)
                        direction = (int)DirectionToGo.Up;
                    break;
                case Key.Left:
                case Key.A:
                    if (previousDirection != (int)DirectionToGo.Right)
                        direction = (int)DirectionToGo.Left;
                    break;
                case Key.Right:
                case Key.D:
                    if (previousDirection != (int)DirectionToGo.Left)
                        direction = (int)DirectionToGo.Right;
                    break;
                case Key.Escape:
                    direction = 0;
                    NavigationService.Navigate(new Pages.StopPage(this));
                    break;

            }
            previousDirection = direction;

        }
        private void PaintNewApple(int index)
        {
            Point apple = new Point(rnd.Next(20, 608), rnd.Next(5, 645));

            Ellipse newEllipse = new Ellipse
            {
                Fill = Brushes.Red,
                Width = (int)SnakeSize.Large,
                Height = (int)SnakeSize.Large
            };

            Canvas.SetTop(newEllipse, apple.Y);
            Canvas.SetLeft(newEllipse, apple.X);
            paintCanvas.Children.Insert(index, newEllipse);
            applePoints.Insert(index, apple);

        }

        private void GameOver()
        {
            MessageBox.Show($"Вы проиграли :( Счёт: {score}", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            //this.Close();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            direction = 0;
            NavigationService.Navigate(new Pages.StopPage(this));

            /*
             * page += 1;
            if (page % 2 == 1)
            {
                Stop.Visibility = Visibility.Hidden;

            }
            else
            {
                Stop.Visibility = Visibility.Visible;
            }*/

        }

    }
}
