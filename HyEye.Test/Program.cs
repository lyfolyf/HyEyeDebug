using GL.Kit.Log;
using GL.Kit.Native;
using System;
using System.Windows.Forms;

namespace HyEye.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (UserAccount.IsAdministrator())
            {
                //如果是管理员，则直接运行
                Application.Run(new Form1());
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
    }
}
