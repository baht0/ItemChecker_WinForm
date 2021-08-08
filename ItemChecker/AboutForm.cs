using ItemChecker.Model;
using System.Windows.Forms;

namespace ItemChecker
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            version_label.Text = "Version: " + Main.version;
            getLatestVersion();
        }

        private void checkUpdate_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            getLatestVersion();
        }
        private void openFolder_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Support.Edit.openUrl(Application.StartupPath);
        }
        private void close_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        private void getLatestVersion()
        {

        }
    }
}
