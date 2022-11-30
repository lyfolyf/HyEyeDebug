using HyEye.API.Repository;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmMaterialAdd : Form
    {
        bool copyCurrent;
        readonly IMaterialRepository materialRepo;

        public FrmMaterialAdd(bool copyCurrent, IMaterialRepository materialRepo)
        {
            InitializeComponent();

            this.copyCurrent = copyCurrent;
            this.materialRepo = materialRepo;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbMaterialName.CheckNotWhiteSpace("请输入料号名称")
                || !tbMaterialName.CheckRegex(ComPattern.LegalPathChar, "料号名称不能包含下列字符：/ \\ : * ? \" < > | ")) return;

            materialRepo.Add(tbMaterialName.Text, copyCurrent);

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tbMaterialName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}
