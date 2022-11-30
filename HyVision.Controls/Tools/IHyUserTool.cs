using HyVision.Models;
using HyVision.Tools.TerminalCollection;
using System;

namespace HyVision.Tools
{
    /// <summary>
    /// 工具类接口
    /// </summary>
    public interface IHyUserTool : IHyCloneable, IInternalResourcesDisposable, IDisposable
    {
        /// <summary>
        /// 运行完成之后发生
        /// </summary>
        event EventHandler Ran;

        /// <summary>
        /// 工具名称改变时发生
        /// </summary>
        event EventHandler<ValueChangedEventArgs<string>> NameChanged;

        /// <summary>
        /// 编辑界面类型
        /// </summary>
        Type ToolEditType { get; }

        /// <summary>
        /// 工具名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 输入
        /// </summary>
        HyTerminalCollection Inputs { get; }

        /// <summary>
        /// 输出
        /// </summary>
        HyTerminalCollection Outputs { get; }

        /// <summary>
        /// 运行
        /// update by LuoDian @ 20211214 添加一个参数，用于区分不同的子料号，加载对应子料号的参数
        /// </summary>
        void Run(string subName);

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        bool Initialize();

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        void Save();
    }
}
