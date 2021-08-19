using System.Collections.Generic;

namespace ItemChecker.Model
{
    public class Float
    {
        public static decimal lowestPrice { get; set; }
        public static decimal medianPrice { get; set; }
        public static decimal csmPrice { get; set; }
        public static decimal precent { get; set; }
        public static decimal floatValue { get; set; }
        public static decimal maxFloat { get; set; }
        public static decimal priceCompare { get; set; }

        //auto
        public static List<string> floatList = new List<string>();
        public static System.Timers.Timer timer = new();
        public static int tick { get; set; }
        //public static List<string> listingId = new List<string>();
        //public static List<string> fee = new List<string>();
        //public static List<string> subtotal = new List<string>();
        //public static List<string> total = new List<string>();
    }
}
