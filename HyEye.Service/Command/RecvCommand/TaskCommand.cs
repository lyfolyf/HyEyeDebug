using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HyEye.Services
{
    /// <summary>
    /// 任务指令
    /// </summary>
    public class TaskCommand
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; internal set; }

        /// <summary>
        /// 取像名称/标定名称
        /// </summary>
        public string AcqOrCalibName { get; internal set; }

        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; internal set; }

        /// <summary>
        /// 取像索引/标定索引
        /// </summary>
        public int AcqOrCalibIndex { get; internal set; }

        public CommandType Type { get; internal set; }

        public List<CommandFieldValue> FieldValues { get; internal set; } = new List<CommandFieldValue>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CommandHeader)
                .Append(",")
                .Append(AcqOrCalibIndex)
                .Append(",")
                .Append(Type);

            foreach (CommandFieldValue field in FieldValues)
            {
                sb.Append(",")
                    .Append(field.Value);
            }

            return sb.ToString();
        }

        public static bool IsTaskCommand(string cmdStr)
        {
            return cmdStr.StartsWith("T");
        }

        public static int Parse(string str, Dictionary<string, Dictionary<int, ReceiveCommandInfoVO>> taskCommandInfos,
            out TaskCommand cmd)
        {
            string[] fields = str.Split(new char[] { ',', '，' });

            if (fields.Length < 3)
            {
                cmd = null;
                return ErrorCodeConst.CommandLengthError;
            }

            string commandHeader = fields[0];

            if (!int.TryParse(fields[1], out int index))
            {
                cmd = null;
                return ErrorCodeConst.IndexError;
            }
            if (!Enum.TryParse(fields[2], out CommandType cmdType))
            {
                cmd = null;
                return ErrorCodeConst.CommandTypeError;
            }
            if (cmdType != CommandType.A && cmdType != CommandType.R && cmdType != CommandType.AR)
            {
                cmd = null;
                return ErrorCodeConst.CommandTypeError;
            }

            TaskCommand taskCmd = new TaskCommand
            {
                CommandHeader = commandHeader,
                AcqOrCalibIndex = index,
                Type = cmdType
            };

            ReceiveCommandInfoVO receiveCommandInfo;

            if (taskCommandInfos.ContainsKey(commandHeader))
            {
                if (index != 0 && !taskCommandInfos[commandHeader].ContainsKey(index))
                {
                    cmd = null;
                    return ErrorCodeConst.IndexError;
                }

                receiveCommandInfo = taskCommandInfos[commandHeader][index];
            }
            else
            {
                cmd = null;
                return ErrorCodeConst.CommandHeaderError;
            }

            taskCmd.TaskName = receiveCommandInfo.TaskName;
            taskCmd.AcqOrCalibName = receiveCommandInfo.Name;

            if (cmdType == CommandType.A || cmdType == CommandType.AR)
            {
                if (fields.Length != receiveCommandInfo.Fields.Count + 3)
                {
                    cmd = null;
                    return ErrorCodeConst.CommandLengthError;
                }

                try
                {
                    for (int i = 3; i < fields.Length; i++)
                    {
                        CommandFieldValue v = new CommandFieldValue
                        {
                            Name = receiveCommandInfo.Fields[i - 3].Name,
                            Use = receiveCommandInfo.Fields[i - 3].Use,
                            Value = fields[i].ChanageType(receiveCommandInfo.Fields[i - 3].DataType)
                        };

                        taskCmd.FieldValues.Add(v);
                    }
                }
                catch (Exception)
                {
                    cmd = null;
                    return ErrorCodeConst.DataTypeError;
                }
            }

            cmd = taskCmd;
            return ErrorCodeConst.OK;
        }
    }

}
