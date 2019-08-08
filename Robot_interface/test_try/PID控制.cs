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
        const string frame_format_SlideCtrole = "06";//PID数据命令
      
        private void skinButton_SlideCtrole_Click(object sender, EventArgs e)
        {
            Thread tWorkingThread = new Thread(Thread_skinButton_SlideCtrole_Click);
            tWorkingThread.Priority = ThreadPriority.Highest;
            tWorkingThread.Start();
        }
        private void Thread_skinButton_SlideCtrole_Click()
        {
            string data, value_p, value_i,value_d, value_VR, value_VL, value_angle;  //需要发送的参数
            value_p = Convert.ToString(Convert.ToInt32(textBox_P.Text), 16);
            value_p = convert_according_bytes(value_p, 1);
            value_i = Convert.ToString(Convert.ToInt32(textBox_I.Text), 16);
            value_i = convert_according_bytes(value_i, 1);
            value_d = Convert.ToString(Convert.ToInt32(textBox_D.Text), 16);
            value_d = convert_according_bytes(value_d, 1);
            value_VL = Convert.ToString(Convert.ToInt32(textBox_VL.Text), 16);
            value_VL = convert_according_bytes(value_VL, 1);
            value_VR = Convert.ToString(Convert.ToInt32(textBox_VR.Text), 16);
            value_VR = convert_according_bytes(value_VR, 1);
            value_angle = Convert.ToString(Convert.ToInt32(textBox_angle.Text), 16);
            value_angle = convert_according_bytes(value_angle, 1);
            data = value_p+ value_i+ value_d+ value_VL+ value_VR+value_angle;
            int i = 0;
            textBox4.Text = "";
            success_flag = 0;
            SlideCtrole_Fun(data);
            Thread.Sleep(100);
            while ((i <= 3) && (success_flag != 1))
            {
                i++;
                SlideCtrole_Fun(data);
                Thread.Sleep(100);
            }
            if (success_flag != 1)
            {
                textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
            }
        }
        //方法：发送指令
        private void SlideCtrole_Fun(string str)
        {
            int buff = str.Length;
            Class_Frame a = new Class_Frame();
            string data = buff == 1 ? "0" + str : str;
            a.STR = data;
            a.FORMAT = frame_format_SlideCtrole;
            a.Packer_Frame();
            pro_flag = true;
            WriteBytesToSerialport(a.FRAME);         
        }
        //将输入的参数转化为对应字节长度的16进制字符
        //1个字节长度对应2位16进制字符
        //2个字节长度对应4位16进制字符
        private string convert_according_bytes(string str, int len)
        {
            if (len == 1)
            {
                str = str.Length == 1 ? "0" + str : str;
            }
            if (len == 2)
            {
                if (str.Length == 1)
                    str = "000" + str;
                else if (str.Length == 2)
                    str = "00" + str;
                else if (str.Length == 3)
                    str = "0" + str;
            }
            return str;
        }

        
    }
}
