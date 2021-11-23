using MINIGAMES.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels
{
    public class SnakeLevel2 : SnakeLevel
    {
        public SnakeLevel2()
        {
            SetSize(15, 15);
            CreateFloor("dirt.png");
            CreateBarriers();
            CreateSnake();
            SetMaxScore(200);
        }

        protected override void CreateBarriers()
        {
            _barriers.AddRange(barStruct.CreateFenceBarriers(widthField, heigthField, "leaves.png"));
            _barriers.AddRange(barStruct.CreateTreeBarrier(4, 4, "wood.png", "leaves.png"));
            _barriers.AddRange(barStruct.CreateTreeBarrier(10, 4, "wood.png", "leaves.png"));
            _barriers.AddRange(barStruct.CreateTreeBarrier(4, 10, "wood.png", "leaves.png"));
            _barriers.AddRange(barStruct.CreateTreeBarrier(10, 10, "wood.png", "leaves.png"));
        }

        protected override void CreateSnake()
        {
            _snake = new SnakePlayer(4, 7, 3, Way.Right);
        }

        public override void SaveScore(int score)
        {
            if (User.userPlayers.snake.levelScore2 < score)
            {
                User.userPlayers.snake.levelScore2 = score;
            }
        }
    }
}
