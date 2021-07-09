using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Settings;

namespace ItemChecker
{
    public partial class ServiceCheckerForm : Form
    {
        CheckListForm checkList = new CheckListForm("SortList");
        public ServiceCheckerForm()
        {
            InitializeComponent();
            Program.serviceCheckerForm = this;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            firstSer_comboBox.SelectedIndex = 0;
            secondSer_comboBox.SelectedIndex = 1;
        }
        private void ServiceCheckerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Main.loading | servChecker_dataGridView.Rows.Count > 200)
            {
                DialogResult result = MessageBox.Show(
                  "Do you want to close?",
                  "Warning",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ServiceChecker.checkStop = true;
                    ServiceChecker._clear();
                    Main.loading = false;
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        //menu
        private void add_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkList.ShowDialog();
            count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
        }
        private void check_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ser_1 = firstSer_comboBox.SelectedIndex;
            int ser_2 = secondSer_comboBox.SelectedIndex;
            if (Main.checkList.Count != 0 & !Main.loading & ser_1 != ser_2)
            {
                MainPresenter.clearDTGView(servChecker_dataGridView);
                servChecker_menuStrip.Enabled = false;
                quick_button.Enabled = false;
                updated_toolStripStatusLabel.Visible = false;
                status_toolStripStatusLabel.Text = "Checking the list...";
                status_toolStripStatusLabel.Visible = true;
                if (GeneralConfig.Default.proxy)
                    timeLeft_toolStripStatusLabel.Visible = true;
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
                services_toolStripStatusLabel.Text = $"{firstSer_comboBox.Text} ➤ {secondSer_comboBox.Text}";
                ServiceChecker.service_one = ser_1;
                ServiceChecker.service_two = ser_2;
                Main.loading = true;

                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.checkMain);
            }
        }
        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_toolStripStatusLabel.Text = "Extract the list to *.csv...";
            status_toolStripStatusLabel.Visible = true;
            ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.extractCsv);
        }

        //comboboxs
        private void firstSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstSer_comboBox.SelectedIndex == 0) //steam
            {
                servChecker_dataGridView.Columns[2].HeaderText = "Price (ST)";
                servChecker_dataGridView.Columns[3].HeaderText = "BuyOrder (ST)";
            }
            if (firstSer_comboBox.SelectedIndex == 1) //csmoney
            {
                servChecker_dataGridView.Columns[2].HeaderText = "Price (CSM)";
                servChecker_dataGridView.Columns[3].HeaderText = "Get (CSM)";
            }
            if (firstSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                servChecker_dataGridView.Columns[2].HeaderText = "Price (LF)";
                servChecker_dataGridView.Columns[3].HeaderText = "Get (LF)";
            }
        }
        private void secondSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secondSer_comboBox.SelectedIndex == 0) //steam
            {
                servChecker_dataGridView.Columns[4].HeaderText = "Price (ST)";
                servChecker_dataGridView.Columns[5].HeaderText = "BuyOrder (ST)";
                servChecker_dataGridView.Columns[8].HeaderText = "Status";
            }
            if (secondSer_comboBox.SelectedIndex == 1) //csmoney
            {
                servChecker_dataGridView.Columns[4].HeaderText = "Price (CSM)";
                servChecker_dataGridView.Columns[5].HeaderText = "Get (CSM)";
                servChecker_dataGridView.Columns[8].HeaderText = "Status (CSM)";
            }
            if (secondSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                servChecker_dataGridView.Columns[4].HeaderText = "Price (LF)";
                servChecker_dataGridView.Columns[5].HeaderText = "Get (LF)";
                servChecker_dataGridView.Columns[8].HeaderText = "Status (LF)";
            }
        }

        //quick
        private void quick_button_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                Main.checkList.Clear();
                Main.checkList.Add(textBox.Text.Trim());

                check_toolStripMenuItem.PerformClick();
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                quick_button.PerformClick();
        }        

        //table
        private void ownList_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = Convert.ToInt32(servChecker_dataGridView.CurrentCell.RowIndex.ToString());
            string iname = servChecker_dataGridView.Rows[row].Cells[1].Value.ToString();
            int index = servChecker_dataGridView.CurrentCell.ColumnIndex;
            string url = null;

            if (index == 1)
            {
                Clipboard.SetText(iname);
            }
            if (index == 2 | index == 3)
            {
                if (ServiceChecker.service_one == 0)
                    url = "https://steamcommunity.com/market/listings/730/";
                if (ServiceChecker.service_one == 1)
                    url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=";
                Edit.openUrl(url + Edit.replaceUrl(iname));
            }
            if (index == 4 | index == 5)
            {
                if (ServiceChecker.service_two == 0)
                    url = "https://steamcommunity.com/market/listings/730/";
                if (ServiceChecker.service_two == 1)
                    url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=";
                Edit.openUrl(url + Edit.replaceUrl(iname));
            }
        }
        private void ownList_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = servChecker_dataGridView.CurrentCell.Value.ToString();
                int row = Convert.ToInt32(servChecker_dataGridView.CurrentCell.RowIndex);
                int cell = servChecker_dataGridView.CurrentCell.ColumnIndex;
                if (1 < cell & cell < 6)
                {
                    Main.save_str = str;
                    servChecker_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
                }
                if (secondSer_comboBox.SelectedIndex == 0)
                {
                    updated_toolStripStatusLabel.Text = "Updated(h): " + Edit.convertTime(Convert.ToDouble(ServiceChecker.stUpdated[row])) + "(ST) | " + Edit.convertTime(Convert.ToDouble(ServiceChecker.csmUpdated[row])) + "(CSM)";
                    updated_toolStripStatusLabel.Visible = true;
                }
            }
            catch { }
        }
        private void ownList_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int cell = servChecker_dataGridView.CurrentCell.ColumnIndex;
            if (1 < cell & cell < 6)
            {
                int row = Convert.ToInt32(servChecker_dataGridView.CurrentCell.RowIndex);
                servChecker_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                Main.save_str = "";
            }
        }
        private void ownList_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Insert & ServiceChecker.service_one == 0)
                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.addQueue);
        }
        private void servChecker_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ServiceCheckerPresenter.drawDTGView();
        }
    }
}