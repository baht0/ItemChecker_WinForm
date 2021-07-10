﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace ItemChecker.Support
{
    public class Edit
    {
        //url
        public static String replaceUrl(string str)
        {
            str = str.Replace("★", "%E2%98%85");
            str = str.Replace("™", "%E2%84%A2");
            str = str.Replace(" ", "%20");
            str = str.Replace("|", "%7C");
            str = str.Replace("(", "%28");
            str = str.Replace(")", "%29");
            str = str.Replace("\r\n", "\n");

            return str;
        }
        public static void openUrl(string url)
        {
            var psi = new ProcessStartInfo(url)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(psi);
        }

        //remove
        public static Double removeRub(string rub)
        {
            string str = rub.Replace(",", ".");
            return Convert.ToDouble(str.Substring(0, str.Length - 5));
        }
        public static Double removeDol(string dol)
        {
            string str = dol.Replace("$", "");
            return Convert.ToDouble(str.Replace(" USD", ""));
        }
        public static Double removeSymbol(string str)
        {
            str = str.Replace(",", ".");
            return Convert.ToDouble(str.Substring(0, str.Length - 1));
        }
        public static String buyOrderId(string idOrder)
        {
            return idOrder.Replace("mybuyorder_", "");
        }

        public static String replaceSymbols(string str)
        {
            str = str.Replace("â„¢", "™");
            str = str.Replace("â˜…", "★");
            return str;
        }
        public static String currencyConverter(string str, double currency)
        {
            double value = Convert.ToDouble(removeSymbol(str), CultureInfo.InvariantCulture);
            value = Math.Round(value * currency, 2);

            return value.ToString() + "₽";
        }
        public static String funcConvert(double value, double currency)
        {
            value = Math.Round(value * currency, 2);

            return value.ToString() + "₽";
        }
        public static Double Precent(double a, double b) //from A to B
        {
            if (a != 0)
                return Math.Round(((b - a) / a) * 100, 2);
            else
                return 0;
        }
        public static Double Difference(double a, double b)
        {
            return Math.Round((a - b), 2);
        }
        public static Double Difference(double a, double b, double currency)
        {
            return Math.Round((a - b) * currency, 2);
        }

        //time
        public static String convertTime(double time)
        {
            DateTime start = UnixTimeToDateTime(time);
            DateTime end = DateTime.Now;
            double tick = (end - start).TotalSeconds;

            TimeSpan tm = TimeSpan.FromSeconds(tick);
            return tm.ToString("hh':'mm");
        }
        public static DateTime UnixTimeToDateTime(double unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }
        public static String calcTimeLeft(DateTime start, int count, int i)
        {
            double min = (count - ++i) / calcTimeLeftSpeed(start, i);
            TimeSpan time = TimeSpan.FromMinutes(min);
            if (min > 60)
                return time.ToString("hh'h 'mm'min'");
            else if (min > 1)
                return time.ToString("mm'min 'ss'sec.'");
            else
                return time.ToString("ss'sec.'");
        }
        public static Double calcTimeLeftSpeed(DateTime start, int i)
        {
            DateTime now = DateTime.Now;
            var time_passed = now.Subtract(start).TotalMinutes;
            return Math.Round(++i / time_passed, 2);
        }
    }
}
