using System;
using System.Windows.Forms;

namespace ItemChecker
{
    static class Program
    {
        public static MainForm mainForm;
        public static ServiceCheckerForm serviceCheckerForm;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
