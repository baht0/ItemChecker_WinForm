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
using System.Data;
using System.ComponentModel;

namespace ItemChecker
{
    public partial class ServiceParserForm : Form
    {
        int past_row = 0;
        public ServiceParserForm()
        {
            InitializeComponent();
            Program.serviceParserForm = this;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
        private void serviceParserForm_Load(object sender, EventArgs e)
        {
            firstSer_comboBox.SelectedIndex = 0;
            secondSer_comboBox.SelectedIndex = 1;

            ServiceParserPresenter.columnDTable();
        }
        private void serviceParserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Main.loading | ServiceParser.dataTable.Rows.Count > 250)
            {
                DialogResult result = MessageBox.Show(
                  "Do you want to close?",
                  "Warning",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if ((Application.OpenForms["FiltersForm"] as FiltersForm) != null)
                        Application.OpenForms["FiltersForm"].Close();
                    ServiceParser.cts.Cancel();
                    ServiceParserPresenter.ClearAll(true, true);
                    Main.loading = false;
                }
                else if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if ((Application.OpenForms["FiltersForm"] as FiltersForm) != null)
                    Application.OpenForms["FiltersForm"].Close();
                ServiceParserPresenter.ClearAll(true, true);
            }
        }

        //menu
        private void checkList_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                CheckListForm checkListForm = new("CheckList");
                checkListForm.ShowDialog();
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
            }
        }
        private void check_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ser_1 = firstSer_comboBox.SelectedIndex;
            int ser_2 = secondSer_comboBox.SelectedIndex;

            if (Main.checkList.Any() & !Main.loading & ser_1 != ser_2)
            {
                Main.loading = true;
                ServiceParserPresenter.ClearAll(true, true);

                ServiceParser.service_one = ser_1;
                ServiceParser.service_two = ser_2;

                for (int i = 2; i < 6; i++)
                    Filters.prices.Add(serviceParser_dataGridView.Columns[i].HeaderText);

                serviceParser_dataGridView.Enabled = false;
                updated_toolStripStatusLabel.Visible = false;
                status_toolStripStatusLabel.Text = "Checking the list...";
                status_toolStripStatusLabel.Visible = true;
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
                services_toolStripStatusLabel.Text = $"From {firstSer_comboBox.Text} To {secondSer_comboBox.Text}";
                services_toolStripStatusLabel.Visible = true;

                ThreadPool.QueueUserWorkItem(ServiceParserPresenter.checkMain);
            }
        }
        private void extractToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Main.loading & serviceParser_dataGridView.Rows.Count > 0)
            {
                Main.loading = true;
                status_toolStripStatusLabel.Text = "Export the list to *.csv...";
                status_toolStripStatusLabel.Visible = true;
                serviceParser_dataGridView.Enabled = false;
                ThreadPool.QueueUserWorkItem(ServiceParserPresenter.exportCsv);
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
                    ServiceParserPresenter.ClearAll(true, true);

                    status_toolStripStatusLabel.Text = "Import the list from *.csv...";
                    status_toolStripStatusLabel.Visible = true;
                    string fileName = Path.GetFileNameWithoutExtension(dialog.FileName).ToString();
                    fileName = fileName.Remove(0, 15).Replace("_", " ");

                    ThreadPool.QueueUserWorkItem(ServiceParserPresenter.importCsv, new object[] { dialog.FileName, fileName });
                }
            }
        }
        private void extractListtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Add prices \"{serviceParser_dataGridView.Columns[2].HeaderText}\" to the list you create?",
                "Question",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (serviceParser_dataGridView.Rows.Count > 0 & result != DialogResult.Cancel)
                ThreadPool.QueueUserWorkItem(ServiceParserPresenter.exportTxt, new object[] { result });
        }

        //comboboxs
        private void firstSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstSer_comboBox.SelectedIndex == 0) //steam
            {
                serviceParser_dataGridView.Columns[2].HeaderText = "Price (ST)";
                serviceParser_dataGridView.Columns[3].HeaderText = "BuyOrder (ST)";
            }
            if (firstSer_comboBox.SelectedIndex == 1) //csmoney
            {
                serviceParser_dataGridView.Columns[2].HeaderText = "Price (CSM)";
                serviceParser_dataGridView.Columns[3].HeaderText = "Get (CSM)";
            }
            if (firstSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                serviceParser_dataGridView.Columns[2].HeaderText = "Price (LF)";
                serviceParser_dataGridView.Columns[3].HeaderText = "Get (LF)";
            }
        }
        private void secondSer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secondSer_comboBox.SelectedIndex == 0) //steam
            {
                serviceParser_dataGridView.Columns[4].HeaderText = "Price (ST)";
                serviceParser_dataGridView.Columns[5].HeaderText = "BuyOrder (ST) [+%]";
                serviceParser_dataGridView.Columns[8].HeaderText = "Status (ST)";
            }
            if (secondSer_comboBox.SelectedIndex == 1) //csmoney
            {
                serviceParser_dataGridView.Columns[4].HeaderText = "Price (CSM)";
                serviceParser_dataGridView.Columns[5].HeaderText = "Get (CSM)";
                serviceParser_dataGridView.Columns[8].HeaderText = "Status (CSM)";
            }
            if (secondSer_comboBox.SelectedIndex == 2) //lootfarm
            {
                serviceParser_dataGridView.Columns[4].HeaderText = "Price (LF)";
                serviceParser_dataGridView.Columns[5].HeaderText = "Get (LF)";
                serviceParser_dataGridView.Columns[8].HeaderText = "Status (LF)";
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
        private void serviceParser_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = serviceParser_dataGridView;
            int row = dataGridView.CurrentCell.RowIndex;
            int index = dataGridView.CurrentCell.ColumnIndex;
            string iname = dataGridView.Rows[row].Cells[1].Value.ToString();
            string market_has_name = Edit.replaceUrl(iname);

            if (index == 1)
                Clipboard.SetText(iname);
            if (index == 2 | index == 3)
            {
                if (ServiceParser.service_one == 0)
                    Edit.openUrl("https://steamcommunity.com/market/listings/730/" + market_has_name);
                if (ServiceParser.service_one == 1)
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            }
            if (index == 4 | index == 5)
            {
                if (ServiceParser.service_two == 0)
                    Edit.openUrl("https://steamcommunity.com/market/listings/730/" + market_has_name);
                if (ServiceParser.service_two == 1)
                    Edit.openCsm(market_has_name, TryskinsConfig.Default.oldDesign);
            }
        }
        private void serviceParser_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = serviceParser_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int cell = dataGridView.CurrentCell.ColumnIndex;
                string value = dataGridView.CurrentCell.Value.ToString();

                if (ServiceParser.service_one == 1 & past_row != row)
                {
                    availability_toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
                    availability_toolStripStatusLabel.Text = $"Availability: Checking...";
                    ThreadPool.QueueUserWorkItem(ServiceParserPresenter.checkCsmItem, new object[] { dataGridView.Rows[row].Cells[1].Value.ToString(), row });
                    availability_toolStripStatusLabel.Visible = true;
                    past_row = row;
                }
                if (ServiceParser.service_one == 2 & past_row != row)
                {
                    availability_toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
                    availability_toolStripStatusLabel.Text = $"Availability: Checking...";
                    ThreadPool.QueueUserWorkItem(ServiceParserPresenter.checkLootFarmItem, new object[] { dataGridView.Rows[row].Cells[1].Value.ToString(), row });
                    availability_toolStripStatusLabel.Visible = true;
                    past_row = row;
                }
                if (1 < cell & cell < 6)
                {
                    Main.save_str = value;
                    dataGridView.Rows[row].Cells[cell].Value = Math.Round(Convert.ToDecimal(value) * GeneralConfig.Default.currency, 2);
                }
                if (ServiceParser.stUpdated.Any() & ServiceParser.csmUpdated.Any())
                {
                    value = dataGridView.Rows[row].Cells[1].Value.ToString();
                    int index = Main.checkList.IndexOf(value);
                    updated_toolStripStatusLabel.Text = "Updated(h):";
                    if (ServiceParser.service_one == 0 | ServiceParser.service_two == 0)
                        updated_toolStripStatusLabel.Text += $" {Edit.convertTime(ServiceParser.stUpdated[index])} (ST)";
                    if (ServiceParser.service_one == 1 | ServiceParser.service_two == 1)
                        updated_toolStripStatusLabel.Text += $" {Edit.convertTime(ServiceParser.csmUpdated[index])} (CSM)";
                    updated_toolStripStatusLabel.Visible = true;
                }
            }
        }
        private void serviceParser_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = serviceParser_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int row = dataGridView.CurrentCell.RowIndex;
                int cell = dataGridView.CurrentCell.ColumnIndex;
                if (1 < cell & cell < 6)
                {
                    dataGridView.Rows[row].Cells[cell].Value = Convert.ToDouble(Main.save_str);
                    Main.save_str = null;
                }
            }
        }
        private void serviceParser_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView = serviceParser_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                int sta = 3;
                int row = dataGridView.CurrentCell.RowIndex;
                int column = dataGridView.CurrentCell.ColumnIndex;

                if (column == sta)
                    dataGridView.CurrentCell = dataGridView.Rows[row].Cells[1];

                string item = dataGridView.Rows[row].Cells[1].Value.ToString();
                decimal price = Edit.removeSymbol(dataGridView.Rows[row].Cells[sta].Value.ToString());

                if (e.KeyCode == Keys.Insert)
                    ThreadPool.QueueUserWorkItem(BuyOrderPresenter.addQueue, new object[] { dataGridView, row, item, price, sta });
            }
            
        }
        private void serviceParser_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = serviceParser_dataGridView;
            if (dataGridView.RowCount > 0)
            {
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                ServiceParserPresenter.drawDTGView();
            }
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

        //filters
        private void search_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!Main.loading & ServiceParser.dataTable.Rows.Count > 1)
            {
                DataView dataView = ServiceParser.dataTable.DefaultView;
                if (Filters.filter != string.Empty)
                    dataView.RowFilter = $"{Filters.filter} AND item_Column LIKE '%{search_textBox.Text}%'";
                else
                    dataView.RowFilter = $"item_Column LIKE '%{search_textBox.Text}%'";
                DataTable dt = dataView.ToTable();

                serviceParser_dataGridView.DataSource = dt;
                serviceParser_dataGridView.Columns[1].HeaderText = $"Item - {dt.Rows.Count}";
                serviceParser_dataGridView.Sort(serviceParser_dataGridView.Columns[6], ListSortDirection.Descending);
                ServiceParserPresenter.drawDTGView();
            }            
        }
        private void filters_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((Application.OpenForms["FiltersForm"] as FiltersForm) == null & ServiceParser.dataTable.Rows.Count > 0)
            {
                FiltersForm filtersForm = new();
                filtersForm.Show();
            }
            else if ((Application.OpenForms["FiltersForm"] as FiltersForm) != null)
                    Application.OpenForms["FiltersForm"].Activate();
        }
    }
}