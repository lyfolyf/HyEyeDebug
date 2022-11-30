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

namespace HyEye.WForm.Display
{
    public partial class FrmLoadImage : Form
    {
        public FrmLoadImage()
        {
            InitializeComponent();
            btnApply.Enabled = false;
            groupBox1.Enabled = false;
        }
        Bitmap bmp = null;
        string path = string.Empty;

        public Bitmap Bmp
        {
            get { return bmp; }
            set
            {
                bmp = value;
            }
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }

        public string Key
        {
            get { return cbSurface.Text; }
            set
            {
                cbSurface.Text = value;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            path = openFileDialog1.FileName;
            bmp = new Bitmap(path);
            hyImageDisplayControlSimple1.Image = bmp;
            btnApply.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (bmp == null)
            {
                this.DialogResult = DialogResult.None;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void rbRotate0_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbRotate90_CheckedChanged(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            bmp = new Bitmap(path);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            hyImageDisplayControlSimple1.Image = bmp;
        }

        private void rbRotate180_CheckedChanged(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            bmp = new Bitmap(path);
            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            hyImageDisplayControlSimple1.Image = bmp;
        }

        private void rbRotate270_CheckedChanged(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            bmp = new Bitmap(path);
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            hyImageDisplayControlSimple1.Image = bmp;
        }
        public IList NameList
        {
            set
            {
                cbSurface.DataSource = value;
            }
        }
        private void FrmLoadImage_Load(object sender, EventArgs e)
        {
            //cbSurface.DataSource = Enum.GetNames(typeof(Surface)).ToList();
        }
    }
}
