using System;

namespace HyEye.Services
{
    /// <summary>
    /// 视觉通讯接口
    /// <para>包括 TCP、PLC 通讯</para>
    /// </summary>
    public interface IVisionCommunication
    {
        event EventHandler ConnectedChanged;

        event Action<string> DataReceived;

        bool Connected { get; }

        void Start();

        void Stop();

        void Send(string msg);
    }
}
