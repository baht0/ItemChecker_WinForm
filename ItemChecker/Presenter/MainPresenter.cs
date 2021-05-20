using OpenQA.Selenium.Chrome;
using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Settings;
using ItemChecker.General;
using ItemChecker.NET;
using System.Drawing;

namespace ItemChecker.Presenter
{
    public class MainPresenter
    {
        //start
        public static void Start(object state)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            try
            {
                Main.loading = true;
                while (!token.IsCancellationRequested)
                {
                    launchBrowser();
                    loginSteam();
                    loginTryskins();
                    preparationData();
                    loadDataSteam();
                    loadDataTryskins();
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");

                    cancelTokenSource.Cancel();
                }
            }
            catch (Exception exp)
            {
                cancelTokenSource.Cancel();
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false; 
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.reload_MainStripMenu.Enabled = true;
                }));
            }
        }
        public static void launchBrowser()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--headless", "--disable-gpu", "no-sandbox", "--window-size=1920,2160");

            Main.Browser = new ChromeDriver(chromeDriverService, option, TimeSpan.FromMinutes(5));
            Main.Browser.Manage().Window.Maximize();
            Main.Browser.Url = "https://steamcommunity.com/login/home/?goto=";
            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            mainForm.Invoke(new MethodInvoker(delegate { mainForm.loading_panel.Visible = false; }));

            progressInvoke();
        }
        private static void loginSteam()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Steam..."; }));

            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            IWebElement username = Main.Browser.FindElement(By.XPath("//input[@name='username']"));
            IWebElement password = Main.Browser.FindElement(By.XPath("//input[@name='password']"));
            IWebElement signin = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn_blue_steamui btn_medium login_btn']")));

            LoginForm fr = new LoginForm();
            mainForm.Invoke(new MethodInvoker(delegate { fr.ShowDialog(); }));

            username.SendKeys(Steam.login);
            password.SendKeys(Steam.pass);
            signin.Click();

            IWebElement code = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='twofactorcode_entry']")));
            code.SendKeys(Steam.code);
            code.SendKeys(OpenQA.Selenium.Keys.Enter);

            Thread.Sleep(4000);
            progressInvoke();
        }
        private static void loginTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Tryskins..."; }));
            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/login/steam");
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));

            IWebElement account = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='OpenID_loggedInAccount']")));
            if (account.Text == Steam.login)
            {
                IWebElement signins = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                signins.Click();
                Thread.Sleep(300);

                progressInvoke();
            }
            else throw new InvalidOperationException("Login Tryskins");
        }

        //load
        public static void preparationData()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Get Info..."; }));

            SteamPresenter.getBalance();
            Main.course = Request.getCourse(GeneralConfig.Default.currencyApiKey);
            Steam.balance_usd = Math.Round(Steam.balance / Main.course, 2);

            Request request = new Request();
            Main.overstock = request.overstock();
            Main.unavailable = request.unavailable();

            mainForm.course_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.course_label, Main.course.ToString() + " ₽")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.overstock_label, "Overstock: " + Main.overstock.Count.ToString())));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.unavailable_label, "Unavailable: " + Main.unavailable.Count.ToString())));

            progressInvoke();
        }
        public static void loadDataSteam()
        {
            if (BuyOrder.count != 0)
            {
                BuyOrderPresenter.getSteamlist();
                BuyOrderPresenter.precentSteam();
                BuyOrderPresenter.createSteamTable();
            }
            else progressInvoke(3);
        }
        public static void loadDataTryskins()
        {
            TryskinsPresenter.checkTryskins();
            if (TrySkins.count != 0) TryskinsPresenter.createTryTable();
            else progressInvoke();
        }

        //reaload
        public static void _reload(object state)
        {
            try
            {
                Main.loading = true;
                stopPush();
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.status_StripStatus.Text = "Processing...";
                    mainForm.status_StripStatus.Visible = true;
                }));
                if (Main.reload == 0)//full
                {
                    clearAll();
                    preparationData();
                    loadDataSteam();
                    loadDataTryskins();
                }
                else if (Main.reload == 1)//tryskins
                {
                    preparationData();
                    BuyOrder._clearQueue();
                    loadDataTryskins();
                }
                else if (Main.reload == 2)//buy order
                {
                    preparationData();
                    loadDataSteam();
                }
                else if (Main.reload == 3)//withdraw
                {
                    WithdrawPresenter.withdrawCheck();
                    WithdrawPresenter.createWithdraw();
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
                Edit.errorLog(exp, Main.version);
            }
            finally
            {
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = true;
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.progressBar_StripStatus.Visible = false;
                }));
            }
        }
        //other
        public static void clearAll()
        {
            stopPush();
            TrySkins._clear();
            BuyOrder._clear();
            BuyOrder._clearQueue();
            Withdraw._clear();

            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.steamMarket_label, "SteamMarket: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.tryskins_label, "TrySkins: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.overstock_label, "Overstock: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.unavailable_label, "Unavailable: -")));

            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.course_label, "0.00 ₽")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.quantity_label, "Quantity: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.available_label, "Available: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.check_label, "Check: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.push_label, "Push: -")));
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.catch_label, "Catch: -")));

            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.status_StripStatus.Text = "Processing...";
                mainForm.status_StripStatus.Visible = true;

                mainForm.reload_MainStripMenu.Enabled = false;
                mainForm.progressBar_StripStatus.Value = 0;
                mainForm.progressBar_StripStatus.Visible = true;
                mainForm.steamMarket_label.ForeColor = Color.Black;
                mainForm.available_label.ForeColor = Color.Black;
                mainForm.queue_linkLabel.Text = "Place order: -";
                mainForm.tradeOffers_linkLabel.Text = "Incoming: -";
                mainForm.balance_StripStatus.Text = "Balance: 0.00";
            }));
        }
        public static void stopPush()
        {
            Main.timer.Stop();
            BuyOrder._clearPush();
            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.timer_StripStatus.Visible = false;
                mainForm.tradeOffers_linkLabel.Enabled = true;
                mainForm.queue_linkLabel.Enabled = true;
                mainForm.push_linkLabel.Text = "Push...";
            }));
        }

        public static void progressInvoke(int i = 1)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value += i; }));
        }
        public static void exit()
        {
            mainForm.notifyIcon.Visible = false;
            Main.Browser.Quit();
            foreach (Process proc in Process.GetProcessesByName("ItemChecker"))
            {
                proc.Kill();
            }
        }
    }
}