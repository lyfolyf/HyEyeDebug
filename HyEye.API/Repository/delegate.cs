using HyEye.Models;
using HyEye.Models.VO;

namespace HyEye.API.Repository
{
    #region 通用

    public delegate void NameHandler(string name);

    public delegate void RenameHandler(string oldName, string newName);

    //add by LuoDian @ 20210723 显示输出结果
    public delegate void ShowOutputImageHandler(string taskName, string outputName, object value);

    //add by LuoDian @ 20211101 显示缺陷List之前需要先清除之前的缺陷
    public delegate void ClearOutputGraphicHandler(string taskName, string outputName);

    #endregion

    #region ROI add by LuoDian @ 20210804 用于ROI操作事件
    public delegate void UpdateROI(int roiIndex);
    #endregion

    #region

    #region Task

    public delegate void TaskInfoHandler(TaskInfoVO taskInfo);

    public delegate void TaskMoveHandler(string taskName1, string taskName2);

    #endregion

    #endregion

    #region AcqImage

    public delegate void AcqImageNameHandler(string taskName, string acqImageName);

    public delegate void AcqImageRenameHandler(string taskName, string oldAcqImageName, string newAcqImageName);

    public delegate void AcqImageMoveHandler(string taskName, string acqImageName1, string acqImageName2);

    #endregion

    #region Calibration

    public delegate void CalibrationHandle(string taskName, string calibName);

    public delegate void TaskCalibrationHandler(string taskName, string acqImageName, CalibrationType calibType, string calibName);

    public delegate void CalibrationRenameHandler(string taskName, CalibrationType calibType, string oldCalibName, string newCalibName);

    public delegate void LoadCalibrationHandle(string taskName, string calibName, string path);

    #endregion

    public delegate void SimulationHandle(string path);
}
