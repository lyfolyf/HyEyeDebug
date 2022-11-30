using GL.Kit.Log;
using GL.Kit.Net;
using GL.Kit.Net.Sockets;
using HyEye.Models;
using PlcSDK;
using System;
using System.Collections.Generic;
using System.Threading;
using static GL.Kit.Log.ActionResult;
using static HyEye.Models.CommServerAction;

namespace HyEye.Services
{
    /// <summary>
    /// PLC 通讯
    /// </summary>
    public class PlcCommunication
    {
        readonly IGLog log;
        readonly IPLC plc;

        Thread thread;

        const short GetNew = 1;
        const short None = 0;

        public event EventHandler ConnectedChanged;
        public event EventHandler<DataEventArgs> DataReceived;

        public PlcDeviceName ReadFlagDeviceName { get; set; }
        public PlcDeviceName WriteFlagDeviceName { get; set; }
        public PlcDeviceName StartReadDeviceName { get; set; }
        public int ReadLength { get; set; }

        public PlcCommunication(NetworkInfo network, IGLog log)
        {
            this.log = log;

            plc = new Hsl(network);
        }

        public bool Connected { get; private set; }

        public void Start()
        {
            try
            {
                plc.Open();

                log.Info(new CommLogMessage("PLC", A_Connection, R_Success));

                Connected = true;

                OnConnectedChanged();

                thread = new Thread(ReadBlock);
                thread.Name = $"PLC 读取线程";
                thread.IsBackground = true;

                thread.Start();
            }
            catch (Exception e)
            {
                log.Error(new CommLogMessage("PLC", A_Connection, R_Fail, e.Message));
                throw;
            }
        }

        bool reconnectFlag = false;

        public void Stop()
        {
            plc.Close();

            Connected = false;

            reconnectFlag = false;

            OnConnectedChanged();
        }

        void ReadBlock()
        {
            while (plc.Opened)
            {
                short[] flag;
                short[] data;

                try
                {
                    flag = plc.ReadBlock(ReadFlagDeviceName, 1);

                    if (flag[0] == GetNew)
                    {
                        data = plc.ReadBlock(StartReadDeviceName, ReadLength);

                        WriteBlock(ReadFlagDeviceName, new short[] { None });

                        OnDataReceived(data);
                    }

                    Thread.Sleep(10);
                }
                catch (PlcSdkException)
                {
                    Stop();

                    reconnectFlag = true;

                    Thread reconnectThread = new Thread(new ThreadStart(Reconnection));
                    reconnectThread.Name = "PLC 断线重连线程";
                    reconnectThread.IsBackground = true;
                    reconnectThread.Start();
                }
            }
        }

        public void WriteBlock(PlcDeviceName deviceName, short[] data)
        {
            plc.WriteBlock(deviceName, data);
        }

        public void WriteBlocks(List<(PlcDeviceName deviceName, short[] data)> values)
        {
            foreach ((PlcDeviceName deviceName, short[] data) in values)
            {
                WriteBlock(deviceName, data);
            }

            WriteBlock(WriteFlagDeviceName, new short[] { GetNew });
        }

        protected void OnConnectedChanged()
        {
            ConnectedChanged?.Invoke(this, EventArgs.Empty);
        }

        protected void OnDataReceived(short[] data)
        {
            DataReceived?.Invoke(this, new DataEventArgs(data));
        }

        void Reconnection()
        {
            while (reconnectFlag)
            {
                try
                {
                    plc.Open();

                    log.Info(new CommLogMessage("PLC", A_Connection, R_Success));

                    Connected = true;
                    reconnectFlag = false;

                    OnConnectedChanged();

                    thread = new Thread(ReadBlock);
                    thread.Name = $"PLC 读取线程";
                    thread.IsBackground = true;

                    thread.Start();
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
