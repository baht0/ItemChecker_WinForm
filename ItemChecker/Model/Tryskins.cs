using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChecker.Model
{
    public class TrySkins
    {
        public static List<string> item = new List<string>();
        public static List<double> sta = new List<double>();
        public static List<double> csm = new List<double>();

        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();

        public static int count { get; set; }
        public static int t { get; set; }
        public static string url = "https://table.altskins.com/site/items";

        public static void _clear()
        {
            TrySkins.item.Clear();
            TrySkins.sta.Clear();
            TrySkins.csm.Clear();
            TrySkins.precent.Clear();
            TrySkins.difference.Clear();

            TrySkins.count = 0;
            TrySkins.t = 0;
            TrySkins.url = null;
        }
    }
}
