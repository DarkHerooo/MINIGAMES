using MINIGAMES.Classes;
using MINIGAMES.Classes._Gamer._Enemy;
using MINIGAMES.Games.RPS.Classes;
using MINIGAMES.Games.RPS.Pages;
using MINIGAMES.Games.Snake.Pages;
using MINIGAMES.Windows.Main;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MINIGAMES.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private Opponent CreateRandomOpponent()
        {
            Random random = new Random();
            int randomNum = random.Next(Enemies.all.Length);
            Enemy enemy = Enemies.all[randomNum];
            return new Opponent(enemy);
        }

        private void PlayRPS()
        {
            Opponent opponentPlayer = new Opponent(User.userPlayers.player);
            Opponent opponentEnemy = CreateRandomOpponent();

            NavigationService.Navigate(new RPSGamePage(opponentPlayer, opponentEnemy));
        }

        private void PlaySnake()
        {
            NavigationService.Navigate(new SnakeSelectModePage());
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            TypeGame typeGame = WinMain.winInfo.typeGame;

            switch (typeGame)
            {
                case TypeGame.RPS: PlayRPS(); break;
                case TypeGame.Snake: PlaySnake(); break;
            }
        }

        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PlayerPage());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MinigamesPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnPlayer.DataContext = null;
            btnPlayer.DataContext = User.userPlayers.player;
        }
    }
}
