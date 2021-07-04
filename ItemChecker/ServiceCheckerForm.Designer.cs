
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
            this.ownList_menuStrip = new System.Windows.Forms.MenuStrip();
            this.add_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.check_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownList_statusStrip = new System.Windows.Forms.StatusStrip();
            this.count_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updated_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.ownList_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondSer_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.firstSer_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.quick_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ownList_menuStrip.SuspendLayout();
            this.ownList_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ownList_dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ownList_menuStrip
            // 
            this.ownList_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripMenuItem,
            this.check_toolStripMenuItem});
            this.ownList_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.ownList_menuStrip.Name = "ownList_menuStrip";
            this.ownList_menuStrip.Size = new System.Drawing.Size(883, 24);
            this.ownList_menuStrip.TabIndex = 0;
            this.ownList_menuStrip.Text = "menuStrip1";
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
            // ownList_statusStrip
            // 
            this.ownList_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.count_toolStripStatusLabel,
            this.space_toolStripStatusLabel,
            this.updated_toolStripStatusLabel,
            this.status_toolStripStatusLabel});
            this.ownList_statusStrip.Location = new System.Drawing.Point(0, 495);
            this.ownList_statusStrip.Name = "ownList_statusStrip";
            this.ownList_statusStrip.Size = new System.Drawing.Size(883, 22);
            this.ownList_statusStrip.TabIndex = 1;
            this.ownList_statusStrip.Text = "statusStrip1";
            // 
            // count_toolStripStatusLabel
            // 
            this.count_toolStripStatusLabel.Name = "count_toolStripStatusLabel";
            this.count_toolStripStatusLabel.Size = new System.Drawing.Size(51, 17);
            this.count_toolStripStatusLabel.Text = "Count: -";
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
            // ownList_dataGridView
            // 
            this.ownList_dataGridView.AllowUserToAddRows = false;
            this.ownList_dataGridView.AllowUserToDeleteRows = false;
            this.ownList_dataGridView.AllowUserToResizeRows = false;
            this.ownList_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ownList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ownList_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.Column1,
            this.Column2,
            this.Column3});
            this.ownList_dataGridView.Location = new System.Drawing.Point(12, 110);
            this.ownList_dataGridView.Name = "ownList_dataGridView";
            this.ownList_dataGridView.ReadOnly = true;
            this.ownList_dataGridView.RowHeadersVisible = false;
            this.ownList_dataGridView.RowTemplate.Height = 25;
            this.ownList_dataGridView.Size = new System.Drawing.Size(859, 373);
            this.ownList_dataGridView.TabIndex = 9;
            this.ownList_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellDoubleClick);
            this.ownList_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellEnter);
            this.ownList_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellLeave);
            this.ownList_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ownList_dataGridView_KeyDown);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.Width = 5;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Item";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 342;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Price (S1)";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 65;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Price (S1)";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 65;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Price (S2)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 65;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Price (S2)";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 65;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Precent";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 65;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Difference";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 65;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
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
            // ServiceCheckerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ownList_dataGridView);
            this.Controls.Add(this.ownList_statusStrip);
            this.Controls.Add(this.ownList_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ownList_menuStrip;
            this.MinimumSize = new System.Drawing.Size(899, 556);
            this.Name = "ServiceCheckerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceCheckerForm_FormClosing);
            this.ownList_menuStrip.ResumeLayout(false);
            this.ownList_menuStrip.PerformLayout();
            this.ownList_statusStrip.ResumeLayout(false);
            this.ownList_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ownList_dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip ownList_menuStrip;
        public System.Windows.Forms.StatusStrip ownList_statusStrip;
        public System.Windows.Forms.ToolStripMenuItem add_toolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem check_toolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel count_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel space_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel updated_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel status_toolStripStatusLabel;
        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.DataGridView ownList_dataGridView;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox secondSer_comboBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox firstSer_comboBox;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button quick_button;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}