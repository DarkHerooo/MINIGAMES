using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MINIGAMES.Games.Snake.Classes
{
    public class SnakePlayer
    {
        public List<SnakeBody> bodies = new List<SnakeBody>();
        public bool turn = false;

        public SnakePlayer(int headX, int headY, int startLength, Way startWay)
        {
            string uriString = ReturnImgUriHead(startWay, true);
            SnakeBody snakeHead = new SnakeBody(headX, headY, uriString, startWay);
            bodies.Add(snakeHead);

            CreateStartSnake(startLength);
        }

        /// <summary>
        /// Создаёт стартовую змею с указанной длиной
        /// </summary>
        /// <param name="startLength"></param>
        private void CreateStartSnake(int startLength)
        {
            SnakeBody snakeHead = bodies[0];
            string imgUri = ReturnImgUriBody(snakeHead.way);
            switch (snakeHead.way)
            {
                case Way.Left:
                    for (int i = 1; i < startLength; i++)
                    {
                        SnakeBody body = new SnakeBody(snakeHead.x + i, 
                            snakeHead.y, imgUri, snakeHead.way);
                        bodies.Add(body);
                    }
                    break;
                case Way.Up:
                    for (int i = 1; i < startLength; i++)
                    {
                        SnakeBody body = new SnakeBody(snakeHead.x,
                            snakeHead.y + i, imgUri, snakeHead.way);
                        bodies.Add(body);
                    }
                    break;
                case Way.Right:
                    for (int i = 1; i < startLength; i++)
                    {
                        SnakeBody body = new SnakeBody(snakeHead.x - i,
                            snakeHead.y, imgUri, snakeHead.way);
                        bodies.Add(body);
                    }
                    break;
                case Way.Down:
                    for (int i = 1; i < startLength; i++)
                    {
                        SnakeBody body = new SnakeBody(snakeHead.x, 
                            snakeHead.y - i, imgUri, snakeHead.way);
                        bodies.Add(body);
                    }
                    break;
            }

            SnakeBody snakeTail = bodies[bodies.Count - 1];
            imgUri = ReturnImgUriTail(snakeHead.way);
            snakeTail.SetImage(imgUri);
            
        }

        private string ReturnImgUriHead(Way way, bool life)
        {
            string uriString = "Head/";
            uriString += life ? "Life/" : "Dead/";

            switch (way)
            {
                case Way.Left: uriString += "left.png"; break;
                case Way.Up: uriString += "up.png"; break;
                case Way.Right: uriString += "right.png"; break;
                case Way.Down: uriString += "down.png"; break;
            }

            return uriString;
        }

        private string ReturnImgUriBody(Way way)
        {
            string uriString = "Body/";

            if (way == Way.Left || way == Way.Right)
            {
                uriString += "horizontal.png";
            }
            else if (way == Way.Up || way == Way.Down)
            {
                uriString += "vertical.png";
            }

            return uriString;
        }

        private string ReturnImgUriTail(Way way)
        {
            string uriString = "Tail/";

            switch (way)
            {
                case Way.Left: uriString += "left.png"; break;
                case Way.Up: uriString += "up.png"; break;
                case Way.Right: uriString += "right.png"; break;
                case Way.Down: uriString += "down.png"; break;
            }

            return uriString;
        }

        /// <summary>
        /// Двигает голову змеи
        /// </summary>
        private void MoveHead()
        {
            SnakeBody snakeHead = bodies[0];
            switch (snakeHead.way)
            {
                case Way.Left: snakeHead.x--; break;
                case Way.Up: snakeHead.y--; break;
                case Way.Right: snakeHead.x++; break;
                case Way.Down: snakeHead.y++; break;
            }
            snakeHead.SetLocation();

            string uriString = ReturnImgUriHead(snakeHead.way, true);
            snakeHead.SetImage(uriString);

            TryTurn();
        }

        /// <summary>
        /// Двигает тело змеи
        /// </summary>
        private void MoveBody()
        {
            for (int i = bodies.Count - 1; i > 0; i--)
            {
                SnakeBody selectBody = bodies[i];
                SnakeBody nextBody = bodies[i - 1];

                selectBody.x = nextBody.x;
                selectBody.y = nextBody.y;
                selectBody.SetLocation();

                if (i == bodies.Count - 1)
                {
                    selectBody.way = nextBody.way;
                    string uriString = ReturnImgUriTail(selectBody.way);
                    selectBody.SetImage(uriString);
                }

                if (i != bodies.Count - 1 && i != 1)
                {
                    selectBody.way = nextBody.way;
                    selectBody.SetImage(nextBody.imgUri);
                }
            }
        }

        /// <summary>
        /// Производит попытку установить картинку-поворот, если змея поворачивает
        /// </summary>
        private void TryTurn()
        {
            if (turn)
            {
                Way headWay = bodies[0].way;
                Way turnWay = bodies[1].way;

                string urlString = "Turn/";
                if ((headWay == Way.Left && turnWay == Way.Down) ||
                    (headWay == Way.Up && turnWay == Way.Right))
                {
                    urlString += "downLeft.png";
                }
                else if ((headWay == Way.Up && turnWay == Way.Left) ||
                    (headWay == Way.Right && turnWay == Way.Down))
                {
                    urlString += "downRight.png";
                }
                else if ((headWay == Way.Left && turnWay == Way.Up) ||
                    (headWay == Way.Down && turnWay == Way.Right))
                {
                    urlString += "upLeft.png";
                }
                else if ((headWay == Way.Right && turnWay == Way.Up) ||
                    (headWay == Way.Down && turnWay == Way.Left))
                {
                    urlString += "upRight.png";
                }

                bodies[1].way = bodies[0].way;
                bodies[1].SetImage(urlString);
            }
            else
            {
                string urlString = ReturnImgUriBody(bodies[0].way);
                bodies[1].SetImage(urlString);
            }
        }

        /// <summary>
        /// Производит движение змеи
        /// </summary>
        public void Move()
        {
            MoveBody();
            MoveHead();
        }

        /// <summary>
        /// Производит рост змеи
        /// </summary>
        public void Growth()
        {
            SnakeBody snakeTail = bodies[bodies.Count - 1];
            SnakeBody newBody = new SnakeBody(snakeTail.x, snakeTail.y, snakeTail.imgUri, snakeTail.way);
            bodies.Add(newBody);
        }

        /// <summary>
        /// Убивает змею
        /// </summary>
        public void Dead()
        {
            SnakeBody snakeHead = bodies[0];
            string uriString = ReturnImgUriHead(snakeHead.way, false);

            snakeHead.SetImage(uriString);
            User.userPlayers.snake.GameOver();
        }
    }
}
