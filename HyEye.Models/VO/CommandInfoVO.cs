using System;
using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class ReceiveCommandInfoVO
    {
        public string TaskName { get; set; }

        public string Name { get; set; }

        public string CommandHeader { get; set; }

        public int Index { get; set; }

        public CalibrationType? CalibrationType { get; set; }

        public List<CommandFieldInfoVO> Fields { get; set; } = new List<CommandFieldInfoVO>();
    }

    public class SendCommandInfoVO
    {
        public string TaskName { get; set; }

        /// <summary>
        /// 拍照名称/取像名称
        /// </summary>
        public string Name { get; set; }

        public string CommandHeader { get; set; }

        public int Index { get; set; }

        public string Fields { get; set; }
    }

    public class CommandFieldInfoVO
    {
        public string Name { get; set; }

        public Type DataType { get; set; }

        public CommandFieldUse Use { get; set; }
    }
}
