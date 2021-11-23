using Newtonsoft.Json;
using MINIGAMES.Classes._Gamer;
using System.IO;
using MINIGAMES.Pages.Snake.Classes;
using MINIGAMES.Games.RPS.Classes;

namespace MINIGAMES.Classes
{
    public static class User
    {
        public static UserPlayers userPlayers { get; set; }

        public static void OpenUser()
        {
            if (File.Exists("../../Data/player.json"))
            {
                userPlayers = JsonConvert.DeserializeObject<UserPlayers>(File.ReadAllText("../../Data/player.json"));
            }
            else
            {
                userPlayers = new UserPlayers();
                userPlayers.player = new Player("Игрок", @"\Images\Players\user.png");
                userPlayers.rps = new RPSData();
                userPlayers.snake = new SnakeData();
            }
        }

        public static void SaveUser()
        {
            File.WriteAllText("../../Data/player.json", JsonConvert.SerializeObject(userPlayers));
        }
    }
}
