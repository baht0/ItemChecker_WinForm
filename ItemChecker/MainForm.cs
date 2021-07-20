using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Settings;
using Keys = System.Windows.Forms.Keys;

namespace ItemChecker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Program.mainForm = this;

            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show(
                    "The program is already running!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                Process.GetCurrentProcess().Kill();
            }
            //foreach (Process proc in Process.GetProcessesByName("chrome")) proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("conhost")) proc.Kill();
            notifyIcon.Visible = true;
            ver_label.Text = "Version: " + Main.version;

            MainPresenter.updateSettings();
        }
        public void MainForm_Shown(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Main.loading = true;
            status_StripStatus.Text = "Launch Browser...";

            BuyOrderPresenter buyOrderPresenter = new();
            WithdrawPresenter withdrawPresenter = new();
            BuyOrder.timer.Elapsed += new ElapsedEventHandler(buyOrderPresenter.timerTick);
            Withdraw.timer.Elapsed += new ElapsedEventHandler(withdrawPresenter.timerTick);
            BuyOrder.timer.Interval = Withdraw.timer.Interval = 1000;

            Main.proxyList.AddRange(GeneralConfig.Default.proxyList.Split("\n"));
            Withdraw.favoriteList.AddRange(WithdrawConfig.Default.favoriteList.Split("\n"));

            ThreadPool.QueueUserWorkItem(MainPresenter.Start);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!loading_panel.Visible)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.Hide();
                }
            }
            else
            {
                notifyIcon.Visible = false;
                Main.Browser.Quit();
            }
        }

        //file
        private void settings_MainStripMenu_Click(object sender, EventArgs e)
        {
            SettingsForm fr = new SettingsForm();
            fr.ShowDialog();     
        }
        private void restart_MainStripMenu_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Do you really want restart program?",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                loading_panel.Visible = true;
                progressBar_StripStatus.Maximum = 9;
                MainPresenter.clearAll();
                Main.Browser.Quit();
                Thread.Sleep(1000);
                status_StripStatus.Visible = true;
                ThreadPool.QueueUserWorkItem(MainPresenter.Start);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Do you want to log out?",
              "Warning",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BuyOrderPresenter.stopPush();
                WithdrawPresenter.stopCheckFavorite();
                MainPresenter.exit();
            }
        }
        //reload
        private void full_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 0;
                ThreadPool.QueueUserWorkItem(MainPresenter._reload, new object[] { 6 });
            }
        }
        private void tryskins_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 1;
                ThreadPool.QueueUserWorkItem(MainPresenter._reload, new object[] { 3 });
            }
        }
        private void buyOrders_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 2;
                ThreadPool.QueueUserWorkItem(MainPresenter._reload, new object[] { 4 });
            }
        }
        private void updateData_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 3;
                ThreadPool.QueueUserWorkItem(MainPresenter._reload, new object[] { 1 });
            }
        }
        //tools
        private void checkOwnList_MainStripMenu_Click(object sender, EventArgs e)
        {
            ServiceCheckerForm serviceChecker = new ServiceCheckerForm();
            serviceChecker.Show();
        }
        private void floatCheck_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                CheckListForm checkListForm = new("FloatList");
                checkListForm.ShowDialog();
            }
        }
        //withdraw
        private void showWithdraw_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                if (!withdraw_dataGridView.Visible)
                {
                    BuyOrderPresenter.stopPush();
                    WithdrawPresenter.stopCheckFavorite();
                    showWithdraw_toolStripMenuItem.Text = "Close";
                    withdraw_dataGridView.Visible = true;
                    reload_MainStripMenu.Enabled = false;
                    reloadWithdraw_toolStripMenuItem.Enabled = true;
                    status_StripStatus.Text = "Check Withdraw...";
                    status_StripStatus.Visible = true;
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(WithdrawPresenter.withdraw);
                }
                else
                {
                    reloadWithdraw_toolStripMenuItem.Enabled = false;
                    reload_MainStripMenu.Enabled = true;
                    withdraw_dataGridView.Visible = false;
                    showWithdraw_toolStripMenuItem.Text = "Load";
                }
            }
        }
        private void reloadWithdraw_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                status_StripStatus.Text = "Check Withdraw...";
                status_StripStatus.Visible = true;
                Main.loading = true;
                ThreadPool.QueueUserWorkItem(WithdrawPresenter.withdraw);
            }
        }
        private void checkCsmWithdraw_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.loading = true;
                BuyOrderPresenter.stopPush();
                WithdrawPresenter.stopCheckFavorite();
                status_StripStatus.Text = "Checking Cs.Money...";
                status_StripStatus.Visible = true;
                ThreadPool.QueueUserWorkItem(WithdrawPresenter.inventoryCsm);
            }
        }
        private void checkFavorite_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WithdrawPresenter.checkStart();
        }

        //linkLabels
        private void tradeOffers_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Main.loading & !String.IsNullOrEmpty(GeneralConfig.Default.steamApiKey))
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to ACCEPT trade offers?",
                    "Question",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(TradeOfferPresenter.tradeOffers);
                }
            }                
        }
        private void queue_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Main.loading & BuyOrder.queue.Count > 0)
            {
                if (BuyOrder.queue_rub < BuyOrder.available_amount)
                {
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(BuyOrderPresenter.placeOrder);
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                        "The total of the selected items exceeds the allowed amount.\nProceed?",
                        "Warning",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Main.loading = true;
                        ThreadPool.QueueUserWorkItem(BuyOrderPresenter.placeOrder);
                    }
                }
            }            
        }
        private void push_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BuyOrderPresenter.startPush();
        }
        private void timer_StripStatus_Click(object sender, EventArgs e)
        {
            if (push_linkLabel.Text == "Stop...")
                BuyOrder.tick = 1;
        }

        //tryskins table
        private void tryskins_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(tryskins_dataGridView.CurrentCell.RowIndex.ToString());
                string item = tryskins_dataGridView.Rows[row].Cells[1].Value.ToString();
                int cell = tryskins_dataGridView.CurrentCell.ColumnIndex;
                string market_has_name = Edit.replaceUrl(item);
                string url;

                if (cell == 1)
                {
                    Clipboard.SetText(item);
                }
                if (cell == 2)
                {
                    url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                    Edit.openUrl(url);
                }
                if (cell == 3)
                {
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
                }
                if (cell == 4)
                {
                    url = TrySkins.url.Replace("ItemsFilter%5Bname%5D=", "ItemsFilter%5Bname%5D=" + market_has_name);
                    Edit.openUrl(url);
                }
            }
            catch { }
        }
        private void tryskins_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(tryskins_dataGridView.CurrentCell.RowIndex);
                string str = tryskins_dataGridView.CurrentCell.Value.ToString();
                int cell = tryskins_dataGridView.CurrentCell.ColumnIndex;

                if (cell == 2 || cell == 3 & str != "")
                {
                    Main.save_str = str;
                    tryskins_dataGridView.Rows[row].Cells[cell].Value = Edit.currencyConverter(str, Main.course);
                }
            }
            catch { }
        }
        private void tryskins_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(tryskins_dataGridView.CurrentCell.RowIndex);
                int cell = tryskins_dataGridView.CurrentCell.ColumnIndex;
                if (cell == 2 || cell == 3)
                {
                    tryskins_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                    Main.save_str = "";
                }
            }
            catch { }
        }
        private void tryskins_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
                ThreadPool.QueueUserWorkItem(TryskinsPresenter.addQueue);
        }
        private void tryskins_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TryskinsPresenter.drawDTGView();
        }
        //buyorder table
        private void buyOrder_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = Convert.ToInt32(buyOrder_dataGridView.CurrentCell.RowIndex.ToString());
            string item = buyOrder_dataGridView.Rows[row].Cells[1].Value.ToString();
            string market_has_name = Edit.replaceUrl(item);
            string url;

            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 1)
            {
                Clipboard.SetText(item);
            }
            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 2)
            {
                url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                Edit.openUrl(url);
            }
            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 3)
            {
                Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            }
        }
        private void buyOrder_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(buyOrder_dataGridView.CurrentCell.RowIndex);
                string str = buyOrder_dataGridView.CurrentCell.Value.ToString();
                int cell = buyOrder_dataGridView.CurrentCell.ColumnIndex;
                if (cell == 3)
                {
                    Main.save_str = str;
                    buyOrder_dataGridView.Rows[row].Cells[cell].Value = Edit.currencyConverter(str, Main.course);
                }
            }
            catch { }
        }
        private void buyOrder_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(buyOrder_dataGridView.CurrentCell.RowIndex);
                int cell = buyOrder_dataGridView.CurrentCell.ColumnIndex;
                if (cell == 3)
                {
                    buyOrder_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                    Main.save_str = "";
                }
            }
            catch { }
        }
        private void buyOrder_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show(
                      "Do you want to DELETE this item?",
                      "Warning",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    ThreadPool.QueueUserWorkItem(BuyOrderPresenter.CancelOrder);
            }
        }
        private void buyOrder_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BuyOrderPresenter.drawDTGView();
        }
        //withdraw table
        private void withdraw_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(withdraw_dataGridView.CurrentCell.RowIndex.ToString());
                string item = withdraw_dataGridView.Rows[row].Cells[1].Value.ToString();
                int cell = withdraw_dataGridView.CurrentCell.ColumnIndex;
                string market_has_name = Edit.replaceUrl(item);
                string url;

                if (cell == 1 || cell == 2)
                {
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
                    withdraw_dataGridView.Rows[row].Cells[1].Style.BackColor = System.Drawing.Color.Silver;
                }
                if (cell == 3 || cell == 4)
                {
                    url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                    Edit.openUrl(url);
                }
                if (cell == 5)
                {
                    url = Withdraw.url.Replace("ItemsFilter%5Bname%5D=", "ItemsFilter%5Bname%5D=" + market_has_name);
                    Edit.openUrl(url);
                }
            }
            catch { }
        }
        private void withdraw_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = withdraw_dataGridView.CurrentCell.Value.ToString();
                int row = withdraw_dataGridView.CurrentCell.RowIndex;
                int cell = withdraw_dataGridView.CurrentCell.ColumnIndex;
                if (cell == 2 || cell == 3)
                {
                    Main.save_str = str;
                    withdraw_dataGridView.Rows[row].Cells[cell].Value = Edit.currencyConverter(str, Main.course);
                }
            }
            catch { }
        }
        private void withdraw_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = withdraw_dataGridView.CurrentCell.RowIndex;
                int cell = withdraw_dataGridView.CurrentCell.ColumnIndex;
                if (cell == 2 || cell == 3)
                {
                    withdraw_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                    Main.save_str = "";
                }
            }
            catch { }
        }
        private void withdraw_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            WithdrawPresenter.drawDTGView();
        }

        //tree
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        private void checkOwnList_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkOwnList_MainStripMenu.PerformClick();
        }
        private void floatCheck_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            floatCheck_MainStripMenu.PerformClick();
        }
        private void settings_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings_MainStripMenu.PerformClick();
        }
        private void exit_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem.PerformClick();
        }
        //links
        private void marketLink_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.openUrl("https://steamcommunity.com/market/");
        }
        private void inventoryLink_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.openUrl("https://steamcommunity.com/my/inventory#730/");
        }
        private void csmoneyLink_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!TryskinsConfig.Default.oldDesign)
                Edit.openUrl("https://cs.money");
            else
                Edit.openUrl("https://old.cs.money");
        }
        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.openUrl("https://cs.money/transactions/");
        }
        private void tryskinsLink_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.openUrl(TrySkins.url);
        }
    }
}
