using static ItemChecker.Program;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using ItemChecker.Settings;
using ItemChecker.Support;
using ItemChecker.Net;
using ItemChecker.Model;
using System.Data;

namespace ItemChecker.Presenter
{
    public class MainPresenter
    {
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
        }
        public static void Reload(object state)
        {
            try
            {
                Main.loading = true;
                MainPresenter.stopTimers();
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.reload_MainStripMenu.Enabled = false;
                    mainForm.status_StripStatus.Text = "Processing...";
                    mainForm.status_StripStatus.Visible = true; }));

                preparationData();
                if (Main.reload == 0)//full
                {
                    ClearData();
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
                BuyOrderPresenter.availableAmount();
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
                    mainForm.status_StripStatus.Visible = false; }));
                messageBalloonTip(null, ToolTipIcon.Info);
            }
        }
        public static void Restart()
        {
            MainPresenter.ClearData();
            MainPresenter.ClearForm();
            Main.Browser.Quit();

            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.WindowState = FormWindowState.Minimized;
                mainForm.ShowInTaskbar = false; }));

            startingForm.Show();
            startingForm.Activate();
        }
        //Clear
        public static void clearDTableRows(DataGridView dataGridView)
        {
            DataTable dataTable = (DataTable)dataGridView.DataSource;
            if (dataTable != null)
                mainForm.Invoke(new MethodInvoker(delegate { dataTable.Rows.Clear(); }));
        }
        private static void ClearData()
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
        }
        private static void ClearForm()
        {
            mainForm.Invoke(new MethodInvoker(delegate
            {
                mainForm.status_StripStatus.Text = "Restarting...";
                mainForm.status_StripStatus.Visible = true;

                mainForm.steamMarket_label.Text = "SteamMarket: -";
                mainForm.tryskins_label.Text = "TrySkins: -";
                mainForm.overstock_label.Text = "Overstock: -";
                mainForm.unavailable_label.Text = "Unavailable: -";

                mainForm.course_label.Text = "0.00 ₽";
                mainForm.tryskins_dataGridView.Columns[1].HeaderText = "Item (TrySkins)";
                mainForm.buyOrder_dataGridView.Columns[1].HeaderText = "Item (BuyOrders)";
                mainForm.available_label.Text = "Available: -"; 
                mainForm.available_label.ForeColor = Color.Black;
                mainForm.queue_label.Text = "Queue: -";

                mainForm.withdraw_dataGridView.Visible = false;
                mainForm.showWithdraw_toolStripMenuItem.Text = "Withdraw";

                mainForm.reload_MainStripMenu.Enabled = false;
                mainForm.steamMarket_label.ForeColor = Color.Black;
                mainForm.available_label.ForeColor = Color.Black;
                mainForm.queue_linkLabel.Text = "Place order: -";
                mainForm.tradeOffers_linkLabel.Text = "Incoming: -";
                mainForm.balance_StripStatus.Text = "Balance: 0.00 / 0.00";
            }));
        }        

        //other
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
        public static void Exit()
        {
            try
            {
                mainForm.notifyIcon.Visible = false;
                Main.Browser.Quit();
            }
            catch
            {
                if (GeneralConfig.Default.exitChrome)
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