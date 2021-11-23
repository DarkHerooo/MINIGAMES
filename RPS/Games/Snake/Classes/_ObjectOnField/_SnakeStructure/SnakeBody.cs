using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure
{
    public class SnakeBody : SnakeStructure
    {
        public SnakeBody(int x, int y, string imgUri, Way way) : base(x, y, imgUri, way)
        {
            SetImage();
        }

        public override void SetImage()
        {
            string imgUri = "Body/";

            if (way == Way.Left || way == Way.Right)
            {
                imgUri += "horizontal.png";
            }
            else if (way == Way.Up || way == Way.Down)
            {
                imgUri += "vertical.png";
            }

            this.imgName = imgUri;
            image.Source = new BitmapImage
                (new Uri("/Games/Snake/Images/SnakeStructure/" + imgUri, UriKind.Relative));
        }

        public override void SetImage(string imgUri)
        {
            this.imgName = imgUri;
            image.Source = new BitmapImage
                (new Uri("/Games/Snake/Images/SnakeStructure/" + imgUri, UriKind.Relative));
        }
    }
}