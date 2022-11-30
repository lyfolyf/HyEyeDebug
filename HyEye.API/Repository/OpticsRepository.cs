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
    public interface IOpticsRepository
    {
        event Action<OpticsInfoVO> OpticsInfoChanged;

        //update by LuoDian @ 20211215 添加一个子料号，用于获取子料号下的光源和相机参数
        OpticsInfoVO GetOptics(string taskName, string acqName, string calibName, string subName);

        void DeleteOpticsByAcqImage(string taskName, string acqImageName);

        void DeleteOpticsByCalibration(string taskName, string calibName);

        void AddOrUpdate(OpticsInfoVO opticsInfoVO);

        //add by LuoDian @ 20211213 用于子料号的快速切换
        void AddNewOptics(string curSubName);
        void DeleteOpticsBySubName(string subName);
        Optics FindOpticsBySubName(string curSubName);

        void Save();
    }

    public class OpticsRepository : IOpticsRepository
    {
        public event Action<OpticsInfoVO> OpticsInfoChanged;

        //update by LuoDian @ 20211210 用于子料号的快速切换
        //readonly List<OpticsInfo> Optics;
        readonly List<Optics> Optics;

        readonly ILightControllerRepository controllerRepo;
        readonly ITaskRepository taskRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        //add by LuoDian @ 20211210 用于子料号的快速切换
        readonly IMaterialRepository materialRepo;

        bool changedToken = false;

        public OpticsRepository(
            ILightControllerRepository controllerRepo,
            ITaskRepository taskRepo,
            IMapper mapper,
            IMaterialRepository materialRepo,
            IGLog log)
        {
            Optics = ApiConfig.OpticsConfig.Optics;
            foreach(Optics optic in Optics)
            {
                if (optic != null && string.IsNullOrEmpty(optic.MaterialSubName))
                    optic.MaterialSubName = "default";
            }

            this.controllerRepo = controllerRepo;
            this.taskRepo = taskRepo;
            this.mapper = mapper;
            this.log = log;

            //add by LuoDian @ 20211210 用于子料号的快速切换
            this.materialRepo = materialRepo;

            this.taskRepo.TaskRename += TaskRepo_TaskRename;
            this.taskRepo.AcqImageDelete += TaskRepo_AcqImageDelete;
            this.taskRepo.AcqImageRename += TaskRepo_AcqImageRename;
            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;
            this.taskRepo.AfterSave += TaskRepo_TaskSave;

            this.controllerRepo.ControllerDelete += ControllerRepo_LightControllerDelete;
            this.controllerRepo.ControllerChanged += ControllerRepo_ControllerChanged;
            this.controllerRepo.AfterSave += ControllerRepo_AfterSave;
        }

        #region Task 事件

        private void TaskRepo_TaskRename(string oldTaskName, string newTaskName)
        {
            if (oldTaskName == newTaskName) return;

            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                foreach (OpticsInfo opticsInfo in optics.OpticsInfo)
                {
                    if (opticsInfo.TaskName == oldTaskName)
                    {
                        opticsInfo.TaskName = newTaskName;
                    }
                }
            }
            
            changedToken = true;
        }

        private void TaskRepo_AcqImageDelete(string taskName, string acqImageName)
        {
            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                OpticsInfo _optics = optics.OpticsInfo.FirstOrDefault(a => a.TaskName == taskName && a.AcquireImageName == acqImageName);
                if (_optics != null)
                {
                    optics.OpticsInfo.Remove(_optics);

                    log.Info(new ApiLogMessage(taskName, acqImageName, A_Delete, R_Success, "删除光学设置"));

                    changedToken = true;
                }
            }
        }

        private void TaskRepo_AcqImageRename(string taskName, string oldAcqImageName, string newAcqImageName)
        {
            if (oldAcqImageName == newAcqImageName) return;

            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                OpticsInfo _optics = optics.OpticsInfo.FirstOrDefault(a => a.TaskName == taskName && a.AcquireImageName == oldAcqImageName);

                if (_optics != null)
                {
                    _optics.AcquireImageName = newAcqImageName;
                    changedToken = true;
                }
            }
        }

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                OpticsInfo _optics = optics.OpticsInfo.FirstOrDefault(a => a.TaskName == taskName && a.CalibrationName == calibName);
                if (_optics != null)
                {
                    optics.OpticsInfo.Remove(_optics);

                    log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, "删除光学设置"));

                    changedToken = true;
                }
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                OpticsInfo _optics = optics.OpticsInfo.FirstOrDefault(a => a.TaskName == taskName && a.CalibrationName == oldCalibName);
                if (_optics != null)
                {
                    _optics.CalibrationName = newCalibName;

                    changedToken = true;
                }
            }
        }

        private void TaskRepo_TaskSave()
        {
            Save();
        }

        #endregion

        #region 光源控制器

        private void ControllerRepo_LightControllerDelete(string controllerName)
        {
            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                foreach (OpticsInfo opticsInfo in optics.OpticsInfo)
                {
                    for (int i = 0; i < opticsInfo.LightControllerValues.Count;)
                    {
                        if (opticsInfo.LightControllerValues[i].LightControllerName == controllerName)
                        {
                            opticsInfo.LightControllerValues.RemoveAt(i);

                            changedToken = true;

                            log.Info(new ApiLogMessage(opticsInfo.TaskName, opticsInfo.CalibrationName ?? opticsInfo.AcquireImageName,
                                A_Update, R_Success, $"光学配置中删除控制器[{controllerName}]"));
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }
        }

        private void ControllerRepo_ControllerChanged(string controllerName, LightControllerInfoVO newControllerInfo)
        {
            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            foreach (Optics optics in Optics)
            {
                foreach (OpticsInfo opticsInfo in optics.OpticsInfo)
                {
                    foreach (LightControllerValueInfo controllerValue in opticsInfo.LightControllerValues)
                    {
                        if (controllerValue.LightControllerName == controllerName)
                        {
                            // 修改光源控制器信息，这里要修改名称，并判断通道 Index 是否大于通道数

                            if (controllerValue.LightControllerName != newControllerInfo.Name)
                            {
                                controllerValue.LightControllerName = newControllerInfo.Name;

                                changedToken = true;
                            }

                            if (controllerValue.ChannelValues.RemoveAll(a => a.ChannelIndex > newControllerInfo.ChannelCount) > 0)
                            {
                                changedToken = true;
                            }
                        }
                    }
                }
            }
        }

        private void ControllerRepo_AfterSave()
        {
            Save();
        }

        #endregion

        //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
        public void DeleteOpticsByAcqImage(string taskName, string acqImageName)
        {
            Optics opticsTemp = Optics.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (opticsTemp == null)
                return;

            opticsTemp.OpticsInfo.Remove(a => a.TaskName == taskName && a.AcquireImageName == acqImageName);
        }

        //update by LuoDian @ 20211215 添加一个子料号，用于获取子料号下的光源和相机参数
        public OpticsInfoVO GetOptics(string taskName, string acqName, string calibName, string subName)
        {
            if (subName == null)
                return null;

            Optics optics = Optics.Find(a => a.MaterialSubName == subName);
            if (optics == null)
                optics = new Optics { MaterialSubName = materialRepo.CurSubName, OpticsInfo = new List<OpticsInfo>() };

            //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
            OpticsInfo opticsInfo = optics.OpticsInfo.FirstOrDefault(a => a.TaskName == taskName && a.AcquireImageName == acqName
                && a.CalibrationName == calibName);

            return mapper.Map<OpticsInfoVO>(opticsInfo);
        }

        //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
        public void DeleteOpticsByCalibration(string taskName, string calibName)
        {
            Optics opticsTemp = Optics.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (opticsTemp == null)
                return;

            opticsTemp.OpticsInfo.Remove(a => a.TaskName == taskName && a.CalibrationName == calibName);
        }

        //update by LuoDian @ 20211210 添加了一层List<Optics>，用于子料号的快速切换
        public void AddOrUpdate(OpticsInfoVO opticsInfoVO)
        {
            OpticsInfo opticsInfo = mapper.Map<OpticsInfo>(opticsInfoVO);

            int index = -1;
            Optics optics = Optics.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (optics != null && optics.OpticsInfo != null && optics.OpticsInfo.Count > 0)
                index = optics.OpticsInfo.FindIndex(a => a.TaskName == opticsInfo.TaskName && a.AcquireImageName == opticsInfo.AcquireImageName);
            else
                optics = new Optics { MaterialSubName = materialRepo.CurSubName, OpticsInfo = new List<OpticsInfo>() };

            if (index == -1)
            {
                optics.OpticsInfo.Add(opticsInfo);

                log.Info(new ApiLogMessage(opticsInfo.TaskName, opticsInfo.CalibrationName ?? opticsInfo.AcquireImageName, A_Add, R_Success, "新增光学设置"));
            }
            else
            {
                optics.OpticsInfo[index] = opticsInfo;

                log.Info(new ApiLogMessage(opticsInfo.TaskName, opticsInfo.CalibrationName ?? opticsInfo.AcquireImageName, A_Update, R_Success, "修改光学设置"));
            }

            changedToken = true;

            Optics opticsTemp = Optics.Find(a => a.MaterialSubName == materialRepo.CurSubName);
            if (opticsTemp == null)
                Optics.Add(optics);
            else
                opticsTemp.OpticsInfo = optics.OpticsInfo;

            OpticsInfoChanged?.Invoke(opticsInfoVO);
        }

        public void Save()
        {
            if (changedToken)
            {
                changedToken = false;

                ApiConfig.Save(ApiConfig.OpticsConfig);

                log.Info(new ApiLogMessage("光学设置", null, A_Save, R_Success));
            }

            // 这里是怕光源控制器没保存
            controllerRepo.Save();
        }

        //add by LuoDian @ 20211210 用于子料号的快速切换
        public void AddNewOptics(string curSubName)
        {
            Optics optics;
            if (Optics.Count > 0)
                optics = Optics[0].Clone();
            else
                optics = new Optics { MaterialSubName = curSubName, OpticsInfo = new List<OpticsInfo>() };
            optics.MaterialSubName = curSubName;
            Optics.Add(optics);

            changedToken = true;
        }

        //add by LuoDian @ 20211210 用于子料号的快速切换
        public void DeleteOpticsBySubName(string subName)
        {
            Optics.Remove(a => a.MaterialSubName == subName);
            changedToken = true;
        }

        //add by LuoDian @ 20211215 用于子料号的快速切换
        public Optics FindOpticsBySubName(string curSubName)
        {
            return Optics.Find(a => a.MaterialSubName == curSubName);
        }
    }
}
