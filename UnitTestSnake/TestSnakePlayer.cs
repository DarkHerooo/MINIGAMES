using Microsoft.VisualStudio.TestTools.UnitTesting;
using MINIGAMES.Games.Snake.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure;
using System;

namespace UnitTestSnake
{
    [TestClass]
    public class TestSnakePlayer
    {
        [TestMethod]
        public void CheckSnakeHeadMove()
        {
            int snakeHeadX = 0, snakeHeadY = 0;

            SnakePlayer snake = new SnakePlayer(5, 5, 3, Way.Right);
            snakeHeadX = snake.head.x;
            snakeHeadX++;
            snake.Move();

            Assert.AreEqual(snake.head.x, snakeHeadX, "Голова змеи подвинулась некорректно по X");

            snake.head.way = Way.Up;
            snakeHeadY = snake.head.y;
            snakeHeadY--;
            snake.Move();
            Assert.AreEqual(snake.head.y, snakeHeadY, "Голова змеи подвинулась некорректно по Y");
        }

        [TestMethod]
        public void CheckSnakeBodyMove()
        {
            SnakePlayer snake = new SnakePlayer(7, 10, 5, Way.Down);
            int snakeHeadX = snake.head.x;
            int snakeHeadY = snake.head.y;
            Way snakeHeadWay = snake.head.way;
            snake.Move();

            SnakeBody firstSnakeBody = snake.bodies[0];
            Assert.AreEqual(firstSnakeBody.x, snakeHeadX, "Тело змеи подвинулось некорректно по X");
            Assert.AreEqual(firstSnakeBody.y, snakeHeadY, "Тело змеи подвинулось некорректно по Y");
            Assert.AreEqual(firstSnakeBody.way, snakeHeadWay, "Тело змеи получило неправильное направление");

            snake.head.way = Way.Right;
            int penultimateSnakeBodyIndex = snake.bodies.Count - 2;
            int penultimateSnakeBodyX = snake.bodies[penultimateSnakeBodyIndex].x;
            int penultimateSnakeBodyY = snake.bodies[penultimateSnakeBodyIndex].y;
            Way penultimateSnakeBodyWay = snake.bodies[penultimateSnakeBodyIndex].way;
            snake.Move();

            SnakeBody lastSnakeBody = snake.bodies[penultimateSnakeBodyIndex + 1];
            Assert.AreEqual(lastSnakeBody.x, penultimateSnakeBodyX, "Тело змеи подвинулось некорректно по X");
            Assert.AreEqual(lastSnakeBody.y, penultimateSnakeBodyY, "Тело змеи подвинулось некорректно по Y");
            Assert.AreEqual(lastSnakeBody.way, penultimateSnakeBodyWay, "Тело змеи получило неправильное направление");
        }

        [TestMethod]
        public void CheckSnakeTailMove()
        {
            SnakePlayer snake = new SnakePlayer(4, 13, 9, Way.Up);
            int lastSnakeBodyIndex = snake.bodies.Count - 1;
            int lastSnakeBodyX = snake.bodies[lastSnakeBodyIndex].x;
            int lastSnakeBodyY = snake.bodies[lastSnakeBodyIndex].y;
            Way lastSnakeBodyWay = snake.bodies[lastSnakeBodyIndex].way;
            snake.Move();

            SnakeTail snakeTail = snake.tail;
            Assert.AreEqual(snakeTail.x, lastSnakeBodyX, "Хвост змеи подвинулся некорректно по X");
            Assert.AreEqual(snakeTail.y, lastSnakeBodyY, "Хвост змеи подвинулся некорректно по Y");
            Assert.AreEqual(snakeTail.way, lastSnakeBodyWay, "Хвост змеи получил неправильное направление");
        }

        [TestMethod]
        public void CheckSnakeTurn()
        {
            SnakePlayer snake = new SnakePlayer(10, 10, 5, Way.Left);
            SnakeBody firstSnakeBody = snake.bodies[0];

            snake.turn = false;
            string imgNameBodyHorizontal = "Body/horizontal.png";
            Assert.AreEqual(firstSnakeBody.imgName, imgNameBodyHorizontal, "Тело змеи визуально не горизонтально");

            snake.head.way = Way.Up;
            snake.turn = true;
            snake.Move();
            string imgNameBodyTurnUp = "Turn/downRight.png";
            Assert.AreEqual(firstSnakeBody.imgName, imgNameBodyTurnUp, "Тело змеи визуально не повёрнуто наверх");

            int lastSnakeBodyIndex = snake.bodies.Count - 1;
            SnakeBody lastSnakeBody = snake.bodies[lastSnakeBodyIndex];

            for (int i = 0; i < lastSnakeBodyIndex; i++)
            {
                snake.Move();
            }

            Assert.AreEqual(lastSnakeBody.imgName, imgNameBodyTurnUp, "Тело змеи визуально не повёрнуто наверх");
        }

        [TestMethod]
        public void CheckSnakeGrowth()
        {
            SnakePlayer snake = new SnakePlayer(1, 1, 12, Way.Up);
            int countSnakeBodies = snake.bodies.Count;
            snake.Growth();
            Assert.AreNotEqual(snake.bodies.Count, countSnakeBodies, "Змейка не выросла");

            countSnakeBodies = snake.bodies.Count;
            SnakeBody newSnakeBody = snake.bodies[countSnakeBodies - 1];
            SnakeTail snakeTail = snake.tail;
            Assert.AreEqual(newSnakeBody.x, snakeTail.x, "Координата X нового тела змейки неверна");
            Assert.AreEqual(newSnakeBody.y, snakeTail.y, "Координата Y нового тела змейки неверна");
            Assert.AreEqual(newSnakeBody.way, snakeTail.way, "Направление нового тела змейки неверна");

            snake.Move();
            Assert.AreNotEqual(newSnakeBody.y, snakeTail.y, "Координата Y нового тела змейки неверна");
        }

        [TestMethod]
        public void CheckSnakeDead()
        {
            SnakePlayer snake = new SnakePlayer(5, 7, 5, Way.Right);
            bool snakeLife = snake.head.life;

            snake.Dead();
            Assert.AreNotEqual(snake.head.life, snakeLife, "Змейка не умерла");

            string imgNameDeadHeadRight = "Head/Dead/right.png";
            Assert.AreEqual(snake.head.imgName, imgNameDeadHeadRight, "Змейка визуально не умерла");
        }

        [TestMethod]
        public void CheckCreateStartSnake()
        {
            int snakeLength = 7;
            SnakePlayer snake = new SnakePlayer(10, 10, snakeLength, Way.Right);

            int countBodies = snake.bodies.Count;
            Assert.AreEqual(countBodies + 2, snakeLength, "Длина змейки некорректна");
        }

    }
}
