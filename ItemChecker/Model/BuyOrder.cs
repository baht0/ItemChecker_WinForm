using System.Collections.Generic;
using System.Timers;

namespace ItemChecker.Model
{
    public class BuyOrder
    {
        //order
        public static int my_buy_orders { get; set; }
        public static decimal available_amount { get; set; }
        public static decimal sum { get; set; }

        public static List<string> item = new List<string>();
        public static List<string> order_id = new List<string>();
        public static List<decimal> price = new List<decimal>();
        public static List<decimal> csm_price = new List<decimal>();
        public static List<decimal> precent = new List<decimal>();
        public static List<decimal> difference = new List<decimal>();
        //place order
        public static List<string> queue = new List<string>();
        public static decimal queue_rub { get; set; }
        //push
        public static Timer timer = new();
        public static int tick { get; set; }

        public static void _clear()
        {
            BuyOrder.item.Clear();
            BuyOrder.order_id.Clear();
            BuyOrder.price.Clear();
            BuyOrder.csm_price.Clear();
            BuyOrder.precent.Clear();
            BuyOrder.difference.Clear();

            BuyOrder.sum = 0;
            BuyOrder.available_amount = 0;
        }
        public static void _clearQueue()
        {
            BuyOrder.queue.Clear();
            BuyOrder.queue_rub = 0;
        }
        public static void removeAtItem(int index)
        {
            BuyOrder.item.RemoveAt(index);
            BuyOrder.order_id.RemoveAt(index);
            BuyOrder.price.RemoveAt(index);
            BuyOrder.csm_price.RemoveAt(index);
            BuyOrder.precent.RemoveAt(index);
            BuyOrder.difference.RemoveAt(index);
        }
    }
}
