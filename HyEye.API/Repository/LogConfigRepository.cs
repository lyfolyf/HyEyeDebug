using GL.Kit.Log;
using HyEye.API.Config;
using System;

namespace HyEye.API.Repository
{
    public interface ILogRepository
    {
        event Action<LogLevel> FileLevelChanged;

        LogLevel FileLevel { get; set; }

        int SaveDays { get; set; }

        string MaximumFileSize { get; set; }

        int MaxSizeRollBackups { get; set; }

        LogLevel DisplayLevel { get; set; }

        int DisplayMaxCount { get; set; }

        void Save();
    }

    public class LogRepository : ILogRepository
    {
        readonly LogConfig config = ApiConfig.LogConfig;

        public event Action<LogLevel> FileLevelChanged;

        #region File

        public LogLevel FileLevel
        {
            get { return config.WriteLevel; }
            set
            {
                if (config.WriteLevel != value)
                {
                    config.WriteLevel = value;

                    FileLevelChanged?.Invoke(value);
                }
            }
        }

        #region GLog

        /// <summary>
        /// 保存天数
        /// </summary>
        public int SaveDays
        {
            get { return config.SaveDays; }
            set { config.SaveDays = value; }
        }

        #endregion

        #region log4net

        public string MaximumFileSize
        {
            get { return config.MaximumFileSize; }
            set { config.MaximumFileSize = value; }
        }

        public int MaxSizeRollBackups
        {
            get { return config.MaxSizeRollBackups; }
            set { config.MaxSizeRollBackups = value; }
        }

        #endregion

        #endregion

        #region Display

        public LogLevel DisplayLevel
        {
            get { return config.DisplayLevel; }
            set { config.DisplayLevel = value; }
        }

        public int DisplayMaxCount
        {
            get { return config.DisplayMaxCount; }
            set { config.DisplayMaxCount = value; }
        }

        #endregion

        public void Save()
        {
            ApiConfig.Save(ApiConfig.LogConfig);

            //log.Info(new ApiGeneralLogMessage("日志设置", A_Save, R_Success));
        }
    }
}
