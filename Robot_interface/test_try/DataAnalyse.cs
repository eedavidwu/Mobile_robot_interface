using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_try
{
    public partial class DataAnalyse : Form
    {
        public ArrayList time = new ArrayList();
        public ArrayList acc_data_ax = new ArrayList();
        public ArrayList acc_data_ay = new ArrayList();
        public ArrayList acc_data_az = new ArrayList();
        public ArrayList list_2 = new ArrayList();
        public ArrayList list_3 = new ArrayList();
        Mobile_Robot aa = new Mobile_Robot();
        public bool ax = true;
        public bool ay = true;
        public bool az = true;

        public int elist_length = 9;

        public DataAnalyse()
        {
            InitializeComponent();
        }

        private void DataAnalyse_Load(object sender, EventArgs e)
        {
            Data_Copy();
            Paint_Basic();
        }

        private void Data_Copy()
        {
            list_2 = (ArrayList)Mobile_Robot.List1.Clone();  //将加速度、陀螺仪的数据拷贝存入list_2
            for (int i = 0; i < list_2.Count / 9; i++)
            {
                acc_data_ax.Add(list_2[1 + elist_length * i]);//对应ax加速度 
                acc_data_ay.Add(list_2[2 + elist_length * i]);//对应ay加速度
                acc_data_az.Add(list_2[3 + elist_length * i]);//对应az加速度           
                time.Add(list_2[7 + elist_length * i]);     //对应time值

            }                  
        }

        private Graphics  Paint_Basic()
        {
            //80代表两侧的边距，time.Count为每条曲线上数据点的个数，两个数据点之间相隔5（x轴距离）
            if (panel1.Width > (time.Count * 5 + 80))
                pictureBox1.Width = panel1.Width;
            else
                pictureBox1.Width = time.Count * 5 + 80;
            pictureBox1.Height = panel1.Height;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            Bitmap bmap = new Bitmap(width, height);
            Graphics gph = Graphics.FromImage(bmap);
            gph.Clear(Color.Black);

            gph.DrawLine(Pens.White, 40, 20, 40, height - 30);//画y轴
            gph.DrawLine(Pens.White, 40, height - 30, width - 30, height - 30);//画x轴
            gph.DrawLine(Pens.White, width - 33, height - 27, width - 30, height - 30);//画x轴箭头
            gph.DrawLine(Pens.White, width - 33, height - 33, width - 30, height - 30);
            gph.DrawString("t/50ms", new Font("宋体", 7), Brushes.White, new PointF(width - 45, height - 30));
            gph.DrawLine(Pens.White, 37, 23, 40, 20);//画y轴箭头
            gph.DrawLine(Pens.White, 43, 23, 40, 20);

            //画X轴刻度
            for (int i = 0; i < (width - 80) / 25; i++)      //刻度每加5，x轴上尺寸增加25
            {
                gph.DrawLine(Pens.White, 40 + i * 25, height - 30, 40 + i * 25, height - 33);
                if (i == 1)
                    gph.DrawString((i * 5).ToString(), new Font("宋体", 7), Brushes.White, new PointF(36 + i * 25, height - 30));
                else if (i < 20)
                    gph.DrawString((i * 5).ToString(), new Font("宋体", 7), Brushes.White, new PointF(34 + i * 25, height - 30));
                else if (i < 200)
                    gph.DrawString((i * 5).ToString(), new Font("宋体", 7), Brushes.White, new PointF(31 + i * 25, height - 30));
                else if (i < 2000)
                    gph.DrawString((i * 5).ToString(), new Font("宋体", 7), Brushes.White, new PointF(29 + i * 25, height - 30));
            }

            pictureBox1.Image = bmap;
            return gph;
        }

        private void Paint_Acc_Chart()               //画加速度曲线
        {
            Graphics gph = Paint_Basic();
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            //Y轴刻度
            if (time.Count > 0)
            {
                int n = (height - 60) / 10;//计算y轴上刻度点的个数，（height-60）为y轴的长度，每间隔10取一个刻度点
                int min = Get_MaxOrMin("min");
                int max = Get_MaxOrMin("max");
                int length = max - min;//y轴刻度线的（最大值-最小值）
                double Y = length / (double)n;  //每两个刻度之间实际的数据间隔，（每个刻度点间隔）代表的（实际数据中的间隔）
                for (int i = 0; i < n + 1; i++)
                {
                    gph.DrawLine(Pens.White, 40, height - 30 - 10 * i, 42, height - 30 - 10 * i);
                    gph.DrawString(((int)(min + Y * i)).ToString(), new Font("宋体", 7), Brushes.White, new PointF(5, height - 34 - 10 * i));
                }
                //画3个方向上的加速度曲线
                for (int i = 0; i < time.Count - 1; i++)
                {
                    if (ax == true)
                    {
                        int current_x = Convert.ToInt16(acc_data_ax[i]);//画ax的曲线图
                        int next_x = Convert.ToInt16(acc_data_ax[i + 1]);
                        double ax1 = height - 30 - ((current_x - min) / ((double)length)) * (n * 10);
                        double ax2 = height - 30 - ((next_x - min) / ((double)length)) * (n * 10);
                        gph.DrawLine(Pens.Red, 40 + 5 * i, (float)(ax1), 40 + 5 * (i + 1), (float)(ax2));
                    }
                    if (ay == true)
                    {
                        int current_y = Convert.ToInt16(acc_data_ay[i]);//画ay的曲线图
                        int next_y = Convert.ToInt16(acc_data_ay[i + 1]);
                        double ay1 = height - 30 - ((current_y - min) / ((double)length)) * (n * 10);
                        double ay2 = height - 30 - ((next_y - min) / ((double)length)) * (n * 10);
                        gph.DrawLine(Pens.Blue, 40 + 5 * i, (float)(ay1), 40 + 5 * (i + 1), (float)(ay2));
                    }
                    if (az == true)
                    {
                        int current_z = Convert.ToInt16(acc_data_az[i]);//画az的曲线图
                        int next_z = Convert.ToInt16(acc_data_az[i + 1]);
                        double az1 = height - 30 - ((current_z - min) / ((double)length)) * (n * 10);
                        double az2 = height - 30 - ((next_z - min) / ((double)length)) * (n * 10);
                        gph.DrawLine(Pens.Yellow, 40 + 5 * i, (float)(az1), 40 + 5 * (i + 1), (float)(az2));
                    }
                }
            }    
        }
        private int Get_MaxOrMin(string str)   //获取3个加速度数组中的最大值和最小值
        {
            int min1 = 100000000;
            int max1 = -100000000;
            int min2 = 100000000;
            int max2 = -100000000;
            int min3 = 100000000;
            int max3 = -100000000;
            int min = 100000000;
            int max = -100000000;
            if (ax)
            {               
                for (int i = 0; i < acc_data_ax.Count; i++)
                {
                    int temp1 = Convert.ToInt16(acc_data_ax[i]);
                    if (temp1 > max1)
                        max1 = temp1;
                    if (temp1 < min1)
                        min1 = temp1;
                }
            }
            if (ay)
            {
                for (int i = 0; i < acc_data_ay.Count; i++)
                {
                    int temp2 = Convert.ToInt16(acc_data_ay[i]);
                    if (temp2 > max2)
                        max2 = temp2;
                    if (temp2 < min2)
                        min2 = temp2;
                }
            }
            if (az)
            {
                for (int i = 0; i < acc_data_az.Count; i++)
                {
                    int temp3 = Convert.ToInt16(acc_data_az[i]);
                    if (temp3 > max3)
                        max3 = temp3;
                    if (temp3 < min3)
                        min3 = temp3;
                }
            }
            max = Max(max1, max2, max3);
            min = Min(min1, min2, min3);
            if (str == "max")
                return max;
            else if (str == "min")
                return min;
            else
                return 0;
        }
        private int Max(int a,int b,int c)    //获得3个数中最大值
        {
            int max;
            if (a > b)
            {
                max = a;
                if (a > c)
                    max = a;
                else
                    max = c;
            }
            else
            {
                max = b;
                if (b > c)
                    max = b;
                else
                    max = c;
            }
            return max;
        }
        private int Min(int a, int b, int c)    //获得3个数中最小值
        {
            int min;
            if (a > b)
            {
                min = b;
                if (b > c)
                    min = c;
                else
                    min = b;
            }
            else
            {
                min = a;
                if (a > c)
                    min = c;
                else
                    min = a;
            }
            return min;
        }


        private void button2_Click(object sender, EventArgs e)//刷新
        {
            time.Clear();
            acc_data_ax.Clear();
            acc_data_ay.Clear();
            acc_data_az.Clear();

            Data_Copy();
            Paint_Basic();

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            Paint_Acc_Chart();
        
        }  

        private void button_ax_Click(object sender, EventArgs e)
        {
            if (button_ax.Text == "ax：on")
            {
                button_ax.Text = "ax：off";
                ax = false;
                Paint_Acc_Chart();
            }
            else
            {
                button_ax.Text = "ax：on";
                ax = true;
                Paint_Acc_Chart();
            }
        }

        private void button_ay_Click(object sender, EventArgs e)
        {
            if (button_ay.Text == "ay：on")
            {
                button_ay.Text = "ay：off";
                ay = false;
                Paint_Acc_Chart();
            }
            else
            {
                button_ay.Text = "ay：on";
                ay = true;
                Paint_Acc_Chart();
            }
        }

        private void button_az_Click(object sender, EventArgs e)
        {
            if (button_az.Text == "az：on")
            {
                button_az.Text = "az：off";
                az = false;
                Paint_Acc_Chart();
            }
            else
            {
                button_az.Text = "az：on";
                az = true;
                Paint_Acc_Chart();
            }
        }

        private void button1_Click(object sender, EventArgs e)       //画加速度曲线
        {
            Paint_Acc_Chart();
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

