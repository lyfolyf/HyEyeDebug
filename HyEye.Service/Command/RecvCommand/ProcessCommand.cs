namespace HyEye.Services
{
    public class ProcessCommand
    {
        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; private set; } = ProcessCommandHeader;

        static readonly string ProcessCommandHeader = CommandHeaderType.P.ToString();

        public ProcessCommandType Type { get; set; }

        /// <summary>
        /// 暂停
        /// </summary>
        public static ProcessCommand Suspend = new ProcessCommand { Type = ProcessCommandType.S };

        /// <summary>
        /// 恢复
        /// </summary>
        public static ProcessCommand Resume = new ProcessCommand { Type = ProcessCommandType.R };

        /// <summary>
        /// 复位
        /// </summary>
        public static ProcessCommand Reset = new ProcessCommand { Type = ProcessCommandType.RE };

        static readonly string TypeS = ProcessCommandType.S.ToString();
        static readonly string TypeR = ProcessCommandType.R.ToString();
        static readonly string TypeRE = ProcessCommandType.RE.ToString();

        public static bool IsProcessCommand(string str, out ProcessCommand cmd)
        {
            string[] infos = str.Split(new char[] { ',', '，' });

            if (infos[0] == ProcessCommandHeader)
            {
                if (infos[1] == TypeS)
                {
                    cmd = Suspend;
                    return true;
                }
                else if (infos[1] == TypeR)
                {
                    cmd = Resume;
                    return true;
                }
                else if (infos[1] == TypeRE)
                {
                    cmd = Reset;
                    return true;
                }
                else
                {
                    cmd = null;
                    return false;
                }
            }
            else
            {
                cmd = null;
                return false;
            }
        }
    }

    public enum ProcessCommandType
    {
        /// <summary>
        /// Suspend：挂起，暂停
        /// </summary>
        S,

        /// <summary>
        /// Resume：恢复
        /// </summary>
        R,

        /// <summary>
        /// Reset：复位
        /// </summary>
        RE,
    }
}
