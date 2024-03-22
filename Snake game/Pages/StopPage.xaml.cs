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
    /// Логика взаимодействия для StopPage.xaml
    /// </summary>
    public partial class StopPage : Page
    {
        private GameTest _gameTest;

        public StopPage(GameTest gameTest)
        {
            InitializeComponent();
            _gameTest = gameTest;
        }

        private void ButtonYes(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_gameTest);
            //Stop.Visibility = Visibility.Visible;

        }
        private void ButtonNo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PageMainMenu());
        }


    }
}
