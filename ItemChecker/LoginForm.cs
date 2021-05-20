using System;
using System.Media;
using System.Windows.Forms;
using ItemChecker.Presenter;
using static ItemChecker.Program;
using ItemChecker.Model;

namespace ItemChecker
{
    public partial class LoginForm : Form
    {
        private bool status;

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
        }
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (status != true)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to log out?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes) MainPresenter.exit();
                if (result == DialogResult.No) e.Cancel = true;
            }
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
                status = true;
                Close();
            }
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void settings_button_Click(object sender, EventArgs e)
        {
            SettingsForm fr = new SettingsForm();
            fr.ShowDialog();
        }
    }
}
