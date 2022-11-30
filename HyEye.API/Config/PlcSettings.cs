using HyEye.Models;
using PlcSDK;
using System.Collections.Generic;

namespace HyEye.API.Config
{
    public class PlcSettings
    {
        public PlcDeviceName StartReadDeviceName { get; set; }

        public int ReadLength { get; set; }

        public PlcDeviceName ReadFlagDeviceName { get; set; }

        public PlcDeviceName WriteFlagDeviceName { get; set; }

        public List<PlcCommandInfo> TaskRecvCommands { get; set; } = new List<PlcCommandInfo>();

        public List<PlcCommandInfo> TaskSendCommands { get; set; } = new List<PlcCommandInfo>();

        public List<PlcCommandInfo> CalibRecvCommands { get; set; } = new List<PlcCommandInfo>();

        public List<PlcCommandInfo> CalibSendCommands { get; set; } = new List<PlcCommandInfo>();
    }
}
