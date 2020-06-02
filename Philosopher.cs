using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungryPhilosophers
{
    class Philosopher
    {
        static private int Indexer = 0;             //индексатор
        public int index { get; private set; }        //индекс философа
        public Fork leftFork;                       //вилки доступные философу
        public Fork rightFork;
        public bool HaveLeftFork { get; private set; }       //в руке ли вилка
        public bool HaveRightFork { get; private set; }
        public bool CanEat                             //проверка обеих вилок в руках
        {
            get => HaveLeftFork && HaveRightFork;
        }
        public bool IsEating { get; private set; }
        public Philosopher(Fork leftFork, Fork rightFork)
        {
            this.leftFork = leftFork;
            this.rightFork = rightFork;
            HaveLeftFork = false;
            HaveRightFork = false;
            index = Indexer++;
        }
        public bool TryTakeLeftFork()
        {
            if (IsEating)
                return false;
            if (leftFork.IsFree)
            {
                leftFork.IsFree = false;
                HaveLeftFork = true;
                return true;
            }
            return false;
        }
        public bool TryTakeRightFork()
        {
            if (IsEating)
                return false;
            if (rightFork.IsFree)
            {
                rightFork.IsFree = false;
                HaveRightFork = true;
                return true;
            }
            return false;
        }
        public bool TryMakeEat()                   //пытается начать жрать
        {
            if (!CanEat)
            {
                return false;                 //вилок не хватило((
            }
            IsEating = true;                  //начинает жрать
            return true;
        }
        public void MakeThink()                   //прекращение поедания макарон
        {
            IsEating = false;
            leftFork.IsFree = true;
            rightFork.IsFree = true;
            HaveLeftFork = false;
            HaveRightFork = false;
        }
    }
}
