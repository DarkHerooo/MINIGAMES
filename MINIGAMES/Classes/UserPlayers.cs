using MINIGAMES.Classes._Gamer;
using MINIGAMES.Games.RPS.Classes;
using MINIGAMES.Pages.Snake.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Classes
{
    public class UserPlayers
    {
        public Player player { get; set; }
        public RPSData rps { get; set; }
        public SnakeData snake { get; set; }
    }
}
