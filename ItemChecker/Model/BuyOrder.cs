using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChecker.Model
{
    public class BuyOrder
    {
        //order
        public static int count { get; set; }
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
        public static List<string> ordered = new List<string>();

        public static double order_dol { get; set; }
        public static double order_rub { get; set; }
        public static int queue_count { get; set; }
        public static bool place { get; set; }
        //push
        public static int tick { get; set; }
        public static int int_check = 0;
        public static int int_push = 0;
        public static int int_catch = 0;
    }
}
