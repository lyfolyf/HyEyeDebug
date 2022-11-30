using GL.Kit.Net.Sockets;

namespace HyEye.Test
{
    public class TestConfig
    {
        public NetworkInfo NetworkInfo { get; set; } = new NetworkInfo();

        /// <summary>
        /// 循环次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 每条命令的发送间隔
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 指令
        /// </summary>
        public string[] Commands;
    }
}
