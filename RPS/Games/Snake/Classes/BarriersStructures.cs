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
        /// Создаёт горизонтальную линию из препятствий
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="y"></param>
        /// <param name="length"></param>
        /// <param name="imgName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создаёт вертикальную линию из препятствий
        /// </summary>
        /// <param name="startY"></param>
        /// <param name="x"></param>
        /// <param name="length"></param>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public List<Barrier> CreateVerticalBarrier(int x, int startY, int length,
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