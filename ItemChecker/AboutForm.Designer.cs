
namespace ItemChecker
{
    partial class AboutForm
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
            this.openFolder_linkLabel = new System.Windows.Forms.LinkLabel();
            this.icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.name_label = new System.Windows.Forms.Label();
            this.autor_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.lastVersion_label = new System.Windows.Forms.Label();
            this.close_button = new System.Windows.Forms.Button();
            this.update_linkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.icon_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFolder_linkLabel
            // 
            this.openFolder_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openFolder_linkLabel.AutoSize = true;
            this.openFolder_linkLabel.LinkColor = System.Drawing.SystemColors.ControlDarkDark;
            this.openFolder_linkLabel.Location = new System.Drawing.Point(12, 120);
            this.openFolder_linkLabel.Name = "openFolder_linkLabel";
            this.openFolder_linkLabel.Size = new System.Drawing.Size(97, 15);
            this.openFolder_linkLabel.TabIndex = 5;
            this.openFolder_linkLabel.TabStop = true;
            this.openFolder_linkLabel.Text = "Open App Folder";
            this.openFolder_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openFolder_linkLabel_LinkClicked);
            // 
            // icon_pictureBox
            // 
            this.icon_pictureBox.BackgroundImage = global::ItemChecker.Properties.Resources.icon;
            this.icon_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.icon_pictureBox.InitialImage = null;
            this.icon_pictureBox.Location = new System.Drawing.Point(12, 12);
            this.icon_pictureBox.Name = "icon_pictureBox";
            this.icon_pictureBox.Size = new System.Drawing.Size(100, 100);
            this.icon_pictureBox.TabIndex = 7;
            this.icon_pictureBox.TabStop = false;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.name_label.Location = new System.Drawing.Point(118, 12);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(80, 15);
            this.name_label.TabIndex = 8;
            this.name_label.Text = "ItemChecker";
            // 
            // autor_label
            // 
            this.autor_label.AutoSize = true;
            this.autor_label.Location = new System.Drawing.Point(118, 28);
            this.autor_label.Name = "autor_label";
            this.autor_label.Size = new System.Drawing.Size(73, 15);
            this.autor_label.TabIndex = 9;
            this.autor_label.Text = "Autor: baht0";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Location = new System.Drawing.Point(118, 44);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(84, 15);
            this.version_label.TabIndex = 10;
            this.version_label.Text = "Version: 0.0.0.0";
            // 
            // lastVersion_label
            // 
            this.lastVersion_label.AutoSize = true;
            this.lastVersion_label.Location = new System.Drawing.Point(118, 60);
            this.lastVersion_label.Name = "lastVersion_label";
            this.lastVersion_label.Size = new System.Drawing.Size(118, 15);
            this.lastVersion_label.TabIndex = 12;
            this.lastVersion_label.Text = "Latest version: 0.0.0.0";
            this.lastVersion_label.Visible = false;
            // 
            // close_button
            // 
            this.close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close_button.Location = new System.Drawing.Point(172, 112);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 13;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // update_linkLabel
            // 
            this.update_linkLabel.AutoSize = true;
            this.update_linkLabel.LinkColor = System.Drawing.SystemColors.Highlight;
            this.update_linkLabel.Location = new System.Drawing.Point(118, 75);
            this.update_linkLabel.Name = "update_linkLabel";
            this.update_linkLabel.Size = new System.Drawing.Size(54, 15);
            this.update_linkLabel.TabIndex = 14;
            this.update_linkLabel.TabStop = true;
            this.update_linkLabel.Text = "Update...";
            this.update_linkLabel.Visible = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 147);
            this.Controls.Add(this.update_linkLabel);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.lastVersion_label);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.autor_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.icon_pictureBox);
            this.Controls.Add(this.openFolder_linkLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.icon_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel openFolder_linkLabel;
        private System.Windows.Forms.PictureBox icon_pictureBox;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label autor_label;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Label lastVersion_label;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.LinkLabel update_linkLabel;
    }
}