using HyEye.API.Repository;
using HyEye.Models.VO;
using HyVision.SDK;
using System.Collections.Generic;
using System.Linq;
using VisionSDK;
using VisionSDK._VisionPro;

namespace VisionFactory
{
    public class DisplayComponentSet
    {
        readonly ITaskRepository taskRepo;
        readonly IDisplayLayoutRepository displayRepo;

        public DisplayComponentSet(ITaskRepository taskRepo)
        {
            // Dsiplay 是绑定拍照的，所以这里不需要 TaskAdd 事件
            // 任务删除的时候会触发 AcqImageDelete 事件
            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            taskRepo.AcqImageRename += TaskRepo_AcqImageRename;

            this.taskRepo = taskRepo;
            this.displayRepo = Autofac.AutoFacContainer.Resolve<IDisplayLayoutRepository>();
        }

        //update by LuoDian @ 20210726 添加访问修饰符protected，用于在子类OutputDisplayComponentSet访问
        protected readonly List<IDisplayTaskImageComponent> displayControls = new List<IDisplayTaskImageComponent>();

        public void Init()
        {
            List<TaskInfoVO> taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                if (taskInfo.CameraAcquireImage != null)
                {
                    foreach (AcquireImageInfoVO acqImage in taskInfo.CameraAcquireImage.AcquireImages)
                    {
                        TaskRepo_AcqImageAdd(taskInfo.Name, acqImage.Name);
                    }
                }
            }
        }

        #region 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (VisionProDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.TaskName == oldName)
                    displayControl.TaskName = newName;
            }
        }

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            IDisplayTaskImageComponent displayControl;

            if (displayRepo.Type == HyEye.API.Config.DisplayType.VisionPro)
            {
                displayControl = new VisionProDisplayTaskImageComponent();
            }
            else
            {
                //displayControl = new HyVisionDisplayTaskImageComponent();
                displayControl = new HyVisionViewerTaskImageComponent();
            }

            displayControl.TaskName = taskName;
            displayControl.AcqOrCalibName = acqImageName;
            displayControl.ClearAllImage += DisplayControl_ClearAllImage;

            displayControls.Add(displayControl);
        }

        private void DisplayControl_ClearAllImage(object sender, System.EventArgs e)
        {
            foreach (IDisplayTaskImageComponent displayControl in displayControls)
            {
                displayControl.ClearImage();
            }
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            displayControls.Remove(a => a.TaskName == taskName && a.AcqOrCalibName == acqImageName);
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            //IDisplayTaskImageComponent
            foreach (IDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.AcqOrCalibName == oldAcqImageName && displayControl.TaskName == taskName)
                    displayControl.AcqOrCalibName = newAcqImageName;
            }
            //foreach (VisionProDisplayTaskImageComponent displayControl in displayControls)
            //{
            //    if (displayControl.AcqOrCalibName == oldAcqImageName && displayControl.TaskName == taskName)
            //        displayControl.AcqOrCalibName = newAcqImageName;
            //}
        }

        #endregion

        public IDisplayTaskImageComponent GetDisplayControl(string taskName, string acqImageName)
        {
            return displayControls.FirstOrDefault(c => c.TaskName == taskName && c.AcqOrCalibName == acqImageName);
        }
    }

}
