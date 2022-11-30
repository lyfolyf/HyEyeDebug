using PlcSDK;
using System;
using System.Collections.Generic;

namespace HyEye.Models.VO
{
    public class PlcCommandInfoVO
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
        public List<PlcCommandFieldInfoVO> Fields { get; set; } = new List<PlcCommandFieldInfoVO>();
    }

    public class PlcCommandFieldInfoVO
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 寄存器地址
        /// </summary>
        public PlcDeviceName DeviceName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ValueType { get; set; }
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

        public override string ToString()
        {
            return $"{Name},{DeviceName},{ValueType.Name}";
        }
    }

}
