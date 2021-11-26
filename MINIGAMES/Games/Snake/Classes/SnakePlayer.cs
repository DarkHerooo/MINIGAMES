using MINIGAMES.Classes;
using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure;
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
        public SnakeHead head;
        public List<SnakeBody> bodies = new List<SnakeBody>();
        public SnakeTail tail;
        public bool turn = false;

        public SnakePlayer(int headX, int headY, int startLength, Way startWay)
        {
            head = new SnakeHead(headX, headY, null, startWay);

            CreateStartSnake(startLength);
        }

        /// <summary>
        /// Создаёт стартовую змею с указанной длиной
        /// </summary>
        /// <param name="startLength"></param>
        private void CreateStartSnake(int startLength)
        {
            switch (head.way)
            {
                case Way.Left:
                    for (int i = 1; i < startLength; i++)
                    {
                        if (i == startLength - 1)
                        {
                            tail = new SnakeTail(head.x + i,
                                head.y, null, head.way);
                            tail.SetImage();
                            break;
                        }

                        SnakeBody body = new SnakeBody(head.x + i, 
                            head.y, null, head.way);
                        body.SetImage();
                        bodies.Add(body);
                    }
                    break;
                case Way.Up:
                    for (int i = 1; i < startLength; i++)
                    {
                        if (i == startLength - 1)
                        {
                            tail = new SnakeTail(head.x,
                                head.y + i, null, head.way);
                            tail.SetImage();
                            break;
                        }

                        SnakeBody body = new SnakeBody(head.x,
                            head.y + i, null, head.way);
                        body.SetImage();
                        bodies.Add(body);
                    }
                    break;
                case Way.Right:
                    for (int i = 1; i < startLength; i++)
                    {
                        if (i == startLength - 1)
                        {
                            tail = new SnakeTail(head.x - i,
                                head.y, null, head.way);
                            tail.SetImage();
                            break;
                        }

                        SnakeBody body = new SnakeBody(head.x - i,
                            head.y, null, head.way);
                        body.SetImage();
                        bodies.Add(body);
                    }
                    break;
                case Way.Down:
                    for (int i = 1; i < startLength; i++)
                    {
                        if (i == startLength - 1)
                        {
                            tail = new SnakeTail(head.x,
                                head.y - i, null, head.way);
                            tail.SetImage();
                            break;
                        }

                        SnakeBody body = new SnakeBody(head.x, 
                            head.y - i, null, head.way);
                        body.SetImage();
                        bodies.Add(body);
                    }
                    break;
            }
        }

        /// <summary>
        /// Двигает голову змеи
        /// </summary>
        private void MoveHead()
        {
            switch (head.way)
            {
                case Way.Left: head.x--; break;
                case Way.Up: head.y--; break;
                case Way.Right: head.x++; break;
                case Way.Down: head.y++; break;
            }
            head.SetLocation();
            head.SetImage();

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

                selectBody.way = nextBody.way;
                selectBody.SetImage(nextBody.imgName);
            }

            SnakeBody firstBody = bodies[0];
            firstBody.x = head.x;
            firstBody.y = head.y;
            firstBody.SetLocation();
        }

        /// <summary>
        /// Двигает хвост змеи
        /// </summary>
        private void MoveTail()
        {
            SnakeBody lastBody = bodies[bodies.Count - 1];

            tail.x = lastBody.x;
            tail.y = lastBody.y;
            tail.SetLocation();

            tail.way = lastBody.way;
            tail.SetImage();
        }

        /// <summary>
        /// Производит попытку установить картинку-поворот, если змея поворачивает
        /// </summary>
        private void TryTurn()
        {
            if (turn)
            {
                Way headWay = head.way;
                Way turnWay = bodies[0].way;

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

                bodies[0].way = head.way;
                bodies[0].SetImage(urlString);
            }
            else
            {
                bodies[0].way = head.way;
                bodies[0].SetImage();
            }
        }

        /// <summary>
        /// Производит движение змеи
        /// </summary>
        public void Move()
        {
            MoveTail();
            MoveBody();
            MoveHead();
        }

        /// <summary>
        /// Производит рост змеи
        /// </summary>
        public void Growth()
        {
            SnakeBody newBody = new SnakeBody(tail.x, tail.y, null, tail.way);
            newBody.SetImage();
            bodies.Add(newBody);
        }

        /// <summary>
        /// Убивает змею
        /// </summary>
        public void Dead()
        {
            head.life = false;
            head.way = bodies[0].way;
            head.SetImage();
            User.userPlayers.snake.GameOver();
        }
    }
}
