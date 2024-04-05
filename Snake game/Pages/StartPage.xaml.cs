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

namespace Snake_game.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        //bool IsSave = false;
        public StartPage()
        {
            InitializeComponent();
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
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
