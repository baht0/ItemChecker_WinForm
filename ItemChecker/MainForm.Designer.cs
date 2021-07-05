
namespace ItemChecker
{
    partial class MainForm
    {

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.main_statusStrip = new System.Windows.Forms.StatusStrip();
            this.balance_StripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.space_StripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_StripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar_StripStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.timer_StripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_menuStrip = new System.Windows.Forms.MenuStrip();
            this.file_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settings_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.restart_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reload_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.full_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tryskinsReload_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buyOrdersReload_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.withdrawReload_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateData_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tools_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.withdraw_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.checkOwnList_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.floatCheck_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.availability_groupBox = new System.Windows.Forms.GroupBox();
            this.steamMarket_label = new System.Windows.Forms.Label();
            this.tryskins_label = new System.Windows.Forms.Label();
            this.unavailable_label = new System.Windows.Forms.Label();
            this.overstock_label = new System.Windows.Forms.Label();
            this.tradeOffer_groupBox = new System.Windows.Forms.GroupBox();
            this.tradeOffers_linkLabel = new System.Windows.Forms.LinkLabel();
            this.queue_groupBox = new System.Windows.Forms.GroupBox();
            this.queue_linkLabel = new System.Windows.Forms.LinkLabel();
            this.course_groupBox = new System.Windows.Forms.GroupBox();
            this.course_label = new System.Windows.Forms.Label();
            this.steamItems_groupBox = new System.Windows.Forms.GroupBox();
            this.queue_label = new System.Windows.Forms.Label();
            this.available_label = new System.Windows.Forms.Label();
            this.itemPusher_groupBox = new System.Windows.Forms.GroupBox();
            this.cancel_label = new System.Windows.Forms.Label();
            this.push_linkLabel = new System.Windows.Forms.LinkLabel();
            this.push_label = new System.Windows.Forms.Label();
            this.check_label = new System.Windows.Forms.Label();
            this.tryskins_dataGridView = new System.Windows.Forms.DataGridView();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyOrder_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbar_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkOwnList_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floatCheck_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.links_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryskinsLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.csmoneyLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.inventoryLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marketLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.exit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loading_panel = new System.Windows.Forms.Panel();
            this.ver_label = new System.Windows.Forms.Label();
            this.loading_pictureBox = new System.Windows.Forms.PictureBox();
            this.withdraw_dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main_statusStrip.SuspendLayout();
            this.main_menuStrip.SuspendLayout();
            this.availability_groupBox.SuspendLayout();
            this.tradeOffer_groupBox.SuspendLayout();
            this.queue_groupBox.SuspendLayout();
            this.course_groupBox.SuspendLayout();
            this.steamItems_groupBox.SuspendLayout();
            this.itemPusher_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tryskins_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyOrder_dataGridView)).BeginInit();
            this.taskbar_contextMenuStrip.SuspendLayout();
            this.loading_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.withdraw_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // main_statusStrip
            // 
            this.main_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.balance_StripStatus,
            this.space_StripStatus,
            this.status_StripStatus,
            this.progressBar_StripStatus,
            this.timer_StripStatus});
            this.main_statusStrip.Location = new System.Drawing.Point(0, 589);
            this.main_statusStrip.Name = "main_statusStrip";
            this.main_statusStrip.Size = new System.Drawing.Size(600, 22);
            this.main_statusStrip.SizingGrip = false;
            this.main_statusStrip.TabIndex = 0;
            this.main_statusStrip.Text = "main_statusStrip";
            // 
            // balance_StripStatus
            // 
            this.balance_StripStatus.Name = "balance_StripStatus";
            this.balance_StripStatus.Size = new System.Drawing.Size(75, 17);
            this.balance_StripStatus.Text = "Balance: 0.00";
            // 
            // space_StripStatus
            // 
            this.space_StripStatus.Name = "space_StripStatus";
            this.space_StripStatus.Size = new System.Drawing.Size(360, 17);
            this.space_StripStatus.Spring = true;
            // 
            // status_StripStatus
            // 
            this.status_StripStatus.Name = "status_StripStatus";
            this.status_StripStatus.Size = new System.Drawing.Size(48, 17);
            this.status_StripStatus.Text = "Status...";
            // 
            // progressBar_StripStatus
            // 
            this.progressBar_StripStatus.Maximum = 9;
            this.progressBar_StripStatus.Name = "progressBar_StripStatus";
            this.progressBar_StripStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // timer_StripStatus
            // 
            this.timer_StripStatus.Name = "timer_StripStatus";
            this.timer_StripStatus.Size = new System.Drawing.Size(93, 17);
            this.timer_StripStatus.Text = "Next check: 0:00";
            this.timer_StripStatus.Visible = false;
            this.timer_StripStatus.Click += new System.EventHandler(this.timer_StripStatus_Click);
            // 
            // main_menuStrip
            // 
            this.main_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_MainStripMenu,
            this.reload_MainStripMenu,
            this.tools_MainStripMenu});
            this.main_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.main_menuStrip.Name = "main_menuStrip";
            this.main_menuStrip.Size = new System.Drawing.Size(600, 24);
            this.main_menuStrip.TabIndex = 1;
            this.main_menuStrip.Text = "main_menuStrip";
            // 
            // file_MainStripMenu
            // 
            this.file_MainStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settings_MainStripMenu,
            this.toolStripSeparator,
            this.restart_MainStripMenu,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.file_MainStripMenu.Name = "file_MainStripMenu";
            this.file_MainStripMenu.Size = new System.Drawing.Size(37, 20);
            this.file_MainStripMenu.Text = "&File";
            // 
            // settings_MainStripMenu
            // 
            this.settings_MainStripMenu.Image = global::ItemChecker.Properties.Resources.setting;
            this.settings_MainStripMenu.Name = "settings_MainStripMenu";
            this.settings_MainStripMenu.Size = new System.Drawing.Size(134, 22);
            this.settings_MainStripMenu.Text = "Settings";
            this.settings_MainStripMenu.Click += new System.EventHandler(this.settings_MainStripMenu_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(131, 6);
            // 
            // restart_MainStripMenu
            // 
            this.restart_MainStripMenu.Name = "restart_MainStripMenu";
            this.restart_MainStripMenu.Size = new System.Drawing.Size(134, 22);
            this.restart_MainStripMenu.Text = "Restart";
            this.restart_MainStripMenu.Click += new System.EventHandler(this.restart_MainStripMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // reload_MainStripMenu
            // 
            this.reload_MainStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.full_MainStripMenu,
            this.toolStripSeparator3,
            this.tryskinsReload_MainStripMenu,
            this.buyOrdersReload_MainStripMenu,
            this.withdrawReload_MainStripMenu,
            this.toolStripSeparator2,
            this.updateData_toolStripMenuItem});
            this.reload_MainStripMenu.Enabled = false;
            this.reload_MainStripMenu.Name = "reload_MainStripMenu";
            this.reload_MainStripMenu.Size = new System.Drawing.Size(55, 20);
            this.reload_MainStripMenu.Text = "&Reload";
            // 
            // full_MainStripMenu
            // 
            this.full_MainStripMenu.Name = "full_MainStripMenu";
            this.full_MainStripMenu.Size = new System.Drawing.Size(139, 22);
            this.full_MainStripMenu.Text = "Full";
            this.full_MainStripMenu.Click += new System.EventHandler(this.full_MainStripMenu_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(136, 6);
            // 
            // tryskinsReload_MainStripMenu
            // 
            this.tryskinsReload_MainStripMenu.Name = "tryskinsReload_MainStripMenu";
            this.tryskinsReload_MainStripMenu.Size = new System.Drawing.Size(139, 22);
            this.tryskinsReload_MainStripMenu.Text = "TrySkins";
            this.tryskinsReload_MainStripMenu.Click += new System.EventHandler(this.tryskins_MainStripMenu_Click);
            // 
            // buyOrdersReload_MainStripMenu
            // 
            this.buyOrdersReload_MainStripMenu.Name = "buyOrdersReload_MainStripMenu";
            this.buyOrdersReload_MainStripMenu.Size = new System.Drawing.Size(139, 22);
            this.buyOrdersReload_MainStripMenu.Text = "BuyOrders";
            this.buyOrdersReload_MainStripMenu.Click += new System.EventHandler(this.buyOrders_MainStripMenu_Click);
            // 
            // withdrawReload_MainStripMenu
            // 
            this.withdrawReload_MainStripMenu.Enabled = false;
            this.withdrawReload_MainStripMenu.Name = "withdrawReload_MainStripMenu";
            this.withdrawReload_MainStripMenu.Size = new System.Drawing.Size(139, 22);
            this.withdrawReload_MainStripMenu.Text = "Withdraw";
            this.withdrawReload_MainStripMenu.Click += new System.EventHandler(this.withdrawReload_MainStripMenu_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // updateData_toolStripMenuItem
            // 
            this.updateData_toolStripMenuItem.Name = "updateData_toolStripMenuItem";
            this.updateData_toolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.updateData_toolStripMenuItem.Text = "Update Data";
            this.updateData_toolStripMenuItem.Click += new System.EventHandler(this.updateData_toolStripMenuItem_Click);
            // 
            // tools_MainStripMenu
            // 
            this.tools_MainStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withdraw_MainStripMenu,
            this.toolStripSeparator4,
            this.checkOwnList_MainStripMenu,
            this.floatCheck_MainStripMenu});
            this.tools_MainStripMenu.Name = "tools_MainStripMenu";
            this.tools_MainStripMenu.Size = new System.Drawing.Size(46, 20);
            this.tools_MainStripMenu.Text = "&Tools";
            // 
            // withdraw_MainStripMenu
            // 
            this.withdraw_MainStripMenu.Name = "withdraw_MainStripMenu";
            this.withdraw_MainStripMenu.Size = new System.Drawing.Size(154, 22);
            this.withdraw_MainStripMenu.Text = "Withdraw";
            this.withdraw_MainStripMenu.Click += new System.EventHandler(this.withdrawTable_MainStripMenu_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // checkOwnList_MainStripMenu
            // 
            this.checkOwnList_MainStripMenu.Name = "checkOwnList_MainStripMenu";
            this.checkOwnList_MainStripMenu.Size = new System.Drawing.Size(154, 22);
            this.checkOwnList_MainStripMenu.Text = "ServiceChecker";
            this.checkOwnList_MainStripMenu.Click += new System.EventHandler(this.checkOwnList_MainStripMenu_Click);
            // 
            // floatCheck_MainStripMenu
            // 
            this.floatCheck_MainStripMenu.Name = "floatCheck_MainStripMenu";
            this.floatCheck_MainStripMenu.Size = new System.Drawing.Size(154, 22);
            this.floatCheck_MainStripMenu.Text = "FloatCheck";
            this.floatCheck_MainStripMenu.Click += new System.EventHandler(this.floatCheck_MainStripMenu_Click);
            // 
            // availability_groupBox
            // 
            this.availability_groupBox.Controls.Add(this.steamMarket_label);
            this.availability_groupBox.Controls.Add(this.tryskins_label);
            this.availability_groupBox.Controls.Add(this.unavailable_label);
            this.availability_groupBox.Controls.Add(this.overstock_label);
            this.availability_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.availability_groupBox.Location = new System.Drawing.Point(12, 27);
            this.availability_groupBox.Name = "availability_groupBox";
            this.availability_groupBox.Size = new System.Drawing.Size(375, 40);
            this.availability_groupBox.TabIndex = 2;
            this.availability_groupBox.TabStop = false;
            this.availability_groupBox.Text = "Availability:";
            // 
            // steamMarket_label
            // 
            this.steamMarket_label.AutoSize = true;
            this.steamMarket_label.Location = new System.Drawing.Point(276, 16);
            this.steamMarket_label.Name = "steamMarket_label";
            this.steamMarket_label.Size = new System.Drawing.Size(79, 13);
            this.steamMarket_label.TabIndex = 3;
            this.steamMarket_label.Text = "SteamMarket: -";
            // 
            // tryskins_label
            // 
            this.tryskins_label.AutoSize = true;
            this.tryskins_label.Location = new System.Drawing.Point(204, 16);
            this.tryskins_label.Name = "tryskins_label";
            this.tryskins_label.Size = new System.Drawing.Size(57, 13);
            this.tryskins_label.TabIndex = 2;
            this.tryskins_label.Text = "TrySkins: -";
            // 
            // unavailable_label
            // 
            this.unavailable_label.AutoSize = true;
            this.unavailable_label.Location = new System.Drawing.Point(101, 16);
            this.unavailable_label.Name = "unavailable_label";
            this.unavailable_label.Size = new System.Drawing.Size(72, 13);
            this.unavailable_label.TabIndex = 1;
            this.unavailable_label.Text = "Unavailable: -";
            // 
            // overstock_label
            // 
            this.overstock_label.AutoSize = true;
            this.overstock_label.Location = new System.Drawing.Point(6, 16);
            this.overstock_label.Name = "overstock_label";
            this.overstock_label.Size = new System.Drawing.Size(65, 13);
            this.overstock_label.TabIndex = 0;
            this.overstock_label.Text = "Overstock: -";
            // 
            // tradeOffer_groupBox
            // 
            this.tradeOffer_groupBox.Controls.Add(this.tradeOffers_linkLabel);
            this.tradeOffer_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tradeOffer_groupBox.Location = new System.Drawing.Point(393, 27);
            this.tradeOffer_groupBox.Name = "tradeOffer_groupBox";
            this.tradeOffer_groupBox.Size = new System.Drawing.Size(88, 40);
            this.tradeOffer_groupBox.TabIndex = 3;
            this.tradeOffer_groupBox.TabStop = false;
            this.tradeOffer_groupBox.Text = "TrdadeOffers:";
            // 
            // tradeOffers_linkLabel
            // 
            this.tradeOffers_linkLabel.AutoSize = true;
            this.tradeOffers_linkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(145)))), ((int)(((byte)(207)))));
            this.tradeOffers_linkLabel.Location = new System.Drawing.Point(8, 16);
            this.tradeOffers_linkLabel.Name = "tradeOffers_linkLabel";
            this.tradeOffers_linkLabel.Size = new System.Drawing.Size(59, 13);
            this.tradeOffers_linkLabel.TabIndex = 0;
            this.tradeOffers_linkLabel.TabStop = true;
            this.tradeOffers_linkLabel.Text = "Incoming: -";
            this.tradeOffers_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tradeOffers_linkLabel_LinkClicked);
            // 
            // queue_groupBox
            // 
            this.queue_groupBox.Controls.Add(this.queue_linkLabel);
            this.queue_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.queue_groupBox.Location = new System.Drawing.Point(487, 27);
            this.queue_groupBox.Name = "queue_groupBox";
            this.queue_groupBox.Size = new System.Drawing.Size(101, 40);
            this.queue_groupBox.TabIndex = 4;
            this.queue_groupBox.TabStop = false;
            this.queue_groupBox.Text = "Queue:";
            // 
            // queue_linkLabel
            // 
            this.queue_linkLabel.AutoSize = true;
            this.queue_linkLabel.LinkColor = System.Drawing.Color.Green;
            this.queue_linkLabel.Location = new System.Drawing.Point(7, 16);
            this.queue_linkLabel.Name = "queue_linkLabel";
            this.queue_linkLabel.Size = new System.Drawing.Size(70, 13);
            this.queue_linkLabel.TabIndex = 0;
            this.queue_linkLabel.TabStop = true;
            this.queue_linkLabel.Text = "Place order: -";
            this.queue_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.queue_linkLabel_LinkClicked);
            // 
            // course_groupBox
            // 
            this.course_groupBox.Controls.Add(this.course_label);
            this.course_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.course_groupBox.Location = new System.Drawing.Point(13, 539);
            this.course_groupBox.Name = "course_groupBox";
            this.course_groupBox.Size = new System.Drawing.Size(57, 39);
            this.course_groupBox.TabIndex = 4;
            this.course_groupBox.TabStop = false;
            this.course_groupBox.Text = "Course:";
            // 
            // course_label
            // 
            this.course_label.AutoSize = true;
            this.course_label.Location = new System.Drawing.Point(6, 16);
            this.course_label.Name = "course_label";
            this.course_label.Size = new System.Drawing.Size(37, 13);
            this.course_label.TabIndex = 0;
            this.course_label.Text = "0.00 ₽";
            // 
            // steamItems_groupBox
            // 
            this.steamItems_groupBox.Controls.Add(this.queue_label);
            this.steamItems_groupBox.Controls.Add(this.available_label);
            this.steamItems_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.steamItems_groupBox.Location = new System.Drawing.Point(76, 539);
            this.steamItems_groupBox.Name = "steamItems_groupBox";
            this.steamItems_groupBox.Size = new System.Drawing.Size(246, 40);
            this.steamItems_groupBox.TabIndex = 4;
            this.steamItems_groupBox.TabStop = false;
            this.steamItems_groupBox.Text = "Steam Items:";
            // 
            // queue_label
            // 
            this.queue_label.AutoSize = true;
            this.queue_label.Location = new System.Drawing.Point(140, 16);
            this.queue_label.Name = "queue_label";
            this.queue_label.Size = new System.Drawing.Size(48, 13);
            this.queue_label.TabIndex = 2;
            this.queue_label.Text = "Queue: -";
            // 
            // available_label
            // 
            this.available_label.AutoSize = true;
            this.available_label.Location = new System.Drawing.Point(6, 16);
            this.available_label.Name = "available_label";
            this.available_label.Size = new System.Drawing.Size(59, 13);
            this.available_label.TabIndex = 1;
            this.available_label.Text = "Available: -";
            // 
            // itemPusher_groupBox
            // 
            this.itemPusher_groupBox.Controls.Add(this.cancel_label);
            this.itemPusher_groupBox.Controls.Add(this.push_linkLabel);
            this.itemPusher_groupBox.Controls.Add(this.push_label);
            this.itemPusher_groupBox.Controls.Add(this.check_label);
            this.itemPusher_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.itemPusher_groupBox.Location = new System.Drawing.Point(328, 539);
            this.itemPusher_groupBox.Name = "itemPusher_groupBox";
            this.itemPusher_groupBox.Size = new System.Drawing.Size(259, 40);
            this.itemPusher_groupBox.TabIndex = 4;
            this.itemPusher_groupBox.TabStop = false;
            this.itemPusher_groupBox.Text = "ItemPusher:";
            // 
            // cancel_label
            // 
            this.cancel_label.AutoSize = true;
            this.cancel_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cancel_label.Location = new System.Drawing.Point(194, 16);
            this.cancel_label.Name = "cancel_label";
            this.cancel_label.Size = new System.Drawing.Size(49, 13);
            this.cancel_label.TabIndex = 8;
            this.cancel_label.Text = "Cancel: -";
            // 
            // push_linkLabel
            // 
            this.push_linkLabel.ActiveLinkColor = System.Drawing.Color.Red;
            this.push_linkLabel.AutoSize = true;
            this.push_linkLabel.LinkColor = System.Drawing.Color.OrangeRed;
            this.push_linkLabel.Location = new System.Drawing.Point(13, 16);
            this.push_linkLabel.Name = "push_linkLabel";
            this.push_linkLabel.Size = new System.Drawing.Size(40, 13);
            this.push_linkLabel.TabIndex = 7;
            this.push_linkLabel.TabStop = true;
            this.push_linkLabel.Text = "Push...";
            this.push_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.push_linkLabel_LinkClicked);
            // 
            // push_label
            // 
            this.push_label.AutoSize = true;
            this.push_label.Location = new System.Drawing.Point(139, 16);
            this.push_label.Name = "push_label";
            this.push_label.Size = new System.Drawing.Size(40, 13);
            this.push_label.TabIndex = 5;
            this.push_label.Text = "Push: -";
            // 
            // check_label
            // 
            this.check_label.AutoSize = true;
            this.check_label.Location = new System.Drawing.Point(77, 16);
            this.check_label.Name = "check_label";
            this.check_label.Size = new System.Drawing.Size(47, 13);
            this.check_label.TabIndex = 0;
            this.check_label.Text = "Check: -";
            // 
            // tryskins_dataGridView
            // 
            this.tryskins_dataGridView.AllowUserToAddRows = false;
            this.tryskins_dataGridView.AllowUserToDeleteRows = false;
            this.tryskins_dataGridView.AllowUserToResizeRows = false;
            this.tryskins_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tryskins_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tryskins_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.color,
            this.Item,
            this.sta,
            this.csm,
            this.precent,
            this.diff});
            this.tryskins_dataGridView.Location = new System.Drawing.Point(12, 73);
            this.tryskins_dataGridView.Name = "tryskins_dataGridView";
            this.tryskins_dataGridView.ReadOnly = true;
            this.tryskins_dataGridView.RowHeadersVisible = false;
            this.tryskins_dataGridView.RowTemplate.Height = 25;
            this.tryskins_dataGridView.Size = new System.Drawing.Size(575, 227);
            this.tryskins_dataGridView.TabIndex = 5;
            this.tryskins_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tryskins_dataGridView_CellDoubleClick);
            this.tryskins_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.tryskins_dataGridView_CellEnter);
            this.tryskins_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.tryskins_dataGridView_CellLeave);
            this.tryskins_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tryskins_dataGridView_KeyDown);
            // 
            // color
            // 
            this.color.HeaderText = "";
            this.color.Name = "color";
            this.color.ReadOnly = true;
            this.color.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.color.Width = 5;
            // 
            // Item
            // 
            this.Item.HeaderText = "Item (TrySkins)";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 305;
            // 
            // sta
            // 
            this.sta.HeaderText = "ST(A)";
            this.sta.Name = "sta";
            this.sta.ReadOnly = true;
            this.sta.Width = 66;
            // 
            // csm
            // 
            this.csm.HeaderText = "CSM";
            this.csm.Name = "csm";
            this.csm.ReadOnly = true;
            this.csm.Width = 60;
            // 
            // precent
            // 
            this.precent.HeaderText = "Precent";
            this.precent.Name = "precent";
            this.precent.ReadOnly = true;
            this.precent.Width = 55;
            // 
            // diff
            // 
            this.diff.HeaderText = "Difference";
            this.diff.Name = "diff";
            this.diff.ReadOnly = true;
            this.diff.Width = 64;
            // 
            // buyOrder_dataGridView
            // 
            this.buyOrder_dataGridView.AllowUserToAddRows = false;
            this.buyOrder_dataGridView.AllowUserToDeleteRows = false;
            this.buyOrder_dataGridView.AllowUserToResizeRows = false;
            this.buyOrder_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.buyOrder_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buyOrder_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.buyOrder_dataGridView.Location = new System.Drawing.Point(12, 306);
            this.buyOrder_dataGridView.Name = "buyOrder_dataGridView";
            this.buyOrder_dataGridView.ReadOnly = true;
            this.buyOrder_dataGridView.RowHeadersVisible = false;
            this.buyOrder_dataGridView.RowTemplate.Height = 25;
            this.buyOrder_dataGridView.Size = new System.Drawing.Size(575, 227);
            this.buyOrder_dataGridView.TabIndex = 6;
            this.buyOrder_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.buyOrder_dataGridView_CellDoubleClick);
            this.buyOrder_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.buyOrder_dataGridView_CellEnter);
            this.buyOrder_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.buyOrder_dataGridView_CellLeave);
            this.buyOrder_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buyOrder_dataGridView_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Item (BuyOrders)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 305;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ST(A)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 66;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "CSM";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Precent";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 55;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Difference";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 64;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "The program is running.";
            this.notifyIcon.BalloonTipTitle = "Information";
            this.notifyIcon.ContextMenuStrip = this.taskbar_contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ItemChecker";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // taskbar_contextMenuStrip
            // 
            this.taskbar_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkOwnList_toolStripMenuItem,
            this.floatCheck_toolStripMenuItem,
            this.toolStripSeparator5,
            this.links_toolStripMenuItem,
            this.toolStripSeparator9,
            this.exit_toolStripMenuItem});
            this.taskbar_contextMenuStrip.Name = "taskbar_contextMenuStrip";
            this.taskbar_contextMenuStrip.Size = new System.Drawing.Size(155, 104);
            // 
            // checkOwnList_toolStripMenuItem
            // 
            this.checkOwnList_toolStripMenuItem.Name = "checkOwnList_toolStripMenuItem";
            this.checkOwnList_toolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.checkOwnList_toolStripMenuItem.Text = "ServiceChecker";
            this.checkOwnList_toolStripMenuItem.Click += new System.EventHandler(this.checkOwnList_toolStripMenuItem_Click);
            // 
            // floatCheck_toolStripMenuItem
            // 
            this.floatCheck_toolStripMenuItem.Name = "floatCheck_toolStripMenuItem";
            this.floatCheck_toolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.floatCheck_toolStripMenuItem.Text = "FloatCheck";
            this.floatCheck_toolStripMenuItem.Click += new System.EventHandler(this.floatCheck_toolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // links_toolStripMenuItem
            // 
            this.links_toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tryskinsLink_toolStripMenuItem,
            this.toolStripSeparator10,
            this.csmoneyLink_toolStripMenuItem,
            this.toolStripSeparator11,
            this.inventoryLink_toolStripMenuItem,
            this.marketLink_toolStripMenuItem});
            this.links_toolStripMenuItem.Image = global::ItemChecker.Properties.Resources.link;
            this.links_toolStripMenuItem.Name = "links_toolStripMenuItem";
            this.links_toolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.links_toolStripMenuItem.Text = "Links";
            // 
            // tryskinsLink_toolStripMenuItem
            // 
            this.tryskinsLink_toolStripMenuItem.Name = "tryskinsLink_toolStripMenuItem";
            this.tryskinsLink_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tryskinsLink_toolStripMenuItem.Text = "TrySkins";
            this.tryskinsLink_toolStripMenuItem.Click += new System.EventHandler(this.tryskinsLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
            // 
            // csmoneyLink_toolStripMenuItem
            // 
            this.csmoneyLink_toolStripMenuItem.Name = "csmoneyLink_toolStripMenuItem";
            this.csmoneyLink_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.csmoneyLink_toolStripMenuItem.Text = "CsMoney (old)";
            this.csmoneyLink_toolStripMenuItem.Click += new System.EventHandler(this.csmoneyLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(149, 6);
            // 
            // inventoryLink_toolStripMenuItem
            // 
            this.inventoryLink_toolStripMenuItem.Name = "inventoryLink_toolStripMenuItem";
            this.inventoryLink_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inventoryLink_toolStripMenuItem.Text = "Inventory";
            this.inventoryLink_toolStripMenuItem.Click += new System.EventHandler(this.inventoryLink_toolStripMenuItem_Click);
            // 
            // marketLink_toolStripMenuItem
            // 
            this.marketLink_toolStripMenuItem.Name = "marketLink_toolStripMenuItem";
            this.marketLink_toolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.marketLink_toolStripMenuItem.Text = "SteamMarket";
            this.marketLink_toolStripMenuItem.Click += new System.EventHandler(this.marketLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(151, 6);
            // 
            // exit_toolStripMenuItem
            // 
            this.exit_toolStripMenuItem.Name = "exit_toolStripMenuItem";
            this.exit_toolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exit_toolStripMenuItem.Text = "Exit";
            this.exit_toolStripMenuItem.Click += new System.EventHandler(this.exit_toolStripMenuItem_Click);
            // 
            // loading_panel
            // 
            this.loading_panel.BackColor = System.Drawing.SystemColors.Window;
            this.loading_panel.Controls.Add(this.ver_label);
            this.loading_panel.Controls.Add(this.loading_pictureBox);
            this.loading_panel.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loading_panel.Location = new System.Drawing.Point(0, 0);
            this.loading_panel.Name = "loading_panel";
            this.loading_panel.Size = new System.Drawing.Size(600, 611);
            this.loading_panel.TabIndex = 7;
            // 
            // ver_label
            // 
            this.ver_label.AutoSize = true;
            this.ver_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ver_label.Location = new System.Drawing.Point(12, 589);
            this.ver_label.Name = "ver_label";
            this.ver_label.Size = new System.Drawing.Size(81, 13);
            this.ver_label.TabIndex = 1;
            this.ver_label.Text = "Version: 0.0.0.0";
            // 
            // loading_pictureBox
            // 
            this.loading_pictureBox.Image = global::ItemChecker.Properties.Resources.loading;
            this.loading_pictureBox.Location = new System.Drawing.Point(0, 237);
            this.loading_pictureBox.Name = "loading_pictureBox";
            this.loading_pictureBox.Size = new System.Drawing.Size(600, 137);
            this.loading_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loading_pictureBox.TabIndex = 0;
            this.loading_pictureBox.TabStop = false;
            // 
            // withdraw_dataGridView
            // 
            this.withdraw_dataGridView.AllowUserToAddRows = false;
            this.withdraw_dataGridView.AllowUserToDeleteRows = false;
            this.withdraw_dataGridView.AllowUserToResizeRows = false;
            this.withdraw_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.withdraw_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.withdraw_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.Column1});
            this.withdraw_dataGridView.Location = new System.Drawing.Point(12, 73);
            this.withdraw_dataGridView.Name = "withdraw_dataGridView";
            this.withdraw_dataGridView.ReadOnly = true;
            this.withdraw_dataGridView.RowHeadersVisible = false;
            this.withdraw_dataGridView.RowTemplate.Height = 25;
            this.withdraw_dataGridView.Size = new System.Drawing.Size(575, 460);
            this.withdraw_dataGridView.TabIndex = 8;
            this.withdraw_dataGridView.Visible = false;
            this.withdraw_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.withdraw_dataGridView_CellDoubleClick);
            this.withdraw_dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.withdraw_dataGridView_CellEnter);
            this.withdraw_dataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.withdraw_dataGridView_CellLeave);
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
            this.dataGridViewTextBoxColumn8.HeaderText = "Item (Withdraw)";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 305;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "CSM";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 66;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "ST";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 60;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Sales";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 55;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Precent";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 64;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 611);
            this.Controls.Add(this.loading_panel);
            this.Controls.Add(this.withdraw_dataGridView);
            this.Controls.Add(this.tryskins_dataGridView);
            this.Controls.Add(this.buyOrder_dataGridView);
            this.Controls.Add(this.course_groupBox);
            this.Controls.Add(this.steamItems_groupBox);
            this.Controls.Add(this.itemPusher_groupBox);
            this.Controls.Add(this.queue_groupBox);
            this.Controls.Add(this.tradeOffer_groupBox);
            this.Controls.Add(this.availability_groupBox);
            this.Controls.Add(this.main_statusStrip);
            this.Controls.Add(this.main_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.main_menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(616, 650);
            this.MinimumSize = new System.Drawing.Size(616, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.main_statusStrip.ResumeLayout(false);
            this.main_statusStrip.PerformLayout();
            this.main_menuStrip.ResumeLayout(false);
            this.main_menuStrip.PerformLayout();
            this.availability_groupBox.ResumeLayout(false);
            this.availability_groupBox.PerformLayout();
            this.tradeOffer_groupBox.ResumeLayout(false);
            this.tradeOffer_groupBox.PerformLayout();
            this.queue_groupBox.ResumeLayout(false);
            this.queue_groupBox.PerformLayout();
            this.course_groupBox.ResumeLayout(false);
            this.course_groupBox.PerformLayout();
            this.steamItems_groupBox.ResumeLayout(false);
            this.steamItems_groupBox.PerformLayout();
            this.itemPusher_groupBox.ResumeLayout(false);
            this.itemPusher_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tryskins_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyOrder_dataGridView)).EndInit();
            this.taskbar_contextMenuStrip.ResumeLayout(false);
            this.loading_panel.ResumeLayout(false);
            this.loading_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.withdraw_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.StatusStrip main_statusStrip;
        public System.Windows.Forms.MenuStrip main_menuStrip;
        public System.Windows.Forms.ToolStripMenuItem file_MainStripMenu;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem edit_MainStripMenu;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripMenuItem tools_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem settings_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem restart_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem reload_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem full_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem tryskinsReload_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem buyOrdersReload_MainStripMenu;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripMenuItem withdrawReload_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem checkOwnList_MainStripMenu;
        public System.Windows.Forms.ToolStripMenuItem floatCheck_MainStripMenu;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripMenuItem withdraw_MainStripMenu;
        public System.Windows.Forms.ToolStripStatusLabel balance_StripStatus;
        public System.Windows.Forms.ToolStripStatusLabel space_StripStatus;
        public System.Windows.Forms.ToolStripStatusLabel status_StripStatus;
        public System.Windows.Forms.ToolStripProgressBar progressBar_StripStatus;
        public System.Windows.Forms.ToolStripStatusLabel timer_StripStatus;
        public System.Windows.Forms.GroupBox _groupBox;
        public System.Windows.Forms.GroupBox tradeOffer_groupBox;
        public System.Windows.Forms.GroupBox queue_groupBox;
        public System.Windows.Forms.GroupBox course_groupBox;
        public System.Windows.Forms.GroupBox steamItems_groupBox;
        public System.Windows.Forms.GroupBox itemPusher_groupBox;
        public System.Windows.Forms.GroupBox availability_groupBox;
        public System.Windows.Forms.Label steamMarket_label;
        public System.Windows.Forms.Label tryskins_label;
        public System.Windows.Forms.Label unavailable_label;
        public System.Windows.Forms.Label overstock_label;
        public System.Windows.Forms.LinkLabel tradeOffers_linkLabel;
        public System.Windows.Forms.LinkLabel queue_linkLabel;
        public System.Windows.Forms.Label course_label;
        public System.Windows.Forms.Label available_label;
        public System.Windows.Forms.LinkLabel push_linkLabel;
        public System.Windows.Forms.Label push_label;
        public System.Windows.Forms.Label check_label;
        public System.Windows.Forms.DataGridView tryskins_dataGridView;
        public System.Windows.Forms.DataGridView buyOrder_dataGridView;
        public System.Windows.Forms.DataGridViewTextBoxColumn color;
        public System.Windows.Forms.DataGridViewTextBoxColumn Item;
        public System.Windows.Forms.DataGridViewTextBoxColumn sta;
        public System.Windows.Forms.DataGridViewTextBoxColumn csm;
        public System.Windows.Forms.DataGridViewTextBoxColumn precent;
        public System.Windows.Forms.DataGridViewTextBoxColumn diff;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        public System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        public System.Windows.Forms.ContextMenuStrip taskbar_contextMenuStrip;
        public System.Windows.Forms.ToolStripMenuItem floatCheck_toolStripMenuItem;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripMenuItem exit_toolStripMenuItem;
        public System.Windows.Forms.Panel loading_panel;
        public System.Windows.Forms.Label ver_label;
        public System.Windows.Forms.PictureBox loading_pictureBox;
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.DataGridView withdraw_dataGridView;
        private System.Windows.Forms.ToolStripMenuItem updateData_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkOwnList_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testbutton;
        public System.Windows.Forms.Label queue_label;
        private System.Windows.Forms.ToolStripMenuItem links_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marketLink_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryLink_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem csmoneyLink_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem tryskinsLink_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.Label cancel_label;
    }
}

