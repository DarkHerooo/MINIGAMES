using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure;
using MINIGAMES.Games.Snake.Classes._SnakeLevel;
using MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels;
using MINIGAMES.Pages;
using MINIGAMES.Windows.Main;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private SnakeLevelNum snakeLevelNum;
        private bool infinityGame;
        private SnakeLevel level;

        public SnakeGamePage(SnakeLevelNum snakeLevelNum, bool infinityGame)
        {
            InitializeComponent();
            FrameMain.ClearHistory();
            gridGameOver.Visibility = Visibility.Hidden;

            this.snakeLevelNum = snakeLevelNum;
            this.infinityGame = infinityGame;
            spUserInfo.DataContext = User.userPlayers.player;
            gridGameField.Focus();

            SetSnakeLevel();
            CreateGameField();
            CreateFloor();
            CreateBarriers();
            CreateSnake();
            CreateFood();
            ShowScore();
            PrepareGame();
        }

        /// <summary>
        /// Устанавливает, какой уровень должен быть запущен
        /// </summary>
        private void SetSnakeLevel()
        {
            switch(snakeLevelNum)
            {
                case SnakeLevelNum.First: level = new SnakeLevel1(); break;
                case SnakeLevelNum.Second: level = new SnakeLevel2(); break;
                case SnakeLevelNum.Third: level = new SnakeLevel3(); break;
            }
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
            for (int i = 0; i < level.widthField; i++)
            {
                gridGameField.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < level.heigthField; i++)
            {
                gridGameField.RowDefinitions.Add(new RowDefinition());
            }
        }

        /// <summary>
        /// Создаёт пол на игровом поле
        /// </summary>
        private void CreateFloor()
        {
            foreach (var floor in level.floor)
            {
                gridGameField.Children.Add(floor.image);
            }
        }

        /// <summary>
        /// Создаёт препятствия на игровом поле
        /// </summary>
        private void CreateBarriers()
        {
            foreach(var barrier in level.barriers)
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

            List<ObjectOnField> objectsOnField = new List<ObjectOnField>();
            objectsOnField.AddRange(level.barriers);
            objectsOnField.AddRange(level.snake.bodies);
            objectsOnField.Add(level.snake.head);
            objectsOnField.Add(level.snake.tail);

            List<Food> possibleFood = new List<Food>();

            foreach (var floor in level.floor)
            {
                bool possible = true;

                foreach (var objectOnField in objectsOnField)
                {
                    if (objectOnField.x == floor.x && objectOnField.y == floor.y)
                    {
                        possible = false;
                        break;
                    }
                }

                if (possible)
                {
                    Food food = new Food(floor.x, floor.y, null);
                    possibleFood.Add(food);
                }
            }

            Random random = new Random();
            int randNum = random.Next(possibleFood.Count);
            food = possibleFood[randNum];

            randNum = random.Next(level.possibleFoodStr.Length);
            food.SetImage(level.possibleFoodStr[randNum]);

            gridGameField.Children.Add(food.image);
        }

        /// <summary>
        /// Создаёт змею на игровом поле
        /// </summary>
        private void CreateSnake()
        {
            SnakePlayer snake = level.snake;

            gridGameField.Children.Add(snake.head.image);
            foreach (var body in snake.bodies)
            {
                gridGameField.Children.Add(body.image);
            }
            gridGameField.Children.Add(snake.tail.image);
        }

        /// <summary>
        /// Добавляет на игровое поле последнее тело змейки
        /// </summary>
        private void AddLastBody()
        {
            SnakePlayer snake = level.snake;
            SnakeBody body = snake.bodies[snake.bodies.Count - 1];
            if (body.image.Parent == null)
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
            SnakeHead head = level.snake.head;
            int futureX = head.x;
            int futureY = head.y;
            Way headWay = head.way;

            switch (headWay)
            {
                case Way.Left: futureX--; break;
                case Way.Up: futureY--; break;
                case Way.Right: futureX++; break;
                case Way.Down: futureY++; break;
            }

            List<ObjectOnField> objectsOnField = new List<ObjectOnField>();
            objectsOnField.AddRange(level.barriers);
            objectsOnField.AddRange(level.snake.bodies);

            foreach (var objectOnField in objectsOnField)
            {
                if (objectOnField.x == futureX && objectOnField.y == futureY)
                {
                    SnakeDead();
                    return false;
                }
            }

            AddLastBody();

            return true;
        }

        /// <summary>
        /// Производит попытку съедения еды, если голова змеи попала на неё
        /// </summary>
        private void TryEatFood()
        {
            SnakePlayer snake = level.snake;

            if (snake.bodies[snake.bodies.Count - 1].image.Parent == null)
            {
                gridGameField.Children.Add(snake.bodies[snake.bodies.Count - 1].image);
            }

            if (snake.head.x == food.x && snake.head.y == food.y)
            {
                score += 10;
                snake.Growth();
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
                tblScore.Text = score + "/" + level.maxScore;
            }
        }

        /// <summary>
        /// Проверяет, достигла ли змея максимального количества очков на уровне
        /// </summary>
        private bool SnakeWin()
        {
            if (!infinityGame)
            {
                if (score >= level.maxScore)
                {
                    tblGameMessage.Text = "Уровень пройден!";
                    tblBtnPlay.Text = "Следующий уровень";
                    btnPlay.Click += btnNextLevel_Click;
                    CreateBtnPlayHotkey(btnNextLevel_Click);
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
            level.snake.Dead();
            tblGameMessage.Text = "Вы проиграли!";

            if (infinityGame)
            {
                tblBtnPlay.Text = "Ещё раз!";
                btnPlay.Click += btnRestart_Click;
                CreateBtnPlayHotkey(btnRestart_Click);
            }
            else
            {
                tblBtnPlay.Text = "Начать заново";
                btnPlay.Click += btnPlayAgain_Click;
                CreateBtnPlayHotkey(btnPlayAgain_Click);
            }
            StopGame();
        }

        /// <summary>
        /// Останавливает игру
        /// </summary>
        private void StopGame()
        {
            gameTimer.Stop();

            level.SaveScore(score);

            CreateBtnBackHotkey(btnBack_Click);
            gridGameOver.Visibility = Visibility.Visible;
            gridGameOver.Focus();
        }

        /// <summary>
        /// Создаёт горячую клавишу
        /// </summary>
        /// <param name="hotkey"></param>
        /// <param name="executed"></param>
        private void CreateHotkey(Key hotkey, ExecutedRoutedEventHandler executed)
        {
            RoutedCommand routedCommand = new RoutedCommand();
            routedCommand.InputGestures.Add(new KeyGesture(hotkey));
            CommandBindings.Add(new CommandBinding(routedCommand, executed));
        }

        /// <summary>
        /// Создаёт горячую клавишу для кнопки "Играть"
        /// </summary>
        /// <param name="executed"></param>
        private void CreateBtnPlayHotkey(ExecutedRoutedEventHandler executed)
        {
            CreateHotkey(Key.Enter, executed);
        }

        /// <summary>
        /// Создаёт горячую клавишу для кнопки "Вернуться"
        /// </summary>
        /// <param name="executed"></param>
        private void CreateBtnBackHotkey(ExecutedRoutedEventHandler executed)
        {
            CreateHotkey(Key.Escape, executed);
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
            if (CheckSnakeHead()) level.snake.Move();

            level.snake.turn = false;
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
            if (!level.snake.turn)
            {
                Way headWay = level.snake.head.way;

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

                if (headWay != level.snake.head.way)
                {
                    level.snake.head.way = headWay;
                    level.snake.turn = true;
                }
            }

            e.Handled = true;
        }

        private void gridGameOver_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            gridGameOver.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.ClearHistory();
            NavigationService.Navigate(new MainMenuPage());
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SnakeGamePage(snakeLevelNum, infinityGame));
        }

        private void btnNextLevel_Click(object sender, RoutedEventArgs e)
        {
            int levelNum = (int)snakeLevelNum;
            int maxLevelNum = Enum.GetValues(typeof(SnakeLevelNum)).Cast<int>().Max();

            if (levelNum != maxLevelNum)
            {
                levelNum++;
                NavigationService.Navigate(new SnakeGamePage((SnakeLevelNum)levelNum, infinityGame));
            }
            else
            {
                NavigationService.Navigate(new SnakeWinPage());
            }
        }

        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            snakeLevelNum = SnakeLevelNum.First;
            NavigationService.Navigate(new SnakeGamePage(snakeLevelNum, infinityGame));
        }
    }
}
