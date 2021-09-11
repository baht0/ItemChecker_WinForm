using System.Drawing;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Net;
using Newtonsoft.Json.Linq;

namespace ItemChecker
{
    public partial class NewForm : Form
    {
        int i = 10;
        public NewForm()
        {
            InitializeComponent();
        }

        private void NewForm_Load(object sender, System.EventArgs e)
        {
            getUpdatesList();
            comboBox.SelectedItem = ProjectInfo.latest[0];

            if (Properties.Settings.Default.whatIsNew)
            {
                close_button.Text = $"Close ({i--})";
                timer.Enabled = true;
            }
            else
                close_button.Enabled = true;
        }
        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i >= 0 & Properties.Settings.Default.whatIsNew)
                e.Cancel = true;
        }
        private void close_button_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void timer_Tick(object sender, System.EventArgs e)
        {
            close_button.Text = $"Close ({i--})";
            if (i < 0)
            {
                close_button.Text = $"Close";
                close_button.Enabled = true;
                timer.Enabled = false;
            }
        }

        private void getUpdatesList()
        {
            string json = Post.DropboxListFolder("updates");

            JArray updates = JArray.Parse(JObject.Parse(json)["entries"].ToString());

            foreach (JObject update in updates)
            {
                string ver = (string)update["name"];
                ver = ver[0..^4];
                ver = ver[7..^0];
                comboBox.Items.Add(ver);
            }
        }
        private void writeUpdates(string version)
        {
            try
            {
                listView.Items.Clear();
                this.Text = $"What's new? Version: {version}";
                string update = Post.DropboxRead($"updates/update_{version}.txt");
                update = update.Replace("\r\n", "\n");
                string[] lines = update.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    listView.Items.Add(lines[i]);
                    if (!lines[i].Contains("-"))
                        listView.Items[i].Font = new Font("Consales", listView.Items[i].Font.Size, FontStyle.Bold); 
                    else if (lines[i].Contains("!"))
                        listView.Items[i].ForeColor = Color.OrangeRed;
                }
            }
            catch
            {
                listView.Items.Add("Something went wrong :(");
                listView.Items.Add("Apparently the update is not ready.");
            }
            finally
            {
                listView.Columns[0].Width = -1;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            writeUpdates(comboBox.Text);
        }
    }
}