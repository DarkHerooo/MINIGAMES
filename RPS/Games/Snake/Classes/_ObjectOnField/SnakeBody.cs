using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField
{
    public class SnakeBody : ObjectOnField
    {
        public Way way;
        public SnakeBody(int x, int y, string imgUri, Way way) : base(x, y, imgUri)
        {
            this.way = way;
            SetImage(imgUri);
        }

        public override void SetImage(string imgUri)
        {
            this.imgUri = imgUri;
            image.Source = new BitmapImage
                (new Uri("/Games/Snake/Images/SnakeBody/" + imgUri, UriKind.Relative));
        }
    }
}
