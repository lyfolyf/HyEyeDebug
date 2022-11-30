using GL.Kit.Net;
using GL.Kit.Net.Sockets;

namespace HyEye.Models
{
    public class CommunicationInfo
    {
        public ConnectionMethod ConnectionMethod { get; set; }

        public CommProtocol CommProtocol { get; set; }

        public NetworkInfo Network { get; set; }
    }
}
