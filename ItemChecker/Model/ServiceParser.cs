using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace ItemChecker.Model
{
    public class ServiceParser
    {
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

        public static CancellationTokenSource cts = new();
        public static CancellationToken token = cts.Token;

        public static void _clear()
        {
            ServiceParser.stUpdated.Clear();
            ServiceParser.csmUpdated.Clear();

            ServiceParser.price_one.Clear();
            ServiceParser.price2_one.Clear();
            ServiceParser.price_two.Clear();
            ServiceParser.price2_two.Clear();
            ServiceParser.precent.Clear();
            ServiceParser.difference.Clear();
            ServiceParser.status.Clear();
        }
    }
}
