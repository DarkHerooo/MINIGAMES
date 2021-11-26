using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MINIGAMES.Classes
{
    public class Achievement
    {
        public Border border = new Border();
        private string name;
        private string imgSource;
        private int result;

        public Achievement(string name, string imgSource, int result)
        {
            this.name = name;
            this.imgSource = imgSource;
            this.result = result;

            CreateBorder();
        }

        private void CreateBorder()
        {
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(2);
            border.Margin = new Thickness(5, 0, 5, 0);
            border.ToolTip = name;
            border.Child = CreateGrid();
        }

        private Image CreateImage()
        {
            Image image = new Image();
            image.Width = 64;
            image.Height = 64;
            image.Source = new BitmapImage(new Uri(imgSource, UriKind.Relative));
            return image;
        }
        private TextBlock CreateTblResult()
        {
            TextBlock tblResult = new TextBlock();
            tblResult.HorizontalAlignment = HorizontalAlignment.Right;
            tblResult.VerticalAlignment = VerticalAlignment.Bottom;
            tblResult.TextAlignment = TextAlignment.Center;
            tblResult.Background = Brushes.Red;
            tblResult.MinWidth = 16;
            tblResult.Height = 16;
            tblResult.FontSize = 15;
            tblResult.FontWeight = FontWeights.Bold;
            tblResult.Text = result.ToString();
            return tblResult;
        }

        private Grid CreateGrid()
        {
            Image image = CreateImage();
            Panel.SetZIndex(image, 0);

            TextBlock tblResult = CreateTblResult();
            Panel.SetZIndex(tblResult, 1);

            Grid grid = new Grid();
            grid.Children.Add(image);
            grid.Children.Add(tblResult);

            return grid;
        }
    }
}
