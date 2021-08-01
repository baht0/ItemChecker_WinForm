using System.Collections.Generic;

namespace ItemChecker.Model
{
    public class TrySkins
    {
        public static List<string> item = new();
        public static List<decimal> sta = new();
        public static List<decimal> csm = new();

        public static List<decimal> precent = new();
        public static List<decimal> difference = new();

        public static int t { get; set; }
        public static string url = "https://table.altskins.com/site/items";

        public static void _clear()
        {
            TrySkins.item.Clear();
            TrySkins.sta.Clear();
            TrySkins.csm.Clear();
            TrySkins.precent.Clear();
            TrySkins.difference.Clear();

            TrySkins.t = 0;
            TrySkins.url = null;
        }
    }
}
