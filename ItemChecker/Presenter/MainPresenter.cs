using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            CancellationTokenSource cancelTokenSource = new();
            CancellationToken token = cancelTokenSource.Token;
            try
            {                
                while (!token.IsCancellationRequested)
                {
                    LaunchBrowser();

                    ProjectInfoPresenter.getCurrentVersion();
                    ProjectInfoPresenter.checkUpdate();
                    if (ProjectInfo.update.Any())
                        mainForm.Invoke(new MethodInvoker(delegate { mainForm.aboutToolStripMenuItem.Image = new Bitmap(Properties.Resources.point_red); }));

                    SteamPresenter.loginSteam();
                    preparationData();
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
                messageBalloonTip(null, ToolTipIcon.Info);
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

            mainForm.Invoke(new MethodInvoker(delegate { mainForm.loading_panel.Visible = false; }));
            progressInvoke();
        }

        //load
        public static void preparationData()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Get Info..."; }));

            var course = Get.Course(GeneralConfig.Default.currencyApiKey);
            if (course != 0)
            {
                GeneralConfig.Default.currency = course;
                GeneralConfig.Default.Save();
            }
            SteamPresenter.getBalance();
            WithdrawPresenter.getBalance();

            Get get = new();
            Main.overstock = get.Overstock();
            Main.unavailable = get.Unavailable();

            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.balance_StripStatus.Text = $"Balance: {Steam.balance}₽ / {Withdraw.balance}₽";
                mainForm.balance_StripStatus.ToolTipText = $"Balance: ${Steam.balance_usd} / ${Withdraw.balance_usd}";
                mainForm.course_label.Text = $"{GeneralConfig.Default.currency}₽";
                mainForm.overstock_label.Text =  $"Overstock: {Main.overstock.Count}";
                mainForm.unavailable_label.Text = $"Unavailable: {Main.unavailable.Count}"; }));

            progressInvoke();
        }
        public static void _reload(object state)
        {
            try
            {
                Main.loading = true;
                MainPresenter.stopTimers();
                object[] args = state as object[];
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Maximum = Convert.ToInt32(args[0]);
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.status_StripStatus.Text = "Processing...";
                    mainForm.status_StripStatus.Visible = true; }));

                preparationData();
                if (Main.reload == 0)//full
                {
                    clearAll();
                    BuyOrderPresenter.SteamOrders();
                    TryskinsPresenter.Tryskins(false);
                }
                else if (Main.reload == 1)//tryskins
                {
                    BuyOrder._clearQueue();
                    TryskinsPresenter.Tryskins(false);
                }
                else if (Main.reload == 2)//buy order
                {
                    BuyOrderPresenter.SteamOrders();
                }
                else if (Main.reload == 3)//update data
                {
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
                messageBalloonTip(null, ToolTipIcon.Info);
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
            Main.cts.Cancel();
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
        public static void completionUpdate()
        {
            if (Properties.Settings.Default.completionUpdate)
            {
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
                Properties.Settings.Default.Upgrade();
                GeneralConfig.Default.Upgrade();
                SteamConfig.Default.Upgrade();
                TryskinsConfig.Default.Upgrade();
                WithdrawConfig.Default.Upgrade();
                FloatConfig.Default.Upgrade();

                Properties.Settings.Default.completionUpdate = false;
                Properties.Settings.Default.Save();
            }
        }

        public static void stopTimers()
        {
            BuyOrderPresenter.stopBuyOrderPusher();
            WithdrawPresenter.stopCheckFavorite();
            FloatPresenter.stopCheckFloat();
        }
        public static void messageBalloonTip(string text, ToolTipIcon icon)
        {
            if (text == null)
                text = "Loading is complete. Click to show.";
            mainForm.notifyIcon.BalloonTipText = text;
            mainForm.notifyIcon.BalloonTipIcon = icon;
            mainForm.notifyIcon.ShowBalloonTip(1);
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
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}