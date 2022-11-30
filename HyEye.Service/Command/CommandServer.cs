using GL.Kit.Log;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    public interface ICommandService
    {
        void Init();

        (int errCode, ReceiveCommand[] cmd) BuildCommand(string ip, string str);
    }

    public class CommandService : ICommandService
    {
        readonly ICommandRepository commandRepo;
        readonly IGLog log;

        public CommandService(ICommandRepository commandRepo, IGLog log)
        {
            this.commandRepo = commandRepo;
            this.log = log;
        }

        // CommandHeader,AcquireImageIndex
        Dictionary<string, Dictionary<int, ReceiveCommandInfoVO>> taskCommandInfos;
        // CommandHeader
        Dictionary<string, ReceiveCommandInfoVO> calibCommandInfos;

        MsgSN msgSN;

        public void Init()
        {
            InitTaskCommandInfo();

            InitCalibCommandInfo();

            msgSN = new MsgSN();
        }

        void InitTaskCommandInfo()
        {
            taskCommandInfos = new Dictionary<string, Dictionary<int, ReceiveCommandInfoVO>>();

            List<ReceiveCommandInfoVO> commandList = commandRepo.GetTaskRecvCommandInfos();
            foreach (ReceiveCommandInfoVO cmd in commandList)
            {
                Dictionary<int, ReceiveCommandInfoVO> acqImageIndexCmd;
                if (taskCommandInfos.ContainsKey(cmd.CommandHeader))
                {
                    acqImageIndexCmd = taskCommandInfos[cmd.CommandHeader];
                }
                else
                {
                    acqImageIndexCmd = new Dictionary<int, ReceiveCommandInfoVO>();
                    taskCommandInfos.Add(cmd.CommandHeader, acqImageIndexCmd);
                }

                acqImageIndexCmd.Add(cmd.Index, cmd);
            }
        }

        void InitCalibCommandInfo()
        {
            calibCommandInfos = commandRepo.GetCalibRecvCommandInfos()
                .ToDictionary(key => key.CommandHeader, value => value);
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

        const string parrent = "^\\[(.*?)\\](.*?)$";

        public (int errCode, ReceiveCommand[] cmd) BuildCommand(string ip, string str)
        {
            if (commandRepo.EnableCmdIndex)
            {
                log.Info(new CommLogMessage(ip, A_Recv, R_Success, $"\"{str}\""));

                List<ReceiveCommand> result = new List<ReceiveCommand>(2);

                string[] ss = str.Matches(@"\[.*?(?=\[|$)");
                if (ss.Length == 0)
                    return (ErrorCodeConst.CommandFormatError, null);

                foreach (string s in ss)
                {
                    Match match = Regex.Match(s, parrent);
                    if (!match.Success)
                        return (ErrorCodeConst.CommandFormatError, null);

                    if (!long.TryParse(match.Groups[1].Value, out long sn))
                        return (ErrorCodeConst.CommandIndexError, null);

                    string str0 = match.Groups[2].Value;

                    (int errCode, ReceiveCommand cmd) = BuildCommand(sn, str0);
                    if (errCode == ErrorCodeConst.OK)
                    {
                        result.Add(cmd);
                    }
                    else
                    {
                        return (errCode, null);
                    }
                }
                return (ErrorCodeConst.OK, result.ToArray());
            }
            else
            {
                long sn = msgSN.Next();
                log.Info(new CommLogMessage(ip, A_Recv, R_Success, $"\"[{sn}]{str}\""));

                (int errCode, ReceiveCommand cmd) = BuildCommand(sn, str);

                return (errCode, new ReceiveCommand[] { cmd });
            }
        }

        (int errCode, ReceiveCommand cmd) BuildCommand(long sn, string str)
        {
            ReceiveCommand recvCmd = new ReceiveCommand { Index = sn };

            if (TaskCommand.IsTaskCommand(str))
            {
                recvCmd.Type = ReceiveCommandType.Task;
                recvCmd.TaskCommands = new List<TaskCommand>();

                string[] strCmds = str.Split(new char[] { ';', '；' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string strCmd in strCmds)
                {
                    int code = TaskCommand.Parse(strCmd, taskCommandInfos, out TaskCommand pcmd);
                    if (code == ErrorCodeConst.OK)
                    {
                        recvCmd.TaskCommands.Add(pcmd);
                    }
                    else
                    {
                        return (code, null);
                    }
                }

                return (ErrorCodeConst.OK, recvCmd);
            }
            else if (ProcessCommand.IsProcessCommand(str, out ProcessCommand processCommand))
            {
                recvCmd.Type = ReceiveCommandType.Process;
                recvCmd.ProcessCommand = processCommand;
                return (ErrorCodeConst.OK, recvCmd);
            }
            else if (MaterialCommand.IsMaterialCommand(str, out MaterialCommand materialCommand))
            {
                if (materialCommand == null)
                    return (ErrorCodeConst.CommandFormatError, null);

                recvCmd.Type = ReceiveCommandType.Material;
                recvCmd.MaterialCommand = materialCommand;
                return (ErrorCodeConst.OK, recvCmd);
            }
            else if (HeartbeatCommand.IsHeartbeatCommand(str, out HeartbeatCommand heartbeatCmd))
            {
                recvCmd.Type = ReceiveCommandType.Heartbeat;
                recvCmd.HeartbeatCommand = heartbeatCmd;
                return (ErrorCodeConst.OK, recvCmd);
            }
            else if (ScriptCommand.IsScriptCommand(str, out ScriptCommand scriptCommand))
            {
                recvCmd.Type = ReceiveCommandType.Script;
                recvCmd.ScriptCommand = scriptCommand;
                return (ErrorCodeConst.OK, recvCmd);
            }
            else if (CalibCommand.IsCalibCommand(str))
            {
                int errCode = CalibCommand.Parse(str, calibCommandInfos, out CalibCommand calibCmd);
                if (errCode == ErrorCodeConst.OK)
                {
                    recvCmd.Type = ReceiveCommandType.Calibration;
                    recvCmd.CalibCommand = calibCmd;
                }
                return (errCode, recvCmd);
            }

            return (ErrorCodeConst.CommandTypeError, recvCmd);
        }
    }
}
