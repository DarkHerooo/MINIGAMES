using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure
{
    public class SnakeTail : SnakeStructure
    {
        public SnakeTail(int x, int y, string imgUri, Way way) : base(x, y, imgUri, way)
        {
            SetImage();
        }

        public override void SetImage()
        {
            imgName = "Tail/";

            switch (way)
            {
                case Way.Left: imgName += "left.png"; break;
                case Way.Up: imgName += "up.png"; break;
                case Way.Right: imgName += "right.png"; break;
                case Way.Down: imgName += "down.png"; break;
            }

            image.Source = new BitmapImage
                (new Uri("/Games/Snake/Images/SnakeStructure/" + imgName, UriKind.Relative));
        }
    }
}
