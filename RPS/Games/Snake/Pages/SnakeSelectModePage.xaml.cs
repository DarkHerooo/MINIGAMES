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
    /// Логика взаимодействия для SnakeSelectModePage.xaml
    /// </summary>
    public partial class SnakeSelectModePage : Page
    {
        public SnakeSelectModePage()
        {
            InitializeComponent();
        }

        private void btnCampaign_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(0, false));
        }

        private void btnInfinityGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeSelectLevelPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
