using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using static ItemChecker.Program;
using ItemChecker.Settings;
using ItemChecker.Support;
using ItemChecker.Net;
using ItemChecker.Model;
using System.IO;
using System.Data;

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
                while (!token.IsCancellationRequested)
                {
                    launchBrowser();
                    if (!Main.Browser.Url.Contains("id"))
                    {
                        loginSteam();
                        loginTryskins();
                    }
                    else progressInvoke(2);
                    preparationData();
                    loadDataSteam();
                    loadDataTryskins();
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                    if (SteamConfig.Default.startupPush)
                    {
                        Main.loading = false;
                        BuyOrderPresenter.pushStart();
                    }
                    cancelTokenSource.Cancel();
                }
            }
            catch (Exception exp)
            {
                cancelTokenSource.Cancel();
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.version);
                Exceptions.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false; 
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.reload_MainStripMenu.Enabled = true; }));
                messageBalloonTip();
            }
        }
        private static void launchBrowser()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--headless", "--disable-gpu", "no-sandbox", "--window-size=1920,2160", "--disable-extensions");
            if (GeneralConfig.Default.profile) option.AddArguments(@"--user-data-dir=" + Application.StartupPath.Replace(@"\", @"\\") + "\\profile", "profile-directory=Default");
            else Directory.Delete(Application.StartupPath.Replace(@"\", @"\\") + "\\profile", true);
            option.Proxy = null;

            Main.Browser = new ChromeDriver(chromeDriverService, option);
            Main.Browser.Manage().Window.Maximize();
            Main.Browser.Url = "https://steamcommunity.com/login/home/?goto=";
            Main.wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            mainForm.Invoke(new MethodInvoker(delegate { mainForm.loading_panel.Visible = false; }));

            progressInvoke();
        }
        private static void loginSteam()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Steam..."; }));

            IWebElement username = Main.Browser.FindElement(By.XPath("//input[@name='username']"));
            IWebElement password = Main.Browser.FindElement(By.XPath("//input[@name='password']"));
            IWebElement signin = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='btn_blue_steamui btn_medium login_btn']")));

            LoginForm fr = new LoginForm();
            mainForm.Invoke(new MethodInvoker(delegate { fr.ShowDialog(); }));
            Main.loading = true;

            username.SendKeys(Steam.login);
            password.SendKeys(Steam.pass);
            signin.Click();

            IWebElement code = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='twofactorcode_entry']")));
            code.SendKeys(Steam.code);
            code.SendKeys(OpenQA.Selenium.Keys.Enter);

            Thread.Sleep(4000);
            progressInvoke();
        }
        private static void loginTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Tryskins..."; }));
            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/login/steam");

            IWebElement account = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='OpenID_loggedInAccount']")));
            if (account.Text == Steam.login)
            {
                IWebElement signins = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                signins.Click();
                Thread.Sleep(300);

                progressInvoke();
            }
            else throw new InvalidOperationException("Login Tryskins");
        }

        //load
        private static void preparationData()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Get Info..."; }));

            SteamPresenter.getBalance();
            var course = Request.GetCourse(GeneralConfig.Default.currencyApiKey);
            if (course != 0)
            {
                Main.course = course;
                Properties.Settings.Default.course = course;
                Properties.Settings.Default.Save();
            }
            else
                Main.course = Properties.Settings.Default.course;
            Steam.balance_usd = Math.Round(Steam.balance / Main.course, 2);

            Request request = new Request();
            Main.overstock = request.GetOverstock();
            Main.unavailable = request.GetUnavailable();

            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.course_label.Text = Main.course.ToString() + " ₽";
                mainForm.overstock_label.Text =  "Overstock: " + Main.overstock.Count.ToString();
                mainForm.unavailable_label.Text = "Unavailable: " + Main.unavailable.Count.ToString(); }));

            progressInvoke();
        }
        public static void loadDataSteam()
        {
            if (BuyOrder.my_buy_orders != 0)
            {
                BuyOrderPresenter.getSteamlist();
                if (GeneralConfig.Default.proxy & BuyOrder.item.Count >= 30)
                    BuyOrderPresenter.checkOrdersProxy();
                else
                    BuyOrderPresenter.checkOrders();
                BuyOrderPresenter.createDTable();
            }
            else progressInvoke(3);
        }
        private static void loadDataTryskins()
        {
            TryskinsPresenter.getItemsTryskins();
            if (TrySkins.item.Count > 0)
                TryskinsPresenter.createDTable();
            else progressInvoke();
        }

        //reaload
        public static void _reload(object state)
        {
            try
            {
                Main.loading = true;
                stopPush();
                object[] args = state as object[];
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    if (Convert.ToInt32(args[0]) != 0)
                    {
                        mainForm.progressBar_StripStatus.Maximum = Convert.ToInt32(args[0]);
                        mainForm.progressBar_StripStatus.Value = 0;
                        mainForm.progressBar_StripStatus.Visible = true;
                    }
                    mainForm.status_StripStatus.Text = "Processing...";
                    mainForm.status_StripStatus.Visible = true; }));

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
                else if (Main.reload == 3)//update data
                {
                    preparationData();
                    BuyOrderPresenter.availableAmount();
                }
                else if (Main.reload == 4)//withdraw
                {
                    WithdrawPresenter.withdrawCheck();
                    WithdrawPresenter.createDTable();
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = true;
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.progressBar_StripStatus.Visible = false; }));
                messageBalloonTip();
            }
        }
        //other
        public static void clearDTableRows(DataGridView dataGridView)
        {
            DataTable DT = (DataTable)dataGridView.DataSource;
            if (DT != null)
                mainForm.Invoke(new MethodInvoker(delegate { DT.Rows.Clear(); }));
        }
        public static void clearAll()
        {
            stopPush();
            TrySkins._clear();
            BuyOrder._clear();
            BuyOrder._clearQueue();
            Withdraw._clear();

            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.steamMarket_label.Text = "SteamMarket: -";
                mainForm.tryskins_label.Text = "TrySkins: -";
                mainForm.overstock_label.Text = "Overstock: -";
                mainForm.unavailable_label.Text = "Unavailable: -";

                mainForm.course_label.Text = "0.00 ₽";
                mainForm.tryskins_dataGridView.Columns[1].HeaderText = "Item (TrySkins)";
                mainForm.buyOrder_dataGridView.Columns[1].HeaderText = "Item (BuyOrders)";
                mainForm.available_label.Text = "Available: -"; mainForm.available_label.ForeColor = Color.Red;
                mainForm.queue_label.Text = "Queue: -";

                mainForm.status_StripStatus.Text = "Processing...";
                mainForm.status_StripStatus.Visible = true;

                mainForm.tryskins_dataGridView.Rows.Clear();
                mainForm.buyOrder_dataGridView.Rows.Clear();
                mainForm.withdraw_dataGridView.Rows.Clear();
                mainForm.withdraw_dataGridView.Visible = false;
                mainForm.withdraw_MainStripMenu.Text = "Withdraw";

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
            BuyOrder.tick = 0;
            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.timer_StripStatus.Visible = false;
                mainForm.push_linkLabel.Text = "Push..."; }));
        }
        public static void updateSettings()
        {
            if (Properties.Settings.Default.UpdateSettings)
            {
                Properties.Settings.Default.Upgrade();
                GeneralConfig.Default.Upgrade();
                SteamConfig.Default.Upgrade();
                TryskinsConfig.Default.Upgrade();
                WithdrawConfig.Default.Upgrade();
                FloatConfig.Default.Upgrade();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
            }
        }

        public static void messageBalloonTip(string str = "Loading is complete. Open to show.", ToolTipIcon icon = ToolTipIcon.Info, int sec = 5)
        {
            mainForm.notifyIcon.BalloonTipText = str;
            mainForm.notifyIcon.BalloonTipIcon = icon;
            mainForm.notifyIcon.ShowBalloonTip(sec);
        }
        public static void progressInvoke(int i = 1)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value += i; }));
        }
        public static void exit()
        {
            try
            {
                mainForm.notifyIcon.Visible = false;
                Main.Browser.Quit();
                Application.Exit();
            }
            catch
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("conhost")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("ItemChecker")) proc.Kill();
            }
        }
    }
}