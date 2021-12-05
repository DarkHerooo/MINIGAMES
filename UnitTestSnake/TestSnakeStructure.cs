using Microsoft.VisualStudio.TestTools.UnitTesting;
using MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure;
using MINIGAMES.Games.Snake.Classes;
using System;

namespace UnitTestSnake
{
    [TestClass]
    public class TestSnakeStructure
    {
        [TestMethod]
        public void CheckHeadImgName()
        {
            const string imgNameLifeHeadLeft = "Head/Life/left.png";
            const string imgNameLifeHeadUp = "Head/Life/up.png";
            const string imgNameDeadHeadUp = "Head/Dead/up.png";

            SnakeHead snakeHead = new SnakeHead(0, 0, null, Way.Left);
            
            Assert.AreEqual(snakeHead.imgName, imgNameLifeHeadLeft, "Голова змеи визуально не повёрнута направо");

            snakeHead.way = Way.Up;
            snakeHead.SetImage();
            
            Assert.AreEqual(snakeHead.imgName, imgNameLifeHeadUp, "Голова змеи визуально не повёрнута наверх");

            snakeHead.life = false;
            snakeHead.SetImage();
            
            Assert.AreEqual(snakeHead.imgName, imgNameDeadHeadUp, "Голова змеи визуально не погибла");
        }

        [TestMethod]
        public void CheckBodyImgName()
        {
            const string imgNameBodyHorizontal = "Body/horizontal.png";
            const string imgNameBodyVertical = "Body/vertical.png";

            SnakeBody snakeBody = new SnakeBody(0, 0, null, Way.Up);
            Assert.AreEqual(snakeBody.imgName, imgNameBodyVertical, "Тело змеи визуально не вертикально");

            snakeBody.way = Way.Down;
            snakeBody.SetImage();
            Assert.AreEqual(snakeBody.imgName, imgNameBodyVertical, "Тело змеи визуально не вертикально");

            snakeBody.way = Way.Left;
            snakeBody.SetImage();
            Assert.AreEqual(snakeBody.imgName, imgNameBodyHorizontal, "Тело змеи визуально не горизонтально");
        }

        [TestMethod]
        public void CheckTailImgName()
        {
            const string imgNameTailLeft = "Tail/left.png";
            const string imgNameTailUp = "Tail/up.png";
            const string imgNameTailDown = "Tail/down.png";

            SnakeTail snakeTail = new SnakeTail(0, 0, null, Way.Left);
            Assert.AreEqual(snakeTail.imgName, imgNameTailLeft, "Хвост змеи визуально не повёрнут налево");

            snakeTail.way = Way.Up;
            snakeTail.SetImage();
            Assert.AreEqual(snakeTail.imgName, imgNameTailUp, "Хвост змеи визуально не повёрнут наверх");

            snakeTail.way = Way.Down;
            snakeTail.SetImage();
            Assert.AreEqual(snakeTail.imgName, imgNameTailDown, "Хвост змеи визуально не повёрнут вниз");
        }
    }
}
