using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HyEye.Models
{
    [Serializable]
    public class ReceiveCommandInfo
    {
        public string TaskName { get; set; }

        public string Name { get; set; }

        public string CommandHeader { get; set; }

        public int Index { get; set; }

        public CalibrationType? CalibrationType { get; set; }

        public List<CommandFieldInfo> Fields { get; set; } = new List<CommandFieldInfo>();
    }

    [Serializable]
    public class SendCommandInfo
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

    public class CommandFieldInfo
    {
        public string Name { get; set; }

        [XmlIgnore]
        public Type DataType { get; set; }

        [XmlElement(nameof(DataType))]
        public string TypeName
        {
            get { return DataType.FullName; }
            set { DataType = Type.GetType(value); }
        }

        public CommandFieldUse Use { get; set; }
    }
}
