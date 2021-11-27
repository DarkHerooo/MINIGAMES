using MINIGAMES.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для SnakeWinPage.xaml
    /// </summary>
    public partial class SnakeWinPage : Page
    {
        private int width = 20;
        private int heigth = 20;

        public SnakeWinPage()
        {
            InitializeComponent();

            CreateGrid();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < width; i++)
            {
                gridBackground.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < heigth; i++)
            {
                gridBackground.RowDefinitions.Add(new RowDefinition());
            }

            CreateBackground();
        }

        private void CreateBackground()
        {
            string[] files = Directory.GetFiles("../../Games/Snake/Images/Food/");
            List<string> allFoodStr = new List<string>();

            foreach (var foodStr in files)
            {
                FileInfo fileInfo = new FileInfo(foodStr);
                allFoodStr.Add(fileInfo.Name);
            }
            Random random = new Random();

            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int randNum = random.Next(allFoodStr.Count);
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("/Games/Snake/Images/Food/" + allFoodStr[randNum], UriKind.Relative));
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                    gridBackground.Children.Add(image);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }
    }
}
