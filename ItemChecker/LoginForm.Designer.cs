
namespace ItemChecker
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.login_groupBox = new System.Windows.Forms.GroupBox();
            this.pass_label = new System.Windows.Forms.Label();
            this.login_label = new System.Windows.Forms.Label();
            this.viewPass_checkBox = new System.Windows.Forms.CheckBox();
            this.remember_checkBox = new System.Windows.Forms.CheckBox();
            this.pass_textBox = new System.Windows.Forms.TextBox();
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.code_groupBox = new System.Windows.Forms.GroupBox();
            this.code_textBox = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.settings_button = new System.Windows.Forms.Button();
            this.login_groupBox.SuspendLayout();
            this.code_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // login_groupBox
            // 
            this.login_groupBox.Controls.Add(this.pass_label);
            this.login_groupBox.Controls.Add(this.login_label);
            this.login_groupBox.Controls.Add(this.viewPass_checkBox);
            this.login_groupBox.Controls.Add(this.remember_checkBox);
            this.login_groupBox.Controls.Add(this.pass_textBox);
            this.login_groupBox.Controls.Add(this.login_textBox);
            this.login_groupBox.Location = new System.Drawing.Point(12, 11);
            this.login_groupBox.Name = "login_groupBox";
            this.login_groupBox.Size = new System.Drawing.Size(202, 99);
            this.login_groupBox.TabIndex = 0;
            this.login_groupBox.TabStop = false;
            this.login_groupBox.Text = "Steam Account:";
            // 
            // pass_label
            // 
            this.pass_label.AutoSize = true;
            this.pass_label.Location = new System.Drawing.Point(6, 53);
            this.pass_label.Name = "pass_label";
            this.pass_label.Size = new System.Drawing.Size(56, 13);
            this.pass_label.TabIndex = 5;
            this.pass_label.Text = "Password:";
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Location = new System.Drawing.Point(6, 26);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(36, 13);
            this.login_label.TabIndex = 4;
            this.login_label.Text = "Login:";
            // 
            // viewPass_checkBox
            // 
            this.viewPass_checkBox.AutoSize = true;
            this.viewPass_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.viewPass_checkBox.Location = new System.Drawing.Point(175, 53);
            this.viewPass_checkBox.Name = "viewPass_checkBox";
            this.viewPass_checkBox.Size = new System.Drawing.Size(15, 14);
            this.viewPass_checkBox.TabIndex = 3;
            this.viewPass_checkBox.UseVisualStyleBackColor = true;
            this.viewPass_checkBox.CheckedChanged += new System.EventHandler(this.viewPass_checkBox_CheckedChanged);
            // 
            // remember_checkBox
            // 
            this.remember_checkBox.AutoSize = true;
            this.remember_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.remember_checkBox.Location = new System.Drawing.Point(79, 75);
            this.remember_checkBox.Name = "remember_checkBox";
            this.remember_checkBox.Size = new System.Drawing.Size(94, 17);
            this.remember_checkBox.TabIndex = 2;
            this.remember_checkBox.Text = "Remember me";
            this.remember_checkBox.UseVisualStyleBackColor = true;
            // 
            // pass_textBox
            // 
            this.pass_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pass_textBox.Location = new System.Drawing.Point(79, 49);
            this.pass_textBox.Name = "pass_textBox";
            this.pass_textBox.PasswordChar = '•';
            this.pass_textBox.Size = new System.Drawing.Size(90, 20);
            this.pass_textBox.TabIndex = 1;
            // 
            // login_textBox
            // 
            this.login_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.login_textBox.Location = new System.Drawing.Point(79, 23);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(111, 20);
            this.login_textBox.TabIndex = 0;
            // 
            // code_groupBox
            // 
            this.code_groupBox.Controls.Add(this.code_textBox);
            this.code_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.code_groupBox.Location = new System.Drawing.Point(12, 116);
            this.code_groupBox.Name = "code_groupBox";
            this.code_groupBox.Size = new System.Drawing.Size(202, 61);
            this.code_groupBox.TabIndex = 1;
            this.code_groupBox.TabStop = false;
            this.code_groupBox.Text = "Authenticator code:";
            // 
            // code_textBox
            // 
            this.code_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.code_textBox.Location = new System.Drawing.Point(10, 22);
            this.code_textBox.Name = "code_textBox";
            this.code_textBox.Size = new System.Drawing.Size(180, 29);
            this.code_textBox.TabIndex = 0;
            this.code_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.code_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.code_textBox_KeyDown);
            this.code_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.code_textBox_KeyPress);
            // 
            // cancel_button
            // 
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cancel_button.Location = new System.Drawing.Point(139, 182);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 2;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ok_button.Location = new System.Drawing.Point(58, 182);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 3;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // settings_button
            // 
            this.settings_button.BackgroundImage = global::ItemChecker.Properties.Resources.setting;
            this.settings_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settings_button.Location = new System.Drawing.Point(12, 183);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(23, 23);
            this.settings_button.TabIndex = 0;
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 217);
            this.Controls.Add(this.settings_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.code_groupBox);
            this.Controls.Add(this.login_groupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(242, 256);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(242, 256);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.login_groupBox.ResumeLayout(false);
            this.login_groupBox.PerformLayout();
            this.code_groupBox.ResumeLayout(false);
            this.code_groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox login_groupBox;
        private System.Windows.Forms.GroupBox code_groupBox;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.CheckBox viewPass_checkBox;
        private System.Windows.Forms.CheckBox remember_checkBox;
        private System.Windows.Forms.TextBox pass_textBox;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.TextBox code_textBox;
        private System.Windows.Forms.Label pass_label;
        private System.Windows.Forms.Label login_label;
    }
}