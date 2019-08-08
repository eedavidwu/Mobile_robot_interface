using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace test_try
{
    public partial class Mobile_Robot : Form
    {                      
    
        public static int dataUnlock_thread_flag = 0;//标志位：thread_dataUnlock 线程是否结束
        private static int success_flag = 0;//标志位：判断控制命令是否发送成功
        private static bool pro_flag = false;//标志位：优先级标志位，当值为true时，表示正在发送控制命令或等待反馈，此时不能发送请求命令
        public static bool closing = false;//标志位：判断是否正在关闭串口
        public static bool Save_flag = false;//标志位：判断是否将参数写入txt文件
        public static bool port_idle = true;

        const string frame_format_ask_data= "B1";//请求运动数据命令
        const string frame_format_ask_dis = "B2";//请求距离数据命令
        const string frame_format_start= "C1";//请求开始传输数据命令
        const string frame_format_end= "C2";//请求结束传输数据命令
      
        private static short[] what_format=new short[15];               

        private Thread thread_dataUnlock;
        private Thread thread_judge_receive_new;
                     
        public Mobile_Robot()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Write_Textbox3("start");                  
        }
        //扫描可用的串口号
        private void serialscan_Button_Click(object sender, EventArgs e)
        {
            int i, k = 15;
            string[] str = new string[15];
            serialportName.Items.Clear();
            for (i = 1; i < 15; i++)
            {
                try
                {
                    serialPort1.PortName = "COM" + i.ToString();
                    serialPort1.Open();
                    str[i] = "COM" + i.ToString();
                    serialportName.Items.Add("COM" + i.ToString());
                    k = k < i ? k : i;
                    serialPort1.Close();
                }
                catch
                {
                    serialPort1.Close();
                }
            }
            serialportName.Text = str[k];
            serialportName.Enabled = true;
        }

        private void serialOpen_Click(object sender, EventArgs e)
        {

            thread_dataUnlock = new Thread(new ThreadStart(Data_Unlock));
            thread_judge_receive_new = new Thread(new ThreadStart(judge_receive_new));
            if (!serialPort1.IsOpen)          //打开串口
            {
                closing = false;
                try
                {
                    serialPort1.PortName = serialportName.Text;
                    serialPort1.BaudRate = Convert.ToInt32(serialBaudRate.Text);
                    serialPort1.Open();
                    serialOpen.BackgroundImage = Properties.Resources.on;
                    Write_Textbox3("open_com");
                    thread_judge_receive_new.Start();
                    dataUnlock_thread_flag = 1;
                    thread_dataUnlock.Start();

                }
                catch
                {
                    MessageBox.Show("打开失败", "错误");
                    serialPort1.Close();
                    serialOpen.BackgroundImage = Properties.Resources.off1;
                }
            }
            else  //关闭串口
            {
                Thread closingthread = new Thread(close_thread);
                closingthread.Priority = ThreadPriority.Highest;
                closingthread.Start();
            }
        }
        private void close_thread()
        {

            closing = true;
            try
            {
                while ((com_receiving != 0x00) || (com_busy_status != 0x00))
                {
                    Thread.Sleep(2);
                }
                if ((com_receiving == 0x00) && (com_busy_status == 0x00))
                {
                    dataUnlock_thread_flag = 0;
          
                    serialPort1.Close();
                    serialOpen.BackgroundImage = Properties.Resources.off1;
                    Write_Textbox3("close_com");
                }
                else
                {
                    MessageBox.Show("关闭失败", "错误");
                }
            }
            catch { }
        }       
        
        public void Data_Unlock()
        {
            while (dataUnlock_thread_flag == 1)
            {
                if (Flag_Received == 0x11)
                {
                }
            }
        }

        public void judge_receive_new()
        {
            while (true)
            {
                int i,j;
                i = 0;
                j = 0;
                if (com_receiving != 0x00)
                {
                    port_idle = false;
                }
                for (i = 0; i < 10000000; i++)
                {
                    if(com_receiving != 0x00)
                    {
                        j = 1;
                        port_idle = false;
                    }
                    else
                    {
                        port_idle = true;
                    }
                }
                if (port_idle == true & j == 0)
                {
                    button_port_state.BackgroundImage = Properties.Resources.green_bulb;
                }
                else if (port_idle == false | j == 1)
                {
                    button_port_state.BackgroundImage = Properties.Resources.red_bulb;
                }
            }            
        }
                              
        //textbox2转换函数，输入字符串，输出相应命令字符串
        public void Write_Textbox3(string str)
        {
            switch (str)
            {
                case "start":textBox3.AppendText("Select the serial port and open it！" + "\n"); break;
                case "open_com": textBox3.AppendText("串口" + serialportName.Text + "已打开！" + "\n"); break;
                case "close_com": textBox3.AppendText("串口" + serialportName.Text + "已关闭！" + "\n"); break;
                case "datatime": textBox3.AppendText((System.DateTime.Now).ToString() + " "); break;
                case "6": textBox3.AppendText("已收到 前进 命令" + "\n"); break;
                case "7": textBox3.AppendText("已收到 左转 命令" + "\n"); break;
                case "4": textBox3.AppendText("已收到 后退 命令" + "\n"); break;
                case "5": textBox3.AppendText("已收到 停止 命令" + "\n"); break;
                case "9": textBox3.AppendText("已收到 右转 命令" + "\n"); break;
                case "Noframe": textBox3.AppendText("非帧 "); break;
                case "frame": textBox3.AppendText("帧 "); break;
                case "Error_datareceived": textBox3.AppendText("数据传送有错误，无视之= =！" + "\n"); break;
                case "Error_command": textBox3.AppendText("报告长官，机器人未接收到来自我方的指令-_-|||！" + "\n"); break;
                case "start_command": textBox3.AppendText("开始接收来自机器人的数据" + "\n"); break;
                case "close_command": textBox3.AppendText("结束接收来自机器人的数据" + "\n"); break;
            }
        }                                    
        
        //清空
        private void skinButton_Clear_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        
        //刷新
        private void skinButton_reflesh_Click(object sender, EventArgs e)
        {
            label_yaw.Text = "yaw:";
            label_pitch .Text= "pitch:";
            label_roll.Text = "roll:";
            label_acceleration_x.Text = "X Axis：";
            label_acceleration_y.Text = "Y Axis：";
            label_acceleration_z.Text = "Z Axis：";
            label_gyro_x.Text = "X Axis：";
            label_gyro_y.Text = "Y Axis：";
            label_gyro_z.Text = "Z Axis：";
            label_temperature.Text = "Temperature：";
            label_distance.Text = "Distance：";
        }
        
        //关闭程序        
       private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定关闭？", "确定关闭",messButton);
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {                         
                   Environment.Exit(0);                                          
                }
        }       
         
        //画运动曲线              
        private void skinButton_datachart_Click(object sender, EventArgs e)
        {
            DataAnalyse form2 = new DataAnalyse();
            form2.Show();           
            this.Activate();
        }

        private void GroupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }   
}
