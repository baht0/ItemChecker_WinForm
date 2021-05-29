
namespace ItemChecker
{
    partial class CheckOwnListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckOwnListForm));
            this.ownList_menuStrip = new System.Windows.Forms.MenuStrip();
            this.open_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.add_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.service_toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.check_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownList_statusStrip = new System.Windows.Forms.StatusStrip();
            this.count_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.queue_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updated_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.quick_button = new System.Windows.Forms.Button();
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
            this.ownList_menuStrip.SuspendLayout();
            this.ownList_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ownList_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ownList_menuStrip
            // 
            this.ownList_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_toolStripMenuItem,
            this.add_toolStripMenuItem,
            this.service_toolStripComboBox,
            this.check_toolStripMenuItem});
            this.ownList_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.ownList_menuStrip.Name = "ownList_menuStrip";
            this.ownList_menuStrip.Size = new System.Drawing.Size(883, 27);
            this.ownList_menuStrip.TabIndex = 0;
            this.ownList_menuStrip.Text = "menuStrip1";
            // 
            // open_toolStripMenuItem
            // 
            this.open_toolStripMenuItem.Name = "open_toolStripMenuItem";
            this.open_toolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.open_toolStripMenuItem.Text = "Open File";
            this.open_toolStripMenuItem.Click += new System.EventHandler(this.open_toolStripMenuItem_Click);
            // 
            // add_toolStripMenuItem
            // 
            this.add_toolStripMenuItem.Name = "add_toolStripMenuItem";
            this.add_toolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.add_toolStripMenuItem.Text = "Add List";
            this.add_toolStripMenuItem.Click += new System.EventHandler(this.add_toolStripMenuItem_Click);
            // 
            // service_toolStripComboBox
            // 
            this.service_toolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.service_toolStripComboBox.Items.AddRange(new object[] {
            "Cs.Money",
            "Loot.Farm"});
            this.service_toolStripComboBox.Name = "service_toolStripComboBox";
            this.service_toolStripComboBox.Size = new System.Drawing.Size(121, 23);
            // 
            // check_toolStripMenuItem
            // 
            this.check_toolStripMenuItem.Name = "check_toolStripMenuItem";
            this.check_toolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.check_toolStripMenuItem.Text = "Check";
            this.check_toolStripMenuItem.Click += new System.EventHandler(this.check_toolStripMenuItem_Click);
            // 
            // ownList_statusStrip
            // 
            this.ownList_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.count_toolStripStatusLabel,
            this.queue_toolStripStatusLabel,
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
            // queue_toolStripStatusLabel
            // 
            this.queue_toolStripStatusLabel.Name = "queue_toolStripStatusLabel";
            this.queue_toolStripStatusLabel.Size = new System.Drawing.Size(126, 17);
            this.queue_toolStripStatusLabel.Text = "BuyOrder: 0.00$ | 0.00₽";
            this.queue_toolStripStatusLabel.Visible = false;
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
            this.textBox.Location = new System.Drawing.Point(12, 30);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(752, 23);
            this.textBox.TabIndex = 2;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // quick_button
            // 
            this.quick_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_button.Location = new System.Drawing.Point(770, 30);
            this.quick_button.Name = "quick_button";
            this.quick_button.Size = new System.Drawing.Size(101, 23);
            this.quick_button.TabIndex = 3;
            this.quick_button.Text = "Quick check";
            this.quick_button.UseVisualStyleBackColor = true;
            this.quick_button.Click += new System.EventHandler(this.quick_button_Click);
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
            this.ownList_dataGridView.Location = new System.Drawing.Point(12, 59);
            this.ownList_dataGridView.Name = "ownList_dataGridView";
            this.ownList_dataGridView.ReadOnly = true;
            this.ownList_dataGridView.RowHeadersVisible = false;
            this.ownList_dataGridView.RowTemplate.Height = 25;
            this.ownList_dataGridView.Size = new System.Drawing.Size(859, 433);
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
            this.dataGridViewTextBoxColumn9.HeaderText = "Price (ST)";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 65;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Price";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 65;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "BuyOrder (STA)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 65;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "GetPrice";
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
            // CheckOwnListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 517);
            this.Controls.Add(this.ownList_dataGridView);
            this.Controls.Add(this.quick_button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.ownList_statusStrip);
            this.Controls.Add(this.ownList_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ownList_menuStrip;
            this.MinimumSize = new System.Drawing.Size(899, 556);
            this.Name = "CheckOwnListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Own List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckOwnListForm_FormClosing);
            this.ownList_menuStrip.ResumeLayout(false);
            this.ownList_menuStrip.PerformLayout();
            this.ownList_statusStrip.ResumeLayout(false);
            this.ownList_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ownList_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip ownList_menuStrip;
        public System.Windows.Forms.StatusStrip ownList_statusStrip;
        public System.Windows.Forms.ToolStripMenuItem open_toolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem add_toolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox service_toolStripComboBox;
        public System.Windows.Forms.ToolStripMenuItem check_toolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel count_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel queue_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel space_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel updated_toolStripStatusLabel;
        public System.Windows.Forms.ToolStripStatusLabel status_toolStripStatusLabel;
        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.Button quick_button;
        public System.Windows.Forms.DataGridView ownList_dataGridView;
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