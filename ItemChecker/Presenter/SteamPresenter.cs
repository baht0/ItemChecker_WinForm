using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Support;

namespace ItemChecker.Presenter
{
    public class SteamPresenter
    {
        public static void getBalance()
        {
            try
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market");
                IWebElement count = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@id='my_market_buylistings_number']")));
                IWebElement balance = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='header_wallet_balance']")));

                Steam.balance = Edit.removeRub(balance.Text);
                if (Steam.balance == Math.Truncate(Steam.balance)) Steam.balance += 0.01;
                BuyOrder.my_buy_orders = Convert.ToInt32(count.Text);

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.balance_StripStatus.Text = "Balance: " + balance.Text;
                    mainForm.available_label.Text = "Available: " + Convert.ToString(Steam.balance * 10) + "₽"; }));
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
        }
    }
}