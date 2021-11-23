using MINIGAMES.Classes;
using MINIGAMES.Classes._Gamer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes
{
    public class RPSData
    {
        public int countGames { get; set; }
        public int countWinMatches { get; set; }

        public void GameOver()
        {
            countGames++;
        }
        public void WinMatch()
        {
            countWinMatches++;
        }
    }
}
