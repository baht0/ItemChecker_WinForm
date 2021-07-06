using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Reflection;

namespace ItemChecker.Model
{
    public class Main
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static IWebDriver Browser { get; set; }
        public static WebDriverWait wait { get; set; }
        public static string sessionid { get; set; }
        public static double course { get; set; }
        public static string save_str { get; set; }
        public static int reload { get; set; }
        public static bool loading { get; set; }

        public static System.Timers.Timer timer = new System.Timers.Timer();

        //lists
        public static List<string> overstock = new List<string>();
        public static List<string> unavailable = new List<string>();
        internal static List<string> checkList = new List<string>();
        internal static List<string> proxyList = new List<string>();
    }
}
