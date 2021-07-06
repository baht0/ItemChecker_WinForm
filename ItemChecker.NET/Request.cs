using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ItemChecker.Support;

namespace ItemChecker.Net
{
    public class Request
    {
        //post
        public static String PostRequest(string body, string url)
        {
            string js_post = @"
                async function postBuyOrder(url = '') {
                    const response = await fetch(url, {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' },
                        body: '" + body + @"',
                        credentials: 'include'
                    });
                return response.json();
                }
                postBuyOrder('" + url + @"').then(data => { return data; });";

            return js_post;
        }
        public static String BuyListing(string listing_id, double fee, double subtotal, double total, string sessionid)
        {
            string body = "sessionid=" + sessionid + "&currency=5&fee=" + fee.ToString() + "&subtotal=" + subtotal.ToString() + "&total=" + total.ToString() + "&quantity=1&first_name=&last_name=&billing_address=&billing_address_two=&billing_country=&billing_city=&billing_state=&billing_postal_code=&save_my_address=1";
            string url = "https://steamcommunity.com/market/buylisting/" + listing_id;

            return PostRequest(body, url);
        }
        public static String CreateBuyOrder(string market_hash_name, double last_order, string sessionid)
        {
            string price_total = (last_order * 100 + 1).ToString();
            string body = "sessionid=" + sessionid + @"&currency=5&appid=730&market_hash_name=" + market_hash_name + @"&price_total=" + price_total + @"&quantity=1&billing_state=&save_my_address=0";
            string url = "https://steamcommunity.com/market/createbuyorder/";

            return PostRequest(body, url);
        }
        public static String CancelBuyOrder(string buy_orderid, string sessionid)
        {
            string body = "sessionid=" + sessionid + @"&buy_orderid=" + buy_orderid;
            string url = "https://steamcommunity.com/market/cancelbuyorder/";

            return PostRequest(body, url);
        }
        public static String AcceptTrade(string tradeofferid, string partner_id, string sessionid)
        {
            string body = "sessionid=" + sessionid + @"&serverid=1&tradeofferid=" + tradeofferid + @"&partner=" + partner_id + @"&captcha=";
            string url = "https://steamcommunity.com/tradeoffer/" + tradeofferid + "/accept";

            return PostRequest(body, url);
        }

        //get
        public static String GetRequest(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            var json = sr.ReadToEnd();

            return json;
        }
        public static String GetRequest(string url, string address)
        {
            WebRequest request = WebRequest.Create(url);
            request.Proxy = new WebProxy(address.Trim(), true);
            request.Timeout = 5000;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            var json = sr.ReadToEnd();

            return json;
        }
        public static Tuple<String, Boolean> MrinkaRequest(string str)
        {
            try
            {
                string url = @"http://188.166.72.201:8080/singleitem?i=" + str;
                return Tuple.Create(GetRequest(url), true);
            }
            catch {
                return Tuple.Create("", false); 
            }
        }
        public static String PriceOverview(string market_hash_name, int currency)
        {
            return GetRequest(@"https://steamcommunity.com/market/priceoverview/?country=RU&currency=" + currency + "&appid=730&market_hash_name=" + market_hash_name);
        }
        public static String GetTradeOffers(string steam_api_key)
        {
            return GetRequest(@"http://api.steampowered.com/IEconService/GetTradeOffers/v1/?key=" + steam_api_key + "&get_received_offers=1&active_only=100");
        }
        public static Int32 ItemNameId(string market_hash_name)
        {
            var html = GetRequest("https://steamcommunity.com/market/listings/730/" + market_hash_name);
            html = html.Substring(html.IndexOf("Market_LoadOrderSpread"));
            var a = html.IndexOf("(");
            var b = html.IndexOf(")");
            string str = html.Substring(a, b);

            int id = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(str, @"[^\d]+", ""));
            return id;
        }
        public static Double ItemOrdersHistogram(int item_nameid)
        {
            var json = GetRequest("https://steamcommunity.com/market/itemordershistogram?country=RU&language=english&currency=5&item_nameid=" + item_nameid + "&two_factor=0");

            var highest_buy_order = Convert.ToDouble(JObject.Parse(json)["highest_buy_order"].ToString());
            return highest_buy_order / 100;
        }

        public static Double GetCourse(string currency_api_key)
        {
            try
            {
                string url = @"https://free.currconv.com/api/v7/convert?q=USD_RUB&compact=ultra&apiKey=" + currency_api_key;
                var json = GetRequest(url);

                return Math.Round(Convert.ToDouble(JObject.Parse(json)["USD_RUB"].ToString()), 2);
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, "1.0.0.0");
                return 0.00;
            }
        }

        public List<string> GetOverstock()
        {
            WebClient client = new WebClient();
            var str = client.DownloadString("https://cs.money/list_overstock?appId=730");
            var json = JsonConvert.DeserializeObject<RootObject[]>(str);
            List<string> list = new List<string>();

            foreach (var rootObject in json) list.Add(Edit.replaceSymbols(rootObject.market_hash_name));
            return list;
        }
        public List<string> GetUnavailable()
        {
            WebClient client = new WebClient();
            var str = client.DownloadString("https://cs.money/list_unavailable?appId=730");
            var json = JsonConvert.DeserializeObject<RootObject[]>(str);
            List<string> list = new List<string>();

            foreach (var rootObject in json) list.Add(Edit.replaceSymbols(rootObject.market_hash_name));
            return list;
        }
        private class RootObject
        {
            public string market_hash_name { get; set; }
        }
    }
}