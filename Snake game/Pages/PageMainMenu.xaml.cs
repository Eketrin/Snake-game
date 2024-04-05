using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Snake_game;

namespace Snake_game.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMainMenu.xaml
    /// </summary>
    public partial class PageMainMenu : Page
    {
        

        public PageMainMenu()
        {
            InitializeComponent();
            ScoreOnPage.Text = Convert.ToString(MainWindow.score);
            DataGridUser.ItemsSource =
            Entities.GetContext().Table_All_Players.ToList();
        }

        private void ButtonNewGame(object sender, RoutedEventArgs e)
        {
           //Holder.IsRestart = true;
           NavigationService.Navigate(null);
           //NavigationService.Navigate(null);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DataGridUser_SelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {

        }
    }
}
