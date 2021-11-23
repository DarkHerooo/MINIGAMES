using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes._Item._ItemCombination
{
    public static class ItemCombinations
    {
        public static readonly ItemCombination rock =
            new ItemCombination(Items.rock, Items.paper);
        public static readonly ItemCombination scissors =
            new ItemCombination(Items.scissors, Items.rock);
        public static readonly ItemCombination paper =
            new ItemCombination(Items.paper, Items.scissors);

        public static readonly ItemCombination[] all =
        {
            rock, scissors, paper
        };
    }
}
