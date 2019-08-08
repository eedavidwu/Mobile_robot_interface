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
        const string frame_format3 = "04";//
     
        private void skinButton_send_Click(object sender, EventArgs e)
        {
            if (skinRadioButton_all.Checked == true)
            {
                Thread t = new Thread(Thread_skinButton_send_Click);
                t.Priority = ThreadPriority.Highest;
                t.Start();
            }
        }
        private void Thread_skinButton_send_Click()
        {
            try
            {
                string data, a, b, c, d;
                a = Convert.ToString((Convert.ToInt32(skinTextBox_leftup.Text) + 50), 16);
                a = (a.Length == 1 ? "0" + a : a);
                b = Convert.ToString((Convert.ToInt32(skinTextBox_rightup.Text) + 50), 16);
                b = (b.Length == 1 ? "0" + b : b);
                c = Convert.ToString((Convert.ToInt32(skinTextBox_leftdown.Text) + 50), 16);
                c = (c.Length == 1 ? "0" + c : c);
                d = Convert.ToString((Convert.ToInt32(skinTextBox_rightdown.Text) + 50), 16);
                d = (d.Length == 1 ? "0" + d : d);
                data = a + b + c + d;
                int i = 0;
                textBox4.Text = "";
                success_flag = 0;
                Propeller_All_Fun(data);
                Thread.Sleep(100);
                while ((i <= 3) && (success_flag != 1))
                {
                    i++;
                    Propeller_All_Fun(data);
                    Thread.Sleep(100);
                }
                if (success_flag != 1)
                {
                    textBox3.AppendText("机器人接收命令失败，请重新点击按钮" + "\n");
                }
            }
            catch
            {

            }
        }
        public void Propeller_All_Fun(string str)
        {
            int buff = str.Length;
            Class_Frame a = new Class_Frame();
            string data = (buff == 1 ? "0" + str : str);
            a.STR = data;
            a.FORMAT = frame_format3;
            a.Packer_Frame();
            pro_flag = true;
            WriteBytesToSerialport(a.FRAME);       
        }
    }
}
