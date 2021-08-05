using System.Collections.Generic;
using System.Windows.Forms;

namespace ItemChecker.Model
{
    public  class Filters
    {
        public static string filter = string.Empty;

        public static List<int> category = new();
        public static List<int> status = new();
        public static List<int> exterior = new();
        public static List<int> types = new();

        public static CheckState hide_category, hide_status, hide_exterior, hide_types = CheckState.Unchecked;

        public static List<string> prices = new();

        public static bool price_1, price_2, price_3, price_4 = false;

        public static decimal price_1From, price_1To = 0.0m;
        public static decimal price_2From, price_2To = 0.0m;
        public static decimal price_3From, price_3To = 0.0m;
        public static decimal price_4From, price_4To = 0.0m;

        public static decimal precentFrom, precentTo = 0.00m;
        public static decimal diffFrom, diffTo = 0.00m;

        public static bool hide_0, hide_100 = true;

        public static bool close = false;

        public static void _clear()
        {
            Filters.category.Clear();
            Filters.status.Clear();
            Filters.exterior.Clear();
            Filters.types.Clear();
        }
        public static void _clearAll()
        {
            _clear();
            hide_category = hide_status = hide_exterior = hide_types = CheckState.Unchecked;

            price_1 = price_2 = price_3 = price_4 = false;
            price_1From = price_1To = price_2From = price_2To = price_3From = price_3To = price_4From = price_4To = 0.0m;

            precentFrom = precentTo = diffFrom = diffTo = 0.00m;
            hide_0 = hide_100 = true;

            filter = string.Empty;
        }
    }
}
