using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm.Settings
{
    public partial class FrmBatchAddTask : Form
    {
        public FrmBatchAddTask(ICameraRepository cameraRepo)
        {
            InitializeComponent();

            List<CameraInfoVO> cameraInfos = cameraRepo.GetCamerasWithVirtual();

            cmbCameras.Items.AddRange(cameraInfos.ToArray());

            cmbVisionType.DataSource = Enum.GetValues(typeof(TaskType))
               .Cast<Enum>().Where(a => (TaskType)a != TaskType.Extern).ToList();
        }

        public int TaskCount { get; private set; }

        public TaskType Type { get; private set; }

        public CameraInfoVO CameraInfo { get; private set; }

        public int AcqImageCount { get; private set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!cmbCameras.CheckSelected("请选择相机") || !cmbVisionType.CheckSelected("请选择视觉类型")) return;

            TaskCount = (int)nudTaskCount.Value;
            Type = (TaskType)cmbVisionType.SelectedItem;
            CameraInfo = (CameraInfoVO)cmbCameras.SelectedItem;
            AcqImageCount = (int)nudAcqCount.Value;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
