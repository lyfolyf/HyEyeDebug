using PlcSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace HyEye.Models
{
    public class PlcCommandInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 指令头
        /// </summary>
        public CommandHeader CommandHeader { get; set; }
        /// <summary>
        /// 拍照名称/标定名称
        /// </summary>
        public string AcqName { get; set; }
        /// <summary>
        /// 拍照索引
        /// </summary>
        public int AcqIndex { get; set; }
        /// <summary>
        /// 寄存器起始地址
        /// </summary>
        public PlcDeviceName StartDeviceName { get; set; }
        /// <summary>
        /// 寄存器总长度
        /// </summary>
        public int DeviceLength { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<PlcCommandFieldInfo> Fields { get; set; } = new List<PlcCommandFieldInfo>();
    }

    public class PlcCommandFieldInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public PlcDeviceName DeviceName { get; set; }

        string valueTypeString;

        [XmlElement(ElementName = "ValueType")]
        public string ValueTypeString
        {
            get { return valueTypeString; }
            set
            {
                // 只能设置一次
                if (valueTypeString == null)
                {
                    valueTypeString = value;
                    if (valueTypeString == "System.Drawing.Bitmap")
                        ValueType = typeof(Bitmap);
                    else
                        ValueType = Type.GetType(value);
                }
            }
        }

        Type valueType;

        [XmlIgnore]
        public Type ValueType
        {
            get { return valueType; }
            set
            {
                valueType = value;
                valueTypeString = valueType.FullName;
            }
        }
        /// <summary>
        /// 寄存器长度
        /// </summary>
        public int DeviceLength { get; set; }
        /// <summary>
        /// 寄存器值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 系数
        /// </summary>
        public int ValueRatio { get; set; }
        /// <summary>
        /// 字段用途
        /// </summary>
        public CommandFieldUse Use { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }

}
