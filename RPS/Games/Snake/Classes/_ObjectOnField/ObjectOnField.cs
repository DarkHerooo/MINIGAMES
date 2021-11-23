using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField
{
    public abstract class ObjectOnField
    {
        public Image image = new Image();
        public string imgUri;
        public int x;
        public int y;

        public ObjectOnField(int x, int y, string imgUri)
        {
            this.x = x;
            this.y = y;
            this.imgUri = imgUri;
            image.Stretch = Stretch.Fill;
            SetLocation();
        }

        public void SetLocation()
        {
            Grid.SetColumn(image, x);
            Grid.SetRow(image, y);
        }

        public abstract void SetImage(string imgUri);
    }
}
