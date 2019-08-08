using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;

namespace test_try
{
    public partial class Mobile_Robot : Form
    {
        static string location = System.Environment.CurrentDirectory;//获取debug所在位置

        //保存数据
        private void skinButton_list_Click(object sender, EventArgs e)
        {
            if (skinButton_list.Text == "数据存储（关）")  //开始存储数据
            {
                skinButton_list.Text = "数据存储（开）";
                Save_flag = true;
                if (List.Count > 0)
                {
                    List.Clear();
                }
                if (List1.Count > 0)
                {
                    List1.Clear();
                }
            }
            else
            {
                skinButton_list.Text = "数据存储（关）";
                Save_flag = false;
            }
        }
        //写入文件
        private void skinButton_txt_Click(object sender, EventArgs e)
        {
            Write_text();
            Write_text1();
           // skinButton_txt.Enabled = false;
        }
        public void Write_text()
        {
            skinButton_txt.Enabled = false;
            //int length = List.Count/5;
            //string Length = Convert.ToString(length, 16);
            //textBox3.AppendText("List长度为： " + Length +"\n");
            string Date = DateTime.Now.ToString("yyy-MM-dd");
            FileStream acce = new FileStream(location + "\\" + "姿态角数据" + Date + ".txt", FileMode.Append);
            StreamWriter sw_acce = new StreamWriter(acce);
            for (int i = 0; i < List.Count; i++)
            {
                sw_acce.Write(List[i]);
            }
            sw_acce.Write("\r\n");
            sw_acce.Flush();
            sw_acce.Close();
            List.Clear();
            //skinButton_txt.Enabled = true;
        }
        public void Write_text1()
        {
            //skinButton_txt.Enabled = false;
            string Date = DateTime.Now.ToString("yyy-MM-dd");
            FileStream acce = new FileStream(location + "\\" + "加速度和螺旋仪数据" + Date + ".txt", FileMode.Append);
            StreamWriter sw_acce = new StreamWriter(acce);
            for (int i = 0; i < List1.Count; i++)
            {
                sw_acce.Write(List1[i]);
            }
            sw_acce.Write("\r\n");
            sw_acce.Flush();
            sw_acce.Close();
            List1.Clear();
            skinButton_txt.Enabled = true;
        }
    }
}
