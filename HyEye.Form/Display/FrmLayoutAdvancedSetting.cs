using HyEye.API.Repository;
using System;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmLayoutAdvancedSetting : Form
    {
        readonly IDisplayLayoutRepository displayLayoutRepo;

        public FrmLayoutAdvancedSetting(IDisplayLayoutRepository displayLayoutRepo)
        {
            InitializeComponent();

            this.displayLayoutRepo = displayLayoutRepo;
        }

        private void FrmLayoutAdvancedSetting_Load(object sender, EventArgs e)
        {
            if (displayLayoutRepo.AutoSize)
                rdoAutoSize.Checked = true;
            else
                rdoUserSize.Checked = true;
            nudWidth.Value = displayLayoutRepo.Width;
            nudHeight.Value = displayLayoutRepo.Height;
        }

        private void rdoAutoSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAutoSize.Checked)
            {
                nudWidth.Enabled = false;
                nudHeight.Enabled = false;
            }
        }

        private void rdoUserSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUserSize.Checked)
            {
                nudWidth.Enabled = true;
                nudHeight.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            displayLayoutRepo.AutoSize = rdoAutoSize.Checked;
            displayLayoutRepo.Width = (int)nudWidth.Value;
            displayLayoutRepo.Height = (int)nudHeight.Value;

            displayLayoutRepo.Save();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
