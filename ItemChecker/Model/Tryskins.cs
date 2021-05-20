using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChecker.Model
{
    public class TrySkins
    {
        public static int count { get; set; }
        public static string url { get; set; }
        public static int t { get; set; }

        public static List<string> item = new List<string>();
        public static List<double> sta = new List<double>();
        public static List<double> csm = new List<double>();

        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();
    }
}
