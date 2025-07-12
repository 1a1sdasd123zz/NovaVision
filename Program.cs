using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using NovaVision.VisionForm.MainForm;

namespace NovaVision
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex instance = new Mutex(initiallyOwned: true, "MutexName", out var createdNew);
            string allowCreatedMore = ConfigurationManager.AppSettings["AllowCreatedMore"];
            if ((string.IsNullOrWhiteSpace(allowCreatedMore) || allowCreatedMore.ToLower() == "false") && !createdNew)
            {
                MessageBox.Show(@"已经启动了一个程序，请先退出！", @"系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                Application.Run(new Frm_Main());
            }
        }
    }
}
