using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes._Item
{
    public class Items
    {
        private static string str = @"..\Images\Items\";

        public static readonly Item rock =
            new Item("Камень", str + "rock.png");
        public static readonly Item scissors =
            new Item("Ножницы", str + "scissors.png");
        public static readonly Item paper =
            new Item("Бумага", str + "paper.png");

        public static readonly Item[] all =
        {
            rock, scissors, paper
        };
    }
}
