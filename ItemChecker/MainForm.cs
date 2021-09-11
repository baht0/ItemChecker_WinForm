using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Settings;
using Keys = System.Windows.Forms.Keys;
using System.Drawing;

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
            if (GeneralConfig.Default.exitChrome)
                foreach (Process proc in Process.GetProcessesByName("chrome")) proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("conhost"))
            {
                try {
                    proc.Kill();
                }
                catch {
                    continue;
                }
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        //file
        private void settings_MainStripMenu_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["SettingsForm"] as SettingsForm) == null)
            {
                SettingsForm settingsForm = new();
                settingsForm.ShowDialog();
            }
            else
                Application.OpenForms["SettingsForm"].Activate();
        }
        private void restart_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                DialogResult result = MessageBox.Show(
                    "Do you really want restart program?",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Main.loading = true;

                    MainPresenter.Restart();
                    ThreadPool.QueueUserWorkItem(StartingPresenter.Start);
                }
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
                MainPresenter.stopTimers();
                MainPresenter.Exit();
            }
        }
        //reload
        private void full_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 0;
                ThreadPool.QueueUserWorkItem(MainPresenter.Reload);
            }
        }
        private void tryskins_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 1;
                ThreadPool.QueueUserWorkItem(MainPresenter.Reload);
            }
        }
        private void buyOrders_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 2;
                ThreadPool.QueueUserWorkItem(MainPresenter.Reload);
            }
        }
        private void updateData_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 3;
                ThreadPool.QueueUserWorkItem(MainPresenter.Reload);
            }
        }
        //withdraw
        private void showWithdraw_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading & !withdraw_dataGridView.Visible)
            {
                MainPresenter.stopTimers();
                showWithdraw_toolStripMenuItem.Text = "Close";
                withdraw_dataGridView.Visible = true;
                reload_MainStripMenu.Enabled = false;
                reloadWithdraw_toolStripMenuItem.Enabled = true;
                status_StripStatus.Text = "Check Withdraw...";
                status_StripStatus.Visible = true;
                Main.loading = true;
                ThreadPool.QueueUserWorkItem(WithdrawPresenter.withdraw);
            }
            else if (withdraw_dataGridView.Visible)
            {
                reloadWithdraw_toolStripMenuItem.Enabled = false;
                reload_MainStripMenu.Enabled = true;
                withdraw_dataGridView.Visible = false;
                showWithdraw_toolStripMenuItem.Text = "Load";
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
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to withdraw all available\nitems from the Cs.Money inventory?",
                    "Question", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Main.loading = true;
                    MainPresenter.stopTimers();
                    status_StripStatus.Text = "Checking Cs.Money...";
                    status_StripStatus.Visible = true;
                    ThreadPool.QueueUserWorkItem(WithdrawPresenter.inventoryCsm);
                }
            }
        }
        //tools
        private void serviceParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ServiceParserForm"] as ServiceParserForm) == null)
            {
                ServiceParserForm serviceChecker = new();
                serviceChecker.Show();
            }
            else
                Application.OpenForms["ServiceParserForm"].Activate();
        }
        //<extract
        private void trySkinsTotxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrySkins.item != null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo("extract");
                if (!dirInfo.Exists) dirInfo.Create();
                string str = null;
                foreach (string i in TrySkins.item)
                    str += i + "\r\n";
                str = str.Remove(str.Length - 2);
                File.WriteAllText($"extract/tryskinsList_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.txt", str);
            }
        }
        private void buyOrdersTotxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BuyOrder.item != null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo("extract");
                if (!dirInfo.Exists) dirInfo.Create();
                string str = null;
                foreach (string i in BuyOrder.item)
                    str += i + "\r\n";
                str = str.Remove(str.Length - 2);
                File.WriteAllText($"extract/buyOrdersList_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.txt", str);
            }
        }
        //extract>
        private void buyOrderPush_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading & BuyOrder.item.Any() & !BuyOrder.timer.Enabled & !Withdraw.timer.Enabled  & !Float.timer.Enabled)
            {
                pusherItems_label.Text = $"Items: {BuyOrder.item.Count}";
                pusherBuyOrder_groupBox.Visible = true;
                timer_StripStatus.Visible = true;
                buyOrderPush_toolStripMenuItem.ForeColor = Color.OrangeRed;

                BuyOrder.tick = SteamConfig.Default.timer * 60;
                BuyOrder.timer.Enabled = true;
            }
            else if (BuyOrder.timer.Enabled & BuyOrder.tick > 1)
                BuyOrderPresenter.stopBuyOrderPusher();
            else if (!BuyOrder.item.Any())
            {
                MessageBox.Show(
                  "You need to add at least 1 item to continue.",
                  "Warning",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
            }
        }
        private void favoriteCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading & Withdraw.favoriteList.Any() & !Withdraw.timer.Enabled & !BuyOrder.timer.Enabled & !Float.timer.Enabled)
            {
                favoriteItems_label.Text = $"Items: {Withdraw.favoriteList.Count}";
                favoriteCheck_groupBox.Visible = true;
                timer_StripStatus.Visible = true;
                favoriteCheckToolStripMenuItem.ForeColor = Color.OrangeRed;

                Withdraw.tick = WithdrawConfig.Default.timer;
                Withdraw.timer.Enabled = true;
            }
            else if (Withdraw.timer.Enabled & Withdraw.tick > 1)
                WithdrawPresenter.stopCheckFavorite();
            else if (!Withdraw.favoriteList.Any())
            {
                MessageBox.Show(
                  "You need to add at least 1 item to continue.",
                  "Warning",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);

                SettingsForm settingsForm = new(3);
                settingsForm.ShowDialog();
            }
        }
        private void floatCheck_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading & Float.floatList.Any() & !Float.timer.Enabled & !Withdraw.timer.Enabled & !BuyOrder.timer.Enabled)
            {
                floatItems_label.Text = $"Items: {Float.floatList.Count}";
                floatCheck_groupBox.Visible = true;
                timer_StripStatus.Visible = true;
                floatCheck_MainStripMenu.ForeColor = Color.OrangeRed;

                Float.tick = FloatConfig.Default.timer * 60;
                Float.timer.Enabled = true;
            }
            else if (Float.timer.Enabled & Float.tick > 1)
                FloatPresenter.stopCheckFloat();
            else if (!Float.floatList.Any())
            {
                MessageBox.Show(
                   "You need to add at least 1 item to continue.",
                   "Warning",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);

                SettingsForm settingsForm = new(4);
                settingsForm.ShowDialog();
            }
        }
        //about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new();
            aboutForm.ShowDialog();
        }

        //linkLabels
        private void tradeOffers_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Main.loading & !String.IsNullOrEmpty(SteamConfig.Default.steamApiKey))
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to ACCEPT trade offers?",
                    "Question",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    MainPresenter.stopTimers();
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(TradeOfferPresenter.tradeOffers);
                }
            }
            else if (String.IsNullOrEmpty(SteamConfig.Default.steamApiKey))
            {
                MessageBox.Show(
                    "To continue you need a Steam API Key.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                SettingsForm settingsForm = new(1);
                settingsForm.ShowDialog();
            }
        }
        private void queue_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Main.loading & BuyOrder.queue.Count > 0)
            {
                if (BuyOrder.queue_rub < BuyOrder.available_amount)
                {
                    MainPresenter.stopTimers();
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
        private void timer_StripStatus_Click(object sender, EventArgs e)
        {
            if (Main.loading)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to terminate the current process?", 
                    "Question", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    Main.cts.Cancel();
            }
            if (BuyOrder.timer.Enabled)
                BuyOrder.tick = 1;
            else if (Withdraw.timer.Enabled)
                Withdraw.tick = 1;
            else if (Float.timer.Enabled)
                Float.tick = 1;
        }
        private void timer_StripStatus_MouseEnter(object sender, EventArgs e)
        {
            if (Main.loading)
            {
                timer_StripStatus.ForeColor = Color.Red;
                timer_StripStatus.ToolTipText = "Click to stop.";
            }
            else
                timer_StripStatus.ToolTipText = "Click to speed up.";

        }
        private void timer_StripStatus_MouseLeave(object sender, EventArgs e)
        {
            if (Main.loading)
                timer_StripStatus.ForeColor = Color.Black;
        }

        //tryskins table
        private void tryskins_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = tryskins_dataGridView;
            int row = dataGridView.CurrentCell.RowIndex;
            int column = dataGridView.CurrentCell.ColumnIndex;
            string item = dataGridView.Rows[row].Cells[1].Value.ToString();
            string market_has_name = Edit.replaceUrl(item);
            string url;

            if (column == 1)
                Clipboard.SetText(item);
            if (column == 2)
            {
                url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                Edit.openUrl(url);
            }
            if (column == 3)
                Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            if (column == 4)
            {
                url = TrySkins.url.Replace("ItemsFilter%5Bname%5D=", "ItemsFilter%5Bname%5D=" + market_has_name);
                Edit.openUrl(url);
            }
        }
        private void tryskins_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = tryskins_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;
                string str = dataGridView.CurrentCell.Value.ToString();

                if (column == 2 | column == 3)
                {
                    Main.save_str = str;
                    dataGridView.Rows[row].Cells[column].Value = Edit.currencyConverter(str, GeneralConfig.Default.currency);
                }
            }
        }
        private void tryskins_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = tryskins_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;

                if (column == 2 | column == 3)
                {
                    dataGridView.Rows[row].Cells[column].Value = Main.save_str;
                    Main.save_str = null;
                }
            }
        }
        private void tryskins_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView = tryskins_dataGridView;
            if (dataGridView.RowCount > 0 & e.KeyCode == Keys.Insert)
            {
                int sta = 2;
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;

                if (column == sta)
                    dataGridView.CurrentCell = dataGridView.Rows[row].Cells[1];

                string item = dataGridView.Rows[row].Cells[1].Value.ToString();
                decimal price = Edit.removeSymbol(dataGridView.Rows[row].Cells[sta].Value.ToString());
                
                ThreadPool.QueueUserWorkItem(BuyOrderPresenter.addQueue, new object[] { dataGridView, row, item, price, sta });
            }
        }
        private void tryskins_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = tryskins_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                TryskinsPresenter.drawDTGView();
            }
        }
        //buyorder table
        private void buyOrder_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = buyOrder_dataGridView;
            int row = dataGridView.CurrentCell.RowIndex;
            string item = dataGridView.Rows[row].Cells[1].Value.ToString();
            string market_has_name = Edit.replaceUrl(item);
            string url;

            if (dataGridView.CurrentCell.ColumnIndex == 1)
                Clipboard.SetText(item);
            if (dataGridView.CurrentCell.ColumnIndex == 2)
            {
                url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                Edit.openUrl(url);
            }
            if (dataGridView.CurrentCell.ColumnIndex == 3)
                Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
        }
        private void buyOrder_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = buyOrder_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;
                string str = dataGridView.CurrentCell.Value.ToString();

                if (column == 3 & (str != "Overstock" & str != "Unavailable"))
                {
                    Main.save_str = str;
                    dataGridView.Rows[row].Cells[column].Value = Edit.currencyConverter(str, GeneralConfig.Default.currency);
                }
            }
        }
        private void buyOrder_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = buyOrder_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;

                if (column == 3 & Main.save_str != null)
                {
                    dataGridView.Rows[row].Cells[column].Value = Main.save_str;
                    Main.save_str = null;
                }
            }
        }
        private void buyOrder_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView = buyOrder_dataGridView;
            if (dataGridView.RowCount <= 0)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show(
                      "Do you want to DELETE this item?",
                      "Warning",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                    int row = dataGridView.CurrentCell.RowIndex;
                    string item = dataGridView.Rows[row].Cells[1].Value.ToString();

                    int index = BuyOrder.item.IndexOf(item);
                    BuyOrderPresenter.CancelOrder(index);

                    dataGridView.Rows[row].Cells[2].Style.BackColor = Color.Red;
                    dataGridView.Rows[row].Cells[2].Value = "Cancel";
                }
            }
        }
        private void buyOrder_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = buyOrder_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                BuyOrderPresenter.drawDTGView();
            }
        }
        //withdraw table
        private void withdraw_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = withdraw_dataGridView;
            int row = dataGridView.CurrentCell.RowIndex;
            int column = dataGridView.CurrentCell.ColumnIndex;
            string item = dataGridView.Rows[row].Cells[1].Value.ToString();
            string market_has_name = Edit.replaceUrl(item);
            string url;

            if (column == 1 || column == 2)
            {
                Clipboard.SetText(item);
                Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
                dataGridView.Rows[row].Cells[1].Style.BackColor = Color.Silver;
            }
            if (column == 3 || column == 4)
            {
                url = "https://steamcommunity.com/market/listings/730/" + market_has_name;
                Edit.openUrl(url);
            }
            if (column == 5)
            {
                url = Withdraw.url.Replace("ItemsFilter%5Bname%5D=", "ItemsFilter%5Bname%5D=" + market_has_name);
                Edit.openUrl(url);
            }
        }
        private void withdraw_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = withdraw_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;
                string str = dataGridView.CurrentCell.Value.ToString();

                if (column == 2 | column == 3)
                {
                    Main.save_str = str;
                    dataGridView.Rows[row].Cells[column].Value = Edit.currencyConverter(str, GeneralConfig.Default.currency);
                }
            }
        }
        private void withdraw_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = withdraw_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;

                if (column == 2 | column == 3)
                {
                    dataGridView.Rows[row].Cells[column].Value = Main.save_str;
                    Main.save_str = null;
                }
            }
        }
        private void withdraw_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (withdraw_dataGridView.RowCount > 0)
                WithdrawPresenter.drawDTGView();
        }

        //tree
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((Application.OpenForms["StartingForm"] as StartingForm) == null)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ServiceParserForm"] as ServiceParserForm) != null)
                Application.OpenForms["ServiceParserForm"].Activate();
            else if ((Application.OpenForms["MainForm"] as MainForm) != null)
            {
                Application.OpenForms["MainForm"].BringToFront();
                Application.OpenForms["MainForm"].Activate();
            }
            else if ((Application.OpenForms["MainForm"] as MainForm) == null)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        private void tree_serviceParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serviceParserToolStripMenuItem.PerformClick();
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
