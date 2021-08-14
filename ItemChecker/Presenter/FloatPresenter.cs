using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using ItemChecker.Settings;
using ItemChecker.Support;
using ItemChecker.Net;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Media;
using OpenQA.Selenium.Support.Extensions;
using System.Threading;

namespace ItemChecker.Presenter
{
    public class FloatPresenter
    {
        public static void Check(object state)
        {
            try
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Text = "Float Check...";
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Maximum = Float.items.Count;
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.status_StripStatus.Visible = true; }));
                checkingCycle();
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.assemblyVersion);
                Exceptions.errorMessage(exp, currMethodName);
            }
            finally
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.progressBar_StripStatus.Visible = false; }));
                MainPresenter.messageBalloonTip("FloatCheck completed.");
                Main.loading = false;
            }
        }

        public static void checkingCycle()
        {
            foreach (string item in Float.items)
            {
                try
                {
                    string url = Edit.replaceUrl(item);
                    getPrice(url);

                    string url_request = @"https://steamcommunity.com/market/listings/730/" + url + "/render?start=0&count=" + FloatConfig.Default.countGetItems + "&currency=5&language=english&format=json";
                    var json = Get.Request(url_request);

                    JObject obj = JObject.Parse(json);
                    var attributes = obj["listinginfo"].ToList<JToken>();

                    foreach (JToken attribute in attributes)
                    {
                        JProperty jProperty = attribute.ToObject<JProperty>();
                        string listing_id = jProperty.Name;

                        decimal subtotal = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_price"].ToString());
                        decimal fee_steam = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_steam_fee"].ToString());
                        decimal fee_csgo = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_publisher_fee"].ToString());
                        decimal fee = fee_steam + fee_csgo;
                        decimal total = subtotal + fee;
                        decimal price = total / 100;

                        Float.precent = Math.Round(((price - Float.priceCompare) / price) * 100, 2);
                        if (Float.precent > Convert.ToDecimal(FloatConfig.Default.maxFloatPrecent)) break;

                        string ass_id = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["id"].ToString();

                        string link = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["market_actions"][0]["link"].ToString();
                        link = link.Replace("%listingid%", listing_id);
                        link = link.Replace("%assetid%", ass_id);
                        if (item.Contains("Factory New")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FN;
                        else if (item.Contains("Minimal Wear")) Float.maxFloat = FloatConfig.Default.maxFloatValue_MW;
                        else if (item.Contains("Field-Tested")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FT;
                        else if (item.Contains("Well-Worn")) Float.maxFloat = FloatConfig.Default.maxFloatValue_WW;
                        else if (item.Contains("Battle-Scarred")) Float.maxFloat = FloatConfig.Default.maxFloatValue_BS;
                        if (getFloatValue(link) < Convert.ToDecimal(Float.maxFloat))
                        {
                            buyItem(item, price, listing_id, fee, subtotal, total);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Exceptions.errorLog(exp, Main.assemblyVersion);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }            
        }
        private static void getPrice(string item)
        {
            try
            {
                var json = Get.PriceOverview(item, 5);
                Float.lowestPrice = Edit.removeRub(JObject.Parse(json)["lowest_price"].ToString());
                Float.medianPrice = Edit.removeRub(JObject.Parse(json)["median_price"].ToString());

                Tuple<String, Boolean> response = Tuple.Create("", false);
                do
                {
                    response = Get.MrinkaRequest(Edit.replaceUrl(item));
                    if (!response.Item2)
                    {
                        mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Float Check (429). Please Wait..."; }));
                        Thread.Sleep(30000);
                    }
                }
                while (!response.Item2);
                Float.csmPrice = Convert.ToDecimal(JObject.Parse(response.Item1)["csm"]["sell"].ToString());
                Float.csmPrice = Math.Round(Float.csmPrice * GeneralConfig.Default.currency, 2);

                if (FloatConfig.Default.priceCompare == 0) Float.priceCompare = Float.lowestPrice;
                if (FloatConfig.Default.priceCompare == 1) Float.priceCompare = Float.medianPrice;
                if (FloatConfig.Default.priceCompare == 2) Float.priceCompare = Float.csmPrice;
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        private static Decimal getFloatValue(string link)
        {
            try
            {
                string url = @"https://api.csgofloat.com/?url=" + link;

                var json = Get.Request(url);
                Float.floatValue = Convert.ToDecimal(JObject.Parse(json)["iteminfo"]["floatvalue"].ToString());
                return Float.floatValue;
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
                return 1;
            }

        }
        private static void buyItem(string item, decimal price, string listing_id, decimal fee, decimal subtotal, decimal total)
        {
            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
            SystemSounds.Asterisk.Play();
            string message = $"Found item:\n{item}\n{Float.lowestPrice}₽ (Lowest price) | {Float.medianPrice}₽ (Median price)\n\nFloat: {Float.floatValue}\nPrice ST: {price}₽ ({Math.Round(Float.lowestPrice - price, 2)}₽)\nPrice CSM: {Float.csmPrice}₽ ({Math.Round(Float.csmPrice - price, 2)}₽)\n\nClick YES if you want to buy the item";
            DialogResult result = MessageBox.Show(
                message,
                "Buy item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes) Main.Browser.ExecuteJavaScript(Post.BuyListing(listing_id, fee, subtotal, total, Main.sessionid));
        }
    }
}
