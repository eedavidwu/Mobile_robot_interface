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
        public static short com_busy_status = 0x00;//串口忙标志位
        //发送数据包(控制命令）
        private void WriteBytesToSerialport(string data)
        {
            if (closing) return;////如果串口正在关闭，忽略操作
            byte[] buff = new byte[1];
            if (serialPort1.IsOpen)
            {
                while (com_busy_status != 0x00)
                {
                    Thread.Sleep(5);
                }
                try
                {
                    com_busy_status = 0x10;//将要发生数据，串口忙标志置位
                    for (int i = 0; i < (data.Length - data.Length % 2) / 2; i++)
                    {
                        string str = data.Substring(i * 2, 2);
                        textBox5.AppendText(str + " ");
                        buff[0] = Convert.ToByte(data.Substring(i * 2, 2), 16);//取余3运算防止用户输入的字符为奇数个
                        serialPort1.Write(buff, 0, 1);  //循环发送（如果输入字符为0A0BB，则只发生0A，0B)                        
                    }
                    if (data.Length % 2 != 0) //剩下一位单独处理
                    {
                        string str = data.Substring(data.Length - 1, 1);
                        str = "0" + str;
                        textBox5.AppendText(str + " ");
                        buff[0] = Convert.ToByte(data.Substring(data.Length - 1, 1), 16);
                        serialPort1.Write(buff, 0, 1);
                    }
                    textBox5.AppendText("\n");
                    Thread.Sleep(Convert.ToInt32(combo_3.Text));
                    com_busy_status = 0x00;//数据发送结束，串口忙标志清零
                }
                catch
                {
                    MessageBox.Show("输入错误！", "错误");
                }
            }
            else
            {
                MessageBox.Show("请确认是否打开串口", "错误");
            }
        }

        //发送数据包（请求命令）
        private void WriteBytesToSerialport_1(string data)
        {
            if (closing) return;////如果串口正在关闭，忽略操作
            byte[] buff = new byte[1];
            if (serialPort1.IsOpen)
            {
                while (com_busy_status != 0x00)
                {
                    Thread.Sleep(5);
                }
                try
                {
                    com_busy_status = 0x10;//将要发生数据，串口忙标志置位
                    for (int i = 0; i < (data.Length - data.Length % 2) / 2; i++)
                    {
                        string str = data.Substring(i * 2, 2);
                        textBox6.AppendText(str + " ");
                        buff[0] = Convert.ToByte(data.Substring(i * 2, 2), 16);//取余3运算防止用户输入的字符为奇数个
                        serialPort1.Write(buff, 0, 1);  //循环发送（如果输入字符为0A0BB，则只发生0A，0B)
                    }
                    if (data.Length % 2 != 0) //剩下一位单独处理
                    {
                        string str = data.Substring(data.Length - 1, 1);
                        str = "0" + str;
                        textBox6.AppendText(str + " ");
                        buff[0] = Convert.ToByte(data.Substring(data.Length - 1, 1), 16);
                        serialPort1.Write(buff, 0, 1);
                    }
                    textBox6.AppendText("\n");
                    com_busy_status = 0x00;//数据发送结束，串口忙标志清零
                }
                catch
                {
                    MessageBox.Show("输入错误！", "错误");
                }
            }
            else
            {
                MessageBox.Show("请确认是否打开串口", "错误");
            }
        }
    }
}
