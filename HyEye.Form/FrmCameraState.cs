using CameraSDK;
using HyEye.API.Repository;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmCameraState : Form
    {
        readonly ITaskRepository taskRepo;
        readonly ICameraRepository cameraRepo;

        public FrmCameraState(
            ITaskRepository taskRepo,
            ICameraRepository cameraRepo)
        {
            InitializeComponent();

            this.taskRepo = taskRepo;
            this.cameraRepo = cameraRepo;
        }

        Bitmap green = new Bitmap(Properties.Resources.绿圈, 30, 30);
        Bitmap red = new Bitmap(Properties.Resources.红圈, 30, 30);

        private void FrmCameraState_Load(object sender, System.EventArgs e)
        {
            getCameraState();
        }

        void getCameraState()
        {
            dgvCameraState.Rows.Clear();

            HashSet<string> cameraSNs = new HashSet<string>();

            var taskInfos = taskRepo.GetTasks();

            foreach (TaskInfoVO taskInfo in taskInfos.Where(t => t.CameraAcquireImage != null))
            {
                if (cameraSNs.Contains(taskInfo.CameraAcquireImage.CameraSN)) return;

                CameraInfoVO cameraInfo = cameraRepo.GetCameraInfo(taskInfo.CameraAcquireImage.CameraSN);

                ICamera camera = null;
                try
                {
                    camera = cameraRepo.GetCamera(taskInfo.CameraAcquireImage.CameraSN);
                }
                catch
                {

                }

                string name = cameraInfo?.UserDefinedName ?? $"未知相机[{camera?.CameraInfo.UserDefinedName ?? taskInfo.CameraAcquireImage.CameraSN}]";

                if (camera == null)
                {
                    dgvCameraState.Rows.Add(name, red, red);
                }
                else
                {
                    dgvCameraState.Rows.Add(name, green, camera.IsOpen ? green : red);
                }

                cameraSNs.Add(taskInfo.CameraAcquireImage.CameraSN);
            }
        }

        private void tsmiRefresh_Click(object sender, System.EventArgs e)
        {
            getCameraState();
        }
    }
}
