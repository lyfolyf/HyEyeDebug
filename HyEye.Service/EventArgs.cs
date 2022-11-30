using System;

namespace HyEye.Services
{
    public class SendCmdsEventArgs : EventArgs
    {
        public long CommandID { get; set; }

        public IReplyCommand[] Commands { get; }

        public SendCmdsEventArgs(long commandId, IReplyCommand[] commands)
        {
            CommandID = commandId;
            Commands = commands;
        }
    }
}
