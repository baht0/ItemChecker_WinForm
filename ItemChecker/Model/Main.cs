using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ItemChecker.Model
{
    public class Main
    {
        public static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static IWebDriver Browser { get; set; }
        public static System.Timers.Timer timer = new System.Timers.Timer();
        public static string sessionid { get; set; }
        public static double course { get; set; }
        public static string save_str { get; set; }
        public static int reload { get; set; }
        public static bool loading { get; set; }

        //lists
        public static List<string> overstock = new List<string>();
        public static List<string> unavailable = new List<string>();
        internal static List<string> checkList = new List<string>();
    }
}
