using System;
using System.Collections.Generic;

namespace VisionSDK
{
    public class ToolBlockRanEventArgs : EventArgs
    {
        public string TaskName { get; set; }

        public string AcqImageName { get; set; }

        public int AcqImageIndex { get; set; }

        public LinkedDictionary<string, object> Outputs { get; set; }

        public ToolBlockRanEventArgs(string taskName, string acqImageName, int acqImageIndex, LinkedDictionary<string, object> outputs)
        {
            TaskName = taskName;
            AcqImageName = acqImageName;
            AcqImageIndex = acqImageIndex;
            Outputs = outputs;
        }
    }
}
