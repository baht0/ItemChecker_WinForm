using System;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Support;
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
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Float.items.Clear();
            Main.checkList.Clear();
            Close();
        }
    }
}
