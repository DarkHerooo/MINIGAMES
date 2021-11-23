using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Pages;
using MINIGAMES.Windows.Main;
using System.Windows;
using System.Windows.Controls;

namespace MINIGAMES.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MinigamesPage : Page
    {
        WinInfo winInfo = new WinInfo("/Images/LogoAndIcon/minigames.ico",
            "/Images/LogoAndIcon/minigames.png", "Миниигры", TypeGame.Nothing);

        public MinigamesPage()
        {
            InitializeComponent();

            FrameMain.ClearHistory();
            WinMain.SetWinInfo(winInfo);
        }

        private void GoToMainMenu(WinInfo winInfo)
        {
            WinMain.SetWinInfo(winInfo);
            NavigationService.Navigate(new MainMenuPage());
        }

        private void btnRPS_Click(object sender, RoutedEventArgs e)
        {
            string url = "/Games/RPS/Images/LogoAndIcon/";
            WinInfo winInfo = new WinInfo(url + "rps.ico",
            url + "rps.png", "Камень, ножницы, бумага", TypeGame.RPS);

            GoToMainMenu(winInfo);
        }

        private void btnSnake_Click(object sender, RoutedEventArgs e)
        {
            string url = "/Games/Snake/Images/LogoAndIcon/";
            WinInfo winInfo = new WinInfo(url + "snake.ico",
            url + "snake.png", "Змейка", TypeGame.Snake);

            GoToMainMenu(winInfo);
        }
    }
}
