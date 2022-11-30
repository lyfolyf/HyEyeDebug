using System.Collections.Generic;

namespace HyEye.Models
{
    public static class ErrorCodeConst
    {
        public const int OK = 0;

        /// <summary>
        /// 服务未启动
        /// </summary>
        public const int ServiceNotStarted = 1;

        /// <summary>
        /// 指令格式错误
        /// </summary>
        public const int CommandFormatError = 2;

        /// <summary>
        /// 指令索引错误
        /// </summary>
        public const int CommandIndexError = 3;

        /// <summary>
        /// 指令长度错误
        /// </summary>
        public const int CommandLengthError = 4;

        /// <summary>
        /// 指令头错误
        /// </summary>
        public const int CommandHeaderError = 5;

        /// <summary>
        /// 拍照索引错误
        /// </summary>
        public const int IndexError = 6;

        /// <summary>
        /// 指令类型错误
        /// </summary>
        public const int CommandTypeError = 7;

        /// <summary>
        /// 数据类型错误
        /// </summary>
        public const int DataTypeError = 8;

        /// <summary>
        /// 返回数据缺失
        /// </summary>
        public const int DataMissingError = 100;

        /// <summary>
        /// 取像失败
        /// </summary>
        public const int AcqImageError = 101;

        /// <summary>
        /// 光源操作失败
        /// </summary>
        public const int LightOperationError = 102;

        /// <summary>
        /// 触发设置失败
        /// add by LuoDian @ 20220111 用于把触发设置和获取图像数据分开之后，先判断触发设置是否成功
        /// </summary>
        public const int SetTriggerError = 103;

        /// <summary>
        /// 软触发失败
        /// </summary>
        public const int SoftTriggerError = 104;

        /// <summary>
        /// 标定进行中
        /// </summary>
        public const int Calibrating = 200;

        /// <summary>
        /// 标定尚未准备就绪，发生在标定 Start 失败之后发起标定
        /// </summary>
        public const int CalibNotReady = 201;

        /// <summary>
        /// 自动标定未开始
        /// </summary>
        public const int CalibNotStarted = 202;

        /// <summary>
        /// 未找到模板
        /// </summary>
        public const int HandeyeNotFindPattern = 203;

        /// <summary>
        /// 标定失败
        /// </summary>
        public const int CalibrateFail = 204;

        /// <summary>
        /// 标定模式错误
        /// </summary>
        public const int CalibModeError = 205;

        /// <summary>
        /// R 指令超时
        /// </summary>
        public const int CmdRTimeout = 206;

        /// <summary>
        /// ToolBlock 返回的 ErrorCode 为 null
        /// </summary>
        public const int NoneErrorCode = 207;

        /// <summary>
        /// Handeye 启用 Checkerboard 畸变矫正失败
        /// </summary>
        public const int CheckerboardCalibrationError = 208;

        /// <summary>
        /// ToolBlock 运行失败
        /// </summary>
        public const int ToolBlockRunFail = 210;

        /// <summary>
        /// 任务暂停
        /// </summary>
        public const int TaskSuspend = 211;

        /// <summary>
        /// 任务取消
        /// <para>暂停后复位</para>
        /// </summary>
        public const int TaskCancel = 212;

        /// <summary>
        /// Task任务 启动失败  add by LuoDian @ 20220121
        /// </summary>
        public const int TaskStartFail = 213;

        /// <summary>
        /// 获取光学参数失败  add by LuoDian @ 20220228
        /// </summary>
        public const int GetOpticsFail = 214;

        /// <summary>
        /// 脚本执行失败
        /// </summary>
        public const int ScriptError = 300;

        public const int ActiveStop = 1000;

        /// <summary>
        /// 服务器没有Ready add by LuoDian @ 20220123
        /// </summary>
        public const int ServerNotReady = 1010;

        /// <summary>
        /// AI参数设置成功  add by LuoDian @ 20220120 
        /// </summary>
        public const int AISetParamsSucess = 5140;

        /// <summary>
        /// AI初始化成功  add by LuoDian @ 20220120 
        /// </summary>
        public const int AIInitializeSucess = 5150;

        static Dictionary<int, string> err = new Dictionary<int, string>()
        {
            { OK,                             "执行成功" },

            { ServiceNotStarted,              "服务未启动"   },
            { CommandFormatError,             "指令格式错误" },
            { CommandIndexError,              "指令索引错误" },
            { CommandLengthError,             "指令长度错误" },
            { CommandHeaderError,             "指令头错误"   },
            { IndexError,                     "拍照索引错误" },
            { CommandTypeError,               "指令类型错误" },
            { DataTypeError,                  "数据类型错误" },

            { DataMissingError,               "返回数据缺失" },
            { AcqImageError,                  "取像失败"     },
            { LightOperationError,            "光源操作失败" },
            { SetTriggerError,                "触发设置失败" },
            { SoftTriggerError,                "软触发失败" },

            { Calibrating,                    "标定进行中" },
            { CalibNotReady,                  "标定尚未准备就绪" },
            { CalibNotStarted,                "自动标定未开始" },
            { HandeyeNotFindPattern,          "未找到模板" },
            { CalibrateFail,                  "标定失败" },
            { CalibModeError,                 "标定模式错误" },
            { ToolBlockRunFail,               "ToolBlock 运行失败" },
            { NoneErrorCode,                  "ToolBlock 返回的 ErrorCode 为 null" },
            { CmdRTimeout,                    "R 指令超时" },
            { CheckerboardCalibrationError,   "Handeye 启用 Checkerboard 畸变矫正失败" },
            { TaskSuspend,                    "任务暂停" },
            { TaskCancel,                     "任务取消" },

            //add by LuoDian @ 20220121
            { TaskStartFail,                  "Task任务 启动失败" },

            //add by LuoDian @ 20220120 定义AI的错误码
            { AISetParamsSucess,              "AI参数设置成功" },
            { AIInitializeSucess,             "AI初始化成功" },
            //add by LuoDian @ 20220123 服务器没有Ready
            { ServerNotReady,                 "服务器没有Ready" },

            { ScriptError,                    "脚本执行失败" }
        };

        public static string ErrorMessage(int errCode)
        {
            if (err.ContainsKey(errCode))
                return err[errCode];
            else
                return "未定义错误";
        }
    }
}
