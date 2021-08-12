using System;
using System.IO;
using System.Net;

namespace ItemChecker.Net
{
    public class Post
    {
        public static String FetchRequest(string contentType, string body, string url)
        {
            string js_fetch = @"
                async function postReq(url) {
                    const response = await fetch(url, {
                        method: 'POST',
                        headers: { 'Content-Type': '" + contentType + @"; charset=UTF-8' },
                        body: '" + body + @"',
                        credentials: 'include' });
                return response.json(); }
                postReq('" + url + @"').then(data => console.log(data));";

            return js_fetch;
        }
        public static String FetchRequestWithResponse(string contentType, string body, string url)
        {
            string js_fetch = @"
                async function postReq(url) {
                    const response = await fetch(url, {
                        method: 'POST',
                        headers: { 'Content-Type': '" + contentType + @"; charset=UTF-8' },
                        body: '" + body + @"',
                        credentials: 'include' });
                return response.json(); }
                postReq('" + url + @"').then(data => {
                        var json = JSON.stringify(data);
                        document.open();
                        document.write('<html><body><pre>' + json + '</pre></body></html>');
                        document.close();
                        });";

            return js_fetch;
        }
        public static String BuyListing(string listing_id, decimal fee, decimal subtotal, decimal total, string sessionid)
        {
            string body = $"sessionid={sessionid}&currency=5&fee={fee}&subtotal={subtotal}&total={total}&quantity=1&first_name=&last_name=&billing_address=&billing_address_two=&billing_country=&billing_city=&billing_state=&billing_postal_code=&save_my_address=1";
            string url = "https://steamcommunity.com/market/buylisting/" + listing_id;

            return FetchRequest("application/x-www-form-urlencoded", body, url);
        }
        public static String CreateBuyOrder(string market_hash_name, decimal last_order, string sessionid)
        {
            string price_total = (last_order * 100 + 1).ToString();
            string body = "sessionid=" + sessionid + @"&currency=5&appid=730&market_hash_name=" + market_hash_name + @"&price_total=" + price_total + @"&quantity=1&billing_state=&save_my_address=0";
            string url = "https://steamcommunity.com/market/createbuyorder/";

            return FetchRequest("application/x-www-form-urlencoded", body, url);
        }
        public static String CancelBuyOrder(string buy_orderid, string sessionid)
        {
            string body = $"sessionid={sessionid}&buy_orderid={buy_orderid}";
            string url = "https://steamcommunity.com/market/cancelbuyorder/";

            return FetchRequest("application/x-www-form-urlencoded", body, url);
        }
        public static String AcceptTrade(string tradeofferid, string partner_id, string sessionid)
        {
            string body = "sessionid=" + sessionid + @"&serverid=1&tradeofferid=" + tradeofferid + @"&partner=" + partner_id + @"&captcha=";
            string url = "https://steamcommunity.com/tradeoffer/" + tradeofferid + "/accept";

            return FetchRequest("application/x-www-form-urlencoded", body, url);
        }
        public static String RequestDropbox()
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("https://content.dropboxapi.com/2/files/download");

            httpRequest.Headers["Dropbox-API-Arg"] = "{\"path\": \"/info.xml\"}";
            httpRequest.Headers["Authorization"] = "Bearer a94CSH6hwyUAAAAAAAAAAf3zRyhyZknI9J8KM3VZihWEILAuv6Vr3ht_-4RQcJxs";

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream stream = httpResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string s = httpResponse.StatusCode.ToString();

            return streamReader.ReadToEnd();
        }
    }
}
