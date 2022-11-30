using System;

namespace HyEye.Services
{
    public class ReceivedCommandEventArgs : EventArgs
    {
        public ReceiveCommand Command { get; set; }

        public ReceivedCommandEventArgs(ReceiveCommand command)
        {
            Command = command;
        }
    }
}
