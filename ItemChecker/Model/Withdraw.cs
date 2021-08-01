using System.Collections.Generic;

namespace ItemChecker.Model
{
    public class Withdraw
    {
        public static List<string> item = new();
        public static List<int> sales = new();
        public static List<decimal> csm = new();
        public static List<decimal> st = new();
        public static List<decimal> precent = new();
        public static int souvenir { get; set; }
        public static int sticker { get; set; }
        public static string url { get; set; }

        //auto
        public static List<string> favoriteItems = new();
        public static List<decimal> favoritePrices = new();
        public static System.Timers.Timer timer = new();
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
