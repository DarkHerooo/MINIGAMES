using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._SnakeLevel
{
    public abstract class SnakeLevel : ICloneable
    {
        private string _bgImgSource;
        private int _widthField = 0;
        private int _heigthField = 0;
        protected List<Barrier> _barriers = new List<Barrier>();
        protected SnakePlayer _snake;
        private int _maxScore = 0;

        public string bgImgSource
        {
            get
            {
                return _bgImgSource;
            }
        }
        public int widthField
        {
            get
            {
                return _widthField;
            }
        }
        public int heigthField
        {
            get
            {
                return _heigthField;
            }
        }
        public List<Barrier> barriers
        {
            get
            {
                return _barriers;
            }
        }
        public SnakePlayer snake
        {
            get
            {
                return _snake;
            }
        }
        public int maxScore
        {
            get
            {
                return _maxScore;
            }
        }

        protected BarriersStructures barStruct = new BarriersStructures();

        protected void SetBgImgSource(string imgName)
        {
            _bgImgSource = "/Games/Snake/Images/Backgrounds/" + imgName;
        }

        protected void SetWidthAndHeigth(int widthField, int heigthField)
        {
            _widthField = widthField;
            _heigthField = heigthField;
        }

        protected void SetMaxScore(int maxScore)
        {
            _maxScore = maxScore;
        }
        protected abstract void CreateBarriers();

        protected abstract void CreateSnake();

        public abstract void SaveScore(int score);

        public object Clone()
        {
            SnakeLevel snakeLevel = (SnakeLevel)MemberwiseClone();
            snakeLevel._barriers.Clear();
            snakeLevel.CreateBarriers();
            snakeLevel.CreateSnake();
            return snakeLevel;
        }
    }
}
