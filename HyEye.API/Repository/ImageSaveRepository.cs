using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface IImageSaveRepository
    {
        event Action AfterSave;

        bool LowestPriority { get; set; }

        #region 存图扫描
        /// <summary>
        /// 扫描间隔时间
        /// 单位：分钟 默认：1分钟
        /// </summary>
        int ScanTime { get; set; }

        /// <summary>
        /// 剩余空间大小
        /// 单位：GB 默认：10GB
        /// </summary>
        int MinSpace { get; set; }

        /// <summary>
        /// 存图缓存限制张数
        /// 单位：张 默认：100张
        /// </summary>
        int CacheNum { get; set; }
        #endregion

        /// <summary>
        /// 图像保存的根目录
        /// </summary>
        string Root { get; set; }

        SubDireOfSaveImage SubDirectory { get; set; }

        /// <summary>
        /// 使用 OpenCV 保存图片
        /// </summary>
        bool SaveByOpenCV { get; set; }

        /// <summary>
        /// 图像保存配置
        /// </summary>
        List<ImageSaveInfoVO> GetSaveInfos();

        /// <summary>
        /// 图像删除信息
        /// </summary>
        ImageDeleteInfoVO GetDeleteInfo();

        void SetInfo(List<ImageSaveInfoVO> saveInfos, ImageDeleteInfoVO deleteInfo);

        /// <summary>
        /// 保存配置
        /// </summary>
        void Save();

        void SaveForImgNoAfterSave();
    }

    public class ImageSaveRepository : IImageSaveRepository
    {
        public event Action AfterSave;

        readonly IMapper mapper;
        readonly IGLog log;

        readonly ImageConfig config;

        public ImageSaveRepository(
            ITaskRepository taskRepo,
            IMapper mapper,
            IGLog log)
        {
            this.mapper = mapper;
            this.log = log;

            config = ApiConfig.ImageConfig;
            if (config.SaveInfos == null || config.SaveInfos.Count == 0)
            {
                config.SaveInfos = taskRepo.GetTasks().Select(t => new ImageSaveInfo { TaskName = t.Name }).ToList();
            }

            taskRepo.TaskAdd += TaskRepo_TaskAdd;
            taskRepo.TaskRename += TaskRepo_TaskRename;
            taskRepo.TaskDelete += TaskRepo_TaskDelete;
            taskRepo.AfterSave += TaskRepo_AfterSave;
        }

        bool changedToken = false;

        #region Task 事件

        private void TaskRepo_TaskAdd(TaskInfoVO taskInfo)
        {
            config.SaveInfos.Add(new ImageSaveInfo { TaskName = taskInfo.Name });

            changedToken = true;
        }

        private void TaskRepo_TaskRename(string oldName, string newName)
        {
            ImageSaveInfo saveInfo = config.SaveInfos.FirstOrDefault(a => a.TaskName == oldName);
            if (saveInfo != null)
            {
                saveInfo.TaskName = newName;

                changedToken = true;
            }
        }

        private void TaskRepo_TaskDelete(string name)
        {
            config.SaveInfos.Remove(a => a.TaskName == name);

            changedToken = true;
        }

        private void TaskRepo_AfterSave()
        {
            if (changedToken)
            {
                changedToken = false;

                Save();
            }
        }

        #endregion

        public bool LowestPriority
        {
            get { return config.LowestPriority; }
            set
            {
                if (config.LowestPriority != value)
                {
                    config.LowestPriority = value;
                    changedToken = true;
                }
            }
        }

        public int ScanTime
        {
            get { return config.ScanTime; }
            set { config.ScanTime = value; }
        }
        public int MinSpace
        {
            get { return config.MinSpace; }
            set { config.MinSpace = value; }
        }
        public int CacheNum
        {
            get { return config.CacheNum; }
            set { config.CacheNum = value; }
        }

        public string Root
        {
            get { return config.Root; }
            set
            {
                if (config.Root != value)
                {
                    config.Root = value;
                    changedToken = true;
                }
            }
        }

        public SubDireOfSaveImage SubDirectory
        {
            get { return config.SubDirectory; }
            set
            {
                if (config.SubDirectory != value)
                {
                    config.SubDirectory = value;
                    changedToken = true;
                }
            }
        }

        public bool SaveByOpenCV
        {
            get { return config.SaveByOpenCV; }
            set { config.SaveByOpenCV = value; }
        }

        public List<ImageSaveInfoVO> GetSaveInfos()
        {
            return mapper.Map<List<ImageSaveInfoVO>>(config.SaveInfos);
        }

        public ImageDeleteInfoVO GetDeleteInfo()
        {
            return mapper.Map<ImageDeleteInfoVO>(config.DeleteInfo);
        }

        public void SetInfo(List<ImageSaveInfoVO> saveInfos, ImageDeleteInfoVO deleteInfo)
        {
            config.SaveInfos = mapper.Map<List<ImageSaveInfo>>(saveInfos);
            config.DeleteInfo = mapper.Map<ImageDeleteInfo>(deleteInfo);

            changedToken = true;
        }

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(config);

                log.Info(new ApiLogMessage("图像设置", null, A_Save, R_Success));

                AfterSave?.Invoke();
            }
        }

        public void SaveForImgNoAfterSave()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(config);

                log.Info(new ApiLogMessage("图像设置", null, A_Save, R_Success));
            }
        }

    }
}
