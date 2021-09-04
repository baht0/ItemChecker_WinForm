using System.Collections.Generic;

namespace ItemChecker.Model
{
    public class TradeOffer
    {
        public static List<string> tradeofferid = new List<string>();
        public static List<string> partner_id = new List<string>();

        public static void _clear()
        {
            TradeOffer.tradeofferid.Clear();
            TradeOffer.partner_id.Clear();
        }
    }
}
