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
using System.Globalization;

namespace ItemChecker.Presenter
{
    public class MainPresenter
    {
        public static void Start(object state)
        {
            try
            {
                launchBrowser();
                loginSteam();
                loginTryskins();
                loadData();
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
            }
        }
        public static void launchBrowser()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--headless", "--disable-gpu", "no-sandbox", "--window-size=1920,2160");

            Main.Browser = new ChromeDriver(chromeDriverService, option);
            Main.Browser.Manage().Window.Maximize();
            Main.Browser.Url = "https://steamcommunity.com/login/home/?goto=";
            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.loading_panel.Visible = false;
                mainForm.loading_pictureBox.Visible = false;
                mainForm.ver_label.Visible = false;
            }));

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
            try
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
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);

                mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value--; }));
            }
        }

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
        public static void loadData()
        {
            try
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Visible = true; }));
                preparationData();
                if (BuyOrder.count != 0)
                {
                    BuyOrderPresenter.getSteamlist();
                    BuyOrderPresenter.precentSteam();
                    BuyOrderPresenter.createSteamTable();
                }
                else mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Value = mainForm.progressBar_StripStatus.Value + 3;
                    mainForm.buyOrder_dataGridView.Rows.Clear();
                }));

                TryskinsPresenter.checkTryskins();

                if (!TrySkins.item.Contains("empty")) TryskinsPresenter.createTryTable();

                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.reload_MainStripMenu.Enabled = true;
                    mainForm.status_StripStatus.Visible = false;
                }));
            }
            finally
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = true;
                }));
            }
        }

        public static void _reload(object state)
        {
            try
            {
                Main.loading = true;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.status_StripStatus.Text = "Processing...";
                    mainForm.status_StripStatus.Visible = true;
                    mainForm.progressBar_StripStatus.Visible = true;
                }));
                if (Main.reload == 0)//full
                {
                    clearData();
                    loadData();
                }
                else if (Main.reload == 1)//tryskins
                {
                    SteamPresenter.getBalance();
                    TryskinsPresenter.checkTryskins();
                    TryskinsPresenter.createTryTable();
                }
                else if (Main.reload == 2)//steam(a)
                {
                    SteamPresenter.getBalance();
                    BuyOrderPresenter.getSteamlist();
                    BuyOrderPresenter.precentSteam();
                    BuyOrderPresenter.createSteamTable();
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

        public static void progressInvoke()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value++; }));
        }

        public static void clearData()
        {
            try
            {
                stopPush();
                BuyOrder.queue.Clear();
                BuyOrder.ordered.Clear();
                BuyOrder.queue_count = 0;
                BuyOrder.order_dol = 0;
                BuyOrder.order_rub = 0;

                BuyOrder.sum = 0;
                BuyOrder.available_amount = 0;

                mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.steamMarket_label, "SteamMarket: -")));
                mainForm.Invoke(new MethodInvoker(delegate
                {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.steamMarket_label.ForeColor = Color.Black;
                    mainForm.available_label.ForeColor = Color.Black;
                    mainForm.queue_linkLabel.Text = "Place order: -";
                }));
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
                Edit.errorLog(exp, Main.version);
            }
        }
        public static void stopPush()
        {
            Main.timer.Stop();
            BuyOrder.tick = 0;
            mainForm.progressBar_StripStatus.Control.Invoke(new MethodInvoker(delegate
            {
                mainForm.tradeOffers_linkLabel.Enabled = true;
                mainForm.queue_linkLabel.Enabled = true;
                mainForm.push_linkLabel.Text = "Push...";
                mainForm.timer_StripStatus.Visible = false;
            }));
        }
        public static void exit()
        {
            Main.Browser.Quit();
            mainForm.notifyIcon.Visible = false;
            foreach (Process proc in Process.GetProcessesByName("ItemChecker"))
            {
                proc.Kill();
            }
        }
    }
}