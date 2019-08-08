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
        private void skinButton_data_req_Click(object sender, EventArgs e)
        {
            Class_Frame a = new Class_Frame();
            a.STR = "";
            a.FORMAT = "B1";
            a.Packer_Frame();
            WriteBytesToSerialport(a.FRAME);
        }
    }
}
