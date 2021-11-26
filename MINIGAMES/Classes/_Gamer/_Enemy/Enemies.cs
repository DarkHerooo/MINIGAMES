using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Classes._Gamer._Enemy
{
    public static class Enemies
    {
        private static string str = @"..\..\..\Images\Enemies\";

        public static readonly Enemy[] all =
        {
            new Enemy("Ассемблер", str + "assembler.png"),
            new Enemy("Джонни", str + "cat.png"),
            new Enemy("Java", str + "java.png"),
            new Enemy("Майнкрафтер", str + "minecrafter.png"),
            new Enemy("Президент", str + "president.png")
        };
    }
}
