using GL.Kit.Log;
using GL.Kit.Net;
using GL.Kit.Net.Sockets;
using HyEye.Models;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    /// <summary>
    /// 这里统一了客户端和服务端模式
    /// </summary>
    abstract class TcpCommunication
    {
        public event Action ConnectedChanged;

        public event Action<string> DataReceived;

        public ICommunication Communication { get; protected set; }

        bool m_connected = false;
        public bool Connected
        {
            get { return m_connected; }
            protected set
            {
                if (value != m_connected)
                {
                    m_connected = value;
                    ConnectedChanged?.Invoke();
                }
            }
        }

        protected IGLog log;

        public NetworkInfo NetworkInfo { get; protected set; }

        public abstract void Start();

        public abstract void Stop();

        public void Send(string msg)
        {
            if (Communication != null)
            {
                Communication.Send(msg);
                //log.Info(new CommLogMessage(((TcpUserToken)Communication).RemoteEndPoint.ToString(), A_Send, R_Success, $"\"{msg}\""));
            }
            else
            {
                log.Error(new CommLogMessage(((TcpUserToken)Communication).RemoteEndPoint.ToString(), A_Send, R_Fail, "通讯连接未建立或已断开"));
                throw new ServiceException("通讯连接未建立或已断开");
            }
        }

        protected void Communication_DataReceived(object sender, DataEventArgs e)
        {
            string msg = Encoding.UTF8.GetString((byte[])e.Data);
            //log.Info(new CommLogMessage(((TcpUserToken)communication).RemoteEndPoint.ToString(), A_Recv, R_Success, $"\"{msg}\""));

            DataReceived?.Invoke(msg);
        }
    }

    // 这里服务端只做了单连接
    class VTcpServer : TcpCommunication
    {
        TcpServer tcpServer;

        public VTcpServer(NetworkInfo networkInfo, IGLog log)
        {
            base.log = log;
            base.NetworkInfo = networkInfo;
        }

        public override void Start()
        {
            Stop();

            tcpServer = new TcpServer(NetworkInfo);
            tcpServer.Accepted += TcpServer_Accepted;

            tcpServer.Listen();
            log.Info(new CommLogMessage(NetworkInfo.ToString(), A_Listion, R_Start));
        }

        void TcpServer_Accepted(ICommunication communication)
        {
            log.Info(new CommLogMessage(((TcpUserToken)communication).RemoteEndPoint.ToString(), A_Connection, R_Success));

            Communication = communication;

            Communication.DataReceived += Communication_DataReceived;
            Communication.Colsed += Communication_Closed;

            Communication.ReceiveAsync();

            Connected = true;
        }

        public override void Stop()
        {
            if (Communication != null)
            {
                Communication.Close();
            }

            if (tcpServer != null)
            {
                tcpServer.Stop();

                log.Info(new CommLogMessage(NetworkInfo.ToString(), A_Listion, R_Close));

                tcpServer.Accepted -= TcpServer_Accepted;

                tcpServer = null;
            }
        }

        void Communication_Closed(object sender, EventArgs e)
        {
            Connected = false;

            if (Communication != null)
            {
                Communication.DataReceived -= Communication_DataReceived;
                Communication.Colsed -= Communication_Closed;

                Communication = null;
            }
        }
    }

    class VTcpClient : TcpCommunication
    {
        public VTcpClient(NetworkInfo networkInfo, IGLog log)
        {
            base.log = log;
            base.NetworkInfo = networkInfo;
        }

        // 重连间隔
        const int ReconnectionInterval = 1000;
        CancellationTokenSource cts;

        bool running = false;

        public override void Start()
        {
            if (running) return;

            TcpClient tcpClient = new TcpClient(NetworkInfo);
            tcpClient.ClientIP = new IPEndPoint(IPAddress.Any, 45001);
            try
            {
                Communication = tcpClient.Connect();

                log.Info(new CommLogMessage(NetworkInfo.ToString(), A_Connection, R_Success));
            }
            catch (Exception e)
            {
                log.Error(new CommLogMessage(NetworkInfo.ToString(), A_Connection, R_Fail, e.Message));
                throw;
            }
            Communication.DataReceived += Communication_DataReceived;
            Communication.Colsed += Communication_Closed;
            Communication.ReceiveAsync();

            Connected = true;
            running = true;
        }

        public override void Stop()
        {
            running = false;

            if (Communication != null)
            {
                Communication.Close();
            }

            if (cts != null)
            {
                cts.Cancel();
            }
        }

        void Communication_Closed(object sender, EventArgs e)
        {
            Connected = false;

            Communication.DataReceived -= Communication_DataReceived;
            Communication.Colsed -= Communication_Closed;

            if (running)
            {
                log.Error(new CommLogMessage(NetworkInfo.ToString(), A_Connection, R_Disconnect, "开始自动重连..."));

                Communication = null;

                cts = new CancellationTokenSource();

                new Task(() => Reconnection(cts.Token), cts.Token, TaskCreationOptions.LongRunning).Start();
            }
            else
            {
                Communication = null;
            }
        }

        void Reconnection(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                TcpClient tcpClient = new TcpClient(NetworkInfo);
                try
                {
                    Communication = tcpClient.Connect();
                    Communication.DataReceived += Communication_DataReceived;
                    Communication.Colsed += Communication_Closed;
                    Communication.ReceiveAsync();

                    log.Info(new CommLogMessage(NetworkInfo.ToString(), A_Connection, R_Success));

                    Connected = true;
                    cts.Dispose();
                    cts = null;

                    break;
                }
                catch
                {
                    Thread.Sleep(ReconnectionInterval);
                }
            }
        }
    }
}
