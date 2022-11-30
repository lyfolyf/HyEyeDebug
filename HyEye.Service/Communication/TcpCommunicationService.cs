using System;
using System.Text;
using System.Collections.Generic;

using GL.Kit.Log;
using GL.Kit.Net;
using GL.Kit.Net.Sockets;
using static GL.Kit.Log.ActionResult;

using HyEye.Models;
using HyEye.Models.VO;
using HyEye.API.Repository;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    class TcpCommunicationService : _ICommunicationService
    {
        readonly ICommandRepository commandRepo;
        readonly ICommandService commandService;
        readonly IGLog log;

        public event EventHandler ConnectedChanged;
        public event EventHandler<ReceivedCommandEventArgs> CommandsReceived;

        //add by LuoDian @ 20220112 为了实现指令的串行运行，添加一个串行执行的事件
        public event EventHandler<ReceivedCommandEventArgs> CommandsReceivedBlock;


        TcpCommunication tcp;

        public TcpCommunicationService(
            CommunicationInfoVO CommunicationInfo,
            ICommandRepository commandRepo,
            ICommandService commandService,
            IGLog log)
        {
            this.commandRepo = commandRepo;
            this.commandService = commandService;
            this.log = log;

            if (CommunicationInfo.ConnectionMethod == ConnectionMethod.Server)
            {
                tcp = new VTcpServer(CommunicationInfo.Network, log);
            }
            else
            {
                tcp = new VTcpClient(CommunicationInfo.Network, log);
            }

            tcp.ConnectedChanged += Socket_ConnectedChanged;
            tcp.DataReceived += Socket_DataReceived;
        }

        public bool Connected
        {
            get { return tcp != null && tcp.Connected; }
        }

        private void Socket_ConnectedChanged()
        {
            if (!tcp.Connected)
            {
                log.Error(new CommLogMessage(tcp.NetworkInfo.ToString(), A_Connection, R_Disconnect));
            }

            ConnectedChanged?.Invoke(this, EventArgs.Empty);
        }

        // Added by louis on Mar. 16 2022, 用于接受指令数据并构建完成指令
        private StringBuilder cmdBuilder = new StringBuilder();
        //add by LuoDian @ 20211121 添加一个变量，用于存储指令数据的起始符和终止符
        private const string CMD_HEAD = "@";
        private const string CMD_END = "&";
        //add by LuoDian @ 20211121 添加一个线程锁，用于多个client端同时发送指令过来的时候进行同步
        private readonly object DataReceiveLockObj = new object();
        //add by LuoDian @ 20211121 添加一个函数，用于分包
        private List<string> CmdDataSplit(string strCmdData)
        {
            string[] strSplitCmdDataArr = strCmdData.Split(CMD_HEAD.ToCharArray());
            List<string> strCmdDataResult = new List<string>();
            foreach(string strSplitData in strSplitCmdDataArr)
            {
                if (string.IsNullOrEmpty(strSplitData))
                    continue;
                if(!strSplitData.Contains(CMD_END))
                {
                    cmdBuilder.Append(strSplitData);
                    continue;
                }
                string[] strSplitEndCmdDataArr = strSplitData.Split(CMD_END.ToCharArray());
                for(int i = 0; i < strSplitEndCmdDataArr.Length; i++)
                {
                    if (string.IsNullOrEmpty(strSplitEndCmdDataArr[i]))
                        continue;
                    if (!strSplitData.EndsWith(CMD_END) && i == strSplitEndCmdDataArr.Length - 1)
                    {
                        cmdBuilder.Append(strSplitEndCmdDataArr[i]);
                        break;
                    }
                    strCmdDataResult.Add(strSplitEndCmdDataArr[i]);
                }
            }
            return strCmdDataResult;
        }

        private void Socket_DataReceived(string command)
        {
            //add by LuoDian @ 20211121 添加一个线程锁，用于多个client端同时发送指令过来的时候进行同步
            lock(DataReceiveLockObj)
            {
                log?.Info($"收到指令数据：{command}");
                #region add by LuoDian @ 20211121 添加数据分包的逻辑
                if (cmdBuilder.Length == 0 && !command.StartsWith(CMD_HEAD))
                {
                    log?.Error($"指令执行失败！指令数据必须以 '{CMD_HEAD}' 开始，以 '{CMD_END}' 结束！");
                    return;
                }

                cmdBuilder.Append(command);
                if (!command.Contains(CMD_END))
                    return;

                //update by LuoDian @ 20220112 通过起始符@、结束符&、和指令符号[]来控制指令的串行执行还是并行执行，对于一组起始符@和结束符&范围内的，含有多个指令ID[]的指令，串行执行，对于不同组起始符@和结束符&的指令，并行执行
                string strWaitSplitData = cmdBuilder.ToString();
                cmdBuilder.Clear();
                //add by LuoDian @ 20220112 这里通过起始符@和结束符&来分割指令
                List<string> lstCmd = CmdDataSplit(strWaitSplitData);
                foreach(string strCmd in lstCmd)
                {
                    if (string.IsNullOrEmpty(strCmd))
                        continue;

                    //add by LuoDian @ 20220112 根据起始符@和结束符&，对于不同组的指令，并行执行
                    string tempCmd = strCmd.Clone().ToString();
                    new System.Threading.Thread(() =>
                    {
                        string remoteEndPoint = ((TcpUserToken)tcp.Communication).RemoteEndPoint.ToString();

                        //add by LuoDian @ 20220112 这里通过指令ID[], 对同一组起始符@和结束符&内的指令再一次进行分割，分割出来的这组不同指令ID的指令，将串行执行
                        (int errCode, ReceiveCommand[] recvCmds) = commandService.BuildCommand(remoteEndPoint, tempCmd);

                        if (errCode == ErrorCodeConst.OK)
                        {
                            //update by LuoDian @ 20220112 串行执行指令，需要调用另一个事件接口
                            foreach (ReceiveCommand cmd in recvCmds)
                            {
                                log?.Info($"开始执行ID为 [{cmd.Index}] 的指令：{cmd.ToString()}");
                                //CommandsReceived?.Invoke(this, new ReceivedCommandEventArgs(cmd));
                                CommandsReceivedBlock?.Invoke(this, new ReceivedCommandEventArgs(cmd));
                            }
                        }
                        else
                        {
                            log?.Error(new CommLogMessage(remoteEndPoint, A_CmdAnalyze, R_Fail, $"\"{ErrorCodeConst.ErrorMessage(errCode)}\""));

                            Send(0, ErrorCommand.Create(errCode).ToString());
                        }
                    })
                    { IsBackground = true }.Start();
                }
                #endregion
            }
        }

        public void Open()
        {
            tcp.Start();
        }

        public void Close()
        {
            tcp.Stop();
        }

        public void Send(long sn, string msg)
        {
            //add by LuoDian @ 20211122 回复指令的时候，添加起始符@和结束符&
            msg = $"{CMD_HEAD}{msg}{CMD_END}";

            if (tcp != null && tcp.Connected)
            {
                if (commandRepo.EnableCmdIndex)
                {
                    tcp.Send($"[{sn}]{msg}");
                }
                else
                {
                    tcp.Send(msg);
                }

                log.Info(new CommLogMessage(((TcpUserToken)tcp.Communication).RemoteEndPoint.ToString(), A_Send, R_Success, $"\"[{sn}]{msg}\""));
            }
            else
            {
                log?.Warn(new CommLogMessage(null, A_Send, R_Fail, "通讯连接未建立或已断开"));
            }
        }

        public void Init()
        {

        }
    }
}
