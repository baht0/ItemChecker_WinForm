using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ItemChecker.Support
{
    public class Edit
    {
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
        public static void openUrl(string url)
        {
            var psi = new ProcessStartInfo(url)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(psi);
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
