using MINIGAMES.Classes;
using MINIGAMES.Windows.Main;
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

namespace MINIGAMES.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        private string[] imageSources;
        private int index = 0;

        private List<Achievement> achievements = new List<Achievement>();

        public PlayerPage()
        {
            InitializeComponent();

            CreateImageSources();
            ShowPlayerInformation();
        }

        private void CreateImageSources()
        {
            string str = "/Images/Players/";

            imageSources = new string[]
            {
                str + "angryCat.png", 
                str + "goodBoy.png",
                str + "waowCat.png"
            };
        }

        private void ShowAchievements()
        {
            int countInRow = 4;
            for (int i = 0; i < achievements.Count; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Margin = new Thickness(5);

                for (int j = i * countInRow; j < i * countInRow + countInRow; j++)
                {
                    if (j >= achievements.Count) break;

                    stackPanel.Children.Add(achievements[j].border);
                }

                spAchievements.Children.Add(stackPanel);
            }
        }

        public void ShowRPSAchievements()
        {
            string url = "/Games/RPS/Images/Achievements/";

            Achievement[] achievements =
            {
                new Achievement("Количество побед", url + "trophy.png",
                User.userPlayers.rps.countWinMatches),
                new Achievement("Количество игр", url + "game.png",
                User.userPlayers.rps.countGames),
            };

            this.achievements.AddRange(achievements);
            ShowAchievements();
        }

        public void ShowSnakeAchievements()
        {
            string url = "/Games/Snake/Images/Achievements/";

            Achievement[] achievements =
            {
                new Achievement("Рекорд уровня ''Коробка''", url + "box.png",
                User.userPlayers.snake.levelScore1),
                new Achievement("Рекорд уровня ''Лес''", url + "forest.png",
                User.userPlayers.snake.levelScore2),
                new Achievement("Рекорд уровня ''Рождество''", url + "christmas.png",
                User.userPlayers.snake.levelScore3),
                new Achievement("Пройдено кампаний", url + "campage.png",
                User.userPlayers.snake.countCampageWins),
                new Achievement("Количество игр", url + "game.png",
                User.userPlayers.snake.countGames),
            };

            this.achievements.AddRange(achievements);
            ShowAchievements();
        }

        /// <summary>
        /// Показывает информацию об игроке (имя, аватарка, достижения)
        /// </summary>
        private void ShowPlayerInformation()
        {
            FindImageIndex();
            DataContext = User.userPlayers.player;

            TypeGame typeGame = WinMain.winInfo.typeGame;

            switch(typeGame)
            {
                case TypeGame.RPS: ShowRPSAchievements(); break;
                case TypeGame.Snake: ShowSnakeAchievements(); break;
            }
        }

        /// <summary>
        /// Ищет индекс картинки пользователя в массиве всех картинок
        /// </summary>
        private void FindImageIndex()
        {
            for (int i = 0; i < imageSources.Length; i++)
            {
                if (imageSources[i] == User.userPlayers.player.imgSource)
                {
                    index = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Меняет картинку у пользователя
        /// </summary>
        private void ChangeImage()
        {
            User.userPlayers.player.imgSource = imageSources[index];
            DataContext = null;
            DataContext = User.userPlayers.player;
        }

        private void btnGoMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = imageSources.Length - 1;
            }

            ChangeImage();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (index < imageSources.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            ChangeImage();
        }
    }
}
