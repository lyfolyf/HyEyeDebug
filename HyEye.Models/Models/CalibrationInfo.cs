using System.Collections.Generic;

namespace HyEye.Models
{
    public class CalibrationInfo
    {
        /// <summary>
        /// 标定名称
        /// </summary>
        public string Name { get; set; }

        public string TaskName { get; set; }

        /// <summary>
        /// 标定类型
        /// </summary>
        public CalibrationType CalibrationType { get; set; }                                                                                    

        /// <summary>
        /// Checkerboard 标定信息
        /// </summary>
        public CheckerboardInfo CheckerboardInfo { get; set; }

        /// <summary>
        /// HandEye 标定信息
        /// </summary>
        public HandEyeInfo HandEyeInfo { get; set; }

        public HandEyeSingleInfo HandEyeSingleInfo { get; set; }

        public JointInfo JointInfo { get; set; }
    }

    public class CheckerboardInfo
    {

    }

    public class HandEyeInfo
    {
        public int XPointNum { get; set; } = 3;
        public int YPointNum { get; set; } = 3;
        public int APointNum { get; set; } = 3;

        public double XStep { get; set; } = 2d;
        public double YStep { get; set; } = 2d;
        public double AStep { get; set; } = 2d;

        public bool PMAlignOrToolBlock { get; set; } = true;

        /// <summary>
        /// 启用 Checkerboard 畸变校正
        /// </summary>
        public bool EnabledCheckerboard { get; set; }

        /// <summary>
        /// 是否计算旋转中心
        /// </summary>
        public bool EnabledFitCircle { get; set; } = true;

        /// <summary>
        /// 计算旋转中心方法，1多点拟合，2多点夹角拟合
        /// </summary>
        public int FitCircleType { get; set; } = 1;
    }

    public class HandEyeSingleInfo
    {
        public bool PMAlignOrToolBlock { get; set; } = true;

        /// <summary>
        /// 启用 Checkerboard 畸变校正
        /// </summary>
        public bool EnabledCheckerboard { get; set; }
    }

    public class JointInfo
    {
        public int PointCount { get; set; } = 9;

        public TaskAcqImage Master { get; set; }

        public List<TaskAcqImage> Slaves { get; set; }
    }

    public class TaskAcqImage
    {
        public string TaskName { get; set; }

        public string AcqImageName { get; set; }

        public double Offset_X { get; set; }

        public double Offset_Y { get; set; }
    }
}
