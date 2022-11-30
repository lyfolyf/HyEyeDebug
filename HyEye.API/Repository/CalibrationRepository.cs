using AutoMapper;
using GL.Kit.Log;
using HyEye.API.Config;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.ApiAction;

namespace HyEye.API.Repository
{
    public interface ICalibrationRepository
    {
        List<CalibrationInfoVO> GetCalibrations();

        CalibrationInfoVO GetCalibration(string calibName);

        bool ExistsCalibrationName(string calibName);

        void AddCalibration(CalibrationInfoVO calibInfo);

        bool DeleteCalibration(string taskName, string calibName);

        void QuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName);

        void CancelQuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName);

        void SetHandEyeParams(string calibName, HandEyeInfoVO handEyeInfo);

        void SetHandEyePatternMode(string calibName, bool pmAlignOrToolBlock);

        void SetJointParams(string calibName, int pointCount, JointInfoVO jointInfo);

        void RenameTask(string oldName, string newName);

        void RenameCalibration(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName);

        void Save();
    }

    public class CalibrationRepository : ICalibrationRepository
    {
        readonly List<CalibrationInfo> calibInfos;

        readonly IPathRepository pathRepo;
        readonly IBasicRepository basicRepo;
        readonly IMapper mapper;
        readonly IGLog log;

        public CalibrationRepository(
            IPathRepository pathRepo,
            IBasicRepository basicRepo,
            IMapper mapper,
            IGLog log)
        {
            this.pathRepo = pathRepo;
            this.basicRepo = basicRepo;
            this.mapper = mapper;
            this.log = log;

            calibInfos = ApiConfig.CalibarationConfig.Calibrations;
        }

        public List<CalibrationInfoVO> GetCalibrations()
        {
            return mapper.Map<List<CalibrationInfoVO>>(calibInfos);
        }

        public CalibrationInfoVO GetCalibration(string calibName)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == calibName);

            return mapper.Map<CalibrationInfoVO>(calibInfo);
        }

        public bool ExistsCalibrationName(string calibName)
        {
            return calibInfos.Any(a => a.Name == calibName);
        }

        public void AddCalibration(CalibrationInfoVO calibInfo)
        {
            if (ExistsCalibrationName(calibInfo.Name))
            {
                log.Error(new ApiLogMessage(calibInfo.TaskName, calibInfo.Name, A_Add, R_Fail, "名称已存在"));
                throw new ApiException("新增标定失败，名称已存在");
            }

            CalibrationInfo calibrationInof = mapper.Map<CalibrationInfo>(calibInfo);
            calibInfos.Add(calibrationInof);

            log.Info(new ApiLogMessage(calibInfo.TaskName, calibInfo.Name, A_Add, R_Success, "新增标定"));
        }

        public bool DeleteCalibration(string taskName, string calibName)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == calibName);

            if (calibInfo == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, A_Delete, R_Fail, "标定未找到"));
                return false;
            }

            calibInfos.Remove(calibInfo);
            log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, "删除标定"));

            if (basicRepo.DeleteVPP)
            {
                string verifyVpp = pathRepo.GetCalibVerifyVppPath(calibName);
                if (File.Exists(verifyVpp))
                {
                    File.Delete(verifyVpp);
                    log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, $"删除标定验证 VPP：{verifyVpp}"));
                }

                if (calibInfo.CalibrationType == CalibrationType.HandEye)
                {
                    string patternVpp = pathRepo.GetCalibPatternPath(calibName);
                    if (File.Exists(patternVpp))
                    {
                        File.Delete(patternVpp);
                        log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, $"删除模板 VPP：{patternVpp}"));
                    }

                    string circleVpp = pathRepo.GetCalibCircleVppPath(calibName);
                    if (File.Exists(circleVpp))
                    {
                        File.Delete(circleVpp);
                        log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, $"删除旋转中心 VPP：{circleVpp}"));
                    }
                }
            }

            return true;
        }

        public void QuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (calibType != CalibrationType.Joint) return;

            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == calibName);

            calibInfo.JointInfo.Slaves.Add(new TaskAcqImage { TaskName = taskName, AcqImageName = acqImageName });
        }

        public void CancelQuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            if (calibType != CalibrationType.Joint) return;

            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == calibName);

            calibInfo.JointInfo.Slaves.Remove(a => a.TaskName == taskName && a.AcqImageName == acqImageName);
        }

        public void SetHandEyeParams(string calibName, HandEyeInfoVO handEyeInfo)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(
                a => a.Name == calibName && a.CalibrationType == CalibrationType.HandEye);

            if (calibInfo == null)
            {
                log.Error(new ApiLogMessage(calibInfo.TaskName, calibName, A_SetParams, R_Fail, "标定未找到"));
                throw new ApiException("设置标定参数失败，标定未找到");
            }

            HandEyeInfo handEye = mapper.Map<HandEyeInfo>(handEyeInfo);
            calibInfo.HandEyeInfo = handEye;

            log.Info(new ApiLogMessage(calibInfo.TaskName, calibName, A_SetParams, R_Success, "设置标定参数"));
        }

        public void SetHandEyePatternMode(string calibName, bool pmAlignOrToolBlock)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == calibName);

            if (calibInfo == null)
            {
                log.Error(new ApiLogMessage(calibInfo.TaskName, calibName, A_SetParams, R_Fail, "标定未找到"));
                throw new ApiException("设置标定参数失败，标定未找到");
            }

            if (calibInfo.CalibrationType == CalibrationType.HandEye)
                calibInfo.HandEyeInfo.PMAlignOrToolBlock = pmAlignOrToolBlock;
            else if (calibInfo.CalibrationType == CalibrationType.HandEyeSingle)
                calibInfo.HandEyeSingleInfo.PMAlignOrToolBlock = pmAlignOrToolBlock;
        }

        public void SetJointParams(string calibName, int pointCount, JointInfoVO jointInfo)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(
                a => a.Name == calibName && a.CalibrationType == CalibrationType.Joint);

            if (calibInfo == null)
            {
                log.Error(new ApiLogMessage(calibInfo.TaskName, calibName, A_SetParams, R_Fail, "标定未找到"));
                throw new ApiException("设置标定参数失败，标定未找到");
            }

            calibInfo.JointInfo.PointCount = pointCount;
            calibInfo.JointInfo = mapper.Map<JointInfo>(jointInfo);

            log.Info(new ApiLogMessage(calibInfo.TaskName, calibName, A_SetParams, R_Success, "设置标定参数"));
        }

        public void RenameTask(string oldTaskName, string newTaskName)
        {
            foreach (CalibrationInfo calibInfo in calibInfos.Where(a => a.TaskName == oldTaskName))
            {
                calibInfo.TaskName = newTaskName;
            }
        }

        public void RenameCalibration(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            CalibrationInfo calibInfo = calibInfos.FirstOrDefault(a => a.Name == oldCalibName);

            if (calibInfo != null)
            {
                calibInfo.Name = newCalibName;

                if (calibType == CalibrationType.HandEye && calibInfo.HandEyeInfo != null)
                {
                    try
                    {
                        string patternPath = pathRepo.GetCalibPatternPath(oldCalibName);
                        if (File.Exists(patternPath))
                        {
                            string newPatternFilename = pathRepo.GetCalibPatternFilename(newCalibName);
                            FileUtils.Rename(patternPath, newPatternFilename);
                            log.Info(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Success, $"重命名标定模板 VPP：[{patternPath}] => [{newPatternFilename}]"));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Fail, "重命名标定模板 VPP 失败：" + e.Message));
                    }

                    try
                    {
                        string circlePath = pathRepo.GetCalibCircleVppPath(oldCalibName);
                        if (File.Exists(circlePath))
                        {
                            string newCircleFilename = pathRepo.GetCalibCircleVppFilename(newCalibName);
                            FileUtils.Rename(circlePath, newCircleFilename);
                            log.Info(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Success, $"重命名旋转中心 VPP：[{circlePath}] => [{newCircleFilename}]"));
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Fail, "重命名旋转中心 VPP 失败：" + e.Message));
                    }
                }
            }
        }

        public void Save()
        {
            ApiConfig.Save(ApiConfig.CalibarationConfig);

            log.Info(new ApiLogMessage("标定设置", null, A_Save, R_Success));
        }
    }
}
