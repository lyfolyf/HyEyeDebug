using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyEye.WForm.Settings.PLCRegSetting
{
    public class PLCRegView
    {

        public class PLCTask : object
        {
            public int ID { get; set; }

            /// <summary>
            /// 任务名称
            /// </summary>
            public string Task_Name { get; set; }

            /// <summary>
            /// 拍照名称
            /// </summary>
            public string Image_Name { get; set; }

            /// <summary>
            /// 指令头
            /// </summary>
            public string Command_Header { get; set; }

            /// <summary>
            /// 拍照索引
            /// </summary>
            public int Image_Index { get; set; }

            /// <summary>
            /// 起始地址
            /// </summary>
            public string Reg_StartPath { get; set; }

            /// <summary>
            /// 总指令长度
            /// </summary>
            public int Order_Count { get; set; }

        }

        public class PLCTaskDetail : object
        {
            public int ID { get; set; }

            /// <summary>
            /// 寄存器地址
            /// </summary>
            public string Reg_Path { get; set; }

            /// <summary>
            ///值类型 
            /// </summary>
            public string Value_Type { get; set; }

            /// <summary>
            /// 寄存器长度
            /// </summary>
            public int Reg_Len { get; set; }

            /// <summary>
            /// 值
            /// </summary>
            public string Reg_Value { get; set; }

            /// <summary>
            /// 系数
            /// </summary>
            /// 1;10;100;1000;
            public string Reg_rat { get; set; }

            /// <summary>
            /// 字段名称
            /// </summary>
            public string Sub_Name { get; set; }

            /// <summary>
            /// 字段用途
            /// </summary>
            public string Sub_Use { get; set; }

            /// <summary>
            /// 说明
            /// </summary>
            public string Detail { get; set; }

        }

    }
}
