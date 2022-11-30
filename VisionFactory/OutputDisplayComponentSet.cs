using HyEye.API.Repository;
using HyEye.Models.VO;
using HyVision.Models;
using HyVision.SDK;
using HyVision.Tools.ImageDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionSDK;

namespace VisionFactory
{
    public class OutputDisplayComponentSet : DisplayComponentSet
    {
        readonly ITaskRepository taskRepo;
        readonly ToolBlockComponentSet toolBlockComponentSet;

        public OutputDisplayComponentSet(ITaskRepository taskRepo, ToolBlockComponentSet toolBlockComponentSet) : base(taskRepo)
        {
            this.taskRepo = taskRepo;
            this.toolBlockComponentSet = toolBlockComponentSet;
        }

        public new void Init()
        {
            displayControls.Clear();
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

                IToolBlockComponent toolBlockComponent = toolBlockComponentSet.GetComponent(taskInfo.Name);
                if (toolBlockComponent == null)
                    throw new Exception($"未能找到名称为“{taskInfo.Name}”的ToolBlock组件！");

                if (toolBlockComponent.GetType() != typeof(HyToolBlockComponent))
                    throw new Exception($"目前只支持“{typeof(HyToolBlockComponent)}”的输出结果图像显示");

                HyToolBlockComponent hyToolBlockComponent = (HyToolBlockComponent)toolBlockComponent;
                if (hyToolBlockComponent.GetOutputsTerminal() != null && hyToolBlockComponent.GetOutputsTerminal().Count > 0)
                {
                    foreach (HyTerminal terminal in hyToolBlockComponent.GetOutputsTerminal().Where(a => a.ValueType == typeof(HyImage) || a.ValueType == typeof(System.Drawing.Bitmap)))
                    {
                        TaskRepo_AcqImageAdd(taskInfo.Name, terminal.Name);
                    }
                }
            }
        }

        #region 事件

        public void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (OutputDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.TaskName == oldName)
                    displayControl.TaskName = newName;
            }
        }

        public void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            OutputDisplayTaskImageComponent displayControl = new OutputDisplayTaskImageComponent
            {
                TaskName = taskName,
                AcqOrCalibName = acqImageName,
                Index = displayControls.Count,
                IsBinding = false
            };
            //displayControl.

            displayControls.Add(displayControl);
        }

        public void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            displayControls.Remove(a => a.TaskName == taskName && a.AcqOrCalibName == acqImageName);
        }

        public void TaskRepo_AcqImageUpdate(IDisplayTaskImageComponent displayControl)
        {
            TaskRepo_AcqImageDelete(displayControl.TaskName, displayControl.AcqOrCalibName);
            displayControls.Add(displayControl);
        }

        public void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            foreach (OutputDisplayTaskImageComponent displayControl in displayControls)
            {
                if (displayControl.AcqOrCalibName == oldAcqImageName && displayControl.TaskName == taskName)
                    displayControl.AcqOrCalibName = newAcqImageName;
            }
        }

        public IDisplayTaskImageComponent TaskRepo_ControlSelect(string taskName, string acqImageName)
        {
            IDisplayTaskImageComponent component = displayControls.Find(a => a.TaskName == taskName && a.AcqOrCalibName == acqImageName);
            return component;
        }

        public void TaskRepo_ResetBinding()
        {
            foreach (OutputDisplayTaskImageComponent displayControl in displayControls)
                displayControl.IsBinding = false;
        }

        public IDisplayTaskImageComponent GetDisplayControlByGraphicName(string taskName, string graphicName)
        {
            List<OutputDisplayTaskImageComponent> lstDisplayComponents = new List<OutputDisplayTaskImageComponent>();
            foreach (IDisplayTaskImageComponent iComponent in displayControls)
            {
                lstDisplayComponents.Add((OutputDisplayTaskImageComponent)iComponent);
            }
            return lstDisplayComponents.FirstOrDefault(c => c.TaskName == taskName && c.GraphicNames.Contains(graphicName));
        }
        #endregion

    }
}
