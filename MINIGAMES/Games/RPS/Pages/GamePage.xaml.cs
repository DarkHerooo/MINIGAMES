using MINIGAMES.Games.RPS.Classes;
using MINIGAMES.Games.RPS.Classes._Item._ItemCombination;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MINIGAMES.Games.RPS.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class RPSGamePage : Page
    {
        private int countRounds = 1;
        private int maxWinRounds = 5;

        private Opponent opponentPlayer;
        private Opponent opponentEnemy;

        public RPSGamePage(Opponent opponentPlayer, Opponent opponentEnemy)
        {
            InitializeComponent();

            this.opponentPlayer = opponentPlayer;
            this.opponentEnemy = opponentEnemy;

            SetOpponentsInformation();
            ShowScore();
            ShowMaxRounds();
            ShowRoundNumber();
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
            tblScore.Text = opponentPlayer.countWinRounds +
                ":" + opponentEnemy.countWinRounds;
        }

        /// <summary>
        /// Показывает максимальное количество раундов
        /// </summary>
        public void ShowMaxRounds()
        {
            tblMaxRounds.Text = "(до " + maxWinRounds + " побед)"; 
        }

        /// <summary>
        /// Показывает текущий раунд
        /// </summary>
        public void ShowRoundNumber()
        {
            tblRoundNumber.Text = "Раунд №" + countRounds;
        }
        
        /// <summary>
        /// Задаёт случайную предмет-комбинацию оппоненту-противнику
        /// </summary>
        public void SetEnemyItemCombination()
        {
            Random random = new Random();
            int randomNum = random.Next(ItemCombinations.all.Length);
            opponentEnemy.itemCombination = ItemCombinations.all[randomNum];
        }

        /// <summary>
        /// Сравнивает количество выигранных раундов оппонентов с 
        /// максимальным количеством
        /// </summary>
        public void CheckResults()
        {
            if (opponentPlayer.countWinRounds >= maxWinRounds
                || opponentEnemy.countWinRounds >= maxWinRounds)
            {
                NavigationService.Navigate(
                    new RPSGameResultPage(opponentPlayer, opponentEnemy));
            }
        }

        /// <summary>
        /// Проверяет предметы оппонентов и возвращает результат
        /// </summary>
        /// <returns>Символ "больше", "меньше" или "равно" в зависимости от
        /// результата</returns>
        public string CheckOpponentItems()
        {
            if (opponentPlayer.itemCombination.item == 
                opponentEnemy.itemCombination.weakItem)
            {
                opponentPlayer.WinRound();
                return ">";
            }
            else if (opponentEnemy.itemCombination.item ==
                opponentPlayer.itemCombination.weakItem)
            {
                opponentEnemy.WinRound();
                return "<";
            }
            else
            {
                return "=";
            }
        }

        /// <summary>
        /// Создаёт и показывает результат раунда
        /// </summary>
        public void CreateRoundResult()
        {
            ItemCombination playerItemCombination =
                opponentPlayer.itemCombination;
            ItemCombination enemyItemCombination =
                opponentEnemy.itemCombination;

            Image imgPlayerItem = new Image();
            imgPlayerItem.Width = 32;
            imgPlayerItem.Height = 32;
            imgPlayerItem.Source =
                new BitmapImage(new Uri(playerItemCombination.item.imgSource, UriKind.Relative));

            Image imgEnemyItem = new Image();
            imgEnemyItem.Width = 32;
            imgEnemyItem.Height = 32;
            imgEnemyItem.Source =
                new BitmapImage(new Uri(enemyItemCombination.item.imgSource, UriKind.Relative));

            TextBlock tblResult = new TextBlock();
            tblResult.FontSize = 25;
            tblResult.FontWeight = FontWeights.Bold;
            tblResult.Margin = new Thickness(0, 5, 0, 5);
            tblResult.Text = CheckOpponentItems();

            StackPanel spResult = new StackPanel();
            spResult.Orientation = Orientation.Horizontal;
            spResult.Children.Add(imgPlayerItem);
            spResult.Children.Add(tblResult);
            spResult.Children.Add(imgEnemyItem);

            Border brdResult = new Border();
            brdResult.Background = Brushes.AliceBlue;
            brdResult.BorderBrush = Brushes.Black;
            brdResult.BorderThickness = new Thickness(2);
            brdResult.Padding = new Thickness(1);
            brdResult.HorizontalAlignment = HorizontalAlignment.Center;
            brdResult.Margin = new Thickness(0, 10, 0, 10);
            brdResult.Child = spResult;

            spRoundResults.Children.Add(brdResult);
            svRoundResults.ScrollToEnd();
        }

        /// <summary>
        /// Запускает раунд
        /// </summary>
        public void PlayRound()
        {
            countRounds++;

            SetEnemyItemCombination();
            CreateRoundResult();
            ShowScore();
            ShowRoundNumber();
            CheckResults();
        }

        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            opponentPlayer.itemCombination = ItemCombinations.rock;
            PlayRound();
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            opponentPlayer.itemCombination = ItemCombinations.scissors;
            PlayRound();
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            opponentPlayer.itemCombination = ItemCombinations.paper;
            PlayRound();
        }

        private void btnSurrender_Click(object sender, RoutedEventArgs e)
        {
            opponentPlayer.countWinRounds = 0;
            opponentEnemy.countWinRounds = maxWinRounds;
            NavigationService.Navigate(
                new RPSGameResultPage(opponentPlayer, opponentEnemy));
        }
    }
}
