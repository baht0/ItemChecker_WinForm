using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Presenter;

namespace ItemChecker
{
    public partial class CheckListForm : Form
    {
        string request;
        public CheckListForm(string str)
        {
            InitializeComponent();
            request = str;
            if (str.Contains("MainForm"))
            {
                richTextBox1.Text = Properties.Settings.Default.floatList.Trim();
            }
            else if (str.Contains("SortList"))
            {
                richTextBox1.Text = Properties.Settings.Default.checkList.Trim();
            }
            this.Text = "CheckList: " + richTextBox1.Lines.Count().ToString();
        }

        private void selectFile_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.RestoreDirectory = true;
            dialog.Filter = "Items List (txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                richTextBox1.Text = File.ReadAllText(dialog.FileName);
                this.Text = "CheckList: " + richTextBox1.Lines.Count().ToString();
            }
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text.Replace("\r\n", "\n");
            if (request.Contains("MainForm"))
            {
                Float.items.Clear();
                Float.items.AddRange(richTextBox1.Lines);
                Properties.Settings.Default.floatList = str;

                Main.loading = true;
                ThreadPool.QueueUserWorkItem(FloatPresenter.Check);
            }
            else if (request.Contains("SortList"))
            {
                Main.checkList.Clear();
                Main.checkList.AddRange(richTextBox1.Lines);
                Properties.Settings.Default.checkList = str;
            }
            Properties.Settings.Default.Save();
            Close();
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Text = "CheckList: " + richTextBox1.Lines.Count().ToString();
        }
    }
}
