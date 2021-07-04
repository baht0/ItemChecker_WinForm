using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;

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
            ServiceChecker.checkStop = true;
            Main.loading = false;
            this.Hide();
            e.Cancel = true;
        }

        //list
        private void add_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkList.ShowDialog();
            count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
        }
        private void firstSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstSer_comboBox.SelectedIndex == 0) //steam
            {
                ownList_dataGridView.Columns[2].HeaderText = "Price (ST)";
                ownList_dataGridView.Columns[3].HeaderText = "BuyOrder (ST)";
            }
            if (firstSer_comboBox.SelectedIndex == 1) //csmoney
            {
                ownList_dataGridView.Columns[2].HeaderText = "Price (CSM)";
                ownList_dataGridView.Columns[3].HeaderText = "Get (CSM)";
            }
            if (firstSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                ownList_dataGridView.Columns[2].HeaderText = "Price (LF)";
                ownList_dataGridView.Columns[3].HeaderText = "Get (LF)";
            }
        }
        private void secondSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secondSer_comboBox.SelectedIndex == 0) //steam
            {
                ownList_dataGridView.Columns[4].HeaderText = "Price (ST)";
                ownList_dataGridView.Columns[5].HeaderText = "BuyOrder (ST)";
                ownList_dataGridView.Columns[8].HeaderText = "Status";
            }
            if (secondSer_comboBox.SelectedIndex == 1) //csmoney
            {
                ownList_dataGridView.Columns[4].HeaderText = "Price (CSM)";
                ownList_dataGridView.Columns[5].HeaderText = "Get (CSM)";
                ownList_dataGridView.Columns[8].HeaderText = "Status (CSM)";
            }
            if (secondSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                ownList_dataGridView.Columns[4].HeaderText = "Price (LF)";
                ownList_dataGridView.Columns[5].HeaderText = "Get (LF)";
                ownList_dataGridView.Columns[8].HeaderText = "Status (LF)";
            }
        }
        private void check_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ser_1 = firstSer_comboBox.SelectedIndex;
            int ser_2 = secondSer_comboBox.SelectedIndex;
            if (Main.checkList.Count != 0 & !Main.loading & ser_1 != ser_2)
            {
                ownList_menuStrip.Enabled = false;
                quick_button.Enabled = false;
                updated_toolStripStatusLabel.Visible = false;
                status_toolStripStatusLabel.Text = "Processing...";
                status_toolStripStatusLabel.Visible = true;
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
                ownList_dataGridView.Rows.Clear();
                this.Text = $"ServiceChecker: {firstSer_comboBox.Text} - {secondSer_comboBox.Text}";
                ServiceChecker.service_one = ser_1;
                ServiceChecker.service_two = ser_2;

                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.checkMain);
            }
        }
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
            int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex.ToString());
            string iname = ownList_dataGridView.Rows[row].Cells[1].Value.ToString();
            int index = ownList_dataGridView.CurrentCell.ColumnIndex;
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
                string str = ownList_dataGridView.CurrentCell.Value.ToString();
                int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex);
                int cell = ownList_dataGridView.CurrentCell.ColumnIndex;
                if (1 < cell & cell < 6)
                {
                    Main.save_str = str;
                    ownList_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
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
            int cell = ownList_dataGridView.CurrentCell.ColumnIndex;
            if (1 < cell & cell < 6)
            {
                int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex);
                ownList_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                Main.save_str = "";
            }
        }
        private void ownList_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Insert & ServiceChecker.service_one == 0) 
                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.addQueue);
        }
    }
}