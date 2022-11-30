using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyEye.WForm.Display
{
    public partial class FrmSetAreaInfo : Form
    {
        int vcount = 0;
        int hcount = 0;
        public FrmSetAreaInfo()
        {
            InitializeComponent();
        }

        public FrmSetAreaInfo(int vcount, int hcount) : this()
        {
            this.vcount = vcount;
            this.hcount = hcount;
            ucAreaSelect1.VCount = vcount;
            ucAreaSelect1.HCount = hcount;
            numericColStart.Minimum = 1;
            numericRowStart.Minimum = 1;
            numericColSpan.Minimum = 1;
            numericRowSpan.Minimum = 1;
            numericColStart.Maximum = vcount;
            numericRowStart.Maximum = hcount;
            numericColSpan.Maximum = vcount;
            numericRowSpan.Maximum = hcount;
        }
        AreaInfo areaInfo = new AreaInfo();

        [Browsable(false)]
        public AreaInfo AreaInfo
        {
            get { return areaInfo; }
            private set
            {
                areaInfo = value;
            }
        }

        private void FrmSetAreaInfo_Load(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void numericRowStart_ValueChanged(object sender, EventArgs e)
        {
            numericRowSpan.Maximum = hcount - AreaInfo.Row;
            UpdateInfo();
        }

        private void numericColStart_ValueChanged(object sender, EventArgs e)
        {
            numericColSpan.Maximum = vcount - AreaInfo.Column;
            UpdateInfo();
        }

        private void numericRowSpan_ValueChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void numericColSpan_ValueChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void txtBlockInfo_TextChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            AreaInfo.Column = (int)numericColStart.Value;
            AreaInfo.Row = (int)numericRowStart.Value;
            AreaInfo.ColumnSpan = (int)numericColSpan.Value;
            AreaInfo.RowSpan = (int)numericRowSpan.Value;
            AreaInfo.Block = txtBlockInfo.Text;
            List<AreaInfo> infos = new List<AreaInfo> { AreaInfo };
            ucAreaSelect1.SetAreaInfos(infos);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void linkLblSetModelImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLoadImage frmLoadImage = new FrmLoadImage();
            if (frmLoadImage.ShowDialog() == DialogResult.OK)
            {
                AreaInfo.ModelPath = frmLoadImage.Path;
            }
        }
    }
}
