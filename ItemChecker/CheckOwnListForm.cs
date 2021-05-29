using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Presenter;
using System.ComponentModel;

namespace ItemChecker
{
    public partial class CheckOwnListForm : Form
    {
        int service;
        public static bool checkStop = false;
        public CheckOwnListForm()
        {
            InitializeComponent();
            Program.checkOwnListForm = this;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            service_toolStripComboBox.SelectedIndex = 0;
        }
        private void CheckOwnListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkStop = true;
            Main.loading = false;
            this.Hide();
            e.Cancel = true;
        }

        //list
        private void open_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.checkList.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Items List (txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Main.checkList = File.ReadLines(dialog.FileName).ToList();
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
            }
        }
        private void add_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.checkList.Clear();
            CheckListForm fr = new CheckListForm("SortList");
            fr.ShowDialog();
            count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;
        }
        private void check_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (service_toolStripComboBox.SelectedIndex == 0) service = 0;
            if (service_toolStripComboBox.SelectedIndex == 1) service = 1;
            ThreadPool.QueueUserWorkItem(checkMain);
        }
        private void checkMain(object state)
        {
            try
            {
                if (Main.checkList.Count != 0 & !Main.loading)
                {
                    Main.loading = true;
                    Invoke(new MethodInvoker(delegate {
                        ownList_menuStrip.Enabled = false;
                        quick_button.Enabled = false;
                        updated_toolStripStatusLabel.Visible = false;
                        status_toolStripStatusLabel.Text = "Processing...";
                        status_toolStripStatusLabel.Visible = true;
                    }));
                    if (service == 0)
                    {
                        Invoke(new MethodInvoker(delegate {
                            ownList_dataGridView.Columns[2].HeaderText = "Price (ST)";
                            ownList_dataGridView.Columns[3].HeaderText = "Price (CSM)";
                            ownList_dataGridView.Columns[5].HeaderText = "GetPrice (CSM)";
                            ownList_dataGridView.Columns[8].HeaderText = "Status (CSM)";
                        }));
                        MrinkaPresenter.checkList();
                    }
                    if (service == 1)
                    {
                        Invoke(new MethodInvoker(delegate {
                            ownList_dataGridView.Columns[2].HeaderText = "Price (ST)";
                            ownList_dataGridView.Columns[3].HeaderText = "Price (LF)";
                            ownList_dataGridView.Columns[5].HeaderText = "GetPrice (LF)";
                            ownList_dataGridView.Columns[8].HeaderText = "Status (LF)";
                        }));
                        LootFarmPresenter.checkList();
                    }
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
                Invoke(new MethodInvoker(delegate
                {
                    status_toolStripStatusLabel.Visible = false;
                    ownList_menuStrip.Enabled = true;
                    quick_button.Enabled = true;
                    ownList_dataGridView.Sort(ownList_dataGridView.Columns[6], ListSortDirection.Descending);
                }));
            }
        }

        private void quick_button_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                Main.checkList.Clear();
                Main.checkList.Add(textBox.Text.Trim());
                count_toolStripStatusLabel.Text = "Count: " + Main.checkList.Count;

                check_toolStripMenuItem.PerformClick();
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) quick_button.PerformClick();
        }

        //table
        private void ownList_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex.ToString());
            string iname = ownList_dataGridView.Rows[row].Cells[1].Value.ToString();
            int index = ownList_dataGridView.CurrentCell.ColumnIndex;
            string url;

            if (index == 1)
            {
                Clipboard.SetText(iname);
            }
            if (index == 2 || index == 4)
            {
                url = "https://steamcommunity.com/market/listings/730/" + Edit.replaceUrl(iname);
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            if (index == 3 || index == 5)
            {
                url = "https://old.cs.money/?utm_source=sponsorship&utm_medium=tryskins&utm_campaign=trskns0819&utm_content=link#skin_name=" + Edit.replaceUrl(iname);
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
        }
        private void ownList_dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = ownList_dataGridView.CurrentCell.Value.ToString();
                int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex);
                int cell = ownList_dataGridView.CurrentCell.ColumnIndex;
                if (1 < cell & cell < 8 & cell != 6)
                {
                    Main.save_str = str;
                    ownList_dataGridView.Rows[row].Cells[cell].Value = Edit.funcConvert(str, Main.course);
                }
                if (service == 0)
                {
                    updated_toolStripStatusLabel.Text = "Updated(h): " + Edit.convertTime(Convert.ToDouble(Mrinka.stUpdated[row])) + "(ST) | " + Edit.convertTime(Convert.ToDouble(Mrinka.csmUpdated[row])) + "(CSM)";
                    updated_toolStripStatusLabel.Visible = true;
                }
            }
            catch { }
        }
        private void ownList_dataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int cell = ownList_dataGridView.CurrentCell.ColumnIndex;
            if (1 < cell & cell < 8 & cell != 6)
            {
                int row = Convert.ToInt32(ownList_dataGridView.CurrentCell.RowIndex);
                ownList_dataGridView.Rows[row].Cells[cell].Value = Main.save_str;
                Main.save_str = "";
            }
        }
        private void ownList_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Insert) ThreadPool.QueueUserWorkItem(MrinkaPresenter.addQueue);
        }        
    }
}