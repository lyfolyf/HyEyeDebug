using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class CalibrationInfoVO
    {
        /// <summary>
        /// 标定名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 标定类型
        /// </summary>
        public CalibrationType CalibrationType { get; set; }

        /// <summary>
        /// Checkerboard 标定信息
        /// </summary>
        public CheckerboardInfoVO CheckerboardInfo { get; set; }

        /// <summary>
        /// HandEye 标定信息
        /// </summary>
        public HandEyeInfoVO HandEyeInfo { get; set; }

        public HandEyeSingleInfoVO HandEyeSingleInfo { get; set; }

        public JointInfoVO JointInfo { get; set; }
    }

    public class CheckerboardInfoVO
    {

    }

    public class HandEyeInfoVO
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
        public bool EnabledFitCircle { get; set; }

        /// <summary>
        /// 计算旋转中心方法，1多点拟合，2多点夹角拟合
        /// </summary>
        public int FitCircleType { get; set; } = 1;

        public int TotalCount
        {
            get { return XPointNum * YPointNum + (EnabledFitCircle ? APointNum : 0); }
        }

        public int XYPointNum
        {
            get { return XPointNum * YPointNum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public (double X, double Y, double A)[] GetPoints()
        {
            (double X, double Y, double A)[] points = new (double X, double Y, double A)[TotalCount];

            int n = XYPointNum;

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    points[i] = (-(XPointNum - 1) * 0.5d * XStep, -(YPointNum - 1) * 0.5d * YStep, 0);
                }
                else
                {
                    int row = i / XPointNum;

                    if (i % XPointNum != 0)
                        points[i] = (points[i - 1].X + (row % 2 == 0 ? XStep : -XStep), points[i - 1].Y, 0);
                    else
                        points[i] = (points[i - 1].X, points[i - 1].Y + YStep, 0);
                }
            }

            if (EnabledFitCircle)
            {
                for (int i = 0; i < APointNum; i++)
                {
                    points[n + i] = (0, 0, ((APointNum - 1) * -0.5d + i) * AStep);
                }
            }

            return points;
        }
    }

    public class HandEyeSingleInfoVO
    {
        public bool PMAlignOrToolBlock { get; set; } = false;

        /// <summary>
        /// 启用 Checkerboard 畸变校正
        /// </summary>
        public bool EnabledCheckerboard { get; set; }
    }

    public class JointInfoVO
    {
        public int PointCount { get; set; } = 9;

        public TaskAcqImageVO Master { get; set; }

        public List<TaskAcqImageVO> Slaves { get; set; }
    }

    public class TaskAcqImageVO
    {
        public string TaskName { get; set; }

        public string AcqImageName { get; set; }

        public double Offset_X { get; set; }

        public double Offset_Y { get; set; }
    }
}
