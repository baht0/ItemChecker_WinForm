using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Support;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static ItemChecker.Program;

namespace ItemChecker.Presenter
{
    class StartingPresenter
    {
        public static void Start(object state)
        {
            CancellationTokenSource cancelTokenSource = new();
            CancellationToken token = cancelTokenSource.Token;
            try
            {
                while (!token.IsCancellationRequested)
                {
                    CompletionUpdate();
                    LaunchBrowser();
                    CheckUpdate();

                    LoginSteam();
                    LoginTryskins();
                    LoginCsm();

                    mainForm.Invoke(new MethodInvoker(delegate {
                        startingForm.Hide();
                        mainForm.WindowState = FormWindowState.Normal;
                        mainForm.ShowInTaskbar = true; }));

                    MainPresenter.preparationData();
                    BuyOrderPresenter.SteamOrders();
                    TryskinsPresenter.Tryskins(TryskinsConfig.Default.dontUpload);

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
                MainPresenter.messageBalloonTip(null, ToolTipIcon.Info);

                if (Properties.Settings.Default.whatIsNew)
                {
                    NewForm newForm = new();
                    mainForm.Invoke(new MethodInvoker(delegate { newForm.ShowDialog(); }));

                    Properties.Settings.Default.whatIsNew = false;
                    Properties.Settings.Default.Save();
                }
            }
        }
        private static void LaunchBrowser()
        {
            startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Launch Browser..."; }));

            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            ChromeOptions option = new();
            option.AddArguments("--headless", "--disable-gpu", "no-sandbox", "--window-size=1920,2160", "--disable-extensions", "--disable-blink-features=AutomationControlled", "ignore-certificate-errors");
            if (GeneralConfig.Default.profile)
                option.AddArguments(@"--user-data-dir=" + Application.StartupPath + "\\profile", "profile-directory=Default");
            else
                Directory.Delete(Application.StartupPath + "\\profile", true);
            option.Proxy = null;

            Main.Browser = new ChromeDriver(chromeDriverService, option, TimeSpan.FromSeconds(30));
            Main.Browser.Manage().Window.Maximize();
            Main.wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
        }

        private static void CheckUpdate()
        {
            startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Check Update..."; }));
            ProjectInfoPresenter.getCurrentVersion();
            ProjectInfoPresenter.checkUpdate();
            if (ProjectInfo.update.Any())
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.aboutToolStripMenuItem.Image = new Bitmap(Properties.Resources.point_red); }));
        }
        private static void CompletionUpdate()
        {
            if (Properties.Settings.Default.completionUpdate)
            {
                startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Completion Update..."; }));
                string update = Application.StartupPath + @"\update";
                if (Directory.Exists(update))
                {
                    string path = Application.StartupPath;
                    string updaterExe = "ItemChecker.Updater.exe";
                    string updaterDll = "ItemChecker.Updater.dll";
                    File.Move($"{update}\\{updaterExe}", path + updaterExe, true);
                    File.Move($"{update}\\{updaterDll}", path + updaterDll, true);
                    Directory.Delete(update, true);
                }
                GeneralConfig.Default.Upgrade();
                SteamConfig.Default.Upgrade();
                TryskinsConfig.Default.Upgrade();
                WithdrawConfig.Default.Upgrade();
                FloatConfig.Default.Upgrade();

                Properties.Settings.Default.completionUpdate = false;
                Properties.Settings.Default.Save();
            }
        }

        //login
        private static void LoginSteam()
        {
            startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Login Steam..."; }));

            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/login/home/?goto=");
            var cookie = Main.Browser.Manage().Cookies.GetCookieNamed("sessionid").ToString();
            Main.sessionid = cookie.Substring(10, 24);

            if (Main.Browser.Url.Contains("id"))
                return;

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
        }
        private static void LoginTryskins()
        {
            startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Login TrySkins..."; }));

            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/site/items");
            if (Main.Browser.Url.Contains("items"))
                return;

            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/login/steam");
            IWebElement account = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='OpenID_loggedInAccount']")));
            if (account.Text == SteamConfig.Default.login | account.Text == Steam.login)
            {
                IWebElement signins = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                signins.Click();
                Thread.Sleep(300);
            }
            else throw new InvalidOperationException("Login Tryskins");
        }
        private static void LoginCsm()
        {
            startingForm.Invoke(new MethodInvoker(delegate { startingForm.status_StripStatus.Text = "Login Cs.Money..."; }));

            Main.Browser.Navigate().GoToUrl("https://cs.money/pending-trades");
            IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
            string json = html.Text;
            if (json.Contains("error"))
            {
                var code_error = JObject.Parse(json)["error"].ToString();
                if (code_error == "6")
                {
                    Main.Browser.Navigate().GoToUrl("https://auth.dota.trade/login?redirectUrl=https://cs.money/&callbackUrl=https://cs.money/login");

                    IWebElement signins = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                    signins.Click();
                    Thread.Sleep(300);
                }
            }
        }
    }
}
