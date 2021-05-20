using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Windows.Forms;
using static ItemChecker.Program;
using ItemChecker.General;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.NET;
using System.Globalization;
using OpenQA.Selenium.Support.Extensions;

namespace ItemChecker.Presenter
{
    public class TradeOfferPresenter
    {
        public static void tradeOffers(object state)
        {
            try
            {
                Main.loading = true;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                checkOffer();
                if (TradeOffer.count > 0)
                {
                    getOfferId();
                    acceptTrade();
                }
            }
            catch (Exception exp)
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.tradeOffers_linkLabel.Text = "Incoming: -"; }));

                Edit.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.tradeOffers_linkLabel.Enabled = true;
                }));
            }
        }
        private static void checkOffer()
        {
            try
            {
                TradeOffer.count = 0;
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/my/tradeoffers/");

                WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
                IWebElement offers = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='profile_rightcol']/div/div[1]/div[3]/a/div[3]")));

                string str = offers.Text;

                str = str.Replace("Pending", "");
                str = str.Replace(" ", "");
                str = str.Replace("\n", "");
                str = str.Replace("\t", "");
                str = str.Replace("(", "");
                int pos = str.LastIndexOf(')');
                str = str.Substring(0, pos);

                TradeOffer.count = Convert.ToInt32(str);
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.tradeOffers_linkLabel.Text = "Incoming: " + str; }));
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);

                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
            }
        }
        private static void getOfferId()
        {
            mainForm.progressBar_StripStatus.Control.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Visible = true; mainForm.progressBar_StripStatus.Value = 0; mainForm.progressBar_StripStatus.Maximum = TradeOffer.count; }));

            TradeOffer.id.Clear();
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            for (int i = 1; i <= TradeOffer.count; i++)
            {
                IWebElement offer_id = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='profile_leftcol']/div[@class='tradeoffer'][" + i + "]")));
                string href = offer_id.GetAttribute("id");

                TradeOffer.id.Add(Edit.tradeOfferId(href));
            }
        }
        private static void acceptTrade()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Accept Trade...";
                mainForm.status_StripStatus.Visible = true;
            }));
            for (int i = 0; i < TradeOffer.count; i++)
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/tradeoffer/" + TradeOffer.id[i]);
                Thread.Sleep(1000);

                Main.Browser.ExecuteJavaScript(Request.acceptTrade(TradeOffer.id[i], TradeOffer.partner_id, Main.sessionid));

                MainPresenter.progressInvoke();
                string label_inc = Convert.ToString(TradeOffer.count - i - 1);
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.tradeOffers_linkLabel.Text = "Incoming: " + label_inc;
                }));
            }
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Visible = false;
            }));
        }
    }
}
