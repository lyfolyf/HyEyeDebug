using HyEye.Models;
using System.Collections.Generic;
using System.Drawing;

namespace HyEye.Services
{
    public delegate void ShowImageHandle(Bitmap bitmap, bool isGrey);

    /// <summary>
    /// 执行任务/标定获取到原始图像事件参数
    /// </summary>
    public delegate void GetSrcImageHandle(HyImageInfo hyImage);

    /// <summary>
    /// 手眼标定经过 Checkerboard 后的图像
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="acqOrCalibName"></param>
    /// <param name="img"></param>
    /// <param name="isGrey"></param>
    public delegate void GetAfterCheckerboardImageHandle(string taskName, string acqOrCalibName, object img, bool isGrey);

    /// <summary>
    /// HandEye 标定获取模板坐标事件参数
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="calibName"></param>
    /// <param name="index"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="a1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="a2"></param>
    public delegate void HandeyeGetPoseHandle(string taskName, string calibName, int index, double x1, double y1, double a1, double? x2, double? y2, double? a2);

    /// <summary>
    /// 标定完成事件参数
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="calibName"></param>
    /// <param name="success"></param>
    public delegate void CalibrationCompletedHandle(string taskName, string calibName, bool success);

    /// <summary>
    /// 完成单次拍照事件参数
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="acqImageName"></param>
    /// <param name="acqImageIndex"></param>
    /// <param name="outputs"></param>
    public delegate void CompletedSingleAcqImageHandle(string taskName, string acqImageName, int acqImageIndex, LinkedDictionary<string, object> outputs);

    /// <summary>
    /// 保存源图的事件参数
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="acqImageName"></param>
    /// <param name="sn"></param>
    public delegate void SaveSrcImageHandle(string taskName, string acqImageName, string sn);
}
