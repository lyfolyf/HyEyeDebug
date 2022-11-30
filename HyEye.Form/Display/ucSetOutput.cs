using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class ucSetOutput : UserControl
    {
        public string TaskName { get; set; }
        public int RecordIndex { get; set; }
        public ucSetOutput(string taskName, int recordIndex)
        {
            TaskName = taskName;
            RecordIndex = recordIndex;

            InitializeComponent();
            gbMain.Text = taskName;

            //暂时禁掉 全部输出
            rbAll.Enabled = false;

            if (recordIndex < 0)
            {
                nudIndex.Value = 0;
                rbIndex.Checked = true;
                nudIndex.Enabled = true;
                //nudIndex.Value = 0;
                //rbAll.Checked = true;
                //nudIndex.Enabled = false;
            }
            else
            {
                nudIndex.Value = recordIndex;
                rbIndex.Checked = true;
                nudIndex.Enabled = true;
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            nudIndex.Enabled = false;
            RecordIndex = -1;
        }

        private void rbIndex_CheckedChanged(object sender, EventArgs e)
        {
            nudIndex.Enabled = true;
            RecordIndex = Convert.ToInt32(nudIndex.Value);
        }

        private void nudIndex_ValueChanged(object sender, EventArgs e)
        {
            RecordIndex = Convert.ToInt32(nudIndex.Value);
        }
    }
}
