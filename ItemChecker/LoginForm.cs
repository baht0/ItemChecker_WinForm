using System;
using System.Media;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Diagnostics;

namespace ItemChecker
{
    public partial class LoginForm : Form
    {
        private bool close;

        public LoginForm()
        {
            InitializeComponent(); 
            
            login_textBox.Text = Properties.Settings.Default.login;
            pass_textBox.Text = Properties.Settings.Default.pass;
            remember_checkBox.Checked = Properties.Settings.Default.remember;
            this.ActiveControl = code_textBox;
            SystemSounds.Beep.Play();
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
            Main.loading = false;
        }
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
                exit();
        }

        //code
        private void code_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ok_button.PerformClick();
            }
        }
        private void code_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void viewPass_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (viewPass_checkBox.Checked)
            {
                pass_textBox.PasswordChar = char.Parse("\0");
            }
            else
            {
                pass_textBox.PasswordChar = '•';
            }
        }

        //buttons
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (login_textBox.Text != "" & pass_textBox.Text != "" & code_textBox.Text != "")
            {
                Steam.login = login_textBox.Text;
                Steam.pass = pass_textBox.Text;
                Steam.code = code_textBox.Text;
                Properties.Settings.Default.login = "";
                Properties.Settings.Default.pass = "";
                Properties.Settings.Default.remember = false;
                if (remember_checkBox.Checked)
                {
                    Properties.Settings.Default.login = login_textBox.Text;
                    Properties.Settings.Default.pass = pass_textBox.Text;
                    Properties.Settings.Default.remember = remember_checkBox.Checked;
                }
                Properties.Settings.Default.Save();
                close = true;
                Main.loading = true;
                Close();
            }
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void settings_button_Click(object sender, EventArgs e)
        {
            SettingsForm fr = new SettingsForm();
            fr.ShowDialog();
        }
        private void exit()
        {
            try
            {
                this.Hide();
                mainForm.Hide();
                mainForm.notifyIcon.Visible = false;
                Main.Browser.Quit();
                Application.Exit();
            }
            catch
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("conhost")) proc.Kill();
                foreach (Process proc in Process.GetProcessesByName("ItemChecker")) proc.Kill();
            }
        }
    }
}