﻿
namespace ItemChecker
{
    partial class ServiceCheckerForm
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceCheckerForm));
            this.servChecker_menuStrip = new System.Windows.Forms.MenuStrip();
            this.add_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.check_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servChecker_statusStrip = new System.Windows.Forms.StatusStrip();
            this.count_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeLeft_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updated_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.servChecker_dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondSer_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.firstSer_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.quick_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.servChecker_menuStrip.SuspendLayout();
            this.servChecker_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servChecker_dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // servChecker_menuStrip
            // 
            this.servChecker_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripMenuItem,
            this.check_toolStripMenuItem,
            this.extractToolStripMenuItem});
            this.servChecker_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.servChecker_menuStrip.Name = "servChecker_menuStrip";
            this.servChecker_menuStrip.Size = new System.Drawing.Size(883, 24);
            this.servChecker_menuStrip.TabIndex = 0;
            this.servChecker_menuStrip.Text = "menuStrip1";
            // 
            // add_toolStripMenuItem
            // 
            this.add_toolStripMenuItem.Name = "add_toolStripMenuItem";
            this.add_toolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.add_toolStripMenuItem.Text = "Add List";
            this.add_toolStripMenuItem.Click += new System.EventHandler(this.add_toolStripMenuItem_Click);
            // 
            // check_toolStripMenuItem
            // 
            this.check_toolStripMenuItem.Name = "check_toolStripMenuItem";
            this.check_toolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.check_toolStripMenuItem.Text = "Check";
            this.check_toolStripMenuItem.Click += new System.EventHandler(this.check_toolStripMenuItem_Click);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.extractToolStripMenuItem.Text = "Extract to *.cvs";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // servChecker_statusStrip
            // 
            this.servChecker_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.count_toolStripStatusLabel,
            this.timeLeft_toolStripStatusLabel,
            this.space_toolStripStatusLabel,
            this.updated_toolStripStatusLabel,
            this.status_toolStripStatusLabel});
            this.servChecker_statusStrip.Location = new System.Drawing.Point(0, 495);
            this.servChecker_statusStrip.Name = "servChecker_statusStrip";
            this.servChecker_statusStrip.Size = new System.Drawing.Size(883, 22);
            this.servChecker_statusStrip.TabIndex = 1;
            this.servChecker_statusStrip.Text = "statusStrip1";
            // 
            // count_toolStripStatusLabel
            // 
            this.count_toolStripStatusLabel.Name = "count_toolStripStatusLabel";
            this.count_toolStripStatusLabel.Size = new System.Drawing.Size(51, 17);
            this.count_toolStripStatusLabel.Text = "Count: -";
            // 
            // timeLeft_toolStripStatusLabel
            // 
            this.timeLeft_toolStripStatusLabel.Name = "timeLeft_toolStripStatusLabel";
            this.timeLeft_toolStripStatusLabel.Size = new System.Drawing.Size(64, 17);
            this.timeLeft_toolStripStatusLabel.Text = "TimeLeft: -";
            this.timeLeft_toolStripStatusLabel.Visible = false;
            // 
            // space_toolStripStatusLabel
            // 
            this.space_toolStripStatusLabel.Name = "space_toolStripStatusLabel";
            this.space_toolStripStatusLabel.Size = new System.Drawing.Size(817, 17);
            this.space_toolStripStatusLabel.Spring = true;
            // 
            // updated_toolStripStatusLabel
            // 
            this.updated_toolStripStatusLabel.Name = "updated_toolStripStatusLabel";
            this.updated_toolStripStatusLabel.Size = new System.Drawing.Size(177, 17);
            this.updated_toolStripStatusLabel.Text = "Updated(h): 0:00(ST) | 0:00(CSM)";
            this.updated_toolStripStatusLabel.Visible = false;
            // 
            // status_toolStripStatusLabel
            // 
            this.status_toolStripStatusLabel.Name = "status_toolStripStatusLabel";
            this.status_toolStripStatusLabel.Size = new System.Drawing.Size(73, 17);
            this.status_toolStripStatusLabel.Text = "Processing...";
            this.status_toolStripStatusLabel.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(6, 37);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(494, 23);
            this.textBox.TabIndex = 2;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // servChecker_dataGridView
            // 
            this.servChecker_dataGridView.AllowUserToAddRows = false;
            this.servChecker_dataGridView.AllowUserToDeleteRows = false;
            this.servChecker_dataGridView.AllowUserToResizeRows = false;
            this.servChecker_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.servChecker_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.servChecker_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servChecker_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.Column2,
            this.Column3});
            this.servChecker_dataGridView.Location = new System.Drawing.Point(12, 110);
            this.servChecker_dataGridView.Name = "servChecker_dataGridView";
            this.servChecker_dataGridView.ReadOnly = true;
            this.servChecker_dataGridView.RowHeadersVisible = false;
            this.servChecker_dataGridView.RowTemplate.Height = 25;
            this.servChecker_dataGridView.Size = new System.Drawing.Size(859, 373);
            this.servChecker_dataGridView.TabIndex = 9;
            this.servChecker_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellDoubleClick);
            this.servChecker_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellEnter);
            this.servChecker_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellLeave);
            this.servChecker_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.servChecker_dataGridView_ColumnHeaderMouseClick);
            this.servChecker_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ownList_dataGridView_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.secondSer_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.firstSer_comboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Second service:";
            // 
            // secondSer_comboBox
            // 
            this.secondSer_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondSer_comboBox.Items.AddRange(new object[] {
            "SteamMarket",
            "Cs.Money",
            "Loot.Farm"});
            this.secondSer_comboBox.Location = new System.Drawing.Point(133, 37);
            this.secondSer_comboBox.Name = "secondSer_comboBox";
            this.secondSer_comboBox.Size = new System.Drawing.Size(121, 23);
            this.secondSer_comboBox.TabIndex = 3;
            this.secondSer_comboBox.SelectedIndexChanged += new System.EventHandler(this.secondSer_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "First service:";
            // 
            // firstSer_comboBox
            // 
            this.firstSer_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstSer_comboBox.Items.AddRange(new object[] {
            "SteamMarket",
            "Cs.Money",
            "Loot.Farm"});
            this.firstSer_comboBox.Location = new System.Drawing.Point(6, 37);
            this.firstSer_comboBox.Name = "firstSer_comboBox";
            this.firstSer_comboBox.Size = new System.Drawing.Size(121, 23);
            this.firstSer_comboBox.TabIndex = 0;
            this.firstSer_comboBox.SelectedIndexChanged += new System.EventHandler(this.firstSer_comboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.quick_button);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox);
            this.groupBox2.Location = new System.Drawing.Point(284, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 74);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick check";
            // 
            // quick_button
            // 
            this.quick_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_button.Location = new System.Drawing.Point(506, 37);
            this.quick_button.Name = "quick_button";
            this.quick_button.Size = new System.Drawing.Size(75, 23);
            this.quick_button.TabIndex = 6;
            this.quick_button.Text = "Quick";
            this.quick_button.UseVisualStyleBackColor = true;
            this.quick_button.Click += new System.EventHandler(this.quick_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Item Name:";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 0.000422392F;
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 310F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Item";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 70F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Price (S1)";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 70F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Price (S1)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.FillWeight = 70F;
            this.dataGridViewTextBoxColumn11.HeaderText = "Price (S2)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.FillWeight = 70F;
            this.dataGridViewTextBoxColumn12.HeaderText = "Price (S2)";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 60F;
            this.Column1.HeaderText = "Precent";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Difference (₽)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 80F;
            this.Column3.HeaderText = "Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // ServiceCheckerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.servChecker_dataGridView);
            this.Controls.Add(this.servChecker_statusStrip);
            this.Controls.Add(this.servChecker_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.servChecker_menuStrip;
            this.MinimumSize = new System.Drawing.Size(899, 556);
            this.Name = "ServiceCheckerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceCheckerForm_FormClosing);
            this.servChecker_menuStrip.ResumeLayout(false);
            this.servChecker_menuStrip.PerformLayout();
            this.servChecker_statusStrip.ResumeLayout(false);
            this.servChecker_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servChecker_dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip servChecker_menuStrip;
        public System.Windows.Forms.StatusStrip servChecker_statusStrip;
        public System.Windows.Forms.ToolStripMenuItem add_toolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem check_toolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel count_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel space_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel updated_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel status_toolStripStatusLabel;
        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.DataGridView servChecker_dataGridView;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox secondSer_comboBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox firstSer_comboBox;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button quick_button;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel timeLeft_toolStripStatusLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}