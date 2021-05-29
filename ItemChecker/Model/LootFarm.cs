using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChecker.Model
{
    public class LootFarm
    {
        public static List<string> item = new List<string>();
        public static List<double> price = new List<double>();
        public static List<double> price_st = new List<double>();
        public static List<double> buy_order = new List<double>();
        public static List<double> get_price = new List<double>();
        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();
        public static List<string> status = new List<string>();

        public static void _clear()
        {
            LootFarm.item.Clear();
            LootFarm.price.Clear();
            LootFarm.price_st.Clear();
            LootFarm.buy_order.Clear();
            LootFarm.get_price.Clear();
            LootFarm.precent.Clear();
            LootFarm.difference.Clear();
            LootFarm.status.Clear();
        }
    }
}
