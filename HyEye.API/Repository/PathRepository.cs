using HyEye.Models;

namespace HyEye.API.Repository
{
    public interface IPathRepository
    {
        string TaskVppDirectory { get; }
        string CalibrationVppDirectory { get; }

        string GetTaskVppFilename(string taskName, TaskType taskType);
        string GetTaskVppPath(string taskName, TaskType taskType);

        string GetCalibCircleVppFilename(string calibName);
        string GetCalibCircleVppPath(string calibName);

        string GetCalibPatternFilename(string calibName);
        string GetCalibPatternPath(string calibName);

        string GetCalibVerifyVppFilename(string calibName);
        string GetCalibVerifyVppPath(string calibName);

        string GetJointVppFilename(string calibName, string taskName, string acqName, TaskType taskType);
        string GetJointVppPath(string calibName, string taskName, string acqName, TaskType taskType);
    }

    public class PathRepository : IPathRepository
    {
        readonly IMaterialRepository materialRepo;

        public PathRepository(IMaterialRepository materialRepo)
        {
            this.materialRepo = materialRepo;
        }

        #region Path

        public string TaskVppDirectory
        {
            //update by LuoDian @ 20210922 用于统一应用程序的绝对路径，防止因系统环境变量被串改导致的程序文件加载或写入错误
            get { return $@"{GlobalParams.ApplicationStartupPath}\config\{materialRepo.CurrMaterial}\taskVpp"; }
        }

        public string CalibrationVppDirectory
        {
            get { return $@"config\{materialRepo.CurrMaterial}\calibrationVpp"; }
        }

        // 任务
        public string GetTaskVppFilename(string taskName, TaskType taskType)
        {
            return $@"{taskName}.{(taskType == TaskType.VP ? "vpp" : "hy")}";
        }

        public string GetTaskVppPath(string taskName, TaskType taskType)
        {
            return $@"{TaskVppDirectory}\{GetTaskVppFilename(taskName, taskType)}";
        }

        public string GetCalibCircleVppFilename(string calibName)
        {
            return $@"{calibName}_circle.vpp";
        }

        public string GetCalibCircleVppPath(string calibName)
        {
            return $@"{CalibrationVppDirectory}\{GetCalibCircleVppFilename(calibName)}";
        }

        // 标定模板
        public string GetCalibPatternFilename(string calibName)
        {
            return $@"{calibName}_template.vpp";
        }

        public string GetCalibPatternPath(string calibName)
        {
            return $@"{CalibrationVppDirectory}\{GetCalibPatternFilename(calibName)}";
        }

        // 标定验证
        public string GetCalibVerifyVppFilename(string calibName)
        {
            return $@"{calibName}_verify.vpp";
        }

        public string GetCalibVerifyVppPath(string calibName)
        {
            return $@"{CalibrationVppDirectory}\{GetCalibVerifyVppFilename(calibName)}";
        }

        // 联合标定
        public string GetJointVppFilename(string calibName, string taskName, string acqName, TaskType taskType)
        {
            return $@"{calibName}_{taskName}_{acqName}.{(taskType == TaskType.VP ? "vpp" : "hy")}";
        }

        public string GetJointVppPath(string calibName, string taskName, string acqName, TaskType taskType)
        {
            return $@"{CalibrationVppDirectory}\{calibName}_{taskName}_{acqName}.{(taskType == TaskType.VP ? "vpp" : "hy")}";
        }

        #endregion
    }
}
