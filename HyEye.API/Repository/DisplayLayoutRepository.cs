using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using VisionSDK;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IDisplayLayoutRepository
    {
        event Action LayoutChanged;

        int RowCount { get; set; }

        int ColumnCount { get; set; }

        bool AutoSize { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        DisplayType Type { get; set; }

        /// <summary>
        /// 是否显示结果图
        /// </summary>
        bool ShowRetImage { get; set; }

        List<DisplayLayoutInfoVO> Reset();

        void SetLayout(List<DisplayLayoutInfoVO> displayLayoutInfos);

        DisplayLayoutInfoVO GetLayout(string taskName, string acqImageName);

        void Save();
    }

    public class DisplayLayoutRepository : IDisplayLayoutRepository
    {
        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;
        readonly DisplayLayoutConfig config;

        public event Action LayoutChanged;

        public DisplayLayoutRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            config = ApiConfig.DisplayLayoutConfig;

            Init();

            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.AcqImageAdd += TaskRepo_AcqImageAdd;
            taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            taskRepo.AfterSave += TaskRepo_AfterSave;
        }

        int index = 0;

        void Init()
        {
            if (config.DisplayLayouts.Count == 0)
            {
                var taskInfos = taskRepo.GetTasks();

                foreach (TaskInfoVO task in taskInfos)
                {
                    if (task.CameraAcquireImage != null)
                    {
                        foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                        {
                            DisplayLayoutInfo displayLayout = new DisplayLayoutInfo
                            {
                                TaskName = task.Name,
                                AcquireImageName = acqImage.Name,
                                Index = -1
                            };

                            config.DisplayLayouts.Add(displayLayout);
                        }
                    }
                }
            }
            else
            {
                index = config.DisplayLayouts.Select(d => d.Index).Max();
                if (index == -1)
                    index = 0;
            }
        }

        bool changedToken = false;

        #region Task 事件

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            foreach (DisplayLayoutInfo displayLayout in config.DisplayLayouts)
            {
                if (displayLayout.TaskName == oldName)
                    displayLayout.TaskName = newName;
            }

            changedToken = true;
        }

        private void TaskRepo_AcqImageAdd(string taskName, string acqImageName)
        {
            DisplayLayoutInfo displayLayout = new DisplayLayoutInfo
            {
                TaskName = taskName,
                AcquireImageName = acqImageName,
                Index = -1
            };

            config.DisplayLayouts.Add(displayLayout);

            LayoutChanged?.Invoke();

            changedToken = true;
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            config.DisplayLayouts.Remove(a => a.TaskName == taskName && a.AcquireImageName == acqImageName);

            LayoutChanged?.Invoke();

            changedToken = true;
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            foreach (DisplayLayoutInfo displayLayout in config.DisplayLayouts)
            {
                if (displayLayout.TaskName == taskName && displayLayout.AcquireImageName == oldAcqImageName)
                    displayLayout.AcquireImageName = newAcqImageName;
            }

            changedToken = true;
        }

        private void TaskRepo_AfterSave()
        {
            Save();
        }

        #endregion

        #region 属性

        public int RowCount
        {
            get { return config.RowCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(RowCount));

                if (config.RowCount != value)
                {
                    config.RowCount = value;
                    changedToken = true;
                }
            }
        }

        public int ColumnCount
        {
            get { return config.ColumnCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(ColumnCount));

                if (config.ColumnCount != value)
                {
                    config.ColumnCount = value;
                    changedToken = true;
                }
            }
        }

        public bool AutoSize
        {
            get { return config.AutoSize; }
            set
            {
                if (config.AutoSize != value)
                {
                    config.AutoSize = value;
                    changedToken = true;
                }
            }
        }

        public int Width
        {
            get { return config.Width; }
            set
            {
                if (value < 40)
                    throw new ArgumentOutOfRangeException(nameof(Width));

                if (config.Width != value)
                {
                    config.Width = value;
                    changedToken = true;
                }
            }
        }

        public int Height
        {
            get { return config.Height; }
            set
            {
                if (value < 30)
                    throw new ArgumentOutOfRangeException(nameof(Height));

                if (config.Height != value)
                {
                    config.Height = value;
                    changedToken = true;
                }
            }
        }

        public DisplayType Type
        {
            get { return config.Type; }
            set
            {
                if (config.Type != value)
                {
                    config.Type = value;
                    changedToken = true;
                }
            }
        }

        public bool ShowRetImage
        {
            get { return config.ShowRetImage; }
            set
            {
                if (config.ShowRetImage != value)
                {
                    config.ShowRetImage = value;
                    changedToken = true;
                }
            }
        }

        #endregion

        public DisplayLayoutInfoVO GetLayout(string taskName, string acqImageName)
        {
            DisplayLayoutInfo layoutInfo = config.DisplayLayouts.FirstOrDefault(a => a.TaskName == taskName && a.AcquireImageName == acqImageName);
            return mapper.Map<DisplayLayoutInfoVO>(layoutInfo);
        }

        public List<DisplayLayoutInfoVO> Reset()
        {
            Dictionary<string, DisplayLayoutInfo> cache = config.DisplayLayouts.ToDictionary(k => $"{k.TaskName}/{k.AcquireImageName}");

            config.DisplayLayouts.Clear();

            int index = 0;

            var taskInfos = taskRepo.GetTasks();

            foreach (TaskInfoVO task in taskInfos)
            {
                if (task.CameraAcquireImage != null)
                {
                    foreach (AcquireImageInfoVO acqImage in task.CameraAcquireImage.AcquireImages)
                    {
                        string key = $"{task.Name}/{acqImage.Name}";

                        DisplayLayoutInfo displayLayout;
                        if (cache.ContainsKey(key))
                            displayLayout = cache[key];
                        else
                            displayLayout = new DisplayLayoutInfo
                            {
                                TaskName = task.Name,
                                AcquireImageName = acqImage.Name
                            };

                        if (displayLayout.Index != -1)
                            displayLayout.Index = index++;

                        config.DisplayLayouts.Add(displayLayout);
                    }
                }
            }

            return mapper.Map<List<DisplayLayoutInfoVO>>(config.DisplayLayouts);
        }

        public void SetLayout(List<DisplayLayoutInfoVO> displayLayoutInfos)
        {
            config.DisplayLayouts = mapper.Map<List<DisplayLayoutInfo>>(displayLayoutInfos);

            changedToken = true;
        }

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(ApiConfig.DisplayLayoutConfig);

                log.Info(new ApiLogMessage("界面布局", null, A_Save, R_Success));

                LayoutChanged?.Invoke();
            }
        }
    }
}
