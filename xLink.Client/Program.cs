using System;
using System.Diagnostics;
using System.Windows.Forms;
using NewLife.Log;

namespace xLink.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            XTrace.UseWinForm();

            // 降低进程优先级，避免卡死电脑
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}