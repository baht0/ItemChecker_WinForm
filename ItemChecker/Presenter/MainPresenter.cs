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
            Main.loading = true;

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
        private static void preparationData()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Get Info..."; }));

            SteamPresenter.getBalance();
            Main.course = Request.getCourse(GeneralConfig.Default.currencyApiKey);
            Steam.balance_usd = Math.Round(Steam.balance / Main.course, 2);

            Request request = new Request();
            Main.overstock = request.overstock();
            Main.unavailable = request.unavailable();

            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.course_label.Text = Main.course.ToString() + " ₽";
                mainForm.overstock_label.Text =  "Overstock: " + Main.overstock.Count.ToString();
                mainForm.unavailable_label.Text = "Unavailable: " + Main.unavailable.Count.ToString();
            }));

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
        private static void loadDataTryskins()
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
                object[] args = state as object[];
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.progressBar_StripStatus.Maximum = Convert.ToInt32(args[0]);
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
                else if (Main.reload == 3)//update data
                {
                    preparationData();
                    BuyOrderPresenter.availableAmount();
                }
                else if (Main.reload == 4)//withdraw
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
                mainForm.check_label.Text = "Check: -";
                mainForm.push_label.Text = "Push: -";

                mainForm.status_StripStatus.Text = "Processing...";
                mainForm.status_StripStatus.Visible = true;

                mainForm.tryskins_dataGridView.Rows.Clear();
                mainForm.buyOrder_dataGridView.Rows.Clear();
                mainForm.withdraw_dataGridView.Rows.Clear();
                mainForm.withdraw_dataGridView.Visible = false;
                mainForm.withdrawTable_MainStripMenu.Text = "Withdraw";

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

        public static void nameId()
        {
            try
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/MAC-10%20%7C%20Oceanic%20%28Minimal%20Wear%29");
                //IWebElement element = Main.Browser.FindElement(By.TagName("script"));
                //String htmlCode = (String)((IJavaScriptExecutor)Main.Browser).ExecuteScript("return arguments[0].innerHTML;", element);
                //errorLog(htmlCode);
                WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
                IWebElement nameId = Main.Browser.FindElement(By.XPath("//body[@script[30]]"));
                errorLog(nameId.GetAttribute("innerHTML").ToString());
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
        }
        public static void errorLog(string message)
        {
            if (!File.Exists("ttt.txt")) File.WriteAllText("ttt.txt", "v. [" + DateTime.Now + "]\n" + message + "\n");
            else File.WriteAllText("ttt.txt", string.Format("{0}{1}", "v. [" + DateTime.Now + "]\n" + message + "\n", File.ReadAllText("ttt.txt")));
        }
    }
}