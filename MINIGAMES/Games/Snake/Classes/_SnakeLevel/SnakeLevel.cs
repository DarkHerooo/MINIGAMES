using MINIGAMES.Games.Snake.Classes._ObjectOnField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MINIGAMES.Games.Snake.Classes._SnakeLevel
{
    public abstract class SnakeLevel
    {
        private int _widthField = 0;
        private int _heigthField = 0;
        protected List<Floor> _floor = new List<Floor>();
        protected List<Barrier> _barriers = new List<Barrier>();
        protected SnakePlayer _snake;
        private int _maxScore = 0;

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
        public List<Floor> floor
        {
            get
            {
                return _floor;
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

        protected void SetSize(int widthField, int heigthField)
        {
            _widthField = widthField;
            _heigthField = heigthField;
        }

        protected void SetMaxScore(int maxScore)
        {
            _maxScore = maxScore;
        }

        protected void CreateFloor(string imgName)
        {
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heigthField; j++)
                {
                    Floor floor = new Floor(i, j, imgName);
                    _floor.Add(floor);
                }
            }
        }

        protected abstract void CreateBarriers();

        protected abstract void CreateSnake();

        public abstract void SaveScore(int score);
    }
}
