using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels
{
    public class SnakeLevel1 : SnakeLevel
    {
        public SnakeLevel1()
        {
            SetSize(15, 15);
            CreateFloor("oakPlanks.png");
            CreateBarriers();
            CreateSnake();
            SetMaxScore(400);
        }

        protected override void CreateBarriers()
        {
            _barriers.AddRange(barStruct.CreateFenceBarriers(widthField, heigthField, "wood.png"));
        }

        protected override void CreateSnake()
        {
            Random random = new Random();
            int startLength = 3;
            int randX = random.Next(startLength, widthField - startLength);
            int randY = random.Next(startLength, heigthField - startLength);
            Way[] ways = { Way.Down, Way.Left, Way.Right, Way.Up };
            Way randWay = ways[random.Next(ways.Length)];
            _snake = new SnakePlayer(randX, randY, startLength, randWay);
        }

        public override void SaveScore(int score)
        {
            if (User.userPlayers.snake.levelScore1 < score)
            {
                User.userPlayers.snake.levelScore1 = score;
            }
        }
    }
}
