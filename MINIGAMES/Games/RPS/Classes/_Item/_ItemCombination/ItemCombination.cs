using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.RPS.Classes._Item._ItemCombination
{
    public class ItemCombination
    {
        public Item item;
        public Item weakItem;

        public ItemCombination(Item item, Item weakItem)
        {
            this.item = item;
            this.weakItem = weakItem;
        }
    }
}
