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
using System.Timers;
using System.Drawing;

namespace ItemChecker.Presenter
{
    public class FloatPresenter
    {
        int checkCount = 1;
        public static void stopCheckFloat()
        {
            Float.timer.Enabled = false;
            Float.tick = 1;
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.timer_StripStatus.Visible = false;
                mainForm.floatCheck_groupBox.Visible = false;
                mainForm.floatCheck_MainStripMenu.ForeColor = Color.Black;
            }));
        }
        public void timerTick(Object sender, ElapsedEventArgs e)
        {
            Float.tick--;
            TimeSpan time = TimeSpan.FromSeconds(Float.tick);
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Next check: " + time.ToString("mm':'ss"); }));
            if (Float.tick <= 0)
            {
                if (!Main.loading)
                {
                    Float.timer.Enabled = false;
                    Main.loading = true;

                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Text = "Checking...";
                        mainForm.progressBar_StripStatus.Maximum = Float.floatList.Count;
                        mainForm.progressBar_StripStatus.Value = 0;
                        mainForm.progressBar_StripStatus.Visible = true; }));

                    Main.cts = new();
                    Main.token = Main.cts.Token;

                    ThreadPool.QueueUserWorkItem(Check);
                }
                else
                    FloatPresenter.stopCheckFloat();
            }
        }
        private void Check(object state)
        {
            try
            {
                foreach (string item in Float.floatList)
                {
                    try
                    {
                        string market_hash_name = Edit.replaceUrl(item);
                        getPrice(market_hash_name);

                        string url_request = @"https://steamcommunity.com/market/listings/730/" + market_hash_name + "/render?start=0&count=" + FloatConfig.Default.countGetItems + "&currency=5&language=english&format=json";
                        var json = Get.Request(url_request);

                        JObject obj = JObject.Parse(json);
                        var attributes = obj["listinginfo"].ToList<JToken>();

                        foreach (JToken attribute in attributes)
                        {
                            try
                            {
                                JProperty jProperty = attribute.ToObject<JProperty>();
                                string listing_id = jProperty.Name;

                                decimal subtotal = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_price"].ToString());
                                decimal fee_steam = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_steam_fee"].ToString());
                                decimal fee_csgo = Convert.ToDecimal(JObject.Parse(json)["listinginfo"][listing_id]["converted_publisher_fee"].ToString());
                                decimal fee = fee_steam + fee_csgo;
                                decimal total = subtotal + fee;
                                decimal price = total / 100;

                                Float.precent = Edit.Precent(Float.priceCompare, price);
                                if (Float.precent > FloatConfig.Default.maxFloatPrecent)
                                    break;

                                string ass_id = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["id"].ToString();

                                string link = JObject.Parse(json)["listinginfo"][listing_id]["asset"]["market_actions"][0]["link"].ToString();
                                link = link.Replace("%listingid%", listing_id);
                                link = link.Replace("%assetid%", ass_id);

                                if (item.Contains("Factory New")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FN;
                                else if (item.Contains("Minimal Wear")) Float.maxFloat = FloatConfig.Default.maxFloatValue_MW;
                                else if (item.Contains("Field-Tested")) Float.maxFloat = FloatConfig.Default.maxFloatValue_FT;
                                else if (item.Contains("Well-Worn")) Float.maxFloat = FloatConfig.Default.maxFloatValue_WW;
                                else if (item.Contains("Battle-Scarred")) Float.maxFloat = FloatConfig.Default.maxFloatValue_BS;

                                if (getFloatValue(link) < Float.maxFloat)
                                    buyItem(item, price, listing_id, fee, subtotal, total);
                            }
                            catch
                            {
                                continue;
                            }
                            if (Main.token.IsCancellationRequested)
                                break;
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
                    if (Main.token.IsCancellationRequested)
                        break;
                }
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
            finally
            {
                Main.loading = false;
                Float.tick = FloatConfig.Default.timer * 60;
                Float.timer.Enabled = true;

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.timer_StripStatus.Text = "Next check: 00:00";
                    mainForm.floatCheck_label.Text = $"Check: {checkCount++}";
                    mainForm.progressBar_StripStatus.Visible = false; }));

                if (Main.token.IsCancellationRequested)
                    FloatPresenter.stopCheckFloat();
            }
        }

        private void getPrice(string market_hash_name)
        {
            try
            {
                var json = Get.PriceOverview(market_hash_name);
                Float.lowestPrice = Edit.removeRub(JObject.Parse(json)["lowest_price"].ToString());
                Float.medianPrice = Edit.removeRub(JObject.Parse(json)["median_price"].ToString());

                Tuple<String, Boolean> response = Tuple.Create("", false);
                do
                {
                    response = Get.MrinkaRequest(market_hash_name);
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
        private Decimal getFloatValue(string link)
        {
            try
            {
                var json = Get.Request(@"https://api.csgofloat.com/?url=" + link);
                Float.floatValue = Convert.ToDecimal(JObject.Parse(json)["iteminfo"]["floatvalue"].ToString());
                return Float.floatValue;
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
                return 1;
            }

        }
        private void buyItem(string item, decimal price, string listing_id, decimal fee, decimal subtotal, decimal total)
        {
            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");

            string message = $"Found item:\n{item}\n{Float.lowestPrice}₽ (Lowest price) | {Float.medianPrice}₽ (Median price)\n\nFloat: {Float.floatValue}\nPrice ST: {price}₽ ({Math.Round(Float.lowestPrice - price, 2)}₽)\nPrice CSM: {Float.csmPrice}₽ ({Math.Round(Float.csmPrice - price, 2)}₽)\n\nClick YES if you want to buy the item";
            SystemSounds.Asterisk.Play();

            mainForm.Invoke(new MethodInvoker(delegate {
                DialogResult result = MessageBox.Show(message, "Buy item",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    Main.Browser.ExecuteJavaScript(Post.BuyListing(listing_id, fee, subtotal, total, Main.sessionid));
            }));
        }
    }
}