using System;
using System.Collections.Generic;
using System.Text;

namespace HyEye.Services
{
    /// <summary>
    /// 正常回复指令
    /// </summary>
    public class SendCommand : IReplyCommand
    {
        /// <summary>
        /// 指令头
        /// </summary>
        public string CommandHeader { get; set; }

        /// <summary>
        /// 取像索引/标定索引
        /// </summary>
        public int AcqOrCalibIndex { get; set; }

        public CommandType Type { get; set; }

        public int ErrorCode { get; set; }

        public List<CommandFieldValue> FieldValues { get; set; } = new List<CommandFieldValue>();

        public SendCommand() { }

        public SendCommand(string cmdHeader, int acqOrCalibIndex, CommandType type, int errorCode)
        {
            CommandHeader = cmdHeader;
            AcqOrCalibIndex = acqOrCalibIndex;
            Type = type;
            ErrorCode = errorCode;
        }

        public string ToString(bool format, int decimalPlaces)
        {
            StringBuilder sb = new StringBuilder(64);
            sb.Append(CommandHeader)
              .Append(",")
              .Append(AcqOrCalibIndex.ToString())
              .Append(",")
              .Append(Type.ToString())
              .Append(",")
              .Append(ErrorCode.ToString());

            foreach (CommandFieldValue fieldValue in FieldValues)
            {
                sb.Append(",");

                if (format)
                {
                    sb.Append(fieldValue.Name).Append("=");
                }

                if (fieldValue.Value is double dv)
                    sb.Append(Math.Round(dv, decimalPlaces).ToString());
                else
                    sb.Append(fieldValue.Value);
            }
            return sb.ToString();
        }
    }
}
