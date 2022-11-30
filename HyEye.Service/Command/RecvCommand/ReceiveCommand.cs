using System.Collections.Generic;

namespace HyEye.Services
{
    /// <summary>
    /// 接收指令
    /// </summary>
    public class ReceiveCommand
    {
        public ReceiveCommandType Type { get; set; }

        public long Index { get; set; }

        public List<TaskCommand> TaskCommands { get; set; }

        public CalibCommand CalibCommand { get; set; }

        public ProcessCommand ProcessCommand { get; set; }

        public MaterialCommand MaterialCommand { get; set; }

        public ScriptCommand ScriptCommand { get; set; }

        public HeartbeatCommand HeartbeatCommand { get; set; }

        public override string ToString()
        {
            if (Type == ReceiveCommandType.Task)
            {
                return string.Join(";", TaskCommands);
            }
            else if (Type == ReceiveCommandType.Calibration)
            {
                return CalibCommand.ToString();
            }
            else if (Type == ReceiveCommandType.Process)
            {

            }
            else if (Type == ReceiveCommandType.Material)
            {
                return MaterialCommand.ToString();
            }
            else if (Type == ReceiveCommandType.Script)
            {
                return ScriptCommand.ToString();
            }
            else if (Type == ReceiveCommandType.Heartbeat)
            {
                return HeartbeatCommand.ToString();
            }

            return string.Empty;
        }
    }

    public enum ReceiveCommandType
    {
        Task,

        Calibration,

        Process,

        Material,

        Script,

        Heartbeat
    }
}
