using GL.Kit.Log;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.IO;

namespace HyEye.API.Repository
{
    public interface ISimulationRepository
    {
        event Action EnabledChanged;

        bool Enabled { get; set; }

        string GetLocalImageDirectory();

        void SetLocalImageDirectory(string directory);
    }

    public class SimulationRepository : ISimulationRepository
    {
        public event Action EnabledChanged;

        readonly ITaskRepository taskRepo;
        readonly IBasicRepository basicRepo;
        readonly IGLog log;

        string localImageDirectory = null;

        bool enabled = false;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    EnabledChanged?.Invoke();

                    if (enabled)
                        SetLocalImageDirectory(localImageDirectory);
                }
            }
        }

        public SimulationRepository(
            ITaskRepository taskRepo,
            IBasicRepository basicRepo,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.basicRepo = basicRepo;
            this.log = log;

            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskRename += TaskRepo_TaskRename;

            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageRename += TaskRepo_AcqImageRename;

            taskRepo.CalibAdd += TaskRepo_CalibAdd;
            taskRepo.CalibRename += TaskRepo_CalibRename;

            SetLocalImageDirectory(basicRepo.SimulationPath);

            basicRepo.SimulationChanged += AfterSimulationChanged;
        }

        private void AfterSimulationChanged(string path)
        {
            SetLocalImageDirectory(path);
        }

        #region 事件

        private void TaskRepo_TaskAdd(TaskInfoVO taskInfo)
        {
            Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}");
        }

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            try
            {
                string srcDir = $@"{localImageDirectory}\{oldName}";
                if (Directory.Exists(srcDir))
                {
                    DirectoryUtils.Rename(srcDir, newName);
                }
                else
                {
                    Directory.CreateDirectory($@"{localImageDirectory}\{newName}");
                }
            }
            catch (Exception)
            {
            }
        }

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            Directory.CreateDirectory($@"{localImageDirectory}\{taskName}\{acqImageName}");
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            try
            {
                string srcDir = $@"{localImageDirectory}\{taskName}\{oldAcqImageName}";
                if (Directory.Exists(srcDir))
                {
                    DirectoryUtils.Rename(srcDir, newAcqImageName);
                }
                else
                {
                    Directory.CreateDirectory(srcDir);
                }
            }
            catch (Exception)
            {

            }
        }

        private void TaskRepo_CalibAdd(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            Directory.CreateDirectory($@"{localImageDirectory}\{taskName}\{calibName}");
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            try
            {
                string srcDir = $@"{localImageDirectory}\{taskName}\{oldCalibName}";
                if (Directory.Exists(srcDir))
                {
                    DirectoryUtils.Rename(srcDir, newCalibName);
                }
                else
                {
                    Directory.CreateDirectory(srcDir);
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        public string GetLocalImageDirectory()
        {
            return localImageDirectory;
        }

        public void SetLocalImageDirectory(string directory)
        {
            //if (localImageDirectory == directory) return;

            localImageDirectory = directory;
            try
            {
                Directory.CreateDirectory(localImageDirectory);
            }
            catch
            {
                basicRepo.SimulationPath = null;
                localImageDirectory = basicRepo.SimulationPath;
                Directory.CreateDirectory(localImageDirectory);
            }

            List<TaskInfoVO> taskInfos = taskRepo.GetTasks();
            foreach (TaskInfoVO taskInfo in taskInfos)
            {
                Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}");

                if (taskInfo.CameraAcquireImage == null) continue;

                foreach (AcquireImageInfoVO acqImage in taskInfo.CameraAcquireImage.AcquireImages)
                {
                    Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}\{acqImage.Name}");

                    if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
                    {
                        Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}\{acqImage.CheckerboardName}");
                    }

                    if (acqImage.HandEyeNames != null && acqImage.HandEyeNames.Count > 0)
                    {
                        foreach (string handeyeName in acqImage.HandEyeNames)
                            Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}\{handeyeName}");
                    }

                    if (!string.IsNullOrEmpty(acqImage.JointName))
                    {
                        Directory.CreateDirectory($@"{localImageDirectory}\{taskInfo.Name}\{acqImage.JointName}");
                    }
                }
            }
        }
    }
}
