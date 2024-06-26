﻿using System;
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
using Snake_game.Pages;

namespace Snake_game
{
    public partial class MainWindow : Window
    {
        
        
        //private Table_All_Players _players = new Table_All_Players();

        //все списки
        public List<Point> snakePoints = new List<Point>();
        private List<Point> applePoints = new List<Point>();
        //public int page = 0;

        //инты
        private static Random rnd = new Random();
        private int direction = 0; //сейчас
        private int previousDirection = 0; //прошлое
        private int length = 50; //нарисованы на холсте
        private Point startPosition = new Point(rnd.Next(20, 608), rnd.Next(5, 645));
        private Point nowPosition = new Point();
        
        public static int score = 0; //счёт
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
        
        public MainWindow()
        {
            
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(GetButtonDown);
            
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

            
            
            for (int q = 1; q < (snakePoints.Count - sizeL * 2); q++)
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
        public void GetButtonDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                //case Key.S:
                    if (previousDirection != (int)DirectionToGo.Up)
                        direction = (int)DirectionToGo.Down;
                    break;
                case Key.Up:
                //case Key.W:
                    if (previousDirection != (int)DirectionToGo.Down)
                        direction = (int)DirectionToGo.Up;
                    break;
                case Key.Left:
                //case Key.A:
                    if (previousDirection != (int)DirectionToGo.Right)
                        direction = (int)DirectionToGo.Left;
                    break;
                case Key.Right:
                //case Key.D:
                    if (previousDirection != (int)DirectionToGo.Left)
                        direction = (int)DirectionToGo.Right;
                    break;
                case Key.Escape:
                    direction = 0;
                    MainFrame.Content = new Pages.StopPage();
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
            /*
            StringBuilder errors = new StringBuilder();
            //поиск незаполненных полей

            
            if (string.IsNullOrWhiteSpace(_players.PlayerName))
                errors.AppendLine("Введите имя!");
            _players.PlayerScore = 0;
            //проверка на наличие ошибок и их вывод
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //добавляем новую запись
            if (_players.ID == 0)
            {
                Entities.GetContext().Table_All_Players.Add(_players);
            }
            try
            {
                Entities.GetContext().SaveChanges();
                //MessageBox.Show("Данные сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            */

            MainFrame.Content = new Pages.PageMainMenu();
            RestartG();
            
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            direction = 0;
            MainFrame.Content = new Pages.StopPage();
            
            
        }

       public void RestartG()
        {
            snakePoints.Clear();
            paintCanvas.Children.Clear();
            direction = 0;
            length = 50;
            score = 0;
            applePoints.Clear();
            paintSnake(startPosition);
            nowPosition = startPosition;

            for (var n = 0; n < 10; n++)
            {
                PaintNewApple(n);
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
