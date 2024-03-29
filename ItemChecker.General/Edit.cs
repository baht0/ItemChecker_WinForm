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
        public static void openCsm(string market_hash_name, bool old)
        {
            string url;
            if (!old)
            {
                string stattrak = "false";
                string souvenir = "false";
                if (market_hash_name.Contains("StatTrak"))
                    stattrak = "true";
                if (market_hash_name.Contains("Souvenir"))
                    souvenir = "true";

                url = "https://cs.money/csgo/trade/?search=" + market_hash_name + "&sort=price&order=asc&hasRareFloat=false&hasRareStickers=false&hasRarePattern=false&hasTradeLock=true&isStatTrak=" + stattrak + "&isMarket=false&isSouvenir=" + souvenir;
            }
            else
                url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=" + market_hash_name;
            openUrl(url);
        }

        //remove
        public static Decimal removeRub(string str)
        {
            str = str.Replace(",", ".");
            decimal rub = Convert.ToDecimal(str[0..^5], CultureInfo.InvariantCulture);
            return rub;
        }
        public static Decimal removeDol(string str)
        {
            str = str.Replace("$", null);
            decimal dol = Convert.ToDecimal(str.Replace(" USD", ""), CultureInfo.InvariantCulture);
            return dol;
        }
        public static Decimal removeSymbol(string str)
        {
            str = str.Replace(",", ".");
            return Convert.ToDecimal(str.Substring(0, str.Length - 1), CultureInfo.InvariantCulture);
        }
        public static String buyOrderId(string idOrder)
        {
            return idOrder.Replace("mybuyorder_", "");
        }
        public static Int32 ItemNameId(string html)
        {
            html = html.Substring(html.IndexOf("Market_LoadOrderSpread"));
            var a = html.IndexOf("(");
            var b = html.IndexOf(")");
            string str = html.Substring(a, b);

            int id = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(str, @"[^\d]+", ""));
            return id;
        }

        public static String replaceSymbols(string str)
        {
            str = str.Replace("â„¢", "™");
            str = str.Replace("â˜…", "★");
            return str;
        }
        public static String currencyConverter(string str, decimal currency)
        {
            Decimal value = Convert.ToDecimal(removeSymbol(str), CultureInfo.InvariantCulture);
            value = Math.Round(value * currency, 2);

            return value.ToString() + "₽";
        }
        public static String funcConvert(decimal value, decimal currency)
        {
            value = Math.Round(value * currency, 2);

            return value.ToString() + "₽";
        }
        public static Decimal Precent(decimal a, decimal b) //from A to B
        {
            if (a != 0)
                return Math.Round(((b - a) / a) * 100, 2);
            else
                return 0;
        }
        public static Decimal Difference(decimal a, decimal b)
        {
            return Math.Round((a - b), 2);
        }
        public static Decimal Difference(decimal a, decimal b, decimal currency)
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
