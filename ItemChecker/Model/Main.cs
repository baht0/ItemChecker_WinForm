using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace ItemChecker.Model
{
    public class Main
    {
        public static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static IWebDriver Browser { get; set; }
        public static WebDriverWait wait { get; set; }
        public static string sessionid { get; set; }
        public static string save_str { get; set; }
        public static int reload { get; set; }
        public static bool loading { get; set; }

        public static CancellationTokenSource cts = new();
        public static CancellationToken token = cts.Token;

        //lists
        public static List<string> overstock = new();
        public static List<string> unavailable = new();
        internal static List<string> checkList = new();
        internal static List<string> proxyList = new();
    }
}
