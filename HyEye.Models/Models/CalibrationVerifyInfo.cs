using System;
using System.Xml.Serialization;

namespace HyEye.Models
{
    [Serializable]
    [XmlInclude(typeof(CheckerboardVerifyInfo))]
    [XmlInclude(typeof(HandeyeVerifyInfo))]
    public abstract class CalibrationVerifyInfo
    {
        /// <summary>
        /// 标定名称
        /// </summary>
        public string CalibrationName { get; set; }

        /// <summary>
        /// RMS 验证启用
        /// </summary>
        public bool RmsEnabled { get; set; }

        /// <summary>
        /// RMS 理论值
        /// </summary>
        public double RmsTheoreticalValue { get; } = 0;

        /// <summary>
        /// RMS 公差
        /// </summary>
        public double RmsTolerance { get; set; }

        /// <summary>
        /// 纵横比验证启用
        /// </summary>
        public bool AspectEnabled { get; set; }

        /// <summary>
        /// 纵横比理论值
        /// </summary>
        public double AspectTheoreticalValue { get; } = 1;

        /// <summary>
        /// 纵横比公差
        /// </summary>
        public double AspectTolerance { get; set; }
    }

    /// <summary>
    /// Checkerboard 验证
    /// </summary>
    public class CheckerboardVerifyInfo : CalibrationVerifyInfo
    {
        /// <summary>
        /// 测量标准品验证启用
        /// </summary>
        public bool MeasureEnabled { get; set; }

        /// <summary>
        /// 测量理论值
        /// </summary>
        public double MeasureTheoreticalValue { get; set; }

        /// <summary>
        /// 测量值公差
        /// </summary>
        public double MeasureTolerance { get; set; }
    }

    /// <summary>
    /// Handeye 验证
    /// </summary>
    public class HandeyeVerifyInfo : CalibrationVerifyInfo
    {
        /// <summary>
        /// 倾斜验证启用
        /// </summary>
        public bool SkewEnabled { get; set; }

        /// <summary>
        /// 倾斜理论值
        /// </summary>
        public double SkewTheoreticalValue { get; } = 0;

        /// <summary>
        /// 倾斜实际值
        /// </summary>
        public double SkewTolerance { get; set; }

        /// <summary>
        /// 机械手走位 X 轴方向验证启用
        /// </summary>
        public bool XEnabled { get; set; }

        /// <summary>
        /// X 轴走位理论值
        /// </summary>
        public double XTheoreticalValue { get; set; }

        /// <summary>
        /// X 轴走位实际值
        /// </summary>
        public double XTolerance { get; set; }

        /// <summary>
        /// 机械手走位 Y 轴方向验证启用
        /// </summary>
        public bool YEnabled { get; set; }

        /// <summary>
        /// Y 轴走位理论值
        /// </summary>
        public double YTheoreticalValue { get; set; }

        /// <summary>
        /// Y 轴走位实际值
        /// </summary>
        public double YTolerance { get; set; }

        /// <summary>
        /// 机械手走位角度验证启用
        /// </summary>
        public bool AEnabled { get; set; }

        /// <summary>
        /// 角度理论值
        /// </summary>
        public double ATheoreticalValue { get; set; }

        /// <summary>
        /// 角度实际值
        /// </summary>
        public double ATolerance { get; set; }
    }
}
