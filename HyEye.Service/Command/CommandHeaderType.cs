namespace HyEye.Services
{
    public enum CommandHeaderType
    {
        /// <summary>
        /// 任务指令
        /// </summary>
        T,

        /// <summary>
        /// Checkerboard 标定指令
        /// </summary>
        CB,

        /// <summary>
        /// HandEye 标定指令
        /// </summary>
        HE,

        /// <summary>
        /// 脚本指令
        /// </summary>
        RS,

        /// <summary>
        /// 流程指令
        /// </summary>
        P,

        /// <summary>
        /// 料号切换
        /// </summary>
        MC,
    }
}
