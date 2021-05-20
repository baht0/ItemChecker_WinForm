using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ItemChecker.General;

namespace ItemChecker.NET
{
    public class Request
    {
        //post
        public static String postRequest(string body, string url)
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
        public static String buyListing(string listing_id, double fee, double subtotal, double total, string sessionid)
        {
            string body = "sessionid=" + sessionid + "&currency=5&fee=" + fee.ToString() + "&subtotal=" + subtotal.ToString() + "&total=" + total.ToString() + "&quantity=1&first_name=&last_name=&billing_address=&billing_address_two=&billing_country=&billing_city=&billing_state=&billing_postal_code=&save_my_address=1";
            string url = "https://steamcommunity.com/market/buylisting/" + listing_id;

            return postRequest(body, url);
        }
        public static String createBuyOrder(string hash_name, double last_order, string sessionid)
        {
            string order = (last_order * 100 + 1).ToString();
            string body = "sessionid=" + sessionid + @"&currency=5&appid=730&market_hash_name=" + hash_name + @"&price_total=" + order + @"&quantity=1&billing_state=&save_my_address=0";
            string url = "https://steamcommunity.com/market/createbuyorder/";

            return postRequest(body, url);
        }
        public static String cancelBuyOrder(string buy_orderid, string sessionid)
        {
            string body = "sessionid=" + sessionid + @"&buy_orderid=" + buy_orderid;
            string url = "https://steamcommunity.com/market/cancelbuyorder/";

            return postRequest(body, url);
        }
        public static String acceptTrade(string id, string partner_id, string sessionid)
        {
            string body = "sessionid=" + sessionid + @"&serverid=1&tradeofferid=" + id + @"&partner=" + partner_id + @"&captcha=";
            string url = "https://steamcommunity.com/tradeoffer/" + id + "/accept";

            return postRequest(body, url);
        }

        //get
        public static String getRequest(string url)
        {
            WebRequest reqGET = WebRequest.Create(url);
            WebResponse resp = reqGET.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            var json = sr.ReadToEnd();

            return json;
        }
        public static String mrinkaRequest(string str)
        {
            string url = @"http://188.166.72.201:8080/singleitem?i=" + str;
            return getRequest(url);
        }
        public static String lowPriceRequest(string url, int c)
        {
            return getRequest(@"https://steamcommunity.com/market/priceoverview/?country=RU&currency=" + c + "&appid=730&market_hash_name=" + url);
        }

        public static Double getCourse(string currency_api_key)
        {
            try
            {
                string url = @"https://free.currconv.com/api/v7/convert?q=USD_RUB&compact=ultra&apiKey=" + currency_api_key;
                var json = getRequest(url);

                return Math.Round(Convert.ToDouble(JObject.Parse(json)["USD_RUB"].ToString()), 2);
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, "1.0.0.0");
                return 0.00;
            }
        }

        public List<string> overstock()
        {
            WebClient client = new WebClient();
            var str = client.DownloadString("https://cs.money/list_overstock?appId=730");
            var json = JsonConvert.DeserializeObject<RootObject[]>(str);
            List<string> list = new List<string>();

            foreach (var rootObject in json) list.Add(Edit.replaceUnavailable(rootObject.market_hash_name));
            return list;
        }
        public List<string> unavailable()
        {
            WebClient client = new WebClient();
            var str = client.DownloadString("https://cs.money/list_unavailable?appId=730");
            var json = JsonConvert.DeserializeObject<RootObject[]>(str);
            List<string> list = new List<string>();

            foreach (var rootObject in json) list.Add(Edit.replaceUnavailable(rootObject.market_hash_name));
            return list;
        }
        private class RootObject
        {
            public string market_hash_name { get; set; }
        }
    }
}
