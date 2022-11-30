using GL.Kit.Log;
using HyEye.Services;
using HyEye.Services.Script;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HyEye
{
    /* LinkedDictionary 是一个添加和遍历顺序一致的字典，使用方法和 Dictionary 一致
     * */

    public class HyEyeAdvancedScript : HyEyeAdvancedScriptBase
    {
        readonly IGLog log;
        readonly ITaskUtils ITaskUtils;

        public HyEyeAdvancedScript(
            IGLog log,
            ITaskUtils ITaskUtils)
        {
            this.log = log;
            this.ITaskUtils = ITaskUtils;
        }

        /// <summary>
        /// 初始化
        /// <para>任务开始时执行</para>
        /// </summary>
        public override void Init()
        {

        }

        /// <summary>
        /// 清理
        /// <para>任务停止时执行</para>
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// 通过指令运行脚本
        /// <para>脚本指令指令头为 S</para>
        /// </summary>
        public override void RunScriptCmd(string[] args)
        {

        }

        /// <summary>
        /// 调整输出，在 ToolBlock 运行完成后执行
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="acqIndex">拍照索引</param>
        /// <param name="outputs">ToolBlock 的 Outputs</param>
        public override LinkedDictionary<string, object> ModifyOutputs(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            return base.ModifyOutputs(taskName, acqIndex, outputs);
        }

        /// <summary>
        /// 将结果保存到文件
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="acqIndex">拍照索引</param>
        /// <param name="outputs">ModifyOutputs 方法返回的结果</param>
        /// <remarks>
        /// 默认的保存方式是每个任务每天一个文件，每次拍照一行记录
        /// 按日期分文件夹，文件名以任务名称命名
        /// 文件中，第一列为记录时间，第二列为拍照索引，之后为 output 值
        /// 默认方式中做了处理，同一任务，同一轮拍照必定存在同一文件中
        /// </remarks>
        public override void SaveRecord(string taskName, int acqIndex, LinkedDictionary<string, object> outputs)
        {
            base.SaveRecord(taskName, acqIndex, outputs);
        }

    }
}
