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
using OpenQA.Selenium;
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
                    MessageBoxIcon.Warning
                    );
                Process.GetCurrentProcess().Kill();
            }
            foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
            foreach (Process proc in Process.GetProcessesByName("conhost")) proc.Kill();
            notifyIcon.Visible = true;
            ver_label.Text = "Version: " + Main.version;
        }
        public void MainForm_Shown(object sender, EventArgs e)
        {
            status_StripStatus.Text = "Launch Browser...";
            BuyOrderPresenter buyOrderPresenter = new BuyOrderPresenter();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Main.timer.Elapsed += new ElapsedEventHandler(buyOrderPresenter.timerTick);
            Main.timer.Interval = 1000;
            Main.timer.AutoReset = true;
            ThreadPool.QueueUserWorkItem(MainPresenter.Start);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loading_panel.Visible != true)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.Hide();
                }
            }
            else
            {
                loading_pictureBox.Visible = true;
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
        private void printScreen_MainStripMenu_Click(object sender, EventArgs e)
        {
            ((ITakesScreenshot)Main.Browser).GetScreenshot().SaveAsFile("Screen.png", ScreenshotImageFormat.Png);
            Process.Start(new ProcessStartInfo("cmd", $"/c start {Application.StartupPath + "\\Screen.png"}"));
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
        private void withdrawReload_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                Main.reload = 4;
                ThreadPool.QueueUserWorkItem(MainPresenter._reload, new object[] { 2 });
            }
        }
        //tools
        private void checkOwnList_MainStripMenu_Click(object sender, EventArgs e)
        {
            CheckOwnListForm fr = new CheckOwnListForm();
            fr.Show();
        }
        private void floatCheck_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                CheckListForm fr = new CheckListForm("MainForm");
                fr.ShowDialog();
            }            
        }
        private void withdrawTable_MainStripMenu_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                if (!withdraw_dataGridView.Visible)
                {
                    MainPresenter.stopPush();
                    withdrawTable_MainStripMenu.Text = "Close Withdraw";
                    withdraw_dataGridView.Visible = true;
                    withdrawReload_MainStripMenu.Enabled = true;
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(WithdrawPresenter.withdraw);
                }
                else
                {
                    withdrawReload_MainStripMenu.Enabled = false;
                    withdraw_dataGridView.Visible = false;
                    withdrawTable_MainStripMenu.Text = "Withdraw";
                }
            }
        }

        //linkLabels
        private void tradeOffers_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!Main.loading & String.IsNullOrEmpty(GeneralConfig.Default.steamApiKey))
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
            if (!Main.loading & BuyOrder.queue.Count > 0 & BuyOrder.queue_count > 0)
            {
                if (BuyOrder.order_rub < BuyOrder.available_amount)
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
            if (BuyOrder.count > 0 & !Main.loading)
            {
                if (push_linkLabel.Text == "Push...")
                {
                    BuyOrder.tick = SteamConfig.Default.timer * 60;

                    timer_StripStatus.Visible = true;
                    push_linkLabel.Text = "Stop...";
                    Main.timer.Start();
                }
                else
                {
                    if (BuyOrder.tick > 1) MainPresenter.stopPush();
                }
            }
        }
        private void timer_StripStatus_Click(object sender, EventArgs e)
        {
            if (push_linkLabel.Text == "Stop...") BuyOrder.tick = 1;
        }

        //tryskins table
        private void tryskins_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(tryskins_dataGridView.CurrentCell.RowIndex.ToString());
                string iname = tryskins_dataGridView.Rows[row].Cells[1].Value.ToString();
                int cell = tryskins_dataGridView.CurrentCell.ColumnIndex;
                string str = Edit.replaceUrl(iname);
                string url;

                if (cell == 1)
                {
                    Clipboard.SetText(iname);
                }
                if (cell == 2)
                {
                    url = "https://steamcommunity.com/market/listings/730/" + str;
                    Edit.openUrl(url);
                }
                if (cell == 3)
                {
                    url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=" + str;
                    Edit.openUrl(url);
                }
                if (cell == 4)
                {
                    url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showsteama&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=&ItemsFilter%5BpriceTo1%5D=&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=" + str + "&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=&ItemsFilter%5BpercentTo2%5D=";
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
                    tryskins_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
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
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Insert) ThreadPool.QueueUserWorkItem(TryskinsPresenter.addQueue);
        }
        //buyorder table
        private void buyOrder_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = Convert.ToInt32(buyOrder_dataGridView.CurrentCell.RowIndex.ToString());
            string iname = buyOrder_dataGridView.Rows[row].Cells[1].Value.ToString();
            string str = Edit.replaceUrl(iname);
            string url;

            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 1)
            {
                Clipboard.SetText(iname);
            }
            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 2)
            {
                url = "https://steamcommunity.com/market/listings/730/" + str;
                Edit.openUrl(url);
            }
            if (buyOrder_dataGridView.CurrentCell.ColumnIndex == 3)
            {
                url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=" + str;
                Edit.openUrl(url);
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
                    buyOrder_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
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
                {
                    ThreadPool.QueueUserWorkItem(BuyOrderPresenter.deleteOrder);
                }
            }
        }
        //withdraw
        private void withdraw_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(withdraw_dataGridView.CurrentCell.RowIndex.ToString());
                string iname = withdraw_dataGridView.Rows[row].Cells[1].Value.ToString();
                int cell = withdraw_dataGridView.CurrentCell.ColumnIndex;
                string str = Edit.replaceUrl(iname);
                string url;

                if (cell == 1 || cell == 2)
                {
                    url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=" + str;
                    Edit.openUrl(url);
                    withdraw_dataGridView.Rows[row].Cells[1].Style.BackColor = System.Drawing.Color.Silver;
                }
                if (cell == 3 || cell == 4)
                {
                    url = "https://steamcommunity.com/market/listings/730/" + str;
                    Edit.openUrl(url);
                }
                if (cell == 5)
                {
                    url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showcsmoney&ItemsFilter%5Bservice2%5D=showsteam&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=&ItemsFilter%5BpriceTo1%5D=&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=" + str + "&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=&ItemsFilter%5BpercentTo2%5D=";
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
                    withdraw_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
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
            Edit.openUrl("https://old.cs.money");
        }
        private void tryskinsLink_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.openUrl(TrySkins.url);
        }
    }
}
