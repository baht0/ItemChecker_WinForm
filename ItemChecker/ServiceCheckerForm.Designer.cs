
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
            this.services_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.updated_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox = new System.Windows.Forms.TextBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondSer_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.firstSer_comboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.quick_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Filters_groupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.other_comboBox = new System.Windows.Forms.ComboBox();
            this.status_label = new System.Windows.Forms.Label();
            this.status_comboBox = new System.Windows.Forms.ComboBox();
            this.category_label = new System.Windows.Forms.Label();
            this.category_comboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.priceFrom_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.priceTo_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.Prices_groupBox = new System.Windows.Forms.GroupBox();
            this.column_label = new System.Windows.Forms.Label();
            this.column_comboBox = new System.Windows.Forms.ComboBox();
            this.Precent_groupBox = new System.Windows.Forms.GroupBox();
            this.precentTo_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.precentFrom_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.apply_button = new System.Windows.Forms.Button();
            this.reset_button = new System.Windows.Forms.Button();
            this.buttons_groupBox = new System.Windows.Forms.GroupBox();
            this.servChecker_menuStrip.SuspendLayout();
            this.servChecker_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servChecker_dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Filters_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceFrom_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceTo_numericUpDown)).BeginInit();
            this.Prices_groupBox.SuspendLayout();
            this.Precent_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.precentTo_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.precentFrom_numericUpDown)).BeginInit();
            this.buttons_groupBox.SuspendLayout();
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
            this.services_toolStripStatusLabel,
            this.space_toolStripStatusLabel,
            this.status_toolStripStatusLabel,
            this.updated_toolStripStatusLabel});
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
            this.space_toolStripStatusLabel.Size = new System.Drawing.Size(817, 17);
            this.space_toolStripStatusLabel.Spring = true;
            // 
            // status_toolStripStatusLabel
            // 
            this.status_toolStripStatusLabel.Name = "status_toolStripStatusLabel";
            this.status_toolStripStatusLabel.Size = new System.Drawing.Size(73, 17);
            this.status_toolStripStatusLabel.Text = "Processing...";
            this.status_toolStripStatusLabel.Visible = false;
            // 
            // updated_toolStripStatusLabel
            // 
            this.updated_toolStripStatusLabel.Name = "updated_toolStripStatusLabel";
            this.updated_toolStripStatusLabel.Size = new System.Drawing.Size(177, 17);
            this.updated_toolStripStatusLabel.Text = "Updated(h): 0:00(ST) | 0:00(CSM)";
            this.updated_toolStripStatusLabel.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(6, 37);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(495, 23);
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
            this.color_Column,
            this.item_Column,
            this.price1_Column,
            this.price2_Column,
            this.price3_Column,
            this.price4_Column,
            this.precent_Column,
            this.difference_Column,
            this.status_Column});
            this.servChecker_dataGridView.Location = new System.Drawing.Point(12, 110);
            this.servChecker_dataGridView.Name = "servChecker_dataGridView";
            this.servChecker_dataGridView.ReadOnly = true;
            this.servChecker_dataGridView.RowHeadersVisible = false;
            this.servChecker_dataGridView.RowTemplate.Height = 25;
            this.servChecker_dataGridView.Size = new System.Drawing.Size(859, 298);
            this.servChecker_dataGridView.TabIndex = 9;
            this.servChecker_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellDoubleClick);
            this.servChecker_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellEnter);
            this.servChecker_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ownList_dataGridView_CellLeave);
            this.servChecker_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.servChecker_dataGridView_ColumnHeaderMouseClick);
            this.servChecker_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ownList_dataGridView_KeyDown);
            // 
            // color_Column
            // 
            this.color_Column.FillWeight = 0.000422392F;
            this.color_Column.HeaderText = "";
            this.color_Column.Name = "color_Column";
            this.color_Column.ReadOnly = true;
            this.color_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // item_Column
            // 
            this.item_Column.FillWeight = 310F;
            this.item_Column.HeaderText = "Item";
            this.item_Column.Name = "item_Column";
            this.item_Column.ReadOnly = true;
            // 
            // price1_Column
            // 
            this.price1_Column.FillWeight = 70F;
            this.price1_Column.HeaderText = "Price (S1)";
            this.price1_Column.Name = "price1_Column";
            this.price1_Column.ReadOnly = true;
            // 
            // price2_Column
            // 
            this.price2_Column.FillWeight = 70F;
            this.price2_Column.HeaderText = "Price (S1)";
            this.price2_Column.Name = "price2_Column";
            this.price2_Column.ReadOnly = true;
            // 
            // price3_Column
            // 
            this.price3_Column.FillWeight = 70F;
            this.price3_Column.HeaderText = "Price (S2)";
            this.price3_Column.Name = "price3_Column";
            this.price3_Column.ReadOnly = true;
            // 
            // price4_Column
            // 
            this.price4_Column.FillWeight = 70F;
            this.price4_Column.HeaderText = "Price (S2)";
            this.price4_Column.Name = "price4_Column";
            this.price4_Column.ReadOnly = true;
            // 
            // precent_Column
            // 
            this.precent_Column.FillWeight = 60F;
            this.precent_Column.HeaderText = "Precent";
            this.precent_Column.Name = "precent_Column";
            this.precent_Column.ReadOnly = true;
            // 
            // difference_Column
            // 
            this.difference_Column.HeaderText = "Difference [₽]";
            this.difference_Column.Name = "difference_Column";
            this.difference_Column.ReadOnly = true;
            // 
            // status_Column
            // 
            this.status_Column.FillWeight = 80F;
            this.status_Column.HeaderText = "Status";
            this.status_Column.Name = "status_Column";
            this.status_Column.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.secondSer_comboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.firstSer_comboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ServiceChecker";
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
            this.groupBox2.Controls.Add(this.quick_button);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox);
            this.groupBox2.Location = new System.Drawing.Point(283, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(588, 74);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick check";
            // 
            // quick_button
            // 
            this.quick_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.quick_button.Location = new System.Drawing.Point(507, 37);
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
            // Filters_groupBox
            // 
            this.Filters_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Filters_groupBox.Controls.Add(this.label4);
            this.Filters_groupBox.Controls.Add(this.other_comboBox);
            this.Filters_groupBox.Controls.Add(this.status_label);
            this.Filters_groupBox.Controls.Add(this.status_comboBox);
            this.Filters_groupBox.Controls.Add(this.category_label);
            this.Filters_groupBox.Controls.Add(this.category_comboBox);
            this.Filters_groupBox.Location = new System.Drawing.Point(12, 417);
            this.Filters_groupBox.Name = "Filters_groupBox";
            this.Filters_groupBox.Size = new System.Drawing.Size(301, 75);
            this.Filters_groupBox.TabIndex = 12;
            this.Filters_groupBox.TabStop = false;
            this.Filters_groupBox.Text = "Filters";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Other:";
            // 
            // other_comboBox
            // 
            this.other_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.other_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.other_comboBox.FormattingEnabled = true;
            this.other_comboBox.Items.AddRange(new object[] {
            "Any",
            "Knife",
            "Gloves",
            "Sticker",
            "Patch",
            "Pin",
            "Key",
            "Pass",
            "Music Kit",
            "Graffiti",
            "Case",
            "Package"});
            this.other_comboBox.Location = new System.Drawing.Point(104, 37);
            this.other_comboBox.Name = "other_comboBox";
            this.other_comboBox.Size = new System.Drawing.Size(92, 23);
            this.other_comboBox.TabIndex = 37;
            this.other_comboBox.SelectedIndexChanged += new System.EventHandler(this.other_comboBox_SelectedIndexChanged);
            // 
            // status_label
            // 
            this.status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(202, 18);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(42, 15);
            this.status_label.TabIndex = 36;
            this.status_label.Text = "Status:";
            // 
            // status_comboBox
            // 
            this.status_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status_comboBox.FormattingEnabled = true;
            this.status_comboBox.Items.AddRange(new object[] {
            "Any",
            "Tradable",
            "Unknown",
            "Overstock",
            "Unavailable",
            "Ordered"});
            this.status_comboBox.Location = new System.Drawing.Point(202, 36);
            this.status_comboBox.Name = "status_comboBox";
            this.status_comboBox.Size = new System.Drawing.Size(92, 23);
            this.status_comboBox.TabIndex = 35;
            // 
            // category_label
            // 
            this.category_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.category_label.AutoSize = true;
            this.category_label.Location = new System.Drawing.Point(6, 19);
            this.category_label.Name = "category_label";
            this.category_label.Size = new System.Drawing.Size(58, 15);
            this.category_label.TabIndex = 30;
            this.category_label.Text = "Category:";
            // 
            // category_comboBox
            // 
            this.category_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.category_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category_comboBox.FormattingEnabled = true;
            this.category_comboBox.Items.AddRange(new object[] {
            "Any",
            "Normal",
            "StatTrak™",
            "Souvenir",
            "★",
            "★ StatTrak™"});
            this.category_comboBox.Location = new System.Drawing.Point(6, 37);
            this.category_comboBox.Name = "category_comboBox";
            this.category_comboBox.Size = new System.Drawing.Size(92, 23);
            this.category_comboBox.TabIndex = 29;
            this.category_comboBox.SelectedIndexChanged += new System.EventHandler(this.category_comboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 32;
            this.label7.Text = "From:";
            // 
            // priceFrom_numericUpDown
            // 
            this.priceFrom_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.priceFrom_numericUpDown.Location = new System.Drawing.Point(104, 37);
            this.priceFrom_numericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.priceFrom_numericUpDown.Name = "priceFrom_numericUpDown";
            this.priceFrom_numericUpDown.Size = new System.Drawing.Size(82, 23);
            this.priceFrom_numericUpDown.TabIndex = 31;
            this.priceFrom_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // priceTo_numericUpDown
            // 
            this.priceTo_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.priceTo_numericUpDown.Location = new System.Drawing.Point(192, 37);
            this.priceTo_numericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.priceTo_numericUpDown.Name = "priceTo_numericUpDown";
            this.priceTo_numericUpDown.Size = new System.Drawing.Size(82, 23);
            this.priceTo_numericUpDown.TabIndex = 33;
            this.priceTo_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 15);
            this.label6.TabIndex = 34;
            this.label6.Text = "To:";
            // 
            // Prices_groupBox
            // 
            this.Prices_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Prices_groupBox.Controls.Add(this.column_label);
            this.Prices_groupBox.Controls.Add(this.priceTo_numericUpDown);
            this.Prices_groupBox.Controls.Add(this.column_comboBox);
            this.Prices_groupBox.Controls.Add(this.label7);
            this.Prices_groupBox.Controls.Add(this.priceFrom_numericUpDown);
            this.Prices_groupBox.Controls.Add(this.label6);
            this.Prices_groupBox.Location = new System.Drawing.Point(319, 417);
            this.Prices_groupBox.Name = "Prices_groupBox";
            this.Prices_groupBox.Size = new System.Drawing.Size(283, 75);
            this.Prices_groupBox.TabIndex = 35;
            this.Prices_groupBox.TabStop = false;
            this.Prices_groupBox.Text = "Prices ($)";
            // 
            // column_label
            // 
            this.column_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.column_label.AutoSize = true;
            this.column_label.Location = new System.Drawing.Point(6, 19);
            this.column_label.Name = "column_label";
            this.column_label.Size = new System.Drawing.Size(53, 15);
            this.column_label.TabIndex = 38;
            this.column_label.Text = "Column:";
            // 
            // column_comboBox
            // 
            this.column_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.column_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.column_comboBox.FormattingEnabled = true;
            this.column_comboBox.Items.AddRange(new object[] {
            "None",
            "Price(S1)",
            "Price(S1)",
            "Price(S2)",
            "Price(S2)"});
            this.column_comboBox.Location = new System.Drawing.Point(6, 37);
            this.column_comboBox.Name = "column_comboBox";
            this.column_comboBox.Size = new System.Drawing.Size(92, 23);
            this.column_comboBox.TabIndex = 37;
            // 
            // Precent_groupBox
            // 
            this.Precent_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Precent_groupBox.Controls.Add(this.precentTo_numericUpDown);
            this.Precent_groupBox.Controls.Add(this.label5);
            this.Precent_groupBox.Controls.Add(this.precentFrom_numericUpDown);
            this.Precent_groupBox.Controls.Add(this.label8);
            this.Precent_groupBox.Location = new System.Drawing.Point(608, 417);
            this.Precent_groupBox.Name = "Precent_groupBox";
            this.Precent_groupBox.Size = new System.Drawing.Size(185, 75);
            this.Precent_groupBox.TabIndex = 36;
            this.Precent_groupBox.TabStop = false;
            this.Precent_groupBox.Text = "Precent (%)";
            // 
            // precentTo_numericUpDown
            // 
            this.precentTo_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.precentTo_numericUpDown.Location = new System.Drawing.Point(94, 37);
            this.precentTo_numericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.precentTo_numericUpDown.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.precentTo_numericUpDown.Name = "precentTo_numericUpDown";
            this.precentTo_numericUpDown.Size = new System.Drawing.Size(82, 23);
            this.precentTo_numericUpDown.TabIndex = 33;
            this.precentTo_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "From:";
            // 
            // precentFrom_numericUpDown
            // 
            this.precentFrom_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.precentFrom_numericUpDown.Location = new System.Drawing.Point(6, 37);
            this.precentFrom_numericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.precentFrom_numericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.precentFrom_numericUpDown.Name = "precentFrom_numericUpDown";
            this.precentFrom_numericUpDown.Size = new System.Drawing.Size(82, 23);
            this.precentFrom_numericUpDown.TabIndex = 31;
            this.precentFrom_numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 34;
            this.label8.Text = "To:";
            // 
            // apply_button
            // 
            this.apply_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.apply_button.Location = new System.Drawing.Point(6, 17);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(60, 23);
            this.apply_button.TabIndex = 37;
            this.apply_button.Text = "Apply";
            this.apply_button.UseVisualStyleBackColor = true;
            this.apply_button.Click += new System.EventHandler(this.apply_button_Click);
            // 
            // reset_button
            // 
            this.reset_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reset_button.Location = new System.Drawing.Point(6, 46);
            this.reset_button.Name = "reset_button";
            this.reset_button.Size = new System.Drawing.Size(60, 23);
            this.reset_button.TabIndex = 38;
            this.reset_button.Text = "Reset";
            this.reset_button.UseVisualStyleBackColor = true;
            this.reset_button.Click += new System.EventHandler(this.reset_button_Click);
            // 
            // buttons_groupBox
            // 
            this.buttons_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttons_groupBox.Controls.Add(this.reset_button);
            this.buttons_groupBox.Controls.Add(this.apply_button);
            this.buttons_groupBox.Location = new System.Drawing.Point(799, 417);
            this.buttons_groupBox.Name = "buttons_groupBox";
            this.buttons_groupBox.Size = new System.Drawing.Size(72, 75);
            this.buttons_groupBox.TabIndex = 39;
            this.buttons_groupBox.TabStop = false;
            // 
            // ServiceCheckerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 517);
            this.Controls.Add(this.buttons_groupBox);
            this.Controls.Add(this.Precent_groupBox);
            this.Controls.Add(this.Prices_groupBox);
            this.Controls.Add(this.Filters_groupBox);
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
            this.Filters_groupBox.ResumeLayout(false);
            this.Filters_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priceFrom_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceTo_numericUpDown)).EndInit();
            this.Prices_groupBox.ResumeLayout(false);
            this.Prices_groupBox.PerformLayout();
            this.Precent_groupBox.ResumeLayout(false);
            this.Precent_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.precentTo_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.precentFrom_numericUpDown)).EndInit();
            this.buttons_groupBox.ResumeLayout(false);
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
        public System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel services_toolStripStatusLabel;
        public System.Windows.Forms.ComboBox category_comboBox;
        public System.Windows.Forms.Label category_label;
        public System.Windows.Forms.Label status_label;
        public System.Windows.Forms.ComboBox status_comboBox;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown priceTo_numericUpDown;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown priceFrom_numericUpDown;
        public System.Windows.Forms.Label column_label;
        public System.Windows.Forms.ComboBox column_comboBox;
        public System.Windows.Forms.NumericUpDown precentTo_numericUpDown;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown precentFrom_numericUpDown;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox other_comboBox;
        public System.Windows.Forms.Button apply_button;
        public System.Windows.Forms.Button reset_button;
        public System.Windows.Forms.GroupBox buttons_groupBox;
        public System.Windows.Forms.GroupBox Filters_groupBox;
        public System.Windows.Forms.GroupBox Prices_groupBox;
        public System.Windows.Forms.GroupBox Precent_groupBox;
        public System.Windows.Forms.DataGridViewTextBoxColumn color_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn item_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn price1_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn price2_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn price3_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn price4_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn precent_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn difference_Column;
        public System.Windows.Forms.DataGridViewTextBoxColumn status_Column;
    }
}