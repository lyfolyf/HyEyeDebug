using System;

namespace HyVision.Models
{
    public class HyRunStatus
    {
        public HyToolResultConstants Result { get; }

        public string Message { get; }

        public double TotalTime { get; }

        public Exception Exception { get; }
    }
}
