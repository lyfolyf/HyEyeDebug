using GL.Kit;
using HyEye.Models.VO;
using System;

namespace HyEye.Services
{
    public interface ICommunicationService : ICheckable
    {
        event EventHandler<ReceivedCommandEventArgs> TaskCommandReceived;

        event EventHandler<ReceivedCommandEventArgs> CalibCommandReceived;

        event EventHandler<ReceivedCommandEventArgs> ProcessCommandReceied;

        event Action ConnectedChanged;

        bool Connected { get; }

        bool Running { get; }

        CommunicationInfoVO CommunicationInfo { get; }

        void Start();

        void Stop();

        void Init();

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="sn">指令序号</param>
        /// <param name="msg"></param>
        void Send(long sn, string msg);
    }
}
