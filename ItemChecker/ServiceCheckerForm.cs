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
        private void ServiceCheckerForm_Load(object sender, EventArgs e)
        {
            category_comboBox.SelectedIndex = 0;
            other_comboBox.SelectedIndex = 0;
            status_comboBox.SelectedIndex = 0;
            column_comboBox.SelectedIndex = 0;
            Filters_groupBox.Enabled = false;
            Prices_groupBox.Enabled = false;
            Precent_groupBox.Enabled = false;
            buttons_groupBox.Enabled = false;
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
                ServiceChecker.service_one = ser_1;
                ServiceChecker.service_two = ser_2;
                while (column_comboBox.Items.Count > 1)
                    column_comboBox.Items.RemoveAt(1);
                for (int i = 1; i <= 4; i++)
                    column_comboBox.Items.Insert(i, servChecker_dataGridView.Columns[i + 1].HeaderText);

                servChecker_menuStrip.Enabled = false;
                quick_button.Enabled = false;
                updated_toolStripStatusLabel.Visible = false;
                status_toolStripStatusLabel.Text = "Checking the list...";
                status_toolStripStatusLabel.Visible = true;
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
                services_toolStripStatusLabel.Text = $"From {firstSer_comboBox.Text} To {secondSer_comboBox.Text}";
                services_toolStripStatusLabel.Visible = true;

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
                    servChecker_dataGridView.Rows[row].Cells[cell].Value = Math.Round(Convert.ToDouble(str) * Main.course, 2);
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
                servChecker_dataGridView.Rows[row].Cells[cell].Value = Convert.ToDouble(Main.save_str);
                Main.save_str = null;
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

        //filters
        private void apply_button_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = string.Empty;
                //filters
                if (category_comboBox.SelectedIndex != 0)
                {
                    if (category_comboBox.SelectedIndex != 1)
                        filter += $"AND item_Column LIKE '{category_comboBox.Text}%'";
                    else
                        for (int i = 2; i < 6; i++)
                            filter += $"AND item_Column NOT LIKE '{category_comboBox.Items[i]}%'";
                }
                else if (other_comboBox.SelectedIndex != 0)
                    filter += $"AND item_Column LIKE '%{other_comboBox.Text}%'";
                if (status_comboBox.SelectedIndex != 0)
                    filter += $"AND status_Column LIKE '%{status_comboBox.Text}%'";
                //prices
                if (column_comboBox.SelectedIndex != 0 & priceFrom_numericUpDown.Value != 0)
                    filter += $"AND price{column_comboBox.SelectedIndex}_Column > {priceFrom_numericUpDown.Value}";
                if (column_comboBox.SelectedIndex != 0 & priceTo_numericUpDown.Value != 0)
                    filter += $"AND price{column_comboBox.SelectedIndex}_Column < {priceTo_numericUpDown.Value}";
                //precent
                if (precentFrom_numericUpDown.Value != 0)
                    filter += $"AND precent_Column > {precentFrom_numericUpDown.Value}";
                if (precentTo_numericUpDown.Value != 0)
                    filter += $"AND precent_Column < {precentTo_numericUpDown.Value}";

                if (filter != string.Empty)
                    ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { filter.Remove(0, 4) });
                else
                    ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { string.Empty });
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
                MessageBox.Show("Nothing found.", "Filter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            ServiceCheckerPresenter.ResetFilter();
        }
        private void category_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = other_comboBox.SelectedIndex;
            if (index != 0)
                other_comboBox.SelectedIndex = 0;
        }
        private void other_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = category_comboBox.SelectedIndex;
            if (index != 0)
                category_comboBox.SelectedIndex = 0;
        }
    }
}