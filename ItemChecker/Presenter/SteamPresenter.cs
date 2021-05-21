using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Settings;
using ItemChecker.Support;

namespace ItemChecker.Presenter
{
    public class SteamPresenter
    {
        public static void getBalance()
        {
            try
            {
                BuyOrder.count = 0;
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market");
                WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
                IWebElement count = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@id='my_market_buylistings_number']")));
                IWebElement balance = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='header_wallet_balance']")));

                Steam.balance = Edit.removeRub(balance.Text) + 0.01;
                BuyOrder.count = Convert.ToInt32(count.Text);

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.balance_StripStatus.Text = "Balance: " + balance.Text;
                    mainForm.quantity_label.Text = "Quantity: " + count.Text;
                    mainForm.available_label.Text = "Available: " + Convert.ToString(Steam.balance * 10) + "₽";
                }));
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
            }
        }
    }
}
