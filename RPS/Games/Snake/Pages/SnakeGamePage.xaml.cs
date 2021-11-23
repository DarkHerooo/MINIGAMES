﻿using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using MINIGAMES.Games.Snake.Classes._SnakeLevel;
using MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels;
using MINIGAMES.Pages;
using MINIGAMES.Windows.Main;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MINIGAMES.Games.Snake.Pages
{
    /// <summary>
    /// Логика взаимодействия для SnakeGamePage.xaml
    /// </summary>
    public partial class SnakeGamePage : Page
    {
        private Grid gridPrepare;
        private DispatcherTimer prepareTimer;
        private int countSecondsPrepare = 3;

        private DispatcherTimer gameTimer;
        private int countMillisecondsMove = 125;

        private Food food;
        private int score = 0;

        private SnakeLevel currentSnakeLevel;
        private SnakeLevel[] levels =
        {
            new SnakeLevel1(), new SnakeLevel2()
        };
        private int levelNumber;
        private bool infinityGame;

        public SnakeGamePage(int levelNumber, bool infinityGame)
        {
            InitializeComponent();
            FrameMain.ClearHistory();

            this.levelNumber = levelNumber;
            this.infinityGame = infinityGame;

            currentSnakeLevel = (SnakeLevel)levels[levelNumber].Clone();
            spUserInfo.DataContext = User.userPlayers.player;
            gridGameField.Focus();

            gridGameOver.Visibility = Visibility.Hidden;
            CreateGameField();
            CreateBarriers();
            CreateSnake();
            CreateFood();
            ShowScore();
            PrepareGame();
        }

        /// <summary>
        /// Создаёт Grid для отображения обратного отсчёта до начала игры
        /// </summary>
        private void CreateGridPrepare()
        {
            gridPrepare = new Grid();
            Grid.SetColumn(gridPrepare, 0);

            Rectangle rectangle = new Rectangle();
            rectangle.Fill = Brushes.White;
            rectangle.Opacity = 0.4;
            Panel.SetZIndex(rectangle, 0);
            gridPrepare.Children.Add(rectangle);

            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize = 50;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.Text = countSecondsPrepare.ToString();
            Panel.SetZIndex(textBlock, 1);
            gridPrepare.Children.Add(textBlock);

            gridMain.Children.Add(gridPrepare);
        }

        /// <summary>
        /// Производит подготовку к игре (обратный осчёт)
        /// </summary>
        private void PrepareGame()
        {
            CreateGridPrepare();

            prepareTimer = new DispatcherTimer();
            prepareTimer.Interval = TimeSpan.FromSeconds(1);
            prepareTimer.Tick += PrepareTimer_Tick;
            prepareTimer.Start();
        }

        /// <summary>
        /// Создаёт игровое поле
        /// </summary>
        private void CreateGameField()
        {
            for (int i = 0; i < currentSnakeLevel.widthField; i++)
            {
                gridGameField.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < currentSnakeLevel.heigthField; i++)
            {
                gridGameField.RowDefinitions.Add(new RowDefinition());
            }

            ImageBrush imageBrush = new ImageBrush(); 
            imageBrush.ImageSource =
                new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), currentSnakeLevel.bgImgSource));
            gridGameField.Background = imageBrush;
        }

        /// <summary>
        /// Создаёт препятствия на игровом поле
        /// </summary>
        private void CreateBarriers()
        {
            foreach(var barrier in currentSnakeLevel.barriers)
            {
                gridGameField.Children.Add(barrier.image);
            }
        }

        /// <summary>
        /// Создаёт еду на незанятом поле
        /// </summary>
        private void CreateFood()
        {
            if (food != null)
            {
                foreach (var element in gridGameField.Children)
                {
                    Image img = (Image)element;
                    if (img == food.image)
                    {
                        gridGameField.Children.Remove(img);
                        break;
                    }
                }
            }

            List<Food> allFood = new List<Food>();
            for (int i = 0; i < currentSnakeLevel.widthField; i++)
            {
                for (int j = 0; j < currentSnakeLevel.heigthField; j++)
                {
                    Food food = new Food(i, j, null);
                    allFood.Add(food);
                }
            }

            List<ObjectOnField> objectsOnField = new List<ObjectOnField>();
            objectsOnField.AddRange(currentSnakeLevel.barriers);
            objectsOnField.AddRange(currentSnakeLevel.snake.bodies);
            foreach (var objectOnField in objectsOnField)
            {
                foreach (var food in allFood)
                {
                    if (food.x == objectOnField.x && food.y == objectOnField.y)
                    {
                        allFood.Remove(food);
                        break;
                    }
                }
            }

            List<Food> possibleFood = new List<Food>();
            foreach(var food in allFood)
            {
                if (food == null) continue;
                possibleFood.Add(food);
            }

            Random random = new Random();
            int randNum = random.Next(possibleFood.Count);
            food = possibleFood[randNum];

            string[] foodImages = { "apple.png", "grape.png", "orange.png" };
            randNum = random.Next(foodImages.Length);
            food.SetImage(foodImages[randNum]);

            gridGameField.Children.Add(food.image);
        }

        /// <summary>
        /// Создаёт змею на игровом поле
        /// </summary>
        private void CreateSnake()
        {
            SnakePlayer snake = currentSnakeLevel.snake;
            foreach (var body in snake.bodies)
            {
                gridGameField.Children.Add(body.image);
            }
        }

        /// <summary>
        /// Проверяет голову змеи, не попала ли на препятствие
        /// </summary>
        /// <returns></returns>
        private bool CheckSnakeHead()
        {
            SnakeBody snakeHead = currentSnakeLevel.snake.bodies[0];
            int futureX = snakeHead.x;
            int futureY = snakeHead.y;
            Way headWay = snakeHead.way;

            switch (headWay)
            {
                case Way.Left: futureX--; break;
                case Way.Up: futureY--; break;
                case Way.Right: futureX++; break;
                case Way.Down: futureY++; break;
            }

            List<ObjectOnField> objectsOnField = new List<ObjectOnField>();
            objectsOnField.AddRange(currentSnakeLevel.barriers);
            objectsOnField.AddRange(currentSnakeLevel.snake.bodies);

            foreach (var objectOnField in objectsOnField)
            {
                if (objectOnField.x == futureX && objectOnField.y == futureY)
                {
                    SnakeDead();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Производит попытку съедения еды, если голова змеи попала на неё
        /// </summary>
        private void TryEatFood()
        {
            SnakeBody snakeHead = currentSnakeLevel.snake.bodies[0];
            if (snakeHead.x == food.x && snakeHead.y == food.y)
            {
                score += 10;
                currentSnakeLevel.snake.Growth();
                gridGameField.Children.Add
                    (currentSnakeLevel.snake.bodies[currentSnakeLevel.snake.bodies.Count - 1].image);
                CreateFood();
                ShowScore();
            }
        }

        /// <summary>
        /// Запускает игру
        /// </summary>
        private void StartGame()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(countMillisecondsMove);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        /// <summary>
        /// Показывает количество очков игрока
        /// </summary>
        private void ShowScore()
        {
            if (infinityGame)
            {
                tblScore.Text = score.ToString();
            }
            else
            {
                tblScore.Text = score + "/" + currentSnakeLevel.maxScore;
            }
        }

        /// <summary>
        /// Останавливает игру
        /// </summary>
        private void StopGame()
        {
            gameTimer.Stop();

            currentSnakeLevel.SaveScore(score);

            gridGameOver.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Проверяет, достигла ли змея максимального количества очков на уровне
        /// </summary>
        private bool SnakeWin()
        {
            if (!infinityGame)
            {
                if (score >= currentSnakeLevel.maxScore)
                {
                    tblGameMessage.Text = "Уровень пройден!";
                    tblBtnRestart.Text = "Следующий уровень";
                    btnPlay.Click += btnNextLevel_Click;
                    StopGame();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Происходит при смерти змеи
        /// </summary>
        private void SnakeDead()
        {
            currentSnakeLevel.snake.Dead();
            tblGameMessage.Text = "Вы проиграли!";

            if (infinityGame)
            {
                tblBtnRestart.Text = "Ещё раз!";
                btnPlay.Click += btnRestart_Click;
            }
            else
            {
                tblBtnRestart.Text = "Начать заново";
                btnPlay.Click += btnPlayAgain_Click;
            }
            StopGame();
        }

        /// <summary>
        /// Производит определённые действия на игровом поле за тик
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            TryEatFood();
            if (SnakeWin()) return;
            if (CheckSnakeHead()) currentSnakeLevel.snake.Move();

            currentSnakeLevel.snake.turn = false;
        }

        /// <summary>
        /// Производит обратный отсчёт до начала игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrepareTimer_Tick(object sender, EventArgs e)
        {
            countSecondsPrepare--;

            foreach (var element in gridPrepare.Children)
            {
                if (element is TextBlock tbl)
                {
                    tbl.Text = countSecondsPrepare.ToString();
                }
            }

            if (countSecondsPrepare <= 0)
            {
                gridPrepare.Visibility = Visibility.Hidden;
                prepareTimer.Stop();
                StartGame();
            }
        }

        /// <summary>
        /// Меняет путь в зависимости от нажатой клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridGameField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!currentSnakeLevel.snake.turn)
            {
                Way headWay = currentSnakeLevel.snake.bodies[0].way;

                switch (e.Key)
                {
                    case Key.Left:
                        if (headWay != Way.Right)
                            headWay = Way.Left;
                        break;
                    case Key.Up:
                        if (headWay != Way.Down)
                            headWay = Way.Up;
                        break;
                    case Key.Right:
                        if (headWay != Way.Left)
                            headWay = Way.Right;
                        break;
                    case Key.Down:
                        if (headWay != Way.Up)
                            headWay = Way.Down;
                        break;
                }

                if (headWay != currentSnakeLevel.snake.bodies[0].way)
                {
                    currentSnakeLevel.snake.bodies[0].way = headWay;
                    currentSnakeLevel.snake.turn = true;
                }
            }

            e.Handled = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(levelNumber, infinityGame));
        }

        private void btnNextLevel_Click(object sender, RoutedEventArgs e)
        {
            levelNumber++;

            if (levelNumber < levels.Length)
            {
                NavigationService.Navigate(new SnakeGamePage(levelNumber, infinityGame));
            }
            else
            {
                NavigationService.Navigate(new MainMenuPage());
            }
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            levelNumber = 0;
            NavigationService.Navigate(new SnakeGamePage(levelNumber, infinityGame));
        }
    }
}
