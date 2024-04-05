using System;
using System.Collections;
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

namespace Snake_game.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private Table_All_Players _players = new Table_All_Players();
        
        public StartPage()
        {
            InitializeComponent();
            DataContext = _players;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите играть под таким именем?", "Вниманиие", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
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

            }
            
            
            ///////////

            SaveName.Content = "✔";
            SaveName.IsEnabled = false;
            ButtonNewGame.Visibility = Visibility.Visible;
            Exit.Visibility = Visibility.Visible;

        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
