using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Classes._Gamer
{
    public abstract class Gamer
    {
        public string name { get; set; }
        public string imgSource { get; set; }

        public Gamer(string name, string imgSource)
        {
            this.name = name;
            this.imgSource = imgSource;
        }
    }
}
