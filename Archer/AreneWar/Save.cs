using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreneWar
{
    public class Save
    {
        // Memento
        public class HeroMemento
        {
            public int arrows { get; private set; }
            public int HP { get; private set; }
            public int posX { get; private set; }
            public int posY { get; private set; }

            public HeroMemento(int arrows, int HP, int posX, int posY)
            {
                this.arrows = arrows;
                this.HP = HP;
                this.posX = posX;
                this.posY = posY;

            }
        }
        // Caretaker
        public class GameHistory
        {
            public Stack<HeroMemento> History { get; private set; }
            public GameHistory()
            {
                History = new Stack<HeroMemento>();
            }
        }
    }
}
