
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
            this.icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.name_label = new System.Windows.Forms.Label();
            this.autor_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.lastVersion_label = new System.Windows.Forms.Label();
            this.close_button = new System.Windows.Forms.Button();
            this.checkUpdate_linkLabel = new System.Windows.Forms.LinkLabel();
            this.createInfo_linkLabel = new System.Windows.Forms.LinkLabel();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.point_pictureBox = new System.Windows.Forms.PictureBox();
            this.link_pictureBox = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.icon_pictureBox)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.point_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.link_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // icon_pictureBox
            // 
            this.icon_pictureBox.BackgroundImage = global::ItemChecker.Properties.Resources.icon;
            this.icon_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.icon_pictureBox.InitialImage = null;
            this.icon_pictureBox.Location = new System.Drawing.Point(6, 22);
            this.icon_pictureBox.Name = "icon_pictureBox";
            this.icon_pictureBox.Size = new System.Drawing.Size(100, 100);
            this.icon_pictureBox.TabIndex = 7;
            this.icon_pictureBox.TabStop = false;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.name_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.name_label.Location = new System.Drawing.Point(112, 22);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(80, 15);
            this.name_label.TabIndex = 8;
            this.name_label.Text = "ItemChecker";
            this.name_label.Click += new System.EventHandler(this.name_label_Click);
            // 
            // autor_label
            // 
            this.autor_label.AutoSize = true;
            this.autor_label.Location = new System.Drawing.Point(112, 38);
            this.autor_label.Name = "autor_label";
            this.autor_label.Size = new System.Drawing.Size(73, 15);
            this.autor_label.TabIndex = 9;
            this.autor_label.Text = "Autor: baht0";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Location = new System.Drawing.Point(112, 54);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(84, 15);
            this.version_label.TabIndex = 10;
            this.version_label.Text = "Version: 0.0.0.0";
            // 
            // lastVersion_label
            // 
            this.lastVersion_label.AutoSize = true;
            this.lastVersion_label.Location = new System.Drawing.Point(112, 70);
            this.lastVersion_label.Name = "lastVersion_label";
            this.lastVersion_label.Size = new System.Drawing.Size(118, 15);
            this.lastVersion_label.TabIndex = 12;
            this.lastVersion_label.Text = "Latest version: 0.0.0.0";
            // 
            // close_button
            // 
            this.close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close_button.Location = new System.Drawing.Point(211, 145);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(65, 23);
            this.close_button.TabIndex = 13;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // checkUpdate_linkLabel
            // 
            this.checkUpdate_linkLabel.AutoSize = true;
            this.checkUpdate_linkLabel.LinkColor = System.Drawing.SystemColors.Highlight;
            this.checkUpdate_linkLabel.Location = new System.Drawing.Point(112, 85);
            this.checkUpdate_linkLabel.Name = "checkUpdate_linkLabel";
            this.checkUpdate_linkLabel.Size = new System.Drawing.Size(54, 15);
            this.checkUpdate_linkLabel.TabIndex = 14;
            this.checkUpdate_linkLabel.TabStop = true;
            this.checkUpdate_linkLabel.Text = "Update...";
            this.checkUpdate_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkUpdate_linkLabel_LinkClicked);
            // 
            // createInfo_linkLabel
            // 
            this.createInfo_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createInfo_linkLabel.AutoSize = true;
            this.createInfo_linkLabel.LinkColor = System.Drawing.Color.DimGray;
            this.createInfo_linkLabel.Location = new System.Drawing.Point(143, 150);
            this.createInfo_linkLabel.Name = "createInfo_linkLabel";
            this.createInfo_linkLabel.Size = new System.Drawing.Size(62, 15);
            this.createInfo_linkLabel.TabIndex = 15;
            this.createInfo_linkLabel.TabStop = true;
            this.createInfo_linkLabel.Text = "CreateInfo";
            this.createInfo_linkLabel.Visible = false;
            this.createInfo_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createInfo_linkLabel_LinkClicked);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.point_pictureBox);
            this.groupBox.Controls.Add(this.link_pictureBox);
            this.groupBox.Controls.Add(this.icon_pictureBox);
            this.groupBox.Controls.Add(this.name_label);
            this.groupBox.Controls.Add(this.checkUpdate_linkLabel);
            this.groupBox.Controls.Add(this.autor_label);
            this.groupBox.Controls.Add(this.version_label);
            this.groupBox.Controls.Add(this.lastVersion_label);
            this.groupBox.Location = new System.Drawing.Point(12, 4);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(264, 135);
            this.groupBox.TabIndex = 16;
            this.groupBox.TabStop = false;
            // 
            // point_pictureBox
            // 
            this.point_pictureBox.BackgroundImage = global::ItemChecker.Properties.Resources.point_red;
            this.point_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.point_pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.point_pictureBox.Location = new System.Drawing.Point(232, 70);
            this.point_pictureBox.Name = "point_pictureBox";
            this.point_pictureBox.Size = new System.Drawing.Size(15, 15);
            this.point_pictureBox.TabIndex = 17;
            this.point_pictureBox.TabStop = false;
            this.point_pictureBox.Visible = false;
            // 
            // link_pictureBox
            // 
            this.link_pictureBox.BackgroundImage = global::ItemChecker.Properties.Resources.link;
            this.link_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.link_pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.link_pictureBox.Location = new System.Drawing.Point(192, 22);
            this.link_pictureBox.Name = "link_pictureBox";
            this.link_pictureBox.Size = new System.Drawing.Size(15, 15);
            this.link_pictureBox.TabIndex = 16;
            this.link_pictureBox.TabStop = false;
            this.link_pictureBox.Click += new System.EventHandler(this.link_pictureBox_Click);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 153);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(74, 15);
            this.label.TabIndex = 15;
            this.label.Text = "© 2018-2021";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 177);
            this.Controls.Add(this.label);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.createInfo_linkLabel);
            this.Controls.Add(this.close_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icon_pictureBox)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.point_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.link_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox icon_pictureBox;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label autor_label;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.LinkLabel createInfo_linkLabel;
        private System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.Label lastVersion_label;
        public System.Windows.Forms.LinkLabel checkUpdate_linkLabel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox link_pictureBox;
        private System.Windows.Forms.PictureBox point_pictureBox;
    }
}