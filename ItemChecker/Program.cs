using System;
using System.Windows.Forms;

namespace ItemChecker
{
    static class Program
    {
        public static StartingForm startingForm;
        public static MainForm mainForm;
        public static ServiceParserForm serviceParserForm;
        public static AboutForm aboutForm;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            startingForm = new StartingForm();
            mainForm = new MainForm();
            startingForm.Show();
            mainForm.Show();
            Application.Run();
        }
    }
}
