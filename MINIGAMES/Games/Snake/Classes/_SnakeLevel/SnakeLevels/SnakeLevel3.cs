using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._SnakeLevel.SnakeLevels
{
    public class SnakeLevel3 : SnakeLevel
    {
        public SnakeLevel3()
        {
            SetSize(21, 21);
            CreateFloor("snow.png");
            CreateBarriers();
            CreateSnake();
            SetPossibleFoodStr();
            SetMaxScore(250);
        }

        protected override void CreateBarriers()
        {
            _barriers.AddRange(barStruct.CreateFenceBarriers(widthField, heigthField, "ice.png"));
            CreateChristmasTree(10, 12);

            CreateRightCandy(2, 6);
            CreateSnowman(4, 8);

            CreateRightCandy(2, 16);
            CreateSnowman(4, 18);

            CreateLeftCandy(18, 6);
            CreateSnowman(16, 8);

            CreateLeftCandy(18, 16);
            CreateSnowman(16, 18);
        }

        private void CreateChristmasTree(int bottomX, int bottomY)
        {
            _barriers.Add(new Barrier(bottomX, bottomY, "spruce_wood.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 3, bottomY - 1, 7, "spruce_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 2, bottomY - 2, 5, "spruce_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 2, bottomY - 3, 5, "spruce_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 1, bottomY - 4, 3, "spruce_leaves.png"));
            _barriers.AddRange(barStruct.CreateHorisontalBarrier(bottomX - 1, bottomY - 5, 3, "spruce_leaves.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 6, "glowstone.png"));

            _barriers.Add(new Barrier(bottomX - 2, bottomY - 1, "pink_wool.png"));
            _barriers.Add(new Barrier(bottomX + 2, bottomY - 2, "green_wool.png"));
            _barriers.Add(new Barrier(bottomX - 1, bottomY - 3, "blue_wool.png"));
            _barriers.Add(new Barrier(bottomX + 1, bottomY - 4, "pink_wool.png"));

            _barriers.Add(new Barrier(bottomX - 2, bottomY + 1, "Gift/pink_gift.png"));
            _barriers.Add(new Barrier(bottomX + 2, bottomY + 1, "Gift/yellow_gift.png"));
        }

        private void CreateLeftCandy(int bottomX, int bottomY)
        {
            _barriers.Add(new Barrier(bottomX, bottomY, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 1, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 2, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 3, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX - 1, bottomY - 4, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX - 2, bottomY - 3, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX - 1, bottomY - 3, null));
        }

        private void CreateRightCandy(int bottomX, int bottomY)
        {
            _barriers.Add(new Barrier(bottomX, bottomY, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 1, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 2, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 3, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX + 1, bottomY - 4, "gray_wool.png"));
            _barriers.Add(new Barrier(bottomX + 2, bottomY - 3, "red_wool.png"));
            _barriers.Add(new Barrier(bottomX + 1, bottomY - 3, null));
        }

        private void CreateSnowman(int bottomX, int bottomY)
        {
            string uriString = "Snowman/";
            _barriers.Add(new Barrier(bottomX, bottomY, uriString + "legs.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 1, uriString + "body.png"));
            _barriers.Add(new Barrier(bottomX, bottomY - 2, uriString + "head.png"));
        }

        protected override void CreateSnake()
        {
            _snake = new SnakePlayer(9, 17, 3, Way.Left);
        }

        protected override void SetPossibleFoodStr()
        {
            _possibleFoodStr = new string[] { "chocolate.png", "orange.png", "cookie.png",
                "chocolate_milk.png"};
        }

        public override void SaveScore(int score)
        {
            if (User.userPlayers.snake.levelScore3 < score)
            {
                User.userPlayers.snake.levelScore3 = score;
            }
        }
    }
}
