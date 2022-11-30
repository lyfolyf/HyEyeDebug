using HyEye.WForm;
using System;
using System.Windows.Forms;

namespace HyEye
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                new StartUpDebug().Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TestHeGong());

        }
    }
}
