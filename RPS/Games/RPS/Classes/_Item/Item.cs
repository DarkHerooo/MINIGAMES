using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes._Item
{
    public class Item
    {
        public string name { get; set; }
        public string imgSource { get; set; }

        public Item(string name, string imgSource)
        {
            this.name = name;
            this.imgSource = imgSource;
        }
    }
}
