
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

                                //— Голодный, видать, мыслитель, раз две вилки взял. Ты, седой! На кой тебе две вилки? А в штанах два хера держишь? Ты, сука, глухой? Скажешь, кто ты такой есть, или тебе язык ножом развязать?
                                //— Философ. Спрашиваешь, зачем мне две вилки? Одна — для макарон, другая — тоже. Член у меня один.

namespace HungryPhilosophers
{
    public partial class Form1 : Form
    {
        static int index = 0;
        List<Button> buttons;
        List<CheckBox> CheckBoxes;
        PhilosopherManager manager;
        public Form1()
        {
            InitializeComponent();
            manager = new PhilosopherManager(6);
            buttons = new List<Button>();
            buttons.AddRange(Controls.OfType<Button>().Reverse());
            CheckBoxes = new List<CheckBox>();
            CheckBoxes.AddRange(Controls.OfType<CheckBox>().Reverse());
            for (int i = 0; i < 6; i++)
            {
                buttons[i].BackColor = Color.Green;
                buttons[i].Tag = i;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            buttonClick((int)((Button)sender).Tag);
        }
        private void buttonClick(int i)                                //все нажатия кнопок передают сюда свой номер и делают философа голодным
        {
            manager.Click(i);
        }
        private void timer1_Tick(object sender, EventArgs e)         //отрисовка состояния стола (каждой вилки и каждого философа) на форме
        {
            for (int i = 0; i < manager.forks.Count; i++)
            {
                CheckBoxes[i].Checked = manager.forks[i].IsFree;
            }
            for (int i = 0; i < manager.philosophers.Count; i++)
            {
                buttons[i].BackColor = manager.philosophers[i].IsEating ? Color.Red : manager.queue.Contains(manager.philosophers[i]) ? Color.Orange : Color.Green;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            buttonClick(index++);
            if (index == 6)
                index = 0;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }
    }
}
