using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace YouTube
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var current = Process.GetCurrentProcess();
            var otherInstance = 
                Process.GetProcessesByName(current.ProcessName).Where(p => p.Id != current.Id).FirstOrDefault();

            if (otherInstance == null)
            {
                AppDomain.CurrentDomain.UnhandledException += 
                    new UnhandledExceptionEventHandler(Unhandled_Exception);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormYouTube());
            }
        }

        private static void Unhandled_Exception(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            MessageBox.Show(ex.Message, "Error...", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
        }
    }
}