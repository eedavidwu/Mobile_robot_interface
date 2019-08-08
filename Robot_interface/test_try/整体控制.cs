using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace test_try
{
    public partial class Mobile_Robot : Form
    {
        const byte Forth = 0x36;
        const byte Back = 0x34;
        const byte Up = 0x38;
        const byte Down = 0x32;
        const byte Stop = 0x35;
        const byte Left_Hand = 0x37;
        const byte Right_Hand = 0x39;
        const byte Pitch_up = 0x31;
        const byte Pitch_down = 0x33;
        const string frame_format2 = "02";//整体控制格式
        //整体控制命令
        //上升
        private void button_up_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_up_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_up_Click()
        {
            what_format[0] = Up;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;

            Propeller_ALL_Fun(Up);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Up);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Forward";
        }
   
        private void button_down_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_down_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_down_Click()
        {
            what_format[0] = Down;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            Propeller_ALL_Fun(Down);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Down);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Back";
        }
   
 
        private void button_back_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_back_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_back_Click()
        {
            what_format[0] = Back;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            Propeller_ALL_Fun(Back);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Back);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Right";
        }
        //停止
        //前进
        private void button_forth_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_forth_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_forth_Click()
        {
            what_format[0] = Forth;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            Propeller_ALL_Fun(Forth);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Forth);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Left";
        }
        private void button_stop_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_stop_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_stop_Click()
        {
            what_format[0] = Stop;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            Propeller_ALL_Fun(Stop);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Stop);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Stop";
        }
 
       
        //左转
        private void skinButton_lefthand_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_button_lefthand_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_button_lefthand_Click()
        {
            what_format[0] = Left_Hand;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            Propeller_ALL_Fun(Left_Hand);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                Propeller_ALL_Fun(Left_Hand);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
            textBox1.Text = "Left";
        }
     
        //发送指令函数
        private void Propeller_ALL_Fun(int value)
        {
            string str = Convert.ToString(value, 16);
            int buff = str.Length;
            Class_Frame a = new Class_Frame();
            String data = buff == 1 ? "0" + str : str;
            a.STR = data;
            a.FORMAT = frame_format2;
            a.Packer_Frame();
            pro_flag = true;
            WriteBytesToSerialport(a.FRAME);
        }
    }
}
