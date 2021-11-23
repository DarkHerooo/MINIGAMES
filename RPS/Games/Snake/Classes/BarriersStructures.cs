using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes
{
    public class BarriersStructures
    {
        /// <summary>
        /// Создаёт забор вокруг всей локации
        /// </summary>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public List<Barrier> CreateFenceBarriers(int width, int heigth, string imgName)
        {
            List<Barrier> barriers = new List<Barrier>();
            for (int i = 0; i < width; i++)
            {
                Barrier barrier = new Barrier(i, 0, imgName);
                barriers.Add(barrier);
                barrier = new Barrier(i, heigth - 1, imgName);
                barriers.Add(barrier);
            }

            for (int i = 1; i < heigth - 1; i++)
            {
                Barrier barrier = new Barrier(0, i, imgName);
                barriers.Add(barrier);
                barrier = new Barrier(width - 1, i, imgName);
                barriers.Add(barrier);
            }

            return barriers;
        }

        /// <summary>
        /// Создаёт дерево-препятствие на локации
        /// </summary>
        /// <param name="senterX"></param>
        /// <param name="senterY"></param>
        /// <param name="logImgName"></param>
        /// <param name="leafImgName"></param>
        /// <returns></returns>
        public List<Barrier> CreateTreeBarrier(int senterX, int senterY, string logImgName,
            string leafImgName)
        {
            List<Barrier> barriers = new List<Barrier>();

            Barrier log = new Barrier(senterX, senterY, logImgName);
            barriers.Add(log);

            Barrier leaf1 = new Barrier(senterX - 1, senterY, leafImgName);
            barriers.Add(leaf1);

            Barrier leaf2 = new Barrier(senterX + 1, senterY, leafImgName);
            barriers.Add(leaf2);

            Barrier leaf3 = new Barrier(senterX, senterY - 1, leafImgName);
            barriers.Add(leaf3);

            Barrier leaf4 = new Barrier(senterX, senterY + 1, leafImgName);
            barriers.Add(leaf4);

            return barriers;
        }

        public List<Barrier> CreateHorisontalBarrier(int startX, int y, int length,
            string imgName)
        {
            List<Barrier> barriers = new List<Barrier>();

            for (int i = startX; i < startX + length; i++)
            {
                Barrier barrier = new Barrier(i, y, imgName);
                barriers.Add(barrier);
            }

            return barriers;
        }

        public List<Barrier> CreateVerticalBarrier(int startY, int x, int length,
            string imgName)
        {
            List<Barrier> barriers = new List<Barrier>();

            for (int i = startY; i < startY + length; i++)
            {
                Barrier barrier = new Barrier(x, i, imgName);
                barriers.Add(barrier);
            }

            return barriers;
        }
    }
}
