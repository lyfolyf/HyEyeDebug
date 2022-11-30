using GL.Kit.Log;
using GL.Kit.Net;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using PlcSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    class PlcCommunicationService : _ICommunicationService
    {
        readonly IPlcRepository plcRepo;
        readonly ICommandRepository commandRepo;
        readonly IGLog log;

        public event EventHandler ConnectedChanged;
        public event EventHandler<ReceivedCommandEventArgs> CommandsReceived;

        //add by LuoDian @ 20220112 ÎªÁËÌí¼Ó´®ÐÐÖ´ÐÐÖ¸ÁîµÄÂß¼­£¬Í¨¹ýÐÂ¼ÓµÄÊÂ¼þ½Ó¿ÚÀ´´®ÐÐÖ´ÐÐÖ¸Áî
        public event EventHandler<ReceivedCommandEventArgs> CommandsReceivedBlock;

        PlcCommunication plc;

        PlcDeviceName startReadDeviceName;
        List<PlcCommandInfoVO> taskRecvCmdInfos;
        List<PlcCommandInfoVO> taskSendCmdInfos;
        List<PlcCommandInfoVO> calibRecvCmdInfos;
        List<PlcCommandInfoVO> calibSendCmdInfos;

        MsgSN msgSN;

        public PlcCommunicationService(CommunicationInfoVO communicationInfo,
            ICommandRepository commandRepo,
            IPlcRepository plcRepo,
            IGLog log)
        {
            this.plcRepo = plcRepo;
            this.commandRepo = commandRepo;
            this.log = log;

            plc = new PlcCommunication(communicationInfo.Network, log);
            plc.ConnectedChanged += Plc_ConnectedChanged;
            plc.DataReceived += Plc_DataReceived;

            msgSN = new MsgSN();
        }

        public void Init()
        {
            startReadDeviceName = plcRepo.StartReadDeviceName;
            taskRecvCmdInfos = plcRepo.GetTaskRecvCommandInfos();
            taskSendCmdInfos = plcRepo.GetTaskSendCommandInfos();
            calibRecvCmdInfos = plcRepo.GetCalibRecvCommandInfos();
            calibSendCmdInfos = plcRepo.GetCalibSendCommandInfos();

            plc.ReadFlagDeviceName = plcRepo.ReadFlagDeviceName;
            plc.WriteFlagDeviceName = plcRepo.WriteFlagDeviceName;
            plc.StartReadDeviceName = plcRepo.StartReadDeviceName;
            plc.ReadLength = plcRepo.ReadLength;
        }

        public bool Connected
        {
            get { return plc != null && plc.Connected; }
        }

        private void Plc_ConnectedChanged(object sender, EventArgs e)
        {
            if (!plc.Connected)
            {
                log.Error(new CommLogMessage("PLC", A_Connection, R_Disconnect));
            }

            ConnectedChanged?.Invoke(this, EventArgs.Empty);
        }

        class MsgSN
        {
            volatile int i = 10000000;

            public int Next()
            {
                Interlocked.Add(ref i, 1);

                return i;
            }
        }

        private void Plc_DataReceived(object sender, DataEventArgs e)
        {
            short[] data = (short[])e.Data;

            ReceiveCommand cmd = new ReceiveCommand();
            cmd.Index = msgSN.Next();

            bool b = BuildTaskCommands(data, cmd);

            if (!b)
                BuildCailbCommands(data, cmd);

            log.Info(new CommLogMessage("PLC", A_Recv, R_Success, $"\"{cmd}\""));

            CommandsReceived?.Invoke(this, new ReceivedCommandEventArgs(cmd));
        }

        #region

        bool BuildTaskCommands(short[] data, ReceiveCommand cmd)
        {
            if (taskRecvCmdInfos.Count == 0) return false;

            cmd.TaskCommands = new List<TaskCommand>();

            foreach (PlcCommandInfoVO cmdInfo in taskRecvCmdInfos)
            {
                int offset = cmdInfo.StartDeviceName.Address - startReadDeviceName.Address;
                if (offset < 0 || offset > data.Length - cmdInfo.DeviceLength) continue;

                if (data[offset] == cmdInfo.CommandHeader.Index && data[offset + 1] == cmdInfo.AcqIndex)
                {
                    TaskCommand taskCmd = new TaskCommand
                    {
                        TaskName = cmdInfo.TaskName,
                        CommandHeader = cmdInfo.CommandHeader,
                        AcqOrCalibName = cmdInfo.AcqName,
                        AcqOrCalibIndex = data[offset + 1],
                        Type = (CommandType)data[offset + 2]
                    };

                    if (cmdInfo.Fields != null && cmdInfo.Fields.Count > 0 && (taskCmd.Type != CommandType.R))
                    {
                        foreach (PlcCommandFieldInfoVO field in cmdInfo.Fields)
                        {
                            CommandFieldValue cmdField = new CommandFieldValue
                            {
                                Name = field.Name,
                                Use = field.Use
                            };
                            taskCmd.FieldValues.Add(cmdField);

                            int fieldOffset = field.DeviceName.Address - startReadDeviceName.Address;

                            if (field.ValueType == StringType)
                            {
                                cmdField.Value = GetStringValue(data, fieldOffset, field.DeviceLength);
                            }
                            else if (field.ValueType == ShortType)
                            {
                                cmdField.Value = (double)data[fieldOffset] / field.ValueRatio;
                            }
                            else if (field.ValueType == IntType)
                            {
                                cmdField.Value = (double)GetIntValue(data, fieldOffset) / field.ValueRatio;
                            }
                        }
                    }

                    cmd.TaskCommands.Add(taskCmd);
                }
            }

            if (cmd.TaskCommands.Count > 0)
            {
                cmd.Type = ReceiveCommandType.Task;
                return true;
            }

            return false;
        }

        bool BuildCailbCommands(short[] data, ReceiveCommand cmd)
        {
            if (calibRecvCmdInfos.Count == 0) return false;

            foreach (PlcCommandInfoVO cmdInfo in calibRecvCmdInfos)
            {
                int offset = cmdInfo.StartDeviceName.Address - startReadDeviceName.Address;
                if (offset < 0 || offset > data.Length - cmdInfo.DeviceLength) continue;

                if (data[offset] == cmdInfo.CommandHeader.Index)
                {
                    CalibCommand calibCmd = new CalibCommand
                    {
                        TaskName = cmdInfo.TaskName,
                        CommandHeader = cmdInfo.CommandHeader,
                        CalibName = cmdInfo.AcqName,
                        CalibIndex = data[offset + 1],
                        Type = (CommandType)data[offset + 2]
                    };

                    if (calibCmd.CommandHeader.StartsWith("CB"))
                    {
                        calibCmd.CalibrationType = CalibrationType.Checkerboard;
                    }
                    else if (calibCmd.CommandHeader.StartsWith("HE"))
                    {
                        calibCmd.CalibrationType = CalibrationType.HandEye;
                    }
                    else if (calibCmd.CommandHeader.StartsWith("JN"))
                    {
                        calibCmd.CalibrationType = CalibrationType.Joint;
                    }
                    cmd.CalibCommand = calibCmd;
                    cmd.Type = ReceiveCommandType.Calibration;

                    if (cmdInfo.Fields != null && cmdInfo.Fields.Count > 0)
                    {
                        foreach (PlcCommandFieldInfoVO field in cmdInfo.Fields)
                        {
                            CommandFieldValue cmdField = new CommandFieldValue
                            {
                                Name = field.Name,
                                Use = field.Use
                            };
                            calibCmd.FieldValues.Add(cmdField);

                            int fieldOffset = field.DeviceName.Address - startReadDeviceName.Address;

                            if (field.ValueType == StringType)
                            {
                                cmdField.Value = GetStringValue(data, fieldOffset, field.DeviceLength);
                            }
                            else if (field.ValueType == ShortType)
                            {
                                cmdField.Value = (double)data[fieldOffset] / field.ValueRatio;
                            }
                            else if (field.ValueType == IntType)
                            {
                                cmdField.Value = (double)GetIntValue(data, fieldOffset) / field.ValueRatio;
                            }
                        }
                    }

                    break;
                }
            }

            return false;
        }

        string GetStringValue(short[] data, int fieldOffset, int length)
        {
            StringBuilder sb = new StringBuilder(length  * 2);
            for (int i = 0; i < length; i++)
            {
                if (data[fieldOffset + i] == 0)
                    break;

                sb.Append((char)(data[fieldOffset + i] & 0xFF));
                sb.Append((char)(data[fieldOffset + i] >> 8));
            }
            return sb.ToString().TrimEnd('\0');
        }

        int GetIntValue(short[] data, int fieldOffset)
        {
            return (int)((ushort)data[fieldOffset + 1] << 16 | (ushort)data[fieldOffset]);
        }

        #endregion

        public void Open()
        {
            plc.Start();
        }

        public void Close()
        {
            plc.Stop();
        }

        static Type CmdType = typeof(CommandType);

        static Type ShortType = typeof(short);
        static Type IntType = typeof(int);
        static Type StringType = typeof(string);

        public void Send(long sn, string strMsg)
        {
            string[] msgs = strMsg.Split(';');

            List<(PlcDeviceName deviceName, short[] data)> list = new List<(PlcDeviceName deviceName, short[] data)>(msgs.Length);

            foreach (string msg in msgs)
            {
                if (msg.StartsWith("T"))
                {
                    list.Add(CreateTaskSendCmd(msg));
                }
                else if (msg.StartsWith("CB") || msg.StartsWith("HE"))
                {
                    list.Add(CreateCalibSendCmd(msg));
                }
            }

            plc.WriteBlocks(list);

            log.Info(new CommLogMessage("PLC", A_Send, R_Success, $"\"{strMsg}\""));
        }

        (PlcDeviceName, short[]) CreateTaskSendCmd(string msg)
        {
            string[] infos = msg.Split(',');

            PlcCommandInfoVO plcCommand = taskSendCmdInfos.First(a => a.CommandHeader == infos[0] && a.AcqIndex == int.Parse(infos[1]));

            short[] data = new short[plcCommand.DeviceLength];
            data[0] = (short)plcCommand.CommandHeader.Index;
            data[1] = short.Parse(infos[1]);
            data[2] = (short)(CommandType)Enum.Parse(CmdType, infos[2]);
            data[3] = short.Parse(infos[3]);

            if (infos.Length > 4 && plcCommand.Fields != null && plcCommand.Fields.Count > 0)
            {
                int infoIndex = 4;

                Dictionary<string, string> fieldsDict = infos.Skip(4)
                        .Select(a => new { key = a.Substring(0, a.IndexOf("=")), value = a.Substring(a.IndexOf("=") + 1) })
                        .ToDictionary(a => a.key, a => a.value);

                foreach (PlcCommandFieldInfoVO field in plcCommand.Fields)
                {
                    if (!fieldsDict.ContainsKey(field.Name))
                        continue;

                    int offset = field.DeviceName.Address - plcCommand.StartDeviceName.Address;

                    if (field.ValueType == StringType)
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(fieldsDict[field.Name]);
                        for (int i = 0, h = buffer.Length; i < h && i / 2 < field.DeviceLength; i += 2)
                        {
                            if (i + 1 < h)
                            {
                                data[offset++] = (short)(buffer[i + 1] << 8 | buffer[i]);
                            }
                            else
                            {
                                data[offset] = buffer[i];
                            }
                        }
                    }
                    else if (field.ValueType == ShortType)
                    {
                        short value = (short)(decimal.Parse(fieldsDict[field.Name]) * field.ValueRatio);
                        data[offset] = value;
                    }
                    else if (field.ValueType == IntType)
                    {
                        int value = (int)(decimal.Parse(fieldsDict[field.Name]) * field.ValueRatio);
                        byte[] buffer = BitConverter.GetBytes(value);
                        data[offset] = (short)(buffer[1] << 8 | buffer[0]);
                        data[offset + 1] = (short)(buffer[3] << 8 | buffer[2]);
                    }
                    infoIndex++;
                }

                infoIndex++;
            }

            return (plcCommand.StartDeviceName, data);
        }

        (PlcDeviceName, short[]) CreateCalibSendCmd(string msg)
        {
            string[] infos = msg.Split(',');

            PlcCommandInfoVO plcCommand = calibSendCmdInfos.First(a => a.CommandHeader == infos[0]);

            short[] data = new short[plcCommand.DeviceLength];
            data[0] = (short)plcCommand.CommandHeader.Index;
            data[1] = short.Parse(infos[1]);
            data[2] = (short)(CommandType)Enum.Parse(CmdType, infos[2]);
            data[3] = short.Parse(infos[3]);

            if (infos.Length > 4 && plcCommand.Fields != null && plcCommand.Fields.Count > 0)
            {
                int infoIndex = 4;

                Dictionary<string, string> fieldsDict = infos.Skip(4)
                        .Select(a => new { key = a.Substring(0, a.IndexOf("=")), value = a.Substring(a.IndexOf("=") + 1) })
                        .ToDictionary(a => a.key, a => a.value);

                foreach (PlcCommandFieldInfoVO field in plcCommand.Fields)
                {
                    if (!fieldsDict.ContainsKey(field.Name))
                        continue;

                    int offset = field.DeviceName.Address - plcCommand.StartDeviceName.Address;

                    if (field.ValueType == StringType)
                    {
                        byte[] buffer = Encoding.ASCII.GetBytes(fieldsDict[field.Name]);
                        for (int i = 0, h = buffer.Length; i < h && i / 2 < field.DeviceLength; i += 2)
                        {
                            if (i + 1 < h)
                            {
                                data[offset++] = (short)(buffer[i + 1] << 8 | buffer[i]);
                            }
                            else
                            {
                                data[offset] = buffer[i];
                            }
                        }
                    }
                    else if (field.ValueType == ShortType)
                    {
                        short value = (short)(decimal.Parse(fieldsDict[field.Name]) * field.ValueRatio);
                        data[offset] = value;
                    }
                    else if (field.ValueType == IntType)
                    {
                        int value = (int)(decimal.Parse(fieldsDict[field.Name]) * field.ValueRatio);
                        byte[] buffer = BitConverter.GetBytes(value);
                        data[offset] = (short)(buffer[1] << 8 | buffer[0]);
                        data[offset + 1] = (short)(buffer[3] << 8 | buffer[2]);
                    }
                    infoIndex++;
                }

                infoIndex++;
            }

            return (plcCommand.StartDeviceName, data);
        }

    }
}
