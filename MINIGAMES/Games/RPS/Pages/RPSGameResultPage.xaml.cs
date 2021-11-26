using MINIGAMES.Classes;
using MINIGAMES.Games.RPS.Classes;
using MINIGAMES.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MINIGAMES.Games.RPS.Pages
{
    /// <summary>
    /// Логика взаимодействия для GameResultPage.xaml
    /// </summary>
    public partial class RPSGameResultPage : Page
    {
        Opponent opponentPlayer;
        Opponent opponentEnemy;
        public RPSGameResultPage(Opponent opponentPlayer, Opponent opponentEnemy)
        {
            InitializeComponent();

            this.opponentPlayer = opponentPlayer;
            this.opponentEnemy = opponentEnemy;

            SetOpponentsInformation();
            ShowScore();
            ShowResult();
        }

        /// <summary>
        /// Устанавливает информацию об оппонентах (имя, аватар)
        /// </summary>
        public void SetOpponentsInformation()
        {
            gridPlayer.DataContext = opponentPlayer.gamer;
            gridEnemy.DataContext = opponentEnemy.gamer;
        }

        /// <summary>
        /// Показывает счёт оппонентов
        /// </summary>
        public void ShowScore()
        {
            tblScore.Text = opponentPlayer.countWinRounds + ":" +
                opponentEnemy.countWinRounds;
        }

        /// <summary>
        /// Проверяет количество выигранных раундов у оппонентов и
        /// выводит результат
        /// </summary>
        public void ShowResult()
        {
            if (opponentPlayer.countWinRounds > opponentEnemy.countWinRounds)
            {
                gridMain.Background = Brushes.LightBlue;
                tblResult.Text = "Победа!";
                User.userPlayers.rps.WinMatch();
            }
            else
            {
                gridMain.Background = Brushes.IndianRed;
                tblResult.Text = "Поражение!";
            }

            User.userPlayers.rps.GameOver();
        }

        private void btnGoMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void btnRevenge_Click(object sender, RoutedEventArgs e)
        {
            opponentPlayer.countWinRounds = 0;
            opponentEnemy.countWinRounds = 0;
            NavigationService.Navigate(new RPSGamePage(opponentPlayer, opponentEnemy));
        }
    }
}
