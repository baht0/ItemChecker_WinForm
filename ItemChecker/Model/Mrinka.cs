﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ItemChecker.Model
{
    public class Mrinka
    {
        public static List<double> sellOrder = new List<double>();
        public static List<double> buyOrder = new List<double>();
        public static List<double> csmSell = new List<double>();
        public static List<string> csmBuy = new List<string>();
        public static List<double> precent = new List<double>();
        public static List<double> difference = new List<double>();

        public static List<string> stUpdated = new List<string>();
        public static List<string> csmUpdated = new List<string>();

        public static void _clear()
        {
            Mrinka.sellOrder.Clear();
            Mrinka.buyOrder.Clear();
            Mrinka.csmSell.Clear();
            Mrinka.csmBuy.Clear();
            Mrinka.precent.Clear();
            Mrinka.difference.Clear();

            Mrinka.stUpdated.Clear();
            Mrinka.csmUpdated.Clear();
        }
    }
}
