using GL.Kit;
using HyEye.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VisionSDK
{
    public delegate void RemovedCalibHandle(string taskName, string calibName);

    public delegate void ToolBlockCallBack(string acqName, int acqIndex, int errorCode, LinkedDictionary<string, object> outputs);

    public interface IToolBlockComponent : ICheckable, IDisposable
    {
        /// <summary>
        /// ToolBlock 运行完成后发生
        /// </summary>
        event EventHandler<ToolBlockRanEventArgs> Ran;

        /// <summary>
        /// ToolBlock 中删除标定
        /// </summary>
        event RemovedCalibHandle ComponentRemovedCalib;

        Control DisplayedControl { get; }

        /// <summary>
        /// 重命名任务
        /// </summary>
        void RenameTaskName(string newname);

        /// <summary>
        /// 重置默认参数
        /// </summary>
        void ResetDefaultParam();

        void StartSerial();

        void StopSerial();

        #region 取像

        /// <summary>
        /// 添加取像
        /// </summary>
        void AddAcqImage(string acqImageName);

        /// <summary>
        /// 删除取像
        /// </summary>
        void RemoveAcqImage(string acqImageName);

        #endregion

        #region 工具

        /// <summary>
        /// 添加标定
        /// </summary>
        void AddCalibration(CalibrationType calibType, string calibName);

        /// <summary>
        /// 删除工具
        /// </summary>
        void RemoveTool(string toolName);

        /// <summary>
        /// 重命名工具
        /// </summary>
        void RenameTool(string oldToolName, string newToolName);

        /// <summary>
        /// 获取工具
        /// </summary>
        object GetTool(string toolName);

        Control GetToolEdit(string toolName);

        #endregion

        #region Inputs

        /// <summary>
        /// 获取 Inputs（不包含图像）
        /// <para>用于参数设置页面显示参数</para>
        /// </summary>
        List<ParamInfo> GetInputs();

        bool InputContains(string paramName);

        /// <summary>
        /// 新增 Input
        /// </summary>
        void AddInput(ParamInfo param);

        /// <summary>
        /// 删除 Input
        /// </summary>
        void DeleteInput(string paramName);

        /// <summary>
        /// 重命名 Input
        /// </summary>
        void RenameInput(string oldname, string newname);

        void SetInputValue(string paramName, object value);

        void SetInputDescription(string paramName, string description);

        void ChangeInputType(string paramName, Type newType);

        #endregion

        #region Outputs

        LinkedDictionary<string, object> GetErrorOutputs();

        bool OutputContains(string paramName);

        void AddOutput(ParamInfo output);

        void SetOutputValue(string paramName, object value);

        #endregion


        //update by LuoDian @ 20211214 添加一个参数subName，用于区分不同的子料号，加载对应子料号的参数
        void RunSerial(HyImageInfo hyImage, IEnumerable<(string Name, object Value)> @params, string subName, ToolBlockCallBack callBack = null);

        object CreateRecord(int index);

        void Save();

        void ExpandAll();

        //add by LuoDian @ 20211213 用于子料号的快速切换
        void AddNewHyToolBlock(string curSubName);
        void DeleteHyToolBlockBySubName(string subName);
    }

}
