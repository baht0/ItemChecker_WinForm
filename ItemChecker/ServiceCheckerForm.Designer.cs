
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
            this.csv_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractListtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servChecker_statusStrip = new System.Windows.Forms.StatusStrip();
            this.count_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.services_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.availability_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updated_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.quickCheck_textBox = new System.Windows.Forms.TextBox();
            this.servChecker_dataGridView = new System.Windows.Forms.DataGridView();
            this.color_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price1_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price2_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price3_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price4_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precent_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.difference_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.availability_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondSer_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.firstSer_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clearQCheck_linkLabel = new System.Windows.Forms.LinkLabel();
            this.quick_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.filters_linkLabel = new System.Windows.Forms.LinkLabel();
            this.clearSearch_linkLabel = new System.Windows.Forms.LinkLabel();
            this.search_textBox = new System.Windows.Forms.TextBox();
            this.servChecker_menuStrip.SuspendLayout();
            this.servChecker_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servChecker_dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // servChecker_menuStrip
            // 
            this.servChecker_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add_toolStripMenuItem,
            this.check_toolStripMenuItem,
            this.csv_toolStripMenuItem,
            this.extractListtxtToolStripMenuItem});
            this.servChecker_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.servChecker_menuStrip.Name = "servChecker_menuStrip";
            this.servChecker_menuStrip.Size = new System.Drawing.Size(1089, 24);
            this.servChecker_menuStrip.TabIndex = 0;
            this.servChecker_menuStrip.Text = "menuStrip1";
            // 
            // add_toolStripMenuItem
            // 
            this.add_toolStripMenuItem.Name = "add_toolStripMenuItem";
            this.add_toolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.add_toolStripMenuItem.Text = "CheckList";
            this.add_toolStripMenuItem.Click += new System.EventHandler(this.add_toolStripMenuItem_Click);
            // 
            // check_toolStripMenuItem
            // 
            this.check_toolStripMenuItem.Name = "check_toolStripMenuItem";
            this.check_toolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.check_toolStripMenuItem.Text = "Check";
            this.check_toolStripMenuItem.Click += new System.EventHandler(this.check_toolStripMenuItem_Click);
            // 
            // csv_toolStripMenuItem
            // 
            this.csv_toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.csv_toolStripMenuItem.Image = global::ItemChecker.Properties.Resources.CSV;
            this.csv_toolStripMenuItem.Name = "csv_toolStripMenuItem";
            this.csv_toolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.csv_toolStripMenuItem.Text = "Format CSV";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = global::ItemChecker.Properties.Resources.import;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem1_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Image = global::ItemChecker.Properties.Resources.export;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem1_Click);
            // 
            // extractListtxtToolStripMenuItem
            // 
            this.extractListtxtToolStripMenuItem.Name = "extractListtxtToolStripMenuItem";
            this.extractListtxtToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.extractListtxtToolStripMenuItem.Text = "Extract list *.txt";
            this.extractListtxtToolStripMenuItem.Click += new System.EventHandler(this.extractListtxtToolStripMenuItem_Click);
            // 
            // servChecker_statusStrip
            // 
            this.servChecker_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.count_toolStripStatusLabel,
            this.services_toolStripStatusLabel,
            this.space_toolStripStatusLabel,
            this.availability_toolStripStatusLabel,
            this.updated_toolStripStatusLabel,
            this.status_toolStripStatusLabel});
            this.servChecker_statusStrip.Location = new System.Drawing.Point(0, 509);
            this.servChecker_statusStrip.Name = "servChecker_statusStrip";
            this.servChecker_statusStrip.Size = new System.Drawing.Size(1089, 22);
            this.servChecker_statusStrip.TabIndex = 1;
            this.servChecker_statusStrip.Text = "statusStrip1";
            // 
            // count_toolStripStatusLabel
            // 
            this.count_toolStripStatusLabel.Name = "count_toolStripStatusLabel";
            this.count_toolStripStatusLabel.Size = new System.Drawing.Size(51, 17);
            this.count_toolStripStatusLabel.Text = "Count: -";
            // 
            // services_toolStripStatusLabel
            // 
            this.services_toolStripStatusLabel.Name = "services_toolStripStatusLabel";
            this.services_toolStripStatusLabel.Size = new System.Drawing.Size(58, 17);
            this.services_toolStripStatusLabel.Text = "From - To";
            this.services_toolStripStatusLabel.Visible = false;
            // 
            // space_toolStripStatusLabel
            // 
            this.space_toolStripStatusLabel.Name = "space_toolStripStatusLabel";
            this.space_toolStripStatusLabel.Size = new System.Drawing.Size(1023, 17);
            this.space_toolStripStatusLabel.Spring = true;
            // 
            // availability_toolStripStatusLabel
            // 
            this.availability_toolStripStatusLabel.Name = "availability_toolStripStatusLabel";
            this.availability_toolStripStatusLabel.Size = new System.Drawing.Size(68, 17);
            this.availability_toolStripStatusLabel.Text = "Availability:";
            this.availability_toolStripStatusLabel.Visible = false;
            // 
            // updated_toolStripStatusLabel
            // 
            this.updated_toolStripStatusLabel.Name = "updated_toolStripStatusLabel";
            this.updated_toolStripStatusLabel.Size = new System.Drawing.Size(70, 17);
            this.updated_toolStripStatusLabel.Text = "Updated(h):";
            this.updated_toolStripStatusLabel.Visible = false;
            // 
            // status_toolStripStatusLabel
            // 
            this.status_toolStripStatusLabel.Name = "status_toolStripStatusLabel";
            this.status_toolStripStatusLabel.Size = new System.Drawing.Size(73, 17);
            this.status_toolStripStatusLabel.Text = "Processing...";
            this.status_toolStripStatusLabel.Visible = false;
            // 
            // quickCheck_textBox
            // 
            this.quickCheck_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quickCheck_textBox.Location = new System.Drawing.Point(6, 37);
            this.quickCheck_textBox.Name = "quickCheck_textBox";
            this.quickCheck_textBox.Size = new System.Drawing.Size(705, 23);
            this.quickCheck_textBox.TabIndex = 2;
            this.quickCheck_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
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
            this.color_Column,
            this.item_Column,
            this.price1_Column,
            this.price2_Column,
            this.price3_Column,
            this.price4_Column,
            this.precent_Column,
            this.difference_Column,
            this.status_Column,
            this.availability_Column});
            this.servChecker_dataGridView.Location = new System.Drawing.Point(12, 110);
            this.servChecker_dataGridView.Name = "servChecker_dataGridView";
            this.servChecker_dataGridView.ReadOnly = true;
            this.servChecker_dataGridView.RowHeadersVisible = false;
            this.servChecker_dataGridView.RowTemplate.Height = 25;
            this.servChecker_dataGridView.Size = new System.Drawing.Size(1065, 341);
            this.servChecker_dataGridView.TabIndex = 9;
            this.servChecker_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellDoubleClick);
            this.servChecker_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellEnter);
            this.servChecker_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellLeave);
            this.servChecker_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.servChecker_dataGridView_ColumnHeaderMouseClick);
            this.servChecker_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ownList_dataGridView_KeyDown);
            // 
            // color_Column
            // 
            this.color_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.color_Column.DataPropertyName = "color_Column";
            this.color_Column.FillWeight = 0.000422392F;
            this.color_Column.HeaderText = "";
            this.color_Column.Name = "color_Column";
            this.color_Column.ReadOnly = true;
            this.color_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.color_Column.Width = 5;
            // 
            // item_Column
            // 
            this.item_Column.DataPropertyName = "item_Column";
            this.item_Column.FillWeight = 310F;
            this.item_Column.HeaderText = "Item";
            this.item_Column.Name = "item_Column";
            this.item_Column.ReadOnly = true;
            this.item_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // price1_Column
            // 
            this.price1_Column.DataPropertyName = "price1_Column";
            this.price1_Column.FillWeight = 75F;
            this.price1_Column.HeaderText = "Price (S1)";
            this.price1_Column.Name = "price1_Column";
            this.price1_Column.ReadOnly = true;
            this.price1_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // price2_Column
            // 
            this.price2_Column.DataPropertyName = "price2_Column";
            this.price2_Column.FillWeight = 75F;
            this.price2_Column.HeaderText = "Price (S1)";
            this.price2_Column.Name = "price2_Column";
            this.price2_Column.ReadOnly = true;
            this.price2_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // price3_Column
            // 
            this.price3_Column.DataPropertyName = "price3_Column";
            this.price3_Column.FillWeight = 75F;
            this.price3_Column.HeaderText = "Price (S2)";
            this.price3_Column.Name = "price3_Column";
            this.price3_Column.ReadOnly = true;
            this.price3_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // price4_Column
            // 
            this.price4_Column.DataPropertyName = "price4_Column";
            this.price4_Column.FillWeight = 75F;
            this.price4_Column.HeaderText = "Price (S2)";
            this.price4_Column.Name = "price4_Column";
            this.price4_Column.ReadOnly = true;
            this.price4_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // precent_Column
            // 
            this.precent_Column.DataPropertyName = "precent_Column";
            this.precent_Column.FillWeight = 60F;
            this.precent_Column.HeaderText = "Precent";
            this.precent_Column.Name = "precent_Column";
            this.precent_Column.ReadOnly = true;
            this.precent_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // difference_Column
            // 
            this.difference_Column.DataPropertyName = "difference_Column";
            this.difference_Column.FillWeight = 80F;
            this.difference_Column.HeaderText = "Difference [₽]";
            this.difference_Column.Name = "difference_Column";
            this.difference_Column.ReadOnly = true;
            this.difference_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // status_Column
            // 
            this.status_Column.DataPropertyName = "status_Column";
            this.status_Column.FillWeight = 80F;
            this.status_Column.HeaderText = "Status";
            this.status_Column.Name = "status_Column";
            this.status_Column.ReadOnly = true;
            this.status_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // availability_Column
            // 
            this.availability_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.availability_Column.DataPropertyName = "availability_Column";
            this.availability_Column.FillWeight = 1F;
            this.availability_Column.HeaderText = "";
            this.availability_Column.Name = "availability_Column";
            this.availability_Column.ReadOnly = true;
            this.availability_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.availability_Column.Width = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.secondSer_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.firstSer_comboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Services";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 18);
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
            this.secondSer_comboBox.Location = new System.Drawing.Point(133, 36);
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
            this.groupBox2.Controls.Add(this.clearQCheck_linkLabel);
            this.groupBox2.Controls.Add(this.quick_button);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.quickCheck_textBox);
            this.groupBox2.Location = new System.Drawing.Point(279, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(798, 74);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick check";
            // 
            // clearQCheck_linkLabel
            // 
            this.clearQCheck_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearQCheck_linkLabel.AutoSize = true;
            this.clearQCheck_linkLabel.BackColor = System.Drawing.Color.White;
            this.clearQCheck_linkLabel.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clearQCheck_linkLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearQCheck_linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.clearQCheck_linkLabel.LinkColor = System.Drawing.Color.Gray;
            this.clearQCheck_linkLabel.Location = new System.Drawing.Point(692, 39);
            this.clearQCheck_linkLabel.Name = "clearQCheck_linkLabel";
            this.clearQCheck_linkLabel.Size = new System.Drawing.Size(18, 19);
            this.clearQCheck_linkLabel.TabIndex = 10;
            this.clearQCheck_linkLabel.TabStop = true;
            this.clearQCheck_linkLabel.Text = "X";
            this.clearQCheck_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearQCheck_linkLabel_LinkClicked);
            // 
            // quick_button
            // 
            this.quick_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_button.Location = new System.Drawing.Point(717, 37);
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
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.filters_linkLabel);
            this.groupBox3.Controls.Add(this.clearSearch_linkLabel);
            this.groupBox3.Controls.Add(this.search_textBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 457);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1065, 49);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search Item";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Item Name:";
            // 
            // filters_linkLabel
            // 
            this.filters_linkLabel.AutoSize = true;
            this.filters_linkLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filters_linkLabel.LinkColor = System.Drawing.Color.DodgerBlue;
            this.filters_linkLabel.Location = new System.Drawing.Point(6, 19);
            this.filters_linkLabel.Name = "filters_linkLabel";
            this.filters_linkLabel.Size = new System.Drawing.Size(42, 17);
            this.filters_linkLabel.TabIndex = 10;
            this.filters_linkLabel.TabStop = true;
            this.filters_linkLabel.Text = "Filters";
            this.filters_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.filters_linkLabel_LinkClicked);
            // 
            // clearSearch_linkLabel
            // 
            this.clearSearch_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSearch_linkLabel.AutoSize = true;
            this.clearSearch_linkLabel.BackColor = System.Drawing.Color.White;
            this.clearSearch_linkLabel.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clearSearch_linkLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearSearch_linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.clearSearch_linkLabel.LinkColor = System.Drawing.Color.Gray;
            this.clearSearch_linkLabel.Location = new System.Drawing.Point(1039, 18);
            this.clearSearch_linkLabel.Name = "clearSearch_linkLabel";
            this.clearSearch_linkLabel.Size = new System.Drawing.Size(18, 19);
            this.clearSearch_linkLabel.TabIndex = 9;
            this.clearSearch_linkLabel.TabStop = true;
            this.clearSearch_linkLabel.Text = "X";
            this.clearSearch_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearSearch_linkLabel_LinkClicked);
            // 
            // search_textBox
            // 
            this.search_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search_textBox.Location = new System.Drawing.Point(129, 16);
            this.search_textBox.Name = "search_textBox";
            this.search_textBox.Size = new System.Drawing.Size(930, 23);
            this.search_textBox.TabIndex = 7;
            this.search_textBox.TextChanged += new System.EventHandler(this.search_textBox_TextChanged);
            // 
            // ServiceCheckerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 531);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.servChecker_dataGridView);
            this.Controls.Add(this.servChecker_statusStrip);
            this.Controls.Add(this.servChecker_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.servChecker_menuStrip;
            this.MinimumSize = new System.Drawing.Size(1105, 570);
            this.Name = "ServiceCheckerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServiceChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceCheckerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServiceCheckerForm_Load);
            this.servChecker_menuStrip.ResumeLayout(false);
            this.servChecker_menuStrip.PerformLayout();
            this.servChecker_statusStrip.ResumeLayout(false);
            this.servChecker_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servChecker_dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        public System.Windows.Forms.TextBox quickCheck_textBox;
        public System.Windows.Forms.DataGridView servChecker_dataGridView;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox secondSer_comboBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox firstSer_comboBox;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button quick_button;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ToolStripStatusLabel services_toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem csv_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel availability_toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox search_textBox;
        private System.Windows.Forms.LinkLabel clearSearch_linkLabel;
        private System.Windows.Forms.LinkLabel clearQCheck_linkLabel;
        private System.Windows.Forms.ToolStripMenuItem extractListtxtToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn color_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn price1_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn price2_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn price3_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn price4_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn precent_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn difference_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn status_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn availability_Column;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel filters_linkLabel;
    }
}