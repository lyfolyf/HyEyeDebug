using GL.Kit.Net.Sockets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HyEye.Test
{
    public class SimulationStation
    {
        /// <summary>
        /// 发送指令后发生
        /// </summary>
        public event Action Sended;

        TestConfig config;

        TcpUserToken socket;
        CancellationTokenSource cts;

        public Task Start(TestConfig config)
        {
            this.config = config;

            TcpClient client = new TcpClient(config.NetworkInfo);
            socket = client.Connect();

            cts = new CancellationTokenSource();

            Task task = new Task(() =>
            {
                Run(config.Commands, cts.Token);
            }, cts.Token, TaskCreationOptions.LongRunning);

            task.Start();

            return task;
        }

        public void Stop()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
        }

        void Run(string[] cmds, CancellationToken token)
        {
            for (int i = 0; i < config.Count; i++)
            {
                foreach (string cmd in cmds)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    socket.Send(cmd);

                    Sended?.Invoke();

                    Thread.Sleep(config.Interval);
                }
            }
        }
    }
}
