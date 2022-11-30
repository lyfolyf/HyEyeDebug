using Autofac;
using GL.Kit;
using GL.Kit.Log;
using GL.Kit.Native;
using HyVision;
using System;
using System.IO;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public class StartUpDebug
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Control.CheckForIllegalCrossThreadCalls = false;

            SimpleLog.LogPath = Path.Combine(PathUtils.CurrentDirectory, "Log\\Fatal.log");

            //判断当前登录用户是否为管理员
            if (UserAccount.IsAdministrator())
            {
                HyVisionUtils.CreatePattern();

                Utils.CreatePattern();

                HyVisionUtils.CreateImages();

                SystemPrepare.Run();
                AutoFacContainer.Resolve<API.Repository.IUserRepository>().Login("Test", "123");
                Application.Run(AutoFacContainer.Resolve<FormMain>());
            }
            else
            {
                try
                {
                    ProcessUtils.StartAsAdmin();
                }
                catch (Exception ex)
                {
                    SimpleLog.Error("", ex);
                    Application.Exit();
                }
            }
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (!(e.Exception is MyException))
            {
                SimpleLog.Error("", e.Exception);
            }

            if (!(e.Exception is IgnoreException))
            {
                MessageBoxUtils.ShowError(e.Exception.Message);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (!(ex is MyException))
            {
                SimpleLog.Error("", ex);
            }

            if (!(ex is IgnoreException))
            {
                MessageBoxUtils.ShowError(ex.Message);
            }
        }
    }
}
