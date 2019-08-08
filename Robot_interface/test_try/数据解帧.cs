using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace test_try
{
    public partial class Mobile_Robot : Form
    {
       
        public static float yaw, pitch, roll, tempr;
        public static int ax, ay, az, gx, gy, gz, time;
        public static string Distance;
        public static string ALL_Propel,//变量，传递接收帧里面的整体命令
           Singe_Propel,
           Rev,  
           Propel_leftup,
           Propel_rightup,
           Propel_leftdown,
           Propel_rightdown,
           a, b, Xd, Zd,//变量
           Ud, wd, yawd, yawp, c,
           t1, t2, P1, avx, avz;

        public static ArrayList List = new ArrayList();//定义一个数组，用于存储姿态角数据
        public static ArrayList List1 = new ArrayList();//定义一个数组，用于存储加速度和位置数据

        //判断接收到控制命令返回帧的数据类型，并解帧和显示
        private void Data_show_command_ack()
        {
            if (Check_Data_command())
            {
                if (Flag_Received == 0x11)
                {
                    if (rx_buffer[3] == 0x01)
                    {
                        for (int i = 0; i < rx_buffer[2] + 2; i++)
                        {
                            string str = Convert.ToString(rx_buffer[i], 16).ToUpper();
                            str = str.Length == 1 ? "0" + str : str;
                            textBox4.AppendText(str + " ");
                        }
                        textBox4.AppendText("\n");//在textBox4中显示接收到的值
                        Show_Single_String();
                        Flag_Received = 0x00;
                        success_flag = 1;
                        pro_flag = false;
                    }
                    else if ((rx_buffer[3] == 0x02))
                    {
                        for (int i = 0; i < rx_buffer[2] + 2; i++)
                        {
                            string str = Convert.ToString(rx_buffer[i], 16).ToUpper();
                            str = str.Length == 1 ? "0" + str : str;
                            textBox4.AppendText(str + " ");
                        }
                        textBox4.AppendText("\n");//在textBox4中显示接收到的值
                        Show_ALL_String();
                        Flag_Received = 0x00;
                        success_flag = 1;
                        pro_flag = false;
                    }
                    else if ((rx_buffer[3] == 0x04))
                    {
                        for (int i = 0; i < rx_buffer[2] + 2; i++)
                        {
                            string str = Convert.ToString(rx_buffer[i], 16).ToUpper();
                            str = str.Length == 1 ? "0" + str : str;
                            textBox4.AppendText(str + " ");
                        }
                        textBox4.AppendText("\n");//在textBox4中显示接收到的值
           
                        Flag_Received = 0x00;
                        success_flag = 1;
                        pro_flag = false;
                    }
                    else if ((rx_buffer[3] == 0x06))
                    {
                        for (int i = 0; i < rx_buffer[2] + 2; i++)
                        {
                            string str = Convert.ToString(rx_buffer[i], 16).ToUpper();
                            str = str.Length == 1 ? "0" + str : str;
                            textBox4.AppendText(str + " ");
                        }
                        textBox4.AppendText("\n");//在textBox4中显示接收到的值
                        Flag_Received = 0x00;
                        success_flag = 1;
                        pro_flag = false;
                    }
                }
            }
        }
        //控制命令返回帧校验和
        public bool Check_Data_command()
        {
            int i;
            int checksum = 0;
            try
            {
                for (i = 2; i < rx_buffer[2]; i++)
                    checksum += rx_buffer[i];
            }
            catch
            {

                Write_Textbox3("Error_datareceived");
            }
            if (rx_buffer[2] < 1)
            {
                return false;
            }
            if ((checksum % 256) == rx_buffer[rx_buffer[2]])
                return true; //Checksum successful
            else
            {
                Write_Textbox3("Error_datareceived");
                return false; //Checksum error
            }
        }

        
        //显示姿态角数据，
        //区分正负数，最高位为1时为负数，补码形式存放
        //roll，pitch，yaw每个轴数据占两个字节，低字节在前
        //roll,pitch为实际数值乘以100得到的值，yaw为实际数值乘以10得到的值
        public void Show_IMU()
        {
            int temp;
            temp = 0;
            temp = rx_buffer[22];
            temp <<= 8;
            temp |= rx_buffer[21];
            if ((temp & 0x8000) == 0x8000)//最高位为1，负数，补码形式存放
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);//正数
            roll = (float)(temp / 100.0f); //俯仰角roll

            temp = 0;
            temp = rx_buffer[24];
            temp <<= 8;
            temp |= rx_buffer[23];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            pitch = (float)(temp / 100.0f);//横滚角pitch

            temp = 0;
            temp = rx_buffer[26];
            temp <<= 8;
            temp |= rx_buffer[25];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            yaw = (float)(temp / 10.0f);//航向角yaw

            label_yaw.Text = "yaw:" + yaw.ToString();
            label_pitch.Text = "pitch:" + pitch.ToString();
            label_roll.Text = "roll:" + roll.ToString();

           // if (Save_flag == true)
            {
                List.Add(DateTime.Now.ToString("yyy MM dd hh mm ss fff") + " ");
                List.Add(yaw + " ");
                List.Add(pitch + " ");
                List.Add(roll + " ");
                List.Add("\r\n");
            }
        }
        //显示加速度和陀螺仪数据
        public void Show_Motion()
        {
            int temp;
            //int time;
            temp = 0;
            temp = rx_buffer[8];
            temp <<= 8;
            temp |= rx_buffer[7];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            ax = temp;//加速度计 X轴的ADC值

            temp = 0;
            temp = rx_buffer[10];
            temp <<= 8;
            temp |= rx_buffer[9];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            ay = temp;//加速度计 Y轴的ADC值

            temp = 0;
            temp = rx_buffer[12];
            temp <<= 8;
            temp |= rx_buffer[11];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            az = temp;//加速度计 Z轴的ADC值

            temp = 0;
            temp = rx_buffer[15];
            temp <<= 8;
            temp |= rx_buffer[14];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            gx = temp;//陀螺仪 X轴的ADC值

            temp = 0;
            temp = rx_buffer[17];
            temp <<= 8;
            temp |= rx_buffer[16];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            gy = temp;//陀螺仪 Y轴的ADC值

            temp = 0;
            temp = rx_buffer[19];
            temp <<= 8;
            temp |= rx_buffer[18];
            if ((temp & 0x8000) == 0x8000)
            {
                temp = 0 - ((temp ^ 0xFFFF) + 0x0001);
            }
            else temp = (temp & 0x7fff);
            gz = temp;//陀螺仪 Z轴的ADC值

            time = 49;
            label_acceleration_x.Text = "ax:" + ax.ToString();
            label_acceleration_y.Text = "ay:" + ay.ToString();
            label_acceleration_z.Text = "az:" + az.ToString();
            label_gyro_x.Text = "gx:" + gx.ToString();
            label_gyro_y.Text = "gy:" + gy.ToString();
            label_gyro_z.Text = "gz:" + gz.ToString();

            //if (Save_flag == true)
            {
                List1.Add(DateTime.Now.ToString("yyy MM dd hh mm ss fff") + " ");
                List1.Add(ax + " ");
                List1.Add(ay + " ");
                List1.Add(az + " ");
                List1.Add(gx + " ");
                List1.Add(gy + " ");
                List1.Add(gz + " ");
                List1.Add(time + " ");
                //List1.Add(F1 + " ");
                //List1.Add(F2 + " ");
                //List1.Add(F3 + " ");
                //List1.Add(F4 + " ");
                List1.Add("\r\n");
            }
        }

        //显示整体命令字符串
        public void Show_ALL_String()
        {
            int temp;
            string ACI = "";
            temp = 0;
            temp = rx_buffer[4];
            ACI += AscllToString(temp);                 //该函数才是ascii码转换为字符串
            ALL_Propel = ACI;   //对应于Write_Textbox2(string str)中“1”到“9”这9种情况
            Write_Textbox3("datatime");
            Write_Textbox3(ALL_Propel);
        }
    
        public void Show_Single_String()
        {
            int temp;
            temp = 0;
            temp = rx_buffer[4];
            Singe_Propel = (temp.ToString().Length) == 1 ? "0" + Convert.ToString(temp, 16) : Convert.ToString(temp, 16);//对应于Write_Textbox2(string str)中“01”到“04”这4种情况

            temp = 0;
            temp = rx_buffer[5];
            Rev = Convert.ToString(temp, 10);//

            Write_Textbox3("datatime");
            Write_Textbox3(Singe_Propel);
        }
     
        //acii码转化为字符
        public string AscllToString(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
    }
}

