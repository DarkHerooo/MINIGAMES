using MINIGAMES.Games.Snake.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels;
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

namespace MINIGAMES.Games.Snake.Pages
{
    /// <summary>
    /// Логика взаимодействия для SnakeSelectLevel.xaml
    /// </summary>
    public partial class SnakeSelectLevelPage : Page
    {
        public SnakeSelectLevelPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnFirstLevel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(0, true));
        }

        private void btnSecondLevel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(1, true));
        }

        private void btnThirdLevel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(2, true));
        }
    }
}
