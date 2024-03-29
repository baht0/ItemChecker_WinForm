﻿using System;
using System.Media;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Diagnostics;
using ItemChecker.Settings;
using ItemChecker.Presenter;

namespace ItemChecker
{
    public partial class LoginForm : Form
    {
        private bool close;

        public LoginForm()
        {
            InitializeComponent(); 
            
            login_textBox.Text = SteamConfig.Default.login;
            pass_textBox.Text = SteamConfig.Default.password;
            remember_checkBox.Checked = SteamConfig.Default.remember;
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
            {
                this.Hide();
                mainForm.Hide();
                MainPresenter.Exit();
            }
        }

        //code
        private void code_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ok_button.PerformClick();
        }
        private void code_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void viewPass_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (viewPass_checkBox.Checked)
                pass_textBox.PasswordChar = char.Parse("\0");
            else
                pass_textBox.PasswordChar = '•';
        }

        //buttons
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (login_textBox.Text != null & pass_textBox.Text != null & code_textBox.Text != null)
            {
                Steam.login = login_textBox.Text;
                Steam.pass = pass_textBox.Text;
                Steam.code = code_textBox.Text;
                if (remember_checkBox.Checked)
                {
                    SteamConfig.Default.login = login_textBox.Text;
                    SteamConfig.Default.password = pass_textBox.Text;
                    SteamConfig.Default.remember = remember_checkBox.Checked;
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
            if ((Application.OpenForms["SettingsForm"] as SettingsForm) == null)
            {
                SettingsForm settingsForm = new();
                settingsForm.ShowDialog();
            }
            else
                Application.OpenForms["SettingsForm"].Activate();
        }
    }
}