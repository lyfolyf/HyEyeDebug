using GL.Kit.Log;
using GL.Kit.Net;
using HyEye.API.Repository;
using HyEye.Models;
using HyEye.Models.VO;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    public class CommunicationService : ICommunicationService
    {
        readonly IMaterialRepository materialRepo;
        readonly ICommandRepository commandRepo;
        readonly IPlcRepository plcRepo;
        readonly ICommunicationRepository communicationRepo;
        readonly ICommandService commandService;
        readonly IScriptService scriptService;
        readonly IGLog log;

        public event EventHandler<ReceivedCommandEventArgs> TaskCommandReceived;

        public event EventHandler<ReceivedCommandEventArgs> CalibCommandReceived;

        public event EventHandler<ReceivedCommandEventArgs> ProcessCommandReceied;

        public event Action ConnectedChanged;

        _ICommunicationService comm;

        public bool Connected { get => comm.Connected; }

        public CommunicationInfoVO CommunicationInfo { get; private set; }

        public CommunicationService(
            IMaterialRepository materialRepo,
            ICommandRepository commandRepo,
            IPlcRepository plcRepo,
            ICommunicationRepository communicationRepo,
            ICommandService commandService,
            IScriptService scriptService,
            IGLog log)
        {
            this.materialRepo = materialRepo;
            this.commandRepo = commandRepo;
            this.plcRepo = plcRepo;
            this.communicationRepo = communicationRepo;
            this.commandService = commandService;
            this.scriptService = scriptService;
            this.log = log;

            materialRepo.MaterialChanging += MaterialRepo_MaterialChanging;
        }

        public bool Running { get; private set; }

        public bool Check()
        {
            CommunicationInfo = communicationRepo.GetCommunicationInfo();
            if (CommunicationInfo == null)
            {
                log?.Error(new CommLogMessage("通讯设置", A_Check, R_Fail, "请先设置通讯信息"));

                return false;
            }
            if (CommunicationInfo.Network?.IP == null)
            {
                log?.Error(new CommLogMessage("通讯设置", A_Check, R_Fail, "请先设置通讯信息"));

                return false;
            }

            if (CommunicationInfo.CommProtocol == CommProtocol.PLC)
            {
                if (!plcRepo.Check())
                {
                    log?.Error(new CommLogMessage("PLC 指令配置", A_Check, R_Fail, "PLC 指令配置错误"));

                    return false;
                }
            }

            return true;
        }

        public void Start()
        {
            if (Running) return;

            if (!Check()) return;

            block = new BlockingCollection<ReceiveCommand>();

            cts = new CancellationTokenSource();
            takeImageTask = new Task(() => RunCommand(cts.Token), cts.Token, TaskCreationOptions.LongRunning);

            comm = GetComm();
            comm.Init();
            comm.Open();

            takeImageTask.Start();

            Running = true;
        }

        private _ICommunicationService GetComm()
        {
            _ICommunicationService comm;

            if (CommunicationInfo.CommProtocol == CommProtocol.TCP)
            {
                comm = new TcpCommunicationService(CommunicationInfo, commandRepo, commandService, log);
            }
            else
            {
                comm = new PlcCommunicationService(CommunicationInfo, commandRepo, plcRepo, log);
            }
            comm.CommandsReceived += Comm_CommandsReceived;
            comm.ConnectedChanged += Comm_ConnectedChanged;
            //add by LuoDian @ 20220112 声明一个单独的接口，用于执行需要串行执行的指令
            comm.CommandsReceivedBlock += Comm_CommandsReceived_Block;

            return comm;
        }

        private void Comm_CommandsReceived(object sender, ReceivedCommandEventArgs e)
        {
            if (!block.IsAddingCompleted)
                block.Add(e.Command);
        }

        //add by LuoDian @ 20220112 声明一个单独的接口，用于执行需要串行执行的指令
        private void Comm_CommandsReceived_Block(object sender, ReceivedCommandEventArgs e)
        {
            if (e.Command != null)
            {
                if (e.Command.Type == ReceiveCommandType.Task)
                {
                    TaskCommandReceived?.Invoke(this, new ReceivedCommandEventArgs(e.Command));
                }
                else if (e.Command.Type == ReceiveCommandType.Process)
                {
                    ProcessCommandReceied?.Invoke(this, new ReceivedCommandEventArgs(e.Command));

                    // 暂时没有回复
                }
                else if (e.Command.Type == ReceiveCommandType.Material)
                {
                    m_recvCmd = e.Command;
                    bool b = materialRepo.ChangeMaterial(e.Command.MaterialCommand.Index);

                    if (!b)
                    {
                        Send(e.Command.Index, e.Command.MaterialCommand.ToString() + ",999");
                    }
                }
                else if (e.Command.Type == ReceiveCommandType.Calibration)
                {
                    Task.Run(() =>
                    {
                        CalibCommandReceived?.Invoke(this, new ReceivedCommandEventArgs(e.Command));
                    });
                }
                else if (e.Command.Type == ReceiveCommandType.Heartbeat)
                {
                    RunHeartbeatCommand(e.Command);
                }
                else if (e.Command.Type == ReceiveCommandType.Script)
                {
                    RunScriptCommand(e.Command);
                }
            }
        }

        private void Comm_ConnectedChanged(object sender, EventArgs e)
        {
            ConnectedChanged?.Invoke();
        }

        public void Stop()
        {
            comm.Close();
            comm.CommandsReceived -= Comm_CommandsReceived;
            comm.ConnectedChanged -= Comm_ConnectedChanged;
            //add by LuoDian @ 20220112 声明一个单独的接口，用于执行需要串行执行的指令
            comm.CommandsReceivedBlock -= Comm_CommandsReceived_Block;

            block?.CompleteAdding();
            cts?.Cancel();

            Running = false;
        }

        BlockingCollection<ReceiveCommand> block;
        CancellationTokenSource cts;
        Task takeImageTask;

        ReceiveCommand m_recvCmd;

        private void MaterialRepo_MaterialChanging(object sender, EventArgs e)
        {
            if (m_recvCmd != null)
                Send(m_recvCmd.Index, m_recvCmd.MaterialCommand.ToString() + ",0");
        }

        #region 运行指令

        void RunCommand(CancellationToken _)
        {
            while (!block.IsCompleted)
            {
                ReceiveCommand recvCmd = null;
                try
                {
                    recvCmd = block.Take();
                }
                catch (InvalidOperationException) { }

                if (recvCmd != null)
                {
                    if (recvCmd.Type == ReceiveCommandType.Task)
                    {
                        Task.Run(() =>
                        {
                            TaskCommandReceived?.Invoke(this, new ReceivedCommandEventArgs(recvCmd));
                        });
                    }
                    else if (recvCmd.Type == ReceiveCommandType.Process)
                    {
                        ProcessCommandReceied?.Invoke(this, new ReceivedCommandEventArgs(recvCmd));

                        // 暂时没有回复
                    }
                    else if (recvCmd.Type == ReceiveCommandType.Material)
                    {
                        m_recvCmd = recvCmd;
                        bool b = materialRepo.ChangeMaterial(recvCmd.MaterialCommand.Index);

                        if (!b)
                        {
                            Send(recvCmd.Index, recvCmd.MaterialCommand.ToString() + ",999");
                        }
                    }
                    else if (recvCmd.Type == ReceiveCommandType.Calibration)
                    {
                        Task.Run(() =>
                        {
                            CalibCommandReceived?.Invoke(this, new ReceivedCommandEventArgs(recvCmd));
                        });
                    }
                    else if (recvCmd.Type == ReceiveCommandType.Heartbeat)
                    {
                        RunHeartbeatCommand(recvCmd);
                    }
                    else if (recvCmd.Type == ReceiveCommandType.Script)
                    {
                        RunScriptCommand(recvCmd);
                    }
                }
            }
        }

        void RunHeartbeatCommand(ReceiveCommand heartbeatCmd)
        {
            Send(heartbeatCmd.Index, heartbeatCmd.HeartbeatCommand.ToString());
        }

        void RunScriptCommand(ReceiveCommand scriptCmd)
        {
            try
            {
                scriptService.RunScriptCmd(scriptCmd.ScriptCommand.Args);
                Send(scriptCmd.Index, scriptCmd.ScriptCommand.CommandHeader + ",0");
            }
            catch (Exception e)
            {
                Send(scriptCmd.Index, scriptCmd.ScriptCommand.CommandHeader + ",1");
                log.Error(new CommLogMessage(null, "执行脚本指令", R_Fail, e.Message));
            }
        }

        #endregion

        public void Send(long sn, string msg)
        {
            comm.Send(sn, msg);
        }

        public void Init()
        {
            //comm?.Init();
        }
    }

    interface _ICommunicationService
    {
        event EventHandler ConnectedChanged;
        event EventHandler<ReceivedCommandEventArgs> CommandsReceived;

        //add by LuoDian @ 20220112 为了实现指令的串行运行，添加一个串行执行的事件
        event EventHandler<ReceivedCommandEventArgs> CommandsReceivedBlock;

        bool Connected { get; }

        void Open();

        void Close();

        void Init();

        void Send(long sn, string msg);
    }

}
