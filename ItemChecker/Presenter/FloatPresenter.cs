using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using ItemChecker.Settings;
using ItemChecker.General;
using ItemChecker.NET;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Media;
using OpenQA.Selenium.Support.Extensions;

namespace ItemChecker.Presenter
{
    public class FloatPresenter
    {
        public static void Check(object state)
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Float Check...";
                mainForm.progressBar_StripStatus.Value = 0;
                mainForm.progressBar_StripStatus.Maximum = Float.items.Count;
                mainForm.progressBar_StripStatus.Visible = true;
                mainForm.status_StripStatus.Visible = true;
            }));

            foreach (string item in Float.items)
            {
                try
                {
                    string url = Edit.replaceUrl(item);
                    getPrice(url);

                    string url_link = @"https://steamcommunity.com/market/listings/730/" + url + "/render?start=0&count=" + FloatConfig.Default.countGetItems + "&currency=5&language=english&format=json";
                    var json = Request.getRequest(url_link);

                    JObject obj = JObject.Parse(json);
                    var attributes = obj["listinginfo"].ToList<JToken>();

                    foreach (JToken attribute in attributes)
                    {
                        JProperty jProperty = attribute.ToObject<JProperty>();
                        string listing_id = jProperty.Name;

                        double subtotal = Convert.ToDouble(JObject.Parse(json)["listinginfo"][listing_id]["converted_price"].ToString());
                        double fee_steam = Convert.ToDouble(JObject.Parse(json)["listinginfo"][listing_id]["converted_steam_fee"].ToString());
                        double fee_csgo = Convert.ToDouble(JObject.Parse(json)["listinginfo"][listing_id]["converted_publisher_fee"].ToString());
                        double fee = fee_steam + fee_csgo;
                        double total = subtotal + fee;
                        double price = total / 100;

                        Float.precent = Math.Round(((price - Float.priceCompare) / price) * 100, 2);
                        if (Float.precent > Convert.ToDouble(FloatConfig.Default.maxFloatPrecent)) break;

                        string ass_id = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["id"].ToString();

                        string link = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["market_actions"][0]["link"].ToString();
                        link = link.Replace("%listingid%", listing_id);
                        link = link.Replace("%assetid%", ass_id);
                        if (item.Contains("Factory New")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FN;
                        if (item.Contains("Minimal Wear")) Float.maxFloat = FloatConfig.Default.maxFloatValue_MW;
                        if (item.Contains("Field-Tested")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FT;
                        if (item.Contains("Well-Worn")) Float.maxFloat = FloatConfig.Default.maxFloatValue_WW;
                        if (item.Contains("Battle-Scarred")) Float.maxFloat = FloatConfig.Default.maxFloatValue_BS;
                        if (getFloatValue(link) < Convert.ToDouble(Float.maxFloat))
                        {
                            buyItem(item, price, listing_id, fee, subtotal, total);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Edit.errorLog(exp, Main.version);
                    continue;
                }
                finally { mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value++; })); }
            }
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Visible = false;
                mainForm.progressBar_StripStatus.Visible = false;
            }));
            Main.loading = false;
        }
        private static void getPrice(string item)
        {
            try
            {
                var json = Request.lowPriceRequest(item, 5);
                Float.lowestPrice = Edit.removeRub(JObject.Parse(json)["lowest_price"].ToString());
                Float.medianPrice = Edit.removeRub(JObject.Parse(json)["median_price"].ToString());

                json = Request.mrinkaRequest(item);
                Float.csmPrice = Convert.ToDouble(JObject.Parse(json)["csm"]["sell"].ToString());
                Float.csmPrice = Math.Round(Float.csmPrice * Main.course, 2);

                if (FloatConfig.Default.priceCompare == 0) Float.priceCompare = Float.lowestPrice;
                if (FloatConfig.Default.priceCompare == 1) Float.priceCompare = Float.medianPrice;
                if (FloatConfig.Default.priceCompare == 2) Float.priceCompare = Float.csmPrice;
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
            }
        }
        private static Double getFloatValue(string link)
        {
            try
            {
                string url = @"https://api.csgofloat.com/?url=" + link;

                var json = Request.getRequest(url);
                Float.floatValue = Convert.ToDouble(JObject.Parse(json)["iteminfo"]["floatvalue"].ToString());
                return Float.floatValue;
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
                return 1;
            }

        }
        private static void buyItem(string item, double price, string listing_id, double fee, double subtotal, double total)
        {
            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
            SystemSounds.Asterisk.Play();
            string message = $"Found item:\n{item}\n{Float.lowestPrice}₽ (Lowest price) | {Float.medianPrice}₽ (Median price)\n\nFloat: {Float.floatValue}\nPrice ST: {price}₽ ({Math.Round(Float.lowestPrice - price, 2)}₽)\nPrice CSM: {Float.csmPrice}₽ ({Math.Round(Float.csmPrice - price, 2)}₽)\n\nClick YES if you want to buy the item";
            DialogResult result = MessageBox.Show(
                message,
                "Buy item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes) Main.Browser.ExecuteJavaScript(Request.buyListing(listing_id, fee, subtotal, total, Main.sessionid));
        }
    }
}
