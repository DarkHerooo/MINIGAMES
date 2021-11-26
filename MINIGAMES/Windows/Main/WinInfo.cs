using MINIGAMES.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Windows.Main
{
    public class WinInfo
    {
        public string sourceIco { get; set; }
        public string sourceLogo { get; set; }
        public string header { get; set; }

        public TypeGame typeGame { get; set; }

        public WinInfo(string sourceIco, string sourceLogo, 
            string header, TypeGame typeGame)
        {
            this.sourceIco = sourceIco;
            this.sourceLogo = sourceLogo;
            this.header = header;
            this.typeGame = typeGame;
        }
    }
}
