using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface ICalibrationVerifyRepository
    {
        T GetVerifyInfo<T>(string calibName) where T : CalibrationVerifyInfoVO;

        void SetVerifyInfo(CheckerboardVerifyInfoVO verifyInfo);

        void SetVerifyInfo(HandeyeVerifyInfoVO verifyInfo);

        void Save();
    }

    public class CalibrationVerifyRepository : ICalibrationVerifyRepository
    {
        readonly ITaskRepository taskRepo;
        readonly IPathRepository pathRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        readonly List<CalibrationVerifyInfo> calibrationVerifys;

        public CalibrationVerifyRepository(
            ITaskRepository taskRepo,
            IPathRepository pathRepo,
            IMapper mapper,
            IGLog log)
        {
            this.taskRepo = taskRepo;
            this.pathRepo = pathRepo;

            this.mapper = mapper;
            this.log = log;

            calibrationVerifys = ApiConfig.CalibrationVerifyConfig.CalibrationVerifys;

            this.taskRepo.CalibDelete += TaskRepo_CalibDelete;
            this.taskRepo.CalibRename += TaskRepo_CalibRename;
        }

        bool changedToken = false;

        private void TaskRepo_CalibDelete(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            CalibrationVerifyInfo verifyInfo = calibrationVerifys.FirstOrDefault(a => a.CalibrationName == calibName);
            if (verifyInfo != null)
            {
                calibrationVerifys.Remove(verifyInfo);

                changedToken = true;
            }
        }

        private void TaskRepo_CalibRename(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            CalibrationVerifyInfo verifyInfo = calibrationVerifys.FirstOrDefault(a => a.CalibrationName == oldCalibName);

            if (verifyInfo == null) return;

            verifyInfo.CalibrationName = newCalibName;

            changedToken = true;

            string vppPath = pathRepo.GetCalibVerifyVppPath(oldCalibName);
            try
            {
                if (File.Exists(vppPath))
                {
                    string newVppPath = pathRepo.GetCalibVerifyVppFilename(newCalibName);
                    FileUtils.Rename(vppPath, newVppPath);
                    log.Info(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Success, $"重命名标定验证 VPP：[{vppPath}] => [{newVppPath}]"));
                }
            }
            catch { }
        }

        public T GetVerifyInfo<T>(string calibName) where T : CalibrationVerifyInfoVO
        {
            CalibrationVerifyInfo verifyInfo = calibrationVerifys.FirstOrDefault(a => a.CalibrationName == calibName);

            return mapper.Map<T>(verifyInfo);
        }

        public void SetVerifyInfo(CheckerboardVerifyInfoVO verifyInfo)
        {
            CheckerboardVerifyInfo info = mapper.Map<CheckerboardVerifyInfo>(verifyInfo);

            int index = calibrationVerifys.FindIndex(a => a.CalibrationName == verifyInfo.CalibrationName);

            if (index == -1)
                calibrationVerifys.Add(info);
            else
                calibrationVerifys[index] = info;

            changedToken = true;
        }

        public void SetVerifyInfo(HandeyeVerifyInfoVO verifyInfo)
        {
            HandeyeVerifyInfo info = mapper.Map<HandeyeVerifyInfo>(verifyInfo);

            int index = calibrationVerifys.FindIndex(a => a.CalibrationName == verifyInfo.CalibrationName);

            if (index == -1)
                calibrationVerifys.Add(info);
            else
                calibrationVerifys[index] = info;

            changedToken = true;
        }

        public void Save()
        {
            if (changedToken)
            {
                ApiConfig.Save(ApiConfig.CalibrationVerifyConfig);

                log.Info(new ApiLogMessage("标定验证设置", null, A_Save, R_Success));

                changedToken = false;
            }
        }
    }
}
