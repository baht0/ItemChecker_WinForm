using System;
using System.Threading;
using System.Windows.Forms;
using static ItemChecker.Program;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Net;
using System.Globalization;
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
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                checkOffer();
                if (TradeOffer.tradeofferid.Count > 0) acceptTrade();
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
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
        private static void checkOffer()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Check Offers...";
                mainForm.status_StripStatus.Visible = true;
            }));

            TradeOffer.tradeofferid.Clear();
            TradeOffer.partner_id.Clear();

            var json = Request.tradeOffers(GeneralConfig.Default.steamApiKey);
            var count = ((JArray)JObject.Parse(json)["response"]["trade_offers_received"]).Count;            

            for (int i = 0; i < count; i++)
            {
                TradeOffer.tradeofferid.Add(JObject.Parse(json)["response"]["trade_offers_received"][i]["tradeofferid"].ToString());
                TradeOffer.partner_id.Add(JObject.Parse(json)["response"]["trade_offers_received"][i]["accountid_other"].ToString());
            }
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.tradeOffers_linkLabel.Text = "Incoming: " + TradeOffer.tradeofferid.Count.ToString(); }));
        }
        private static void acceptTrade()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Accept Trades...";
            }));
            for (int i = 0; i < TradeOffer.tradeofferid.Count; i++)
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/tradeoffer/" + TradeOffer.tradeofferid[i]);
                Thread.Sleep(1000);

                Main.Browser.ExecuteJavaScript(Request.acceptTrade(TradeOffer.tradeofferid[i], TradeOffer.partner_id[i], Main.sessionid));

                MainPresenter.progressInvoke();
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.tradeOffers_linkLabel.Text = "Incoming: " + Convert.ToString(TradeOffer.tradeofferid.Count - (i + 1)); }));
            }
        }
    }
}