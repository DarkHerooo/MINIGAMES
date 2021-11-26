using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._ObjectOnField._SnakeStructure
{
    public abstract class SnakeStructure : ObjectOnField
    {
        public Way way;

        public SnakeStructure(int x, int y, string imgUri, Way way) : base(x, y, imgUri)
        {
            this.way = way;
        }

        public virtual void SetImage() { }
    }
}
