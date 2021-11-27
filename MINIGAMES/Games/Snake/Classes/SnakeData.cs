using MINIGAMES.Classes._Gamer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Pages.Snake.Classes
{
    public class SnakeData
    {
        public int countGames { get; set; }
        public int countCampageWins { get; set; }
        public int levelScore1 { get; set; }
        public int levelScore2 { get; set; }
        public int levelScore3 { get; set; }

        public void GameOver()
        {
            countGames++;
        }

        public void WinCampage()
        {
            countCampageWins++;
        }
    }
}
