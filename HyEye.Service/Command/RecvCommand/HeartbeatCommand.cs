using System;

namespace HyEye.Services
{
    /// <summary>
    /// 心跳指令
    /// </summary>
    public class HeartbeatCommand
    {
        const string HeartbeatString = "HyInspect";

        readonly string heartbeatString;

        private HeartbeatCommand(string heartbeatString)
        {
            this.heartbeatString = heartbeatString;
        }

        public static bool IsHeartbeatCommand(string str, out HeartbeatCommand cmd)
        {
            if (str.Equals(HeartbeatString, StringComparison.OrdinalIgnoreCase))
            {
                cmd = new HeartbeatCommand(str);
                return true;
            }
            else
            {
                cmd = null;
                return false;
            }
        }

        public override string ToString()
        {
            return heartbeatString;
        }
    }
}
