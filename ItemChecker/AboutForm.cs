using ItemChecker.General;
using ItemChecker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ItemChecker
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            label2.Text = Main.version;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {"http://steamcommunity.com/profiles/76561198065971089"}"));
        }
    }
}
