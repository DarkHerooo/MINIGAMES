using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MINIGAMES.Windows.Main
{
    public class WinMain
    {
        public static Window window;
        public static WinInfo winInfo;

        public static void SetWinInfo(WinInfo newWinInfo)
        {
            winInfo = newWinInfo;
            window.DataContext = winInfo;
        }
    }
}
