using Autofac;
using GL.Kit;
using GL.Kit.Log;
using GL.Kit.Native;
using HyCommon.License;
using HyVision;
using System;
using System.IO;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public class StartUp
    {
        const string configLic = @"C:\Program Files (x86)\HyEye\lic.config";
        const string dirLic = @"C:\Program Files (x86)\HyEye\";

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Control.CheckForIllegalCrossThreadCalls = false;

            SimpleLog.LogPath = Path.Combine(PathUtils.CurrentDirectory, "Log\\Fatal.log");

            if (UserAccount.IsAdministrator())
            {
                HyVisionUtils.CreatePattern();

                Utils.CreatePattern();

                HyVisionUtils.CreateImages();

                string licType = "ORG";
                FrmLoading frmLoading;

                Directory.CreateDirectory(dirLic);

                if (!File.Exists(configLic))
                {
                    using (StreamWriter sw = File.CreateText(configLic))
                    {
                        sw.WriteLine(licType);
                    }
                }
                else
                {
                    using (StreamReader sr = File.OpenText(configLic))
                    {
                        licType = sr.ReadLine();
                    }
                }

                if (licType == "ORG")
                    SystemPrepare.Run();
                else
                {
                    #region 先判断许可证
                    HyLicense lic = new HyLicense();
                    LicenseState licState = lic.GetLicenseState();
                    int leftTime = lic.GetLeftTime();
                    string type = "RC";
                    if (licState != LicenseState.Valid)
                    {
                        FrmLic frmLic = new FrmLic();
                        Application.Run(frmLic);

                        switch (frmLic.DialogResult)
                        {
                            case DialogResult.OK:
                                type = "RC";
                                break;
                            case DialogResult.Ignore:
                                type = "SW";
                                break;
                            default:
                                Application.Exit();
                                break;
                        }
                        leftTime = frmLic.leftTime;
                    }
                    #endregion
                    SystemPrepare.Run();
                }

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
