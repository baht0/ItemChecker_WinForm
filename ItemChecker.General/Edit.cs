using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ItemChecker.General
{
    public class Edit
    {
        //public static String getVersion()
        //{
        //    return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //}

        //url
        public static String replaceUrl(string str)
        {
            str = str.Replace("★", "%E2%98%85");
            str = str.Replace("™", "%E2%84%A2");
            str = str.Replace(" ", "%20");
            str = str.Replace("|", "%7C");
            str = str.Replace("(", "%28");
            str = str.Replace(")", "%29");
            str = str.Replace("\r\n", "\n");

            return str;
        }
        public static String inverReplaceUrl(string str)
        {
            str = str.Replace("%E2%98%85", "★");
            str = str.Replace("%E2%84%A2", "™");
            str = str.Replace("%20", " ");
            str = str.Replace("%7C", "|");
            str = str.Replace("%28", "(");
            str = str.Replace("%29", ")");

            return str;
        }

        //remove
        public static Double removeRub(string rub)
        {
            string str = rub.Replace(",", ".");
            return Convert.ToDouble(str.Substring(0, str.Length - 5));
        }
        public static Double removeDol(string dol)
        {
            string str = dol.Replace("$", "");
            return Convert.ToDouble(str.Replace(" USD", ""));
        }
        public static Double removeSymbol(string str)
        {
            str = str.Replace(",", ".");
            return Convert.ToDouble(str.Substring(0, str.Length - 1));
        }
        public static String buyOrderId(string idOrder)
        {
            return idOrder.Replace("mybuyorder_", "");
        }
        public static String tradeOfferId(string str)
        {
            return str.Replace("tradeofferid_", "");
        }

        public static String replaceUnavailable(string str)
        {
            str = str.Replace("â„¢", "™");
            str = str.Replace("â˜…", "★");
            return str;
        }
        public static String funcConvert(string str, double currency)
        {
            double value = Convert.ToDouble(removeSymbol(str), CultureInfo.InvariantCulture);
            value = Math.Round(value * currency, 2);

            return value.ToString() + "₽";
        }
        public static Double difference(double csm, double st, double currency)
        {
            return Math.Round((csm - st) * currency, 2);
        }

        //time
        public static String convertTime(double time)
        {
            DateTime end = DateTime.Now;
            DateTime start = UnixTimeToDateTime(time);
            double tick = (end - start).TotalSeconds;

            TimeSpan tm = TimeSpan.FromSeconds(tick);
            return tm.ToString("hh':'mm");
        }
        public static DateTime UnixTimeToDateTime(double unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        public static void invokeLabel(Label label, string text)
        {
            // If the current thread is not the UI thread, InvokeRequired will be true
            if (label.InvokeRequired)
            {
                // If so, call Invoke, passing it a lambda expression which calls
                // Edit with the same label and text, but on the UI thread instead.
                label.Invoke((Action)(() => invokeLabel(label, text)));
                return;
            }
            // If we're running on the UI thread, we'll get here, and can safely update 
            // the label's text.
            label.Text = text;
        }

        //Exception
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
