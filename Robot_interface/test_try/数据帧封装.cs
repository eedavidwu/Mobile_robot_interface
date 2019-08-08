using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_try
{
    public partial class Mobile_Robot : Form
    {
        const string frame_head = "A55A";
        const string frame_tail = "AA";
        //Class_Frame类：封装数据包
        public class Class_Frame
        {
            private string str;  //调用值
            private string format; //帧的格式
            private string frame; //返回值
            string len,//表示除帧头外的帧的字节
                len_num;//帧的校验字节
            private int length;
            public string STR
            {
                get
                {
                    return str;
                }
                set
                {
                    str = value;
                }
            }
            public string FORMAT
            {
                get
                {
                    return format;
                }
                set
                {
                    format = value;
                }
            }
            public string FRAME
            {
                get
                {
                    return frame;
                }
                set
                {
                    frame = value;
                }
            }
            public int LENGTH
            {
                get
                {
                    return length;
                }
                set
                {
                    length = value;
                }
            }
            public string LEN
            {
                get
                {
                    return len;
                }
                set
                {
                    len = value;
                }
            }
            //封装函数
            public void Packer_Frame()
            {
                int num = 0, x;
                for (int i = 0; i < (str.Length - str.Length % 2) / 2; i++)//取余3运算
                {
                    num += Convert.ToInt32(str.Substring(i * 2, 2), 16);//将16进制str.Substring(i * 2, 2)转换成10进制
                }
                if (str.Length % 2 != 0)
                {
                    num = Convert.ToByte(str.Substring(str.Length - 1, 1), 16);
                }
                x = str.Length / 2 + 4;
                len = x < 0x10 ? "0" + Convert.ToString(x, 16) : Convert.ToString(x, 16);
                num += x + Convert.ToInt32(format, 16);
                num = num % 256;
                len_num = num < 0x10 ? "0" + Convert.ToString(num, 16) : Convert.ToString(num, 16);
                FRAME = frame_head + len + format + str + len_num + frame_tail;
                LENGTH = FRAME.Length / 2;
            }
        }
    }
}
