using System.Collections.Generic;

namespace ItemChecker.Model
{
    public class BuyOrder
    {
        //order
        public static int my_buy_orders { get; set; }
        public static double available_amount { get; set; }
        public static double sum { get; set; }

        public static List<string> item = new List<string>();
        public static List<string> url = new List<string>();
        public static List<string> id = new List<string>();
        public static List<double> price = new List<double>();
        public static List<double> csm_price = new List<double>();
        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();
        //place order
        public static List<string> queue = new List<string>();
        public static double queue_rub { get; set; }
        //push
        public static int tick { get; set; }
        public static int int_check = 0;
        public static int int_push = 0;


        public static void _clear()
        {
            BuyOrder.item.Clear();
            BuyOrder.url.Clear();
            BuyOrder.id.Clear();
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
        public static void _clearPush()
        {
            BuyOrder.tick = 0;
            BuyOrder.int_check = 0;
            BuyOrder.int_push = 0;
        }
        public static void removeAtItem(int index)
        {
            BuyOrder.item.RemoveAt(index);
            BuyOrder.url.RemoveAt(index);
            BuyOrder.id.RemoveAt(index);
            BuyOrder.price.RemoveAt(index);
            BuyOrder.csm_price.RemoveAt(index);
            BuyOrder.precent.RemoveAt(index);
            BuyOrder.difference.RemoveAt(index);
        }
    }
}
