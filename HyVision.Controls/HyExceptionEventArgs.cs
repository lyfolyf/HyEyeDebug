using System;

namespace HyVision
{
    public class HyExceptionEventArgs : ExceptionEventArgs
    {
        public object Sender { get; }

        public string Message { get; }

        public HyExceptionEventArgs(object sender, string message, Exception exception)
            : base(exception)
        {
            Sender = sender;
            Message = message;
        }
    }

}
