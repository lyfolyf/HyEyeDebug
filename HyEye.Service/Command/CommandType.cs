using System;

namespace HyEye.Services
{
    /// <summary>
    /// 指令类型
    /// </summary>
    [Flags]
    public enum CommandType
    {
        /// <summary>
        /// 仅拍照
        /// </summary>
        A = 1,

        /// <summary>
        /// 仅获取结果
        /// </summary>
        R = 2,

        /// <summary>
        /// 拍照并获取结果
        /// </summary>
        AR = A | R,

        /// <summary>
        /// 标定开始
        /// </summary>
        S = 4,

        /// <summary>
        /// 标定
        /// </summary>
        C = 8,

        /// <summary>
        /// 标定结束
        /// </summary>
        E = 16,

        AC = 0x8000 | 1,

        ARC = 0x8000 | 3
    }
}
