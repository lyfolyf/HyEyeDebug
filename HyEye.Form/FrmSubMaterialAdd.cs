using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;

namespace HyEye.WForm
{
    /// <summary>
    /// add by LuoDian @ 20211213 用于子料号的快速切换
    /// </summary>
    public partial class FrmSubMaterialAdd : Form
    {
        readonly IMaterialRepository materialRepo;
        readonly IOpticsRepository opticsRepo;
        readonly ToolBlockComponentSet toolBlockComponents;
        readonly ITaskRepository taskRepo;
        public string SubName;

        public FrmSubMaterialAdd(ITaskRepository taskRepo, IMaterialRepository materialRepo,
            IOpticsRepository opticsRepo,
            ToolBlockComponentSet toolBlockComponents)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.materialRepo = materialRepo;
            this.opticsRepo = opticsRepo;
            this.toolBlockComponents = toolBlockComponents;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!tbMaterialName.CheckNotWhiteSpace("请输入料号名称")
                || !tbMaterialName.CheckRegex(ComPattern.LegalPathChar, "料号名称不能包含下列字符：/ \\ : * ? \" < > | ")) return;

            if(opticsRepo.FindOpticsBySubName(tbMaterialName.Text) != null)
            {
                MessageBox.Show($"料号[{tbMaterialName.Text}]已存在！不能添加！");
                return;
            }    

            opticsRepo.AddNewOptics(tbMaterialName.Text);
            opticsRepo.Save();

            var taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                IToolBlockComponent toolBlock = toolBlockComponents.GetComponent(taskInfo.Name);
                toolBlock.AddNewHyToolBlock(tbMaterialName.Text);
                toolBlock.Save();
            }
            
            SubName = tbMaterialName.Text;

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
