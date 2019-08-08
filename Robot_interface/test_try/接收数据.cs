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
        const short rx_head = 0x80;
        const short rx_over = 0x40; //标志位：接收到完整帧数据
        const int RX_BUFFER_SIZE = 100;
        public static byte[] rx_buffer = new byte[100];
        public static int rx_wr_index = 0;
        public static short RC_Flag = 0x00;
        public static short com_receiving = 0x00;//标志：串口是否正在接收数据
        public static short Flag_Received = 0x00;//标志：串口一帧数据是否接收完毕
        public static short frame_start = 0x00;  //标志：帧开始标志
        public static short data_ack = 0x00;     //标志：接收到的帧为传感器的数据
        public static short command_ack = 0x00;  //标志：接收到的帧为控制命令返回帧
        //接收数据
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (closing) return; //如果正在关闭串口，忽略操作，直接返回              
            try
            {
                //button_port_state.BackgroundImage = Properties.Resources.red_bulb;
                com_receiving = 0x10;
                int n = serialPort1.BytesToRead; //接收缓存区中的字节数             
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                serialPort1.Read(buf, 0, n);//读取缓存数据
                foreach (byte data in buf)
                {
                    //传感器数据帧帧头5AA5
                    if (data == 0x5a & frame_start == 0x00)
                    {
                        RC_Flag = rx_head;
                        rx_buffer[rx_wr_index++] = data;
                        frame_start = 0x10;
                    }
                    else if (data == 0xa5 & RC_Flag == rx_head & frame_start == 0x10)
                    {
                        RC_Flag = 0x00;
                        rx_buffer[rx_wr_index++] = data;
                        frame_start = 0x11;
                        data_ack = 0x11;                //标志：表示接收到的帧数据为传感器数据
                    }
                    //返回的控制命令帧帧头A55A
                    else if (data == 0xa5 & frame_start == 0x00)
                    {
                        RC_Flag = rx_head;
                        rx_buffer[rx_wr_index++] = data;
                        frame_start = 0x01;
                    }
                    else if (data == 0x5a & RC_Flag == rx_head & frame_start == 0x01)
                    {
                        RC_Flag = 0x00;
                        rx_buffer[rx_wr_index++] = data;
                        frame_start = 0x11;
                        command_ack = 0x11;        //标志：表示接收到的数据为dsp返回的控制命令
                    }
                    //有效数据部分
                    else if (frame_start == 0x11)
                    {
                        RC_Flag |= 0x00;
                        rx_buffer[rx_wr_index++] = data;
                        //传感器数据帧
                        if (rx_wr_index == (rx_buffer[2] + 6) & data_ack == 0x11)
                        {
                            frame_start = 0x00;
                            rx_wr_index = 0;
                            Flag_Received = 0x11;
                            RC_Flag = rx_over;
                            data_ack = 0x00;                           
                            Show_IMU();
                            Show_Motion();
                        }
                        //控制命令返回帧
                        else if (rx_wr_index == (rx_buffer[2] + 2) & command_ack == 0x11)
                        {
                            frame_start = 0x00;
                            rx_wr_index = 0;
                            Flag_Received = 0x11;
                            RC_Flag = rx_over;
                            command_ack = 0x00;
                            Data_show_command_ack();
                        }
                    }
                    else
                    {
                        rx_wr_index = 0;
                        frame_start = 0x00;
                    }

                    if (rx_wr_index == RX_BUFFER_SIZE)
                    {
                        rx_wr_index--;
                    }
                }
                com_receiving = 0x00;
                //button_port_state.BackgroundImage = Properties.Resources.green_bulb;
            }
            catch
            {
                MessageBox.Show("不能正确读取数据", "错误");
            }
        }
    }
}