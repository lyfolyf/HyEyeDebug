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
    public interface ITaskRepository
    {
        #region 事件

        event TaskInfoHandler TaskAdd;
        event RenameHandler TaskRename;
        event NameHandler TaskDelete;
        event TaskMoveHandler TaskMoveUp;
        event TaskMoveHandler TaskMoveDown;

        // taskName, cameraSN
        event Action<string, string> CameraChanged;

        event AcqImageNameHandler AcqImageAdd;
        event AcqImageNameHandler AcqImageDelete;
        event AcqImageRenameHandler AcqImageRename;
        event AcqImageMoveHandler AcqImageMoveUp;
        event AcqImageMoveHandler AcqImageMoveDown;

        // TaskRepository 中的标定事件和 CalibrationRepository 中的标定事件不可同时添加
        event TaskCalibrationHandler CalibAdd;
        event TaskCalibrationHandler CalibDelete;
        event CalibrationRenameHandler CalibRename;
        event TaskCalibrationHandler CalibQuote;
        event TaskCalibrationHandler CancelCalibQuote;

        event Action AfterSave;

        #endregion

        bool Check(string taskName);

        #region 任务

        List<TaskInfoVO> GetTasks();

        TaskInfoVO GetTaskByName(string taskName);

        void AddTask(string taskName, TaskType taskType);

        bool DeleteTask(string taskName);

        bool ExistsTaskName(string taskName);

        void RenameTask(string oldname, string newname);

        void SetTaskEnabled(string taskName, bool enable);

        void MoveUpTask(int index);

        void MoveDownTask(int index);

        #endregion

        #region 相机

        CameraAcquireImageInfoVO SetCamera(string taskName, CameraInfoVO camera, bool concurrent);

        void SetCamera(string taskName, CameraAcquireImageInfoVO cameraAcquireImage);

        #endregion

        #region 取像

        AcquireImageInfoVO GetAcqImage(string taskName, string acqImageName);

        int GetAcqImageIndex(string taskName, string acqImageName);

        int GetFrameIndex(string taskName, int frameNum);

        void AddAcqImage(string taskName, string acqImageName);

        bool DeleteAcqImage(string taskName, string acqImageName);

        bool ExistsAcqImage(string taskName, string acqImageName);

        void RenameAcqImage(string taskName, string oldname, string newname);

        void MoveUpAcqImage(string taskName, string acqImageName, int acqImageIndex);

        void MoveDownAcqImage(string taskName, string acqImageName, int acqImageIndex);

        #endregion

        #region 标定

        void AddCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName);

        bool DeleteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName);

        void RenameCalibration(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName);

        void QuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName);

        #endregion

        void Save();
    }

    public class TaskRepository : ITaskRepository
    {
        readonly ICameraRepository cameraRepo;
        readonly ICalibrationRepository calibRepo;
        readonly IPathRepository pathRepo;
        readonly IBasicRepository basicRepo;
        readonly IGLog log;
        readonly IMapper mapper;

        public event TaskInfoHandler TaskAdd;
        public event RenameHandler TaskRename;
        public event NameHandler TaskDelete;
        public event TaskMoveHandler TaskMoveUp;
        public event TaskMoveHandler TaskMoveDown;

        public event Action<string, string> CameraChanged;
        public event AcqImageNameHandler AcqImageAdd;
        public event AcqImageNameHandler AcqImageDelete;
        public event AcqImageRenameHandler AcqImageRename;
        public event AcqImageMoveHandler AcqImageMoveUp;
        public event AcqImageMoveHandler AcqImageMoveDown;

        public event TaskCalibrationHandler CalibAdd;
        public event TaskCalibrationHandler CalibDelete;
        public event CalibrationRenameHandler CalibRename;
        public event TaskCalibrationHandler CalibQuote;
        public event TaskCalibrationHandler CancelCalibQuote;
        public event Action AfterSave;

        readonly List<TaskInfo> tasks;

        public TaskRepository(
            ICameraRepository cameraRepo,
            ICalibrationRepository calibRepo,
            IPathRepository pathRepo,
            IBasicRepository basicRepo,
            IMapper mapper,
            IGLog log)
        {
            this.cameraRepo = cameraRepo;
            this.calibRepo = calibRepo;
            this.pathRepo = pathRepo;
            this.basicRepo = basicRepo;
            this.log = log;
            this.mapper = mapper;

            tasks = ApiConfig.TaskConfig.Tasks;

            // 这个属性是后加的，为了兼容以前的版本
            foreach (TaskInfo task in tasks)
            {
                task.Order = order++;
            }
        }

        int order = 0;

        public bool Check(string taskName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task.CameraAcquireImage == null)
            {
                log.Error(new ApiLogMessage(taskName, null, A_Check, R_Fail, "未设置相机"));
                return false;
            }
            return true;
        }

        #region 任务

        public List<TaskInfoVO> GetTasks()
        {
            return mapper.Map<List<TaskInfoVO>>(tasks);
        }

        public TaskInfoVO GetTaskByName(string taskName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);

            return mapper.Map<TaskInfoVO>(task);
        }

        public void AddTask(string taskName, TaskType taskType)
        {
            if (ExistsTaskName(taskName))
            {
                log.Error(new ApiLogMessage(taskName, null, A_Add, R_Fail, "任务名称已存在"));
                throw new ApiException("新增失败，任务名称已存在");
            }

            TaskInfo task = new TaskInfo
            {
                Name = taskName,
                Type = taskType,
                Order = order++
            };

            tasks.Add(task);
            log.Info(new ApiLogMessage(taskName, null, A_Add, R_Success, "新增任务"));

            TaskAdd?.Invoke(mapper.Map<TaskInfoVO>(task));
        }

        public bool DeleteTask(string taskName)
        {
            TaskInfo task = tasks.FirstOrDefault(t => t.Name == taskName);

            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, null, A_Delete, R_Fail, "未找到任务"));
                return false;
            }

            if (task.CameraAcquireImage != null)
            {
                foreach (AcquireImageInfo acqImage in task.CameraAcquireImage.AcquireImages.ToArray())
                {
                    DeleteAcqImage(task, acqImage);
                }
            }

            tasks.Remove(task);
            log.Info(new ApiLogMessage(taskName, null, A_Delete, R_Success, "删除任务"));

            if (basicRepo.DeleteVPP)
            {
                string vppPath = pathRepo.GetTaskVppPath(taskName, task.Type);
                if (File.Exists(vppPath))
                {
                    File.Delete(vppPath);
                    log.Info(new ApiLogMessage(taskName, null, A_Delete, R_Success, "删除任务 VPP：" + vppPath));
                }
            }

            TaskDelete?.Invoke(task.Name);

            return true;
        }

        public void RenameTask(string oldName, string newName)
        {
            if (oldName == newName) return;

            TaskInfo task = tasks.FirstOrDefault(a => a.Name == oldName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(oldName, null, A_Rename, R_Fail, "未找到任务"));
                throw new ApiException("重命名失败，未找到任务");
            }

            task.Name = newName;
            log.Info(new ApiLogMessage(oldName, null, A_Rename, R_Success, $"[{oldName}] => [{newName}]"));

            try
            {
                string vppPath = pathRepo.GetTaskVppPath(oldName, task.Type);
                if (File.Exists(vppPath))
                {
                    string newVppPath = pathRepo.GetTaskVppFilename(newName, task.Type);
                    FileUtils.Rename(vppPath, newVppPath);
                    log.Info(new ApiLogMessage(oldName, null, A_Rename, R_Success, $"重命名任务 VPP：[{vppPath}] => [{newVppPath}]"));
                }
            }
            catch (Exception e)
            {
                log.Error(new ApiLogMessage(oldName, null, A_Rename, R_Fail, "重命名任务 VPP 失败：" + e.Message));
            }

            calibRepo.RenameTask(oldName, newName);

            TaskRename?.Invoke(oldName, newName);
        }

        public bool ExistsTaskName(string taskName)
        {
            return tasks.Any(a => a.Name == taskName);
        }

        public void SetTaskEnabled(string taskName, bool enabled)
        {
            TaskInfo task = tasks.FirstOrDefault(t => t.Name == taskName);

            string action = enabled ? A_Enable : A_Disable;

            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, null, action, R_Fail, "未找到任务"));
                throw new ApiException(action + "失败，未找到任务");
            }

            if (task.Enabled != enabled)
            {
                task.Enabled = enabled;
                log.Info(new ApiLogMessage(taskName, null, action, R_Success, action + "任务"));
            }
        }

        public void MoveUpTask(int index)
        {
            if (index < 0 || index >= tasks.Count) return;

            if (index == 1) return;

            tasks.Exchange<TaskInfo>(index, index - 1);

            int i = tasks[index].Order;
            tasks[index].Order = tasks[index - 1].Order;
            tasks[index - 1].Order = i;

            log.Info(new ApiLogMessage(tasks[index - 1].Name, null, A_MoveUp, R_Success, "上移任务"));

            TaskMoveUp?.Invoke(tasks[index - 1].Name, tasks[index].Name);
        }

        public void MoveDownTask(int index)
        {
            if (index < 0 || index >= tasks.Count) return;

            if (index == tasks.Count - 1) return;

            tasks.Exchange<TaskInfo>(index, index + 1);

            int i = tasks[index].Order;
            tasks[index].Order = tasks[index + 1].Order;
            tasks[index + 1].Order = i;

            log.Info(new ApiLogMessage(tasks[index + 1].Name, null, A_MoveDown, R_Success, "下移任务"));

            TaskMoveDown?.Invoke(tasks[index + 1].Name, tasks[index].Name);
        }

        #endregion

        #region 相机

        public CameraAcquireImageInfoVO SetCamera(string taskName, CameraInfoVO camera, bool concurrent)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, null, A_SetCamera, R_Fail, "未找到任务"));
                throw new ApiException($"设置相机失败，未找到任务");
            }

            bool cameraChanged = task.CameraAcquireImage?.CameraSN != camera.SN;

            if (task.CameraAcquireImage != null)
            {
                task.CameraAcquireImage.CameraSN = camera.SN;
            }
            else
            {
                task.CameraAcquireImage = new CameraAcquireImageInfo
                {
                    CameraSN = camera.SN,
                };
            }

            log.Info(new ApiLogMessage(taskName, null, A_SetCamera, R_Success, $"设置相机 [{camera}]"));

            if (cameraChanged)
                CameraChanged?.Invoke(taskName, camera.SN);

            return mapper.Map<CameraAcquireImageInfoVO>(task.CameraAcquireImage);
        }

        public void SetCamera(string taskName, CameraAcquireImageInfoVO cameraAcquireImage)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, null, A_SetCamera, R_Fail, "未找到任务"));
                throw new ApiException($"设置相机失败，未找到任务");
            }

            if (task.CameraAcquireImage == null)
            {
                task.CameraAcquireImage = new CameraAcquireImageInfo();
            }

            bool cameraChanged = task.CameraAcquireImage.CameraSN != cameraAcquireImage.CameraSN;

            task.CameraAcquireImage.CameraSN = cameraAcquireImage.CameraSN;

            CameraInfoVO camera = cameraRepo.GetCameraInfo(cameraAcquireImage.CameraSN);

            log.Info(new ApiLogMessage(taskName, null, A_SetCamera, R_Success, $"设置相机 [{camera}]"));

            if (cameraChanged)
                CameraChanged?.Invoke(taskName, cameraAcquireImage.CameraSN);
        }

        #endregion

        #region 取像

        public AcquireImageInfoVO GetAcqImage(string taskName, string acqImageName)
        {
            AcquireImageInfo acqImage = tasks.FirstOrDefault(a => a.Name == taskName)
                ?.CameraAcquireImage
                ?.AcquireImages
                ?.FirstOrDefault(a => a.Name == acqImageName);

            return mapper.Map<AcquireImageInfoVO>(acqImage);
        }

        public int GetAcqImageIndex(string taskName, string acqImageName)
        {
            List<AcquireImageInfo> acqImages = tasks.FirstOrDefault(a => a.Name == taskName)?.CameraAcquireImage?.AcquireImages;
            if (acqImages == null)
                return -1;

            int index = 1;
            foreach (AcquireImageInfo acqImage in acqImages)
            {
                if (acqImage.Name == acqImageName)
                    return index;
                else
                    index++;
            }
            return -1;
        }

        /// <summary>
        /// 根据每个任务下的拍照数量和相机的FrameNum换算FrameIndex, 用来校验是否有图像丢失
        /// 目前仅支持一个任务一个相机的情况，一个任务多相机需框架调整后这里传入相机名称或序列号
        /// Added by Louis on Mar. 31 2022
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="frameNum">相机启动后输出的连续帧数量</param>
        /// <returns>换算后的相机的图像序号</returns>
        public int GetFrameIndex(string taskName, int frameNum)
        {
            List<AcquireImageInfo> acqImages = tasks.FirstOrDefault(a => a.Name == taskName)?.CameraAcquireImage?.AcquireImages;

            if (acqImages == null)
                return -1;

            int frameIndex = frameNum % acqImages.Count;
            if (frameIndex == 0)
                frameIndex = acqImages.Count;

            return frameIndex;
        }

        public void AddAcqImage(string taskName, string acqImageName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_Add, R_Fail, "未找到任务"));
                throw new ApiException($"新增失败，未找到任务");
            }

            if (task.CameraAcquireImage.AcquireImages.Exists(a => a.Name == acqImageName))
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_Add, R_Fail, "拍照名称已存在"));
                throw new ApiException($"新增失败，拍照名称已存在");
            }

            task.CameraAcquireImage.AcquireImages.Add(new AcquireImageInfo { Name = acqImageName });

            log.Info(new ApiLogMessage(taskName, acqImageName, A_Add, R_Success, "新增拍照"));

            AcqImageAdd?.Invoke(taskName, acqImageName);
        }

        public bool DeleteAcqImage(string taskName, string acqImageName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_Delete, R_Fail, "未找到任务"));
                return false;
            }

            AcquireImageInfo acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.Name == acqImageName);

            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_Delete, R_Fail, "未找到拍照"));
                return false;
            }
            else
            {
                DeleteAcqImage(task, acqImage);

                return true;
            }
        }

        void DeleteAcqImage(TaskInfo task, AcquireImageInfo acqImage)
        {
            if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
            {
                if (calibRepo.DeleteCalibration(task.Name, acqImage.CheckerboardName))
                {
                    calibrationChangedToken = true;
                    CalibDelete?.Invoke(task.Name, acqImage.Name, CalibrationType.Checkerboard, acqImage.CheckerboardName);
                }
            }

            if (acqImage.HandEyeNames != null && acqImage.HandEyeNames.Count > 0)
            {
                foreach (string handeyeName in acqImage.HandEyeNames)
                {
                    if (calibRepo.DeleteCalibration(task.Name, handeyeName))
                    {
                        calibrationChangedToken = true;
                        CalibDelete?.Invoke(task.Name, acqImage.Name, CalibrationType.HandEye, handeyeName);
                    }
                }
            }

            task.CameraAcquireImage.AcquireImages.Remove(acqImage);

            log.Info(new ApiLogMessage(task.Name, acqImage.Name, A_Delete, R_Success, "删除拍照"));

            AcqImageDelete?.Invoke(task.Name, acqImage.Name);
        }

        public void RenameAcqImage(string taskName, string oldname, string newname)
        {
            if (oldname == newname) return;

            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, oldname, A_Rename, R_Fail, "未找到任务"));
                throw new ApiException("重命名失败，未找到任务");
            }

            AcquireImageInfo acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.Name == oldname);

            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, oldname, A_Rename, R_Fail, "未找到拍照"));
                throw new ApiException($"重命名失败，未找到拍照");
            }
            else
            {
                acqImage.Name = newname;

                log.Info(new ApiLogMessage(taskName, oldname, A_Rename, R_Success, $"[{oldname}] => [{newname}]"));

                AcqImageRename?.Invoke(taskName, oldname, newname);
            }
        }

        public bool ExistsAcqImage(string taskName, string acqImageName)
        {
            return tasks.FirstOrDefault(a => a.Name == taskName)
                ?.CameraAcquireImage
                ?.AcquireImages
                ?.Exists(a => a.Name == acqImageName) == true;
        }

        public void MoveUpAcqImage(string taskName, string acqImageName, int acqImageIndex)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_MoveUp, R_Fail, "未找到任务"));
                throw new ApiException("上移拍照失败，未找到任务");
            }

            if (acqImageIndex < 0 || acqImageIndex >= task.CameraAcquireImage.AcquireImages.Count) return;

            if (acqImageIndex == 0) return;

            task.CameraAcquireImage.AcquireImages.Exchange<AcquireImageInfo>(acqImageIndex, acqImageIndex - 1);

            log.Info(new ApiLogMessage(taskName, acqImageName, A_MoveUp, R_Success, "上移拍照"));

            AcqImageMoveUp?.Invoke(taskName, acqImageName, task.CameraAcquireImage.AcquireImages[acqImageIndex].Name);
        }

        public void MoveDownAcqImage(string taskName, string acqImageName, int acqImageIndex)
        {
            if (acqImageIndex < 0) return;

            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, acqImageName, A_MoveDown, R_Fail, "未找到任务"));
                throw new ApiException("下移拍照失败，未找到任务");
            }

            if (acqImageIndex < 0 || acqImageIndex >= task.CameraAcquireImage.AcquireImages.Count) return;

            if (acqImageIndex == task.CameraAcquireImage.AcquireImages.Count - 1) return;

            task.CameraAcquireImage.AcquireImages.Exchange<AcquireImageInfo>(acqImageIndex, acqImageIndex + 1);

            log.Info(new ApiLogMessage(taskName, acqImageName, A_MoveDown, R_Success, "下移拍照"));

            AcqImageMoveDown?.Invoke(taskName, acqImageName, task.CameraAcquireImage.AcquireImages[acqImageIndex].Name);
        }

        #endregion

        #region 标定

        bool calibrationChangedToken = false;

        public void AddCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, A_Add, R_Fail, "未找到任务"));
                throw new ApiException("新增标定失败，未找到任务");
            }

            AcquireImageInfo acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.Name == acqImageName);

            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, A_Add, R_Fail, "未找到拍照"));
                throw new ApiException($"新增标定失败，未找到拍照");
            }

            if (calibType == CalibrationType.Checkerboard)
            {
                if (!string.IsNullOrEmpty(acqImage.CheckerboardName))
                {
                    log.Error(new ApiLogMessage(taskName, calibName, A_Add, R_Fail, $"[{acqImage.Name}]已有 Checkerboard 标定"));
                    throw new ApiException($"新增标定失败，[{acqImage.Name}]已有 Checkerboard 标定");
                }

                acqImage.CheckerboardName = calibName;

                log.Info(new ApiLogMessage(taskName, calibName, A_Add, R_Success, $"[{acqImage.Name}]新增 Checkerboard 标定"));
            }
            else if (calibType == CalibrationType.HandEye)
            {
                if (acqImage.HandEyeNames == null)
                    acqImage.HandEyeNames = new List<string>();

                acqImage.HandEyeNames.Add(calibName);

                log.Info(new ApiLogMessage(taskName, calibName, A_Add, R_Success, $"[{acqImage.Name}]新增 HandEye（多点） 标定"));
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                acqImage.HandEyeSingleName = calibName;

                log.Info(new ApiLogMessage(taskName, calibName, A_Add, R_Success, $"[{acqImage.Name}]新增 HandEye（单点） 标定"));
            }
            else if (calibType == CalibrationType.Joint)
            {
                if (!string.IsNullOrEmpty(acqImage.JointName))
                {
                    log.Error(new ApiLogMessage(taskName, calibName, A_Add, R_Fail, $"[{acqImage.Name}]已有联合标定"));
                    throw new ApiException($"新增标定失败，[{acqImage.Name}]已有联合标定");
                }

                acqImage.JointName = calibName;

                log.Info(new ApiLogMessage(taskName, calibName, A_Add, R_Success, $"[{acqImage.Name}]新增联合标定"));
            }

            CalibrationInfoVO calibInfo = new CalibrationInfoVO
            {
                TaskName = taskName,
                Name = calibName,
                CalibrationType = calibType
            };
            if (calibType == CalibrationType.HandEye)
            {
                calibInfo.HandEyeInfo = new HandEyeInfoVO();
            }
            else if (calibType == CalibrationType.HandEyeSingle)
            {
                calibInfo.HandEyeSingleInfo = new HandEyeSingleInfoVO();
            }
            else if (calibType == CalibrationType.Joint)
            {
                calibInfo.JointInfo = new JointInfoVO
                {
                    Master = new TaskAcqImageVO { TaskName = taskName, AcqImageName = acqImageName }
                };
            }

            calibRepo.AddCalibration(calibInfo);

            calibrationChangedToken = true;

            CalibAdd?.Invoke(taskName, acqImageName, calibType, calibName);
        }

        public bool DeleteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, A_Delete, R_Fail, "未找到任务"));
                throw new ApiException("删除标定失败，未找到任务");
            }

            AcquireImageInfo acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.Name == acqImageName);

            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, A_Delete, R_Fail, "未找到拍照"));
                throw new ApiException("删除标定失败，未找到拍照");
            }

            bool cancelQuote = false;

            if (calibType == CalibrationType.Checkerboard)
            {
                if (string.IsNullOrEmpty(acqImage.CheckerboardName)) return false;

                acqImage.CheckerboardName = null;

                log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, $"[{acqImage.Name}]删除标定"));
            }
            else if (calibType == CalibrationType.HandEye)
            {
                if (acqImage.HandEyeNames == null) return false;

                acqImage.HandEyeNames.Remove(calibName);

                log.Info(new ApiLogMessage(taskName, calibName, A_Delete, R_Success, $"[{acqImage.Name}]删除标定"));
            }
            else if (calibType == CalibrationType.Joint)
            {
                CalibrationInfoVO calib = calibRepo.GetCalibration(calibName);

                if (calib.JointInfo.Master.TaskName == taskName)
                {
                    // 删除整个标定

                    foreach (TaskInfo t in tasks)
                    {
                        AcquireImageInfo acq = t.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.JointName == calibName);
                        if (acq != null)
                        {
                            acq.JointName = null;
                        }
                    }
                }
                else
                {
                    cancelQuote = true;

                    task.CameraAcquireImage.AcquireImages.First(a => a.Name == acqImageName).JointName = null;

                    calibRepo.CancelQuoteCalibration(taskName, acqImageName, calibType, calibName);

                    CancelCalibQuote?.Invoke(taskName, acqImageName, calibType, calibName);

                    calibrationChangedToken = true;
                }
            }

            if (!cancelQuote && calibRepo.DeleteCalibration(taskName, calibName))
            {
                calibrationChangedToken = true;
                CalibDelete?.Invoke(taskName, acqImageName, calibType, calibName);
            }

            return true;
        }

        public void RenameCalibration(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Fail, "未找到任务"));
                throw new ApiException("重命名标定失败，未找到任务");
            }

            AcquireImageInfo acqImage;
            if (calibType == CalibrationType.Checkerboard)
                acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.CheckerboardName == oldCalibName);
            else
            {
                acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.HandEyeNames != null && a.HandEyeNames.Contains(oldCalibName));
            }

            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Fail, "未找到标定"));
                throw new ApiException("重命名标定失败，未找到标定");
            }

            if (calibType == CalibrationType.Checkerboard)
                acqImage.CheckerboardName = newCalibName;
            else
            {
                int index = acqImage.HandEyeNames.FindIndex(a => a == oldCalibName);
                acqImage.HandEyeNames[index] = newCalibName;
            }

            log.Info(new ApiLogMessage(taskName, oldCalibName, A_Rename, R_Success, $"[{acqImage.Name}]重命名标定：[{oldCalibName}] => [{newCalibName}]"));

            calibRepo.RenameCalibration(taskName, calibType, oldCalibName, newCalibName);

            calibrationChangedToken = true;

            CalibRename?.Invoke(taskName, calibType, oldCalibName, newCalibName);
        }

        public void QuoteCalibration(string taskName, string acqImageName, CalibrationType calibType, string calibName)
        {
            TaskInfo task = tasks.FirstOrDefault(a => a.Name == taskName);
            if (task == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, "引用标定", R_Fail, "未找到任务"));
                throw new ApiException("引用标定失败，未找到任务");
            }

            AcquireImageInfo acqImage = task.CameraAcquireImage.AcquireImages.FirstOrDefault(a => a.Name == acqImageName);
            if (acqImage == null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, "引用标定", R_Fail, "未找到拍照"));
                throw new ApiException($"引用标定失败，未找到拍照");
            }

            if (acqImage.JointName != null)
            {
                log.Error(new ApiLogMessage(taskName, calibName, "引用标定", R_Fail, "该拍照已有引用联合标定"));
                throw new ApiException($"引用标定失败，该拍照已有引用联合标定");
            }

            acqImage.JointName = calibName;

            calibrationChangedToken = true;

            calibRepo.QuoteCalibration(taskName, acqImageName, calibType, calibName);

            CalibQuote?.Invoke(taskName, acqImageName, calibType, calibName);
        }

        #endregion

        public void Save()
        {
            // 每次保存之前，重新整理序号
            order = 0;
            foreach (TaskInfo task in tasks)
            {
                task.Order = order++;
            }

            ApiConfig.Save(ApiConfig.TaskConfig);

            log.Info(new ApiLogMessage("任务设置", null, A_Save, R_Success));

            if (calibrationChangedToken)
            {
                calibRepo.Save();

                calibrationChangedToken = false;
            }

            cameraRepo.Save();

            AfterSave?.Invoke();
        }
    }
}
