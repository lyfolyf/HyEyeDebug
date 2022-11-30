using System;

namespace HyEye.Models
{
    /// <summary>
    /// OK/NG
    /// </summary>
    public enum ImageFlag
    {
        OK = 1,

        NG = 2
    }

    /// <summary>
    /// 图片保存模式
    /// </summary>
    [Flags]
    public enum ImageSaveMode
    {
        None = 0,

        OK = 1,

        NG = 2,

        Both = OK | NG
    }

    /// <summary>
    /// 图片删除条件
    /// </summary>
    public enum ImageDeleteCondition
    {
        /// <summary>
        /// 磁盘使用超过指定大小
        /// </summary>
        DiskUsageExceeds,

        /// <summary>
        /// 磁盘空闲小于指定大小
        /// </summary>
        DiskFreeLess
    }

    /// <summary>
    /// 图片删除模式
    /// </summary>
    public enum ImageDeleteMode
    {
        /// <summary>
        /// 不删除
        /// </summary>
        NoDelete,

        /// <summary>
        /// 周期删除
        /// </summary>
        Cycle,

        /// <summary>
        /// 定时删除
        /// </summary>
        DefiniteTime
    }

    /// <summary>
    /// 定时删除配置
    /// </summary>
    public struct DefiniteTimeDeleteInfo
    {
        public int StartHour { get; set; }

        public int StartMin { get; set; }

        public int StopHour { get; set; }

        public int StopMin { get; set; }

        /// <summary>
        /// 图片保留天数
        /// </summary>
        public int RetentionDays { get; set; }

        public DefiniteTimeDeleteInfo(int startHour, int startMin, int stopHour, int stopMin, int retentionDays)
        {
            StartHour = startHour;
            StartMin = startMin;
            StopHour = stopHour;
            StopMin = stopMin;
            RetentionDays = retentionDays;
        }

        /// <summary>
        /// 开始删除时间
        /// </summary>
        public TimeSpan StartTime()
        {
            return new TimeSpan(StartHour, StartMin, 0);
        }

        public TimeSpan StopTime()
        {
            return new TimeSpan(StopHour, StopMin, 0);
        }
    }

    /// <summary>
    /// 周期删除配置
    /// </summary>
    public struct CycleDeleteInfo
    {
        /// <summary>
        /// 删除执行周期
        /// </summary>
        public int CycleMin { get; set; }

        /// <summary>
        /// 删除条件
        /// </summary>
        public ImageDeleteCondition Condition { get; set; }

        /// <summary>
        /// 临界值
        /// </summary>
        public int CriticalValue { get; set; }

        /// <summary>
        /// 每次达到临界值时删除图片的总大小
        /// </summary>
        public int DeleteSize { get; set; }

        public CycleDeleteInfo(int cycleMin, ImageDeleteCondition condition, int criticalValue, int deleteSize)
        {
            CycleMin = cycleMin;
            Condition = condition;
            CriticalValue = criticalValue;
            DeleteSize = deleteSize;
        }
    }

}
