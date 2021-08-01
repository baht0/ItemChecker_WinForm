using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;
using System.Linq;
using ItemChecker.Settings;
using System.IO;

namespace ItemChecker
{
    public partial class ServiceCheckerForm : Form
    {
        int past_row = 1;
        CheckListForm checkListForm = new("CheckList");
        public ServiceCheckerForm()
        {
            InitializeComponent();
            ServiceChecker.checkStop = false;
            Program.serviceCheckerForm = this;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
        private void ServiceCheckerForm_Load(object sender, EventArgs e)
        {
            firstSer_comboBox.SelectedIndex = 0;
            secondSer_comboBox.SelectedIndex = 1;

            category_comboBox.SelectedIndex = 0;
            other_comboBox.SelectedIndex = 0;
            status_comboBox.SelectedIndex = 0;
            column_comboBox.SelectedIndex = 0;
            hide100_checkBox.Checked = true;
            hide0_checkBox.Checked = true;
            ServiceCheckerPresenter.columnDTable();
        }
        private void ServiceCheckerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Main.loading | ServiceChecker.dataTable.Rows.Count > 150)
            {
                DialogResult result = MessageBox.Show(
                  "Do you want to close?",
                  "Warning",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ServiceChecker.checkStop = true;
                    ServiceCheckerPresenter.ClearAll(true, true, true);
                    Main.loading = false;
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
                ServiceCheckerPresenter.ClearAll(true, true, true);
        }

        //menu
        private void add_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                checkListForm.ShowDialog();
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
            }
        }
        private void check_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ser_1 = firstSer_comboBox.SelectedIndex;
            int ser_2 = secondSer_comboBox.SelectedIndex;

            if (Main.checkList.Count != 0 & !Main.loading & ser_1 != ser_2)
            {
                Main.loading = true;
                ServiceChecker.service_one = ser_1;
                ServiceChecker.service_two = ser_2;
                while (column_comboBox.Items.Count > 1)
                    column_comboBox.Items.RemoveAt(1);
                for (int i = 1; i <= 4; i++)
                    column_comboBox.Items.Insert(i, servChecker_dataGridView.Columns[i + 1].HeaderText);

                servChecker_dataGridView.Enabled = false;
                updated_toolStripStatusLabel.Visible = false;
                status_toolStripStatusLabel.Text = "Checking the list...";
                status_toolStripStatusLabel.Visible = true;
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
                services_toolStripStatusLabel.Text = $"From {firstSer_comboBox.Text} To {secondSer_comboBox.Text}";
                services_toolStripStatusLabel.Visible = true;

                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.checkMain);
            }
        }
        private void extractToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Main.loading & servChecker_dataGridView.Rows.Count > 0)
            {
                Main.loading = true;
                status_toolStripStatusLabel.Text = "Export the list to *.csv...";
                status_toolStripStatusLabel.Visible = true;
                servChecker_dataGridView.Enabled = false;
                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.exportCsv);
            }
        }
        private void importToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                OpenFileDialog dialog = new()
                {
                    InitialDirectory = Application.StartupPath,
                    RestoreDirectory = true,
                    Filter = "Extraction (csv)|*.csv"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Main.loading = true;
                    status_toolStripStatusLabel.Text = "Import the list from *.csv...";
                    status_toolStripStatusLabel.Visible = true;
                    string fileName = Path.GetFileNameWithoutExtension(dialog.FileName).ToString();
                    fileName = fileName.Remove(0, 15).Replace("_", " ");
                    ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.importCsv, new object[] { dialog.FileName, fileName });
                }
            }
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
                servChecker_dataGridView.Columns[5].HeaderText = "BuyOrder (ST) [+%]";
                servChecker_dataGridView.Columns[8].HeaderText = "Status (ST)";
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
            if (quickCheck_textBox.Text != "" & !Main.loading)
            {
                Main.checkList.Clear();
                Main.checkList.Add(quickCheck_textBox.Text.Trim());

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
            string market_has_name = Edit.replaceUrl(iname);

            if (index == 1)
            {
                Clipboard.SetText(iname);
            }
            if (index == 2 | index == 3)
            {
                if (ServiceChecker.service_one == 0)
                    Edit.openUrl("https://steamcommunity.com/market/listings/730/" + market_has_name);
                if (ServiceChecker.service_one == 1)
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            }
            if (index == 4 | index == 5)
            {
                if (ServiceChecker.service_two == 0)
                    Edit.openUrl("https://steamcommunity.com/market/listings/730/" + market_has_name);
                if (ServiceChecker.service_two == 1)
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            }
        }
        private void ownList_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string value = servChecker_dataGridView.CurrentCell.Value.ToString();
                int row = servChecker_dataGridView.CurrentCell.RowIndex;
                int cell = servChecker_dataGridView.CurrentCell.ColumnIndex;

                if (ServiceChecker.service_one == 1 & past_row != row)
                {
                    availability_toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
                    availability_toolStripStatusLabel.Text = $"Availability: Checking...";
                    ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.checkCsmItem, new object[] { servChecker_dataGridView.Rows[row].Cells[1].Value.ToString(), row });
                    availability_toolStripStatusLabel.Visible = true;
                    past_row = row;
                }
                if (ServiceChecker.service_one == 2 & past_row != row)
                {
                    availability_toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
                    availability_toolStripStatusLabel.Text = $"Availability: Checking...";
                    ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.checkLootFarmItem, new object[] { servChecker_dataGridView.Rows[row].Cells[1].Value.ToString(), row });
                    availability_toolStripStatusLabel.Visible = true;
                    past_row = row;
                }
                if (1 < cell & cell < 6)
                {
                    Main.save_str = value;
                    servChecker_dataGridView.Rows[row].Cells[cell].Value = Math.Round(Convert.ToDecimal(value) * Main.course, 2);
                }
                if (ServiceChecker.stUpdated.Any() & ServiceChecker.csmUpdated.Any())
                {
                    value = servChecker_dataGridView.Rows[row].Cells[1].Value.ToString();
                    int index = Main.checkList.IndexOf(value);
                    updated_toolStripStatusLabel.Text = "Updated(h):";
                    if (ServiceChecker.service_one == 0 | ServiceChecker.service_two == 0)
                        updated_toolStripStatusLabel.Text += $" {Edit.convertTime(ServiceChecker.stUpdated[index])} (ST)";
                    if (ServiceChecker.service_one == 1 | ServiceChecker.service_two == 1)
                        updated_toolStripStatusLabel.Text += $" {Edit.convertTime(ServiceChecker.csmUpdated[index])} (CSM)";
                    updated_toolStripStatusLabel.Visible = true;
                }                
            }
            catch { }
        }
        private void ownList_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int row = servChecker_dataGridView.CurrentCell.RowIndex;
            int cell = servChecker_dataGridView.CurrentCell.ColumnIndex;
            if (1 < cell & cell < 6)
            {
                servChecker_dataGridView.Rows[row].Cells[cell].Value = Convert.ToDouble(Main.save_str);
                Main.save_str = null;
            }
        }
        private void ownList_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (ServiceChecker.service_one == 0 & e.KeyCode == Keys.Insert)
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
                if (!Main.loading & ServiceChecker.dataTable.Rows.Count > 1)
                {
                    string filter = string.Empty;
                    //search
                    if (!String.IsNullOrEmpty(search_textBox.Text))
                        filter += $"AND item_Column LIKE '%{search_textBox.Text}%'";
                    //filters
                    if (category_comboBox.SelectedIndex != 0)
                    {
                        if (category_comboBox.SelectedIndex != 1)
                            filter += $"AND item_Column LIKE '{category_comboBox.Text}%'";
                        else
                        {
                            for (int i = 2; i < 6; i++)
                                filter += $"AND item_Column NOT LIKE '%{category_comboBox.Items[i]}%'";
                            for (int i = 4; i < 13; i++)
                                filter += $"AND item_Column NOT LIKE '%{other_comboBox.Items[i]}%'";
                        }
                    }
                    else if (other_comboBox.SelectedIndex != 0)
                    {
                        if (other_comboBox.SelectedIndex == 1)
                            for (int i = 4; i < 13; i++)
                                filter += $"AND item_Column NOT LIKE '%{other_comboBox.Items[i]}%'";
                        else
                            filter += $"AND item_Column LIKE '%{other_comboBox.Text}%'";
                    }
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
                    if (hide100_checkBox.Checked)
                        filter += $"AND precent_Column <> -100";
                    if (hide0_checkBox.Checked)
                        filter += $"AND precent_Column <> 0";

                    if (filter != string.Empty)
                        ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { filter.Remove(0, 4) });
                    else
                        ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { string.Empty });
                }                
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            if(!Main.loading & ServiceChecker.dataTable != null)
                ServiceCheckerPresenter.ClearAll(true, false, false);
        }

        //X
        private void clearQCheck_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!String.IsNullOrEmpty(quickCheck_textBox.Text))
                quickCheck_textBox.Clear();
        }
        private void clearSearch_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!String.IsNullOrEmpty(search_textBox.Text))
                search_textBox.Clear();
        }

        private void extractListtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (servChecker_dataGridView.Rows.Count > 0)
                ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.exportTxt);
        }
    }
}