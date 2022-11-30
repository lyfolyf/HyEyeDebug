using HyEye.API.Repository;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmMaterialRename : Form
    {
        readonly IMaterialRepository materialRepo;

        public FrmMaterialRename(string materialName, IMaterialRepository materialRepo)
        {
            InitializeComponent();

            oldName = materialName;

            this.materialRepo = materialRepo;
        }

        string oldName;

        private void FrmMaterialRename_Load(object sender, EventArgs e)
        {
            tbMaterialName.Text = oldName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbMaterialName.CheckNotWhiteSpace("请输入料号名称")
                || !tbMaterialName.CheckRegex(ComPattern.LegalPathChar, "料号名称不能包含下列字符：/ \\ : * ? \" < > | ")) return;

            materialRepo.Rename(oldName, tbMaterialName.Text);

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
