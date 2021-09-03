using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Net;

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
            this.Text = $"What'is new? Version: {ProjectInfo.latest[0]}";

            writeUpdates();

            if (Properties.Settings.Default.whatIsNew)
            {
                close_button.Text = $"Close ({i--})";
                timer.Enabled = true;
            }
            else
                close_button.Enabled = true;
        }
        private void writeUpdates()
        {
            try
            {
                string updates = Post.RequestDropbox($"updates/updates_{ProjectInfo.latest[0]}.txt");
                updates = updates.Replace("\r\n", "\n");
                string[] lines = updates.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    listView.Items.Add(lines[i]);
                    if (lines[i].Contains(":"))
                        listView.Items[i].Font = new System.Drawing.Font("Consales", listView.Items[i].Font.Size, System.Drawing.FontStyle.Bold);
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
    }
}