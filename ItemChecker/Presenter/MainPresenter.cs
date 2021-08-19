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
using System.Linq;

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
                    else
                        progressInvoke(2);
                    preparationData();
                    loadDataSteam();
                    loadDataTryskins();
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                    if (SteamConfig.Default.startupPush)
                    {
                        Main.loading = false;
                        mainForm.Invoke(new MethodInvoker(delegate { mainForm.buyOrderPush_toolStripMenuItem.PerformClick(); }));
                    }
                    cancelTokenSource.Cancel();
                }
            }
            catch (Exception exp)
            {
                cancelTokenSource.Cancel();
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.assemblyVersion);
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
            ChromeOptions option = new();
            option.AddArguments("--headless", "--disable-gpu", "no-sandbox", "--window-size=1920,2160", "--disable-extensions", "--disable-blink-features=AutomationControlled", "ignore-certificate-errors");
            if (GeneralConfig.Default.profile)
                option.AddArguments(@"--user-data-dir=" + Application.StartupPath.Replace(@"\", @"\\") + "\\profile", "profile-directory=Default");
            else
                Directory.Delete(Application.StartupPath.Replace(@"\", @"\\") + "\\profile", true);
            option.Proxy = null;

            Main.Browser = new ChromeDriver(chromeDriverService, option, TimeSpan.FromSeconds(30));
            Main.Browser.Manage().Window.Maximize();
            Main.Browser.Url = "https://steamcommunity.com/login/home/?goto=";
            Main.wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));

            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            ProjectInfoPresenter.getCurrentVersion();
            ProjectInfoPresenter.checkUpdate();
            if (ProjectInfo.update.Any())
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.aboutToolStripMenuItem.Image = new Bitmap(Properties.Resources.point_red); }));            

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

            var course = Get.Course(GeneralConfig.Default.currencyApiKey);
            if (course != 0)
            {
                GeneralConfig.Default.currency = course;
                GeneralConfig.Default.Save();
            }
            SteamPresenter.getBalance();

            Get get = new();
            Main.overstock = get.Overstock();
            Main.unavailable = get.Unavailable();

            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.course_label.Text = GeneralConfig.Default.currency.ToString() + " ₽";
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
                MainPresenter.stopTimers();
                object[] args = state as object[];
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.progressBar_StripStatus.Maximum = Convert.ToInt32(args[0]);
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Visible = true;
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
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
                Exceptions.errorLog(exp, Main.assemblyVersion);
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
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            if (dataTable != null)
                mainForm.Invoke(new MethodInvoker(delegate { dataTable.Rows.Clear(); }));
        }
        public static void clearAll()
        {
            MainPresenter.stopTimers();
            TrySkins._clear();
            BuyOrder._clear();
            BuyOrder._clearQueue();
            Withdraw._clear();

            clearDTableRows(mainForm.tryskins_dataGridView);
            clearDTableRows(mainForm.buyOrder_dataGridView);
            clearDTableRows(mainForm.withdraw_dataGridView);

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

                mainForm.withdraw_dataGridView.Visible = false;
                mainForm.showWithdraw_toolStripMenuItem.Text = "Withdraw";

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
        public static void updateSettings()
        {
            if (Properties.Settings.Default.updateSettings)
            {
                Properties.Settings.Default.Upgrade();
                GeneralConfig.Default.Upgrade();
                SteamConfig.Default.Upgrade();
                TryskinsConfig.Default.Upgrade();
                WithdrawConfig.Default.Upgrade();
                FloatConfig.Default.Upgrade();
                Properties.Settings.Default.updateSettings = false;
                Properties.Settings.Default.Save();
            }
        }

        public static void stopTimers()
        {
            BuyOrderPresenter.stopBuyOrderPusher();
            WithdrawPresenter.stopCheckFavorite();
            FloatPresenter.stopCheckFloat();
        }
        public static void messageBalloonTip(string str = "Loading is complete. Open to show.", ToolTipIcon icon = ToolTipIcon.Info)
        {
            mainForm.notifyIcon.BalloonTipText = str;
            mainForm.notifyIcon.BalloonTipIcon = icon;
            mainForm.notifyIcon.ShowBalloonTip(2);
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
                foreach (Process proc in Process.GetProcessesByName("chrome")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("conhost")) {
                    try {
                        proc.Kill();
                    }
                    catch {
                        continue;
                    }
                }
                foreach (Process proc in Process.GetProcessesByName("ItemChecker")) proc.Kill();
            }
        }
    }
}