using System;
using System.IO;
using System.Windows.Forms;

namespace ItemChecker.Support
{
    public class Exceptions
    {        
        public static void errorLog(Exception exp, string ver)
        {
            string message = null;
            message += exp.Message + "\n";
            message += exp.StackTrace;
            if (!File.Exists("errorsLog.txt")) File.WriteAllText("errorsLog.txt", "v." + ver + " [" + DateTime.Now + "]\n" + message + "\n");
            else File.WriteAllText("errorsLog.txt", string.Format("{0}{1}", "v." + ver + " [" + DateTime.Now + "]\n" + message + "\n", File.ReadAllText("errorsLog.txt")));
        }
        public static void errorMessage(Exception exp, string currMethodName)
        {
            MessageBox.Show(
                    "Something went wrong :(",
                    "Error: " + currMethodName.Replace("_", " "),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
    }
}