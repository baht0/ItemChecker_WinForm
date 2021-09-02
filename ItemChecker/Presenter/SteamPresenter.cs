using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Support;
using ItemChecker.Settings;

namespace ItemChecker.Presenter
{
    public class SteamPresenter
    {
        public static void loginSteam()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Steam..."; }));

            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/login/home/?goto=");
            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            if (Main.Browser.Url.Contains("id"))
            {
                MainPresenter.progressInvoke();
                return;
            }

            IWebElement username = Main.Browser.FindElement(By.XPath("//input[@name='username']"));
            IWebElement password = Main.Browser.FindElement(By.XPath("//input[@name='password']"));
            IWebElement signin = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn_blue_steamui btn_medium login_btn']")));

            LoginForm loginForm = new();
            mainForm.Invoke(new MethodInvoker(delegate { loginForm.ShowDialog(); }));
            Main.loading = true;

            username.SendKeys(Steam.login);
            password.SendKeys(Steam.pass);
            signin.Click();

            IWebElement code = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='twofactorcode_entry']")));
            code.SendKeys(Steam.code);
            code.SendKeys(OpenQA.Selenium.Keys.Enter);

            System.Threading.Thread.Sleep(4000);
            MainPresenter.progressInvoke();
        }
        public static void getBalance()
        {
            try
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market");
                IWebElement count = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@id='my_market_buylistings_number']")));
                IWebElement balance = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='header_wallet_balance']")));

                Steam.balance = Edit.removeRub(balance.Text);
                BuyOrder.my_buy_orders = Convert.ToInt32(count.Text);
                Steam.balance_usd = Math.Round(Steam.balance / GeneralConfig.Default.currency, 2);

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.available_label.Text = $"Available: {Math.Round(Steam.balance * 10, 2)}₽"; }));
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
    }
}