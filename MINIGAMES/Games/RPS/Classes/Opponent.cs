using MINIGAMES.Classes._Gamer;
using MINIGAMES.Games.RPS.Classes._Item._ItemCombination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes
{
    public class Opponent
    {
        public Gamer gamer;
        public ItemCombination itemCombination;
        public int countWinRounds = 0;

        public Opponent(Gamer gamer)
        {
            this.gamer = gamer;
        }

        public void WinRound()
        {
            countWinRounds++;
        }
    }
}
