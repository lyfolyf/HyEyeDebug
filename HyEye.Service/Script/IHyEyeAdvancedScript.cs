using System;
using System.Collections.Generic;

namespace HyEye.Services.Script
{
    public interface IHyEyeAdvancedScript
    {
        /// <summary>
        /// 初始化
        /// <para>任务开始时执行</para>
        /// </summary>
        void Init();

        /// <summary>
        /// 清理
        /// <para>任务停止时执行</para>
        /// </summary>
        void Clear();

        /// <summary>
        /// 通过指令运行脚本
        /// <para>脚本指令指令头为 S</para>
        /// </summary>
        void RunScriptCmd(string[] args);

        /// <summary>
        /// 调整 ToolBlock 的 Outputs
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="acqIndex">拍照索引</param>
        /// <param name="outputs">ToolBlock 输出</param>
        /// <returns>调整后的输出</returns>
        LinkedDictionary<string, object> ModifyOutputs(string taskName, int acqIndex, LinkedDictionary<string, object> outputs);

        void SaveRecord(string taskName, int acqIndex, LinkedDictionary<string, object> outputs);

        void MergeRecord(string[] taskNames, DateTime beginTime, DateTime endTime);
    }
}
