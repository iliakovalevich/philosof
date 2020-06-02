using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungryPhilosophers
{
    class Fork                                  //вилка
    {
        static private int Indexer = 0;         //индексатор
        public bool isEven;
        public bool IsFree = true;
        public Fork()
        {
            isEven = (Indexer++ % 2 == 0) ? true : false;   //ну, тут понятно
        }
    }
}
