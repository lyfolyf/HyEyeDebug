using Autofac;
using HyEye.API.Repository;
using System.Collections.Generic;
using System.Text;

namespace HyEye.Services
{
    public class ServerFactory
    {
        ICommunicationService CommunicationService;
        ITaskService TaskService;
        ICalibrationService CalibrationService;
        IImageService ImageService;

        ICommandRepository commandRepo;

        public ServerFactory()
        {
            CommunicationService = AutoFacContainer.Resolve<ICommunicationService>();
            TaskService = AutoFacContainer.Resolve<ITaskService>();
            CalibrationService = AutoFacContainer.Resolve<ICalibrationService>();
            ImageService = AutoFacContainer.Resolve<IImageService>();

            commandRepo = AutoFacContainer.Resolve<ICommandRepository>();

            TaskService.SendCommands += SendCommands;
            CalibrationService.SendCommands += SendCommands;
        }

        private void SendCommands(object sender, SendCmdsEventArgs e)
        {
            string msg = CmdToString(e.Commands);

            CommunicationService.Send(e.CommandID, msg);
        }

        public void InitServices()
        {
            CommunicationService.TaskCommandReceived += CommunicationService_TaskCommandReceived;

            CommunicationService.CalibCommandReceived += CommunicationService_CalibCommandReceived;

            CommunicationService.ProcessCommandReceied += CommunicationService_ProcessCommandReceied;
        }

        void CommunicationService_TaskCommandReceived(object sender, ReceivedCommandEventArgs e)
        {
            // 任务指令
            IReplyCommand[] replyCommands = TaskService.RunCommands(e.Command);

            string msg = CmdToString(replyCommands);

            CommunicationService.Send(e.Command.Index, msg);
        }

        async void CommunicationService_CalibCommandReceived(object sender, ReceivedCommandEventArgs e)
        {
            // 标定只支持单条指令，若多条指令合并发送，则只有第一条指令被执行，后面的会忽略
            IReplyCommand replyCommand = await CalibrationService.RunCommands(e.Command);

            string msg = CmdToString(replyCommand);

            CommunicationService.Send(e.Command.Index, msg);
        }

        private void CommunicationService_ProcessCommandReceied(object sender, ReceivedCommandEventArgs e)
        {
            TaskService.RunProcess(e.Command.ProcessCommand);
        }

        string CmdToString(params IReplyCommand[] commands)
        {
            using (IEnumerator<IReplyCommand> en = ((IEnumerable<IReplyCommand>)commands).GetEnumerator())
            {
                if (!en.MoveNext())
                    return string.Empty;

                StringBuilder result = new StringBuilder(commands.Length * 32);
                if (en.Current != null)
                {
                    result.Append(en.Current.ToString(commandRepo.SendCmdFormat, commandRepo.DecimalPlaces));
                }

                while (en.MoveNext())
                {
                    result.Append(';');
                    if (en.Current != null)
                    {
                        result.Append(en.Current.ToString(commandRepo.SendCmdFormat, commandRepo.DecimalPlaces));
                    }
                }
                return result.ToString();
            }
        }
    }
}
