using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HyEye.Services
{
    /// <summary>
    /// 标定指令
    /// </summary>
    public class CalibCommand
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; internal set; }

        /// <summary>
        /// 取像名称/标定名称
        /// </summary>
        public string CalibName { get; internal set; }

        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; internal set; }

        /// <summary>
        /// 取像索引/标定索引
        /// </summary>
        public int CalibIndex { get; internal set; }

        public CommandType Type { get; internal set; }

        public CalibrationType CalibrationType { get; internal set; }

        public List<CommandFieldValue> FieldValues { get; internal set; } = new List<CommandFieldValue>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CommandHeader)
                .Append(",")
                .Append(CalibIndex)
                .Append(",")
                .Append(Type);

            foreach (CommandFieldValue field in FieldValues)
            {
                sb.Append(",")
                    .Append(field.Value);
            }

            return sb.ToString();
        }

        public static bool IsCalibCommand(string cmdStr)
        {
            return cmdStr.StartsWith("CB") || cmdStr.StartsWith("HE");
        }

        public static int Parse(string str,
            Dictionary<string, ReceiveCommandInfoVO> calibCommandInfos,
            out CalibCommand cmd)
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
            if (cmdType != CommandType.C && cmdType != CommandType.S && cmdType != CommandType.E)
            {
                cmd = null;
                return ErrorCodeConst.CommandTypeError;
            }

            CalibCommand calibCmd = new CalibCommand
            {
                CommandHeader = commandHeader,
                CalibIndex = index,
                Type = cmdType
            };

            ReceiveCommandInfoVO receiveCommandInfo;

            if (calibCommandInfos.ContainsKey(commandHeader))
            {
                receiveCommandInfo = calibCommandInfos[commandHeader];
                calibCmd.CalibrationType = receiveCommandInfo.CalibrationType.Value;
            }
            else
            {
                cmd = null;
                return ErrorCodeConst.CommandHeaderError;
            }

            calibCmd.TaskName = receiveCommandInfo.TaskName;
            calibCmd.CalibName = receiveCommandInfo.Name;

            if (cmdType == CommandType.C)
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

                        calibCmd.FieldValues.Add(v);
                    }
                }
                catch (Exception)
                {
                    cmd = null;
                    return ErrorCodeConst.DataTypeError;
                }
            }

            cmd = calibCmd;
            return ErrorCodeConst.OK;
        }
    }
}
