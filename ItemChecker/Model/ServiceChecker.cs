using System.Collections.Generic;
using System.Data;

namespace ItemChecker.Model
{
    public class ServiceChecker
    {
        public static bool checkStop = false;
        public static int service_one, service_two;
        public static DataTable dataTable = new DataTable();

        public static List<string> stUpdated = new List<string>();
        public static List<string> csmUpdated = new List<string>();
        public static List<double> price_one = new List<double>();
        public static List<double> price2_one = new List<double>();
        public static List<double> price_two = new List<double>();
        public static List<double> price2_two = new List<double>();
        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();
        public static List<string> status = new List<string>();
        public static void _clear()
        {
            ServiceChecker.price_one.Clear();
            ServiceChecker.price2_one.Clear();
            ServiceChecker.price_two.Clear();
            ServiceChecker.price2_two.Clear();
            ServiceChecker.precent.Clear();
            ServiceChecker.difference.Clear();
            ServiceChecker.status.Clear();

            ServiceChecker.stUpdated.Clear();
            ServiceChecker.csmUpdated.Clear();
        }
    }
}
