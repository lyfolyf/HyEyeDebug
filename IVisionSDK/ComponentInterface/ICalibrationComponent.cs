using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisionSDK
{
    public interface ICalibrationVerify
    {
        object OutputImage { get; }

        double GetRMS();

        /// <summary>
        /// 获取纵横比
        /// </summary>
        double GetAspect();

        /// <summary>
        /// 获取倾斜值
        /// </summary>
        double GetSkew();
    }

    public interface IHandeyeComponent : ICalibrationVerify, IDisposable
    {
        /// <summary>
        /// PMAlignEdit
        /// </summary>
        Control PatternControl { get; }

        Control NPointToNPointControl { get; }

        Control FitCircleControl { get; }

        void RenameCalibration(string newName);

        void SetPatternImage(object img, bool isGrey);

        void Save();

        void SavePattern();

        int Start();

        bool CheckIndex(int index);

        object RunNP(object img, bool isGrey);

        (int errCode, Dictionary<string, object>) Run(int index, object img, bool isGrey);

        /// <summary>
        /// 标定
        /// </summary>
        void Calibrate(List<(double X1, double Y1, double A1, double X2, double Y2, double A2, bool disable)> points);

        /// <summary>
        /// 重置手眼标定
        /// </summary>
        void Reset();
    }

    public struct HandeyeSinglePoint
    {
        public double X1;

        public double Y1;

        public double? X2;

        public double? Y2;

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable;
    }

    public interface IHandeyeSingleComponent : ICalibrationVerify, IDisposable
    {
        /// <summary>
        /// PMAlignEdit
        /// </summary>
        Control PatternControl { get; }

        Control NPointToNPointControl { get; }

        void RenameCalibration(string newName);

        void SetPatternImage(object img, bool isGrey);

        void Save();

        void SavePattern();

        int Start();

        object RunNP(object img, bool isGrey);

        (int errCode, Dictionary<string, object>) Run(object img, bool isGrey);

        /// <summary>
        /// 标定
        /// </summary>
        void Calibrate(List<HandeyeSinglePoint> points);
    }

    public interface ICheckerboardComponent : ICalibrationVerify, IDisposable
    {
        Control DisplayedControl { get; }

        void RenameCalibration(string newName);

        void Save();

        void SetInputImage(Bitmap bitmap, bool isGrey);

        object Calibrate(Bitmap bitmap, bool isGrey);

        object Run(Bitmap bitmap, bool isGrey);
    }
}
