using ItemChecker.Model;
using ItemChecker.Presenter;
using System.Threading;
using System.Windows.Forms;

namespace ItemChecker
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }
        private void AboutForm_Load(object sender, System.EventArgs e)
        {
            if (Properties.Settings.Default.login == "bahtiarov116" | Steam.login == "bahtiarov116")
                createInfo_linkLabel.Visible = true;

            version_label.Text = "Version: " + Main.assemblyVersion;
            lastVersion_label.Text = $"Latest version: {ProjectInfo.latest[0]}";

            if (ProjectInfo.latest[0] != ProjectInfo.info[0])
                checkUpdate_linkLabel.Text = "Update...";
            else
                checkUpdate_linkLabel.Text = "Check Update...";
        }

        private void checkUpdate_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectInfo.latest[0] != ProjectInfo.info[0])
            {
                DialogResult result = MessageBox.Show($"Want to upgrade from {Main.assemblyVersion} to {ProjectInfo.latest[0]}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    ThreadPool.QueueUserWorkItem(pool => { ProjectInfoPresenter.update(); });
            }
            else
                ThreadPool.QueueUserWorkItem(pool => { ProjectInfoPresenter.checkUpdate(); });
        }

        private void openFolder_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Support.Edit.openUrl(Application.StartupPath);
        }
        private void createInfo_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(pool => { ProjectInfoPresenter.createCurrentVersion(); });
        }
        private void close_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
