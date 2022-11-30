using GL.Kit.Log;
using System;

namespace HyEye.API.Config
{
    [Serializable]
    public class LogConfig
    {
        public LogLevel WriteLevel { get; set; } = LogLevel.Info;

        public int SaveDays { get; set; } = 7;

        public string MaximumFileSize { get; set; } = "200MB";

        public int MaxSizeRollBackups { get; set; } = 10;

        public int DisplayMaxCount { get; set; } = 200;

        public LogLevel DisplayLevel { get; set; } = LogLevel.Info;
    }
}
