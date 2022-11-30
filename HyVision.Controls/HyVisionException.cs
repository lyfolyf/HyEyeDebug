using GL.Kit;
using System;

namespace HyVision
{
    public class HyVisionException : MyException
    {
        public HyVisionException(string message)
            : base(message)
        {

        }

        public HyVisionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
