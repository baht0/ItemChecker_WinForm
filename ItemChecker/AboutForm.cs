﻿using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Settings;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static ItemChecker.Program;

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
            if (SteamConfig.Default.login == "bahtiarov116" | Steam.login == "bahtiarov116")
                createInfo_linkLabel.Visible = true;

            if (ProjectInfo.update.Any())
                point_pictureBox.BackgroundImage = new Bitmap(Properties.Resources.point_red);
            
            version_label.Text = "Version: " + Main.assemblyVersion;

            latestVersion();
        }

        private void checkUpdate_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ProjectInfo.update.Any())
            {
                DialogResult result = MessageBox.Show($"Want to upgrade from {Main.assemblyVersion} to {ProjectInfo.latest[0]}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    ThreadPool.QueueUserWorkItem(pool => { ProjectInfoPresenter.update(); });
            }
            else
            {
                checkUpdate_linkLabel.Enabled = false;
                ThreadPool.QueueUserWorkItem(pool => {
                    ProjectInfoPresenter.checkUpdate();
                    Invoke(new MethodInvoker(delegate { latestVersion(); }));
                });
            }
        }
        private void latestVersion()
        {
            lastVersion_label.Text = $"Latest version: {ProjectInfo.latest[0]}";

            if (ProjectInfo.update.Any())
            {
                checkUpdate_linkLabel.Text = "Update...";
                point_pictureBox.BackgroundImage = new Bitmap(Properties.Resources.point_red);
                mainForm.aboutToolStripMenuItem.Image = new Bitmap(Properties.Resources.point_red);
            }
            else
                checkUpdate_linkLabel.Text = "Check Update...";
            checkUpdate_linkLabel.Enabled = true;
        }
        private void createInfo_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(pool => { ProjectInfoPresenter.createCurrentVersion(); });
        }
        private void close_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        //open
        private void name_label_Click(object sender, System.EventArgs e)
        {
            Support.Edit.openUrl(Application.StartupPath);
        }
        private void link_pictureBox_Click(object sender, System.EventArgs e)
        {
            Support.Edit.openUrl(Application.StartupPath);
        }

        private void lastVersion_label_Click(object sender, System.EventArgs e)
        {
            if (!Properties.Settings.Default.whatIsNew)
            {
                NewForm newForm = new();
                newForm.ShowDialog();
            }
        }
        private void point_pictureBox_Click(object sender, System.EventArgs e)
        {
            if (!Properties.Settings.Default.whatIsNew)
            {
                NewForm newForm = new();
                newForm.ShowDialog();
            }
        }
    }
}
