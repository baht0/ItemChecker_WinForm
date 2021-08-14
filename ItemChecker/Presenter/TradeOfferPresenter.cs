using System;
using System.Threading;
using System.Windows.Forms;
using static ItemChecker.Program;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Net;
using OpenQA.Selenium.Support.Extensions;
using Newtonsoft.Json.Linq;

namespace ItemChecker.Presenter
{
    public class TradeOfferPresenter
    {
        public static void tradeOffers(object state)
        {
            try
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Text = "Check Offers...";
                    mainForm.status_StripStatus.Visible = true;
                }));

                TradeOffer.tradeofferid.Clear();
                TradeOffer.partner_id.Clear();
                
                if (checkOffer()) acceptTrade();
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.tradeOffers_linkLabel.Text = "Incoming: -";
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.status_StripStatus.Visible = false;
                }));
            }
        }
        private static Boolean checkOffer()
        {
            try
            {
                var json = Get.TradeOffers(SteamConfig.Default.steamApiKey);
                int count = ((JArray)JObject.Parse(json)["response"]["trade_offers_received"]).Count;
                for (int i = 0; i < count; i++)
                {
                    int trade_status = Convert.ToInt32(JObject.Parse(json)["response"]["trade_offers_received"][i]["trade_offer_state"].ToString());
                    if (trade_status == 2)
                    {
                        TradeOffer.tradeofferid.Add(JObject.Parse(json)["response"]["trade_offers_received"][i]["tradeofferid"].ToString());
                        TradeOffer.partner_id.Add(JObject.Parse(json)["response"]["trade_offers_received"][i]["accountid_other"].ToString());
                    }
                    else continue;
                }
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Maximum = TradeOffer.tradeofferid.Count;
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.tradeOffers_linkLabel.Text = "Incoming: " + TradeOffer.tradeofferid.Count;
                }));
                return true;
            }
            catch
            {
                return false;
            }            
        }
        private static void acceptTrade()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Accept Trades..."; }));
            for (int i = 0; i < TradeOffer.tradeofferid.Count; i++)
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/tradeoffer/" + TradeOffer.tradeofferid[i]);
                Thread.Sleep(1000);
                Main.Browser.ExecuteJavaScript(Post.AcceptTrade(TradeOffer.tradeofferid[i], TradeOffer.partner_id[i], Main.sessionid));

                MainPresenter.progressInvoke();
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.tradeOffers_linkLabel.Text = "Incoming: " + Convert.ToString(TradeOffer.tradeofferid.Count - (i + 1)); }));
            }
            MainPresenter.messageBalloonTip("Acceptance of trades is complete.");
        }
    }
}