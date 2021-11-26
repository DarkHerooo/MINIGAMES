using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
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
            CreateFloor("grass.png");
            CreateBarriers();
            CreateSnake();
            SetMaxScore(200);
        }

        protected override void CreateBarriers()
        {
            _barriers.AddRange(barStruct.CreateFenceBarriers(widthField, heigthField, "oak_leaves.png"));
            CreateTree(4, 5);
            CreateTree(10, 5);
            CreateTree(4, 12);
            CreateTree(10, 12);
        }

        public void CreateTree(int bottomX, int bottomY)
        {
            _barriers.Add(new Barrier(bottomX, bottomY, "oak_wood.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 1, bottomY - 1, 3, "oak_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 1, bottomY - 2, 3, "oak_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 1, bottomY - 3, 3, "oak_leaves.png"));
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
