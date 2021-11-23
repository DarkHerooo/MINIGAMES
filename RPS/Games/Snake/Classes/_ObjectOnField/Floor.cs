using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField
{
    public class Floor : ObjectOnField
    {
        public Floor(int x, int y, string imgName) : base(x, y, imgName)
        {
            SetImage(imgName);
        }

        public override void SetImage(string imgName)
        {
            image.Source = new BitmapImage
                (new Uri("/Games/Snake/Images/Floor/" + imgName, UriKind.Relative));
        }
    }
}
