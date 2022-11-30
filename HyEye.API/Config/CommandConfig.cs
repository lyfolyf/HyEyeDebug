using HyEye.Models;
using System;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    [Serializable]
    public class CommandConfig
    {
        /// <summary>
        /// 发送指令格式
        /// <para>True: Key1 = Value1,Key2 = Value2,Key3 = Value3</para>
        /// <para>False: Value1,Value2,Value3...</para>
        /// </summary>
        public bool SendCmdFormat { get; set; } = true;

        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalPlaces { get; set; } = 3;

        /// <summary>
        /// 是否启用指令索引
        /// </summary>
        public bool EnableCmdIndex { get; set; } = false;

        /// <summary>
        /// 启用握手指令
        /// <para>在收到 A 指令或 AR 指令后，立刻回复一条指令</para>
        /// </summary>
        public bool EnableHandCmd { get; set; }

        /// <summary>
        /// 启用指令索引对齐
        /// <para>仅在 EnableCmdIndex = true 时有效</para>
        /// <para>当为 true 时，R 指令和 A 指令的指令索引必须相等，否则 R 指令获取不到结果</para>
        /// <para>此属性为解决结果错位问题</para>
        /// </summary>
        public bool IndexAlign { get; set; } = true;

        public List<ReceiveCommandInfo> TaskReceiveCommands { get; set; } = new List<ReceiveCommandInfo>();

        public List<SendCommandInfo> TaskSendCommands { get; set; } = new List<SendCommandInfo>();

        public List<ReceiveCommandInfo> CalibrationReceiveCommands { get; set; } = new List<ReceiveCommandInfo>();

        public List<SendCommandInfo> CalibrationSendCommands { get; set; } = new List<SendCommandInfo>();
    }
}
