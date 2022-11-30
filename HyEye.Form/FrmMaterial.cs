using Autofac;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VisionFactory;
using VisionSDK;

namespace HyEye.WForm
{
    public partial class FrmMaterial : Form
    {
        readonly IMaterialRepository materialRepo;

        //add by LuoDian @ 20211213 用于子料号的快速切换
        readonly IOpticsRepository opticsRepo;
        readonly ToolBlockComponentSet toolBlockComponents;
        readonly ITaskRepository taskRepo;

        public FrmMaterial(ITaskRepository taskRepo, IMaterialRepository materialRepo, 
            IOpticsRepository opticsRepo, ToolBlockComponentSet toolBlockComponents)
        {
            InitializeComponent();

            this.materialRepo = materialRepo;

            //add by LuoDian @ 20211213 用于子料号的快速切换
            this.taskRepo = taskRepo;
            this.opticsRepo = opticsRepo;
            this.toolBlockComponents = toolBlockComponents;
        }

        private void FrmMaterial_Load(object sender, EventArgs e)
        {
            LoadMaterial();
        }

        void LoadMaterial()
        {
            dgvMaterials.Rows.Clear();

            List<MaterialInfoVO> materials = materialRepo.GetMaterials();
            string curMaterial = materialRepo.CurrMaterial;

            foreach (MaterialInfoVO m in materials)
            {
                //add by LuoDian @ 20211209 用于子料号的快速切换
                if(m.LstSubName == null || m.LstSubName.Count < 1 || string.IsNullOrEmpty(m.LstSubName[0]))
                {
                    List<string> lstSubName = new List<string>();
                    lstSubName.Add(materialRepo.CurSubName);
                    m.LstSubName = lstSubName;
                }

                foreach(string subName in m.LstSubName)
                {
                    dgvMaterials.Rows.Add(m.Index, m.Name, subName, m.Name == curMaterial ? "已启用" : "未启用", subName == materialRepo.CurSubName ? m.Name == curMaterial ? "已启用" : "未启用" : "未启用");
                }
            }
        }

        private void dgvMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            int index = int.Parse(dgvMaterials.Rows[e.RowIndex].Cells["colMaterialIndex"].Value.ToString());
            string materialName = dgvMaterials.Rows[e.RowIndex].Cells["colMaterialName"].Value.ToString();

            if (e.ColumnIndex == 3)
            {
                SetCurrent(index, materialName);
            }

            //add by LuoDian @ 20211209 用于子料号的快速切换
            else if(e.ColumnIndex == 4)
            {
                materialRepo.ChangeMaterialSubName(dgvMaterials.Rows[e.RowIndex].Cells["colSubMaterialName"].Value.ToString());
                LoadMaterial();
            }

            else if (e.ColumnIndex == 5)
            {
                Backup(materialName);
            }
            else if (e.ColumnIndex == 6)
            {
                Delete();
            }
        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBoxUtils.ShowQuestionCanCancel("是否将当前配置复制到新料号？");

            if (dialogResult == DialogResult.Cancel) return;

            bool copyCurrent = dialogResult == DialogResult.Yes;

            FrmMaterialAdd f = AutoFacContainer.Resolve<FrmMaterialAdd>(new NamedParameter("copyCurrent", copyCurrent));

            if (f.ShowDialog() == DialogResult.OK)
                LoadMaterial();
        }

        private void btnDeleteMaterial_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void btnRenameMaterial_Click(object sender, EventArgs e)
        {
            Rename();
        }

        void SetCurrent(int index, string materialName)
        {
            if (materialName == materialRepo.CurrMaterial) return;

            if (MessageBoxUtils.ShowQuestion($"要切换到料号[{materialName}]吗？") == DialogResult.No) return;

            materialRepo.ChangeMaterial(index);

            DialogResult = DialogResult.Retry;
        }

        void Backup(string materialName)
        {
            if (MessageBoxUtils.ShowQuestion("确定要备份该料号吗？") == DialogResult.No) return;

            materialRepo.Backup(materialName);

            LoadMaterial();
        }

        void Delete()
        {
            if (dgvMaterials.CurrentCell == null) return;

            string materialName = dgvMaterials.CurrentRow.Cells["colMaterialName"].Value.ToString();

            if (materialName == materialRepo.CurrMaterial)
            {
                MessageBoxUtils.ShowWarn("无法删除当前正在使用的料号");
                return;
            }

            if (MessageBoxUtils.ShowQuestion("确定要删除该料号吗？") == DialogResult.No) return;

            materialRepo.Delete(materialName);

            //update by LuoDian @ 20211209 用于子料号的快速切换
            //dgvMaterials.Rows.RemoveAt(dgvMaterials.CurrentRow.Index);
            LoadMaterial();
        }

        private void tsmiRenameMaterial_Click(object sender, EventArgs e)
        {
            Rename();
        }

        void Rename()
        {
            if (dgvMaterials.CurrentCell == null) return;

            string materialName = dgvMaterials.CurrentRow.Cells["colMaterialName"].Value.ToString();

            FrmMaterialRename f = AutoFacContainer.Resolve<FrmMaterialRename>(new NamedParameter("materialName", materialName));
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadMaterial();
            }
        }

        //add by Luodian @ 20211213 添加子料号
        private void btnAddSubMaterial_Click(object sender, EventArgs e)
        {
            List<string> lstSubName = materialRepo.GetMaterials().Find(a => a.Name == materialRepo.CurrMaterial).LstSubName;
            if (lstSubName == null)
                lstSubName = new List<string>();
            FrmSubMaterialAdd addSubMaterial = AutoFacContainer.Resolve<FrmSubMaterialAdd>();
            if (addSubMaterial.ShowDialog() == DialogResult.OK)
            {
                if(lstSubName.Contains(addSubMaterial.SubName))
                {
                    MessageBox.Show($"当前料号[{materialRepo.CurrMaterial}]已存在子料号[{addSubMaterial.SubName}]！新建子料号失败！");
                    return;
                }
                lstSubName.Add(addSubMaterial.SubName);

                Models.MaterialInfo tempMaterial = materialRepo.GetMaterialsForAddSubName().Find(a => a.Name == materialRepo.CurrMaterial);
                tempMaterial.LstSubName = lstSubName;
                materialRepo.Save();

                LoadMaterial();
            }
        }

        //add by Luodian @ 20211213 删除子料号
        private void btnDeleteSubMaterial_Click(object sender, EventArgs e)
        {
            if (dgvMaterials.CurrentCell == null) return;

            string subMaterialName = dgvMaterials.CurrentRow.Cells["colSubMaterialName"].Value.ToString();
            string materialName = dgvMaterials.CurrentRow.Cells["colMaterialName"].Value.ToString();

            if(materialName != materialRepo.CurrMaterial)
            {
                MessageBoxUtils.ShowWarn($"只能删除当前料号配方[{materialRepo.CurrMaterial}]下的没有使用的子料号");
                return;
            }

            if (subMaterialName == materialRepo.CurSubName)
            {
                MessageBoxUtils.ShowWarn("无法删除当前正在使用的子料号");
                return;
            }

            if (MessageBoxUtils.ShowQuestion("确定要删除该料号吗？") == DialogResult.No) return;

            Models.MaterialInfo curMaterial = materialRepo.GetMaterialsForAddSubName().Find(a => a.Name == materialRepo.CurrMaterial);
            curMaterial.LstSubName.Remove(subMaterialName);
            materialRepo.Save();

            opticsRepo.DeleteOpticsBySubName(subMaterialName);
            opticsRepo.Save();

            var taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                IToolBlockComponent toolBlock = toolBlockComponents.GetComponent(taskInfo.Name);
                toolBlock.DeleteHyToolBlockBySubName(subMaterialName);
                toolBlock.Save();
            }

            LoadMaterial();
        }
    }
}
