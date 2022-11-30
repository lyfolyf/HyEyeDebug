using HyEye.API.Repository;
using HyEye.Models;
using System.Collections.Generic;
using System.Linq;
using VisionSDK;
using VisionSDK._VisionPro;

namespace VisionFactory
{
    public class HandEyeDisplayComponentSet
    {
        readonly ITaskRepository taskRepo;

        public HandEyeDisplayComponentSet(ITaskRepository taskRepo)
        {
            taskRepo.TaskRename += TaskRepo_TaskRename;

            // 暂时不考虑标定引用的问题
            // 当两个标定同时打开，并且其中一个是另一个的引用时，应该是要报错的
            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibDelete += TaskRepo_CalibDelete;
            taskRepo.CalibRename += TaskRepo_CalibRename;

            this.taskRepo = taskRepo;
        }

        readonly List<IDisplayTaskImageComponent> displayControls = new List<IDisplayTaskImageComponent>();

        #region 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (VisionProDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.TaskName == oldName)
                    displayControl.TaskName = newName;
            }
        }

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (calibType != CalibrationType.HandEye) return;
        }

        private void TaskRepo_CalibDelete(string taskName, string _, CalibrationType calibType, string calibName)
        {
            if (calibType != CalibrationType.HandEye) return;

            displayControls.Remove(a => a.TaskName == taskName && a.AcqOrCalibName == calibName);
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            if (calibType != CalibrationType.HandEye) return;

            foreach (VisionProDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.AcqOrCalibName == oldCalibName && displayControl.TaskName == taskName)
                    displayControl.AcqOrCalibName = newCalibName;
            }
        }

        #endregion

        public IDisplayTaskImageComponent GetDisplayControl(string taskName, string calibName)
        {
            IDisplayTaskImageComponent component = displayControls.FirstOrDefault(c => c.TaskName == taskName && c.AcqOrCalibName == calibName);

            if (component == null)
            {
                component = new VisionProDisplayTaskImageComponent()
                {
                    TaskName = taskName,
                    AcqOrCalibName = calibName
                };
                displayControls.Add(component);
            }

            return component;
        }
    }
}
