using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Net;
using ItemChecker.Presenter;
using ItemChecker.Support;
using ItemChecker.Settings;
using Newtonsoft.Json.Linq;

namespace ItemChecker
{
    public partial class CheckListForm : Form
    {
        string request;
        public CheckListForm(string str)
        {
            InitializeComponent();
            request = str;
            if (str.Contains("FloatList"))
            {
                getToolStripMenuItem.Visible = false;
                richTextBox1.Text = FloatConfig.Default.floatList.Trim();
            }
            else if (str.Contains("SortList"))
            {
                getToolStripMenuItem.Text = "Get items";
                lootFarmToolStripMenuItem.Visible = true;
                csMoneyToolStripMenuItem.Visible = true;
                richTextBox1.Text = Properties.Settings.Default.checkList.Trim();
            }
            else if(str.Contains("FavoriteList"))
            {
                getToolStripMenuItem.Visible = false;
                richTextBox1.Text = WithdrawConfig.Default.favoriteList.Trim();
            }
            else if (str.Contains("ProxyList"))
            {
                getToolStripMenuItem.Text = "Get proxys";
                label1.Text = "Example: 127.0.0.1:80. Protocol: HTTP.";
                richTextBox1.Text = GeneralConfig.Default.proxyList.Trim();
            }
            this.Text = $"{str}: {richTextBox1.Lines.Count()}";
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text.Replace("\r\n", "\n");
            if (request.Contains("FloatList"))
            {
                Float.items.Clear();
                Float.items.AddRange(richTextBox1.Lines);
                FloatConfig.Default.floatList = str;

                Main.loading = true;
                ThreadPool.QueueUserWorkItem(FloatPresenter.Check);
            }
            else if (request.Contains("SortList"))
            {
                Main.checkList.Clear();
                Main.checkList.AddRange(richTextBox1.Lines);
                Properties.Settings.Default.checkList = str;
            }
            else if (request.Contains("FavoriteList"))
            {
                Withdraw.favoriteList.Clear();
                Withdraw.favoriteList.AddRange(richTextBox1.Lines);
                WithdrawConfig.Default.favoriteList = str;
            }
            else if (request.Contains("ProxyList"))
            {
                Main.proxyList.Clear();
                Main.proxyList.AddRange(richTextBox1.Lines);
                GeneralConfig.Default.proxyList = str;
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
            this.Text = $"{request}: {richTextBox1.Lines.Count()}";
        }

        //menu
        private void selectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.RestoreDirectory = true;
            dialog.Filter = "Items List (txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                richTextBox1.Text = File.ReadAllText(dialog.FileName);
                this.Text = $"{request}: {richTextBox1.Lines.Count()}";
            }
        }
        private void getToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (request.Contains("ProxyList"))
            {
                Edit.openUrl("https://advanced.name/freeproxy?type=http");
                Edit.openUrl("https://geonode.com/free-proxy-list");
                Edit.openUrl("https://awmproxy.net/freeproxy.php");
                Edit.openUrl("http://free-proxy.cz/en/proxylist/country/all/http/ping/all");
            }
        }
        private void lootFarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (request.Contains("SortList"))
            {
                richTextBox1.Clear();
                ThreadPool.QueueUserWorkItem(WriteInRichTextbox, new object[] { "https://loot.farm/fullprice.json" });
            }
        }
        private void csMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (request.Contains("SortList"))
            {
                richTextBox1.Clear();
                ThreadPool.QueueUserWorkItem(WriteInRichTextbox, new object[] { "https://broskins.com/csmoneyfeed.php?_=1625556262031" });
            }
        }
        void WriteInRichTextbox(object state)
        {
            try
            {
                object[] args = state as object[];
                string url = args[0].ToString();
                var json = Request.GetRequest(url);
                if (url.Contains("csmoney"))
                    json = JObject.Parse(json)["data"].ToString();
                JArray jArray = JArray.Parse(json);

                string str = null;
                for (int i = 0; i < jArray.Count; i++)
                {
                    string item = jArray[i]["name"].ToString();
                    if (url.Contains("loot.farm"))
                        str += item + "\n";
                    else if (!Main.unavailable.Contains(item))
                        str += item + "\n";

                }
                Invoke(new MethodInvoker(delegate { richTextBox1.Text = str.Trim(); }));
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.version);
                Exceptions.errorMessage(exp, currMethodName);
            }
        }
    }
}