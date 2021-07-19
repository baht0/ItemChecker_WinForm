using System.Collections.Generic;
using System.Timers;

namespace ItemChecker.Model
{
    public class Withdraw
    {
        public static List<string> item = new List<string>();
        public static List<int> sales = new List<int>();
        public static List<double> csm = new List<double>();
        public static List<double> st = new List<double>();
        public static List<double> precent = new List<double>();
        public static int souvenir { get; set; }
        public static int sticker { get; set; }
        public static string url { get; set; }

        //auto
        public static List<string> favoriteList = new();
        public static Timer timer = new();
        public static int tick { get; set; }

        public static void _clear()
        {
            Withdraw.item.Clear();
            Withdraw.sales.Clear();
            Withdraw.csm.Clear();
            Withdraw.st.Clear();
            Withdraw.precent.Clear();

            Withdraw.souvenir = 0;
            Withdraw.sticker = 0;
        }
    }
}
