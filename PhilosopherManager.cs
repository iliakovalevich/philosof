using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HungryPhilosophers
{
    class PhilosopherManager                                 //менеджер философов    
    {
        public List<Philosopher> queue;                      //очередь
        public List<Philosopher> philosophers;
        public List<Fork> forks;
        public PhilosopherManager(int count)
        {
            queue = new List<Philosopher>();
            philosophers = new List<Philosopher>();
            forks = new List<Fork>();
            for (int i = 0; i < count; i++)
            {
                forks.Add(new Fork());
            }
            for (int i = 0; i < count; i++)
            {
                philosophers.Add(new Philosopher(forks[(i == 0 ? forks.Count : i) - 1], forks[i]));
            }
        }

        public void Click(int index)                              //либо философ начинает жрать либо уходит в очередь
        {
            if (philosophers[index].IsEating)                     //переключение из поедания в думанье
            {
                philosophers[index].MakeThink();
                if (queue.Count == 0)
                    return;
                for (int i = 0; i < queue.Count && queue.Count != 0;)          //проверяет всю очередь желающих покушать философов
                {
                    if (forks[queue[i].index].isEven)
                    {
                        if (queue[i].HaveLeftFork || queue[i].TryTakeLeftFork())
                        {
                            queue[i].TryTakeRightFork();
                        }
                    }
                    else
                    {
                        if (queue[i].HaveRightFork || queue[i].TryTakeRightFork())
                        {
                            queue[i].TryTakeLeftFork();
                        }
                    }
                    if (queue[i].CanEat)
                    {
                        queue[i].TryMakeEat();
                        queue.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            else
            {
                if (forks[index].isEven)                          //идёт проверка на чётность вилки
                {
                    if (philosophers[index].TryTakeLeftFork())
                    {
                        philosophers[index].TryTakeRightFork();
                    }
                }
                else
                {
                    if (philosophers[index].TryTakeRightFork())
                    {
                        philosophers[index].TryTakeLeftFork();
                    }
                }
                if (philosophers[index].CanEat)
                {
                    philosophers[index].TryMakeEat();
                }
                else
                {
                    if (!queue.Contains(philosophers[index]) && !philosophers[index].IsEating)
                    {
                        queue.Add(philosophers[index]);
                    }
                }
            }
        }
    }
}