using System.Collections.Generic;
using System.Data;

namespace ItemChecker.Model
{
    public class ServiceChecker
    {
        public static bool checkStop = false;
        public static int service_one, service_two;

        public static List<double> stUpdated = new();
        public static List<double> csmUpdated = new();

        public static DataTable dataTable = new();
        public static List<decimal> price_one = new();
        public static List<decimal> price2_one = new();
        public static List<decimal> price_two = new();
        public static List<decimal> price2_two = new();
        public static List<decimal> precent = new();
        public static List<decimal> difference = new();
        public static List<string> status = new();

        public static void _clear()
        {
            ServiceChecker.stUpdated.Clear();
            ServiceChecker.csmUpdated.Clear();

            ServiceChecker.price_one.Clear();
            ServiceChecker.price2_one.Clear();
            ServiceChecker.price_two.Clear();
            ServiceChecker.price2_two.Clear();
            ServiceChecker.precent.Clear();
            ServiceChecker.difference.Clear();
            ServiceChecker.status.Clear();
        }
    }
}
