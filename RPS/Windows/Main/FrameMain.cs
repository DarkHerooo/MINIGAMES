using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MINIGAMES.Windows.Main
{
    public class FrameMain
    {
        public static Frame frame;

        public static void ClearHistory()
        {
            while (frame.CanGoBack)
            {
                frame.RemoveBackEntry();
            }
        }
    }
}
