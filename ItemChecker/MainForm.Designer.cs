
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateData_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withdraw_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWithdraw_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadWithdraw_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.checkCsmWithdraw_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tools_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trySkinsTotxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyOrdersTotxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buyOrderPush_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floatCheck_MainStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.floatCheck_groupBox = new System.Windows.Forms.GroupBox();
            this.floatItems_label = new System.Windows.Forms.Label();
            this.floatPurchases_label = new System.Windows.Forms.Label();
            this.floatCheck_label = new System.Windows.Forms.Label();
            this.pusherBuyOrder_groupBox = new System.Windows.Forms.GroupBox();
            this.pusherItems_label = new System.Windows.Forms.Label();
            this.pusherCancel_label = new System.Windows.Forms.Label();
            this.pusherPush_label = new System.Windows.Forms.Label();
            this.pusherCheck_label = new System.Windows.Forms.Label();
            this.favoriteCheck_groupBox = new System.Windows.Forms.GroupBox();
            this.favoriteItems_label = new System.Windows.Forms.Label();
            this.favoriteTrades_label = new System.Windows.Forms.Label();
            this.favoriteCheck_label = new System.Windows.Forms.Label();
            this.tryskins_dataGridView = new System.Windows.Forms.DataGridView();
            this.colorTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csmTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precentTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.differenceTS_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyOrder_dataGridView = new System.Windows.Forms.DataGridView();
            this.colorBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csmBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precentBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.differenceBO_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbar_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tree_serviceParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.links_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tryskinsLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csmoneyLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.inventoryLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marketLink_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.settings_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withdraw_dataGridView = new System.Windows.Forms.DataGridView();
            this.colorWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csmWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salesWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precentWD_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loading_pictureBox = new System.Windows.Forms.PictureBox();
            this.ver_label = new System.Windows.Forms.Label();
            this.loading_panel = new System.Windows.Forms.Panel();
            this.info_toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolsMessage_groupBox = new System.Windows.Forms.GroupBox();
            this.toolsMessage_label = new System.Windows.Forms.Label();
            this.main_statusStrip.SuspendLayout();
            this.main_menuStrip.SuspendLayout();
            this.availability_groupBox.SuspendLayout();
            this.tradeOffer_groupBox.SuspendLayout();
            this.queue_groupBox.SuspendLayout();
            this.course_groupBox.SuspendLayout();
            this.steamItems_groupBox.SuspendLayout();
            this.floatCheck_groupBox.SuspendLayout();
            this.pusherBuyOrder_groupBox.SuspendLayout();
            this.favoriteCheck_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tryskins_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyOrder_dataGridView)).BeginInit();
            this.taskbar_contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.withdraw_dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading_pictureBox)).BeginInit();
            this.loading_panel.SuspendLayout();
            this.toolsMessage_groupBox.SuspendLayout();
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
            this.main_statusStrip.ShowItemToolTips = true;
            this.main_statusStrip.Size = new System.Drawing.Size(600, 22);
            this.main_statusStrip.SizingGrip = false;
            this.main_statusStrip.TabIndex = 0;
            this.main_statusStrip.Text = "main_statusStrip";
            // 
            // balance_StripStatus
            // 
            this.balance_StripStatus.Name = "balance_StripStatus";
            this.balance_StripStatus.Size = new System.Drawing.Size(119, 17);
            this.balance_StripStatus.Text = "Balance: 0.00₽ / 0.00₽";
            this.balance_StripStatus.ToolTipText = "Balance: $0.00 / $0.00";
            // 
            // space_StripStatus
            // 
            this.space_StripStatus.Name = "space_StripStatus";
            this.space_StripStatus.Size = new System.Drawing.Size(316, 17);
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
            this.progressBar_StripStatus.Maximum = 10;
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
            this.timer_StripStatus.MouseEnter += new System.EventHandler(this.timer_StripStatus_MouseEnter);
            this.timer_StripStatus.MouseLeave += new System.EventHandler(this.timer_StripStatus_MouseLeave);
            // 
            // main_menuStrip
            // 
            this.main_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_MainStripMenu,
            this.reload_MainStripMenu,
            this.withdraw_toolStripMenuItem,
            this.tools_MainStripMenu,
            this.aboutToolStripMenuItem});
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
            this.settings_MainStripMenu.Size = new System.Drawing.Size(116, 22);
            this.settings_MainStripMenu.Text = "Settings";
            this.settings_MainStripMenu.Click += new System.EventHandler(this.settings_MainStripMenu_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(113, 6);
            // 
            // restart_MainStripMenu
            // 
            this.restart_MainStripMenu.Name = "restart_MainStripMenu";
            this.restart_MainStripMenu.Size = new System.Drawing.Size(116, 22);
            this.restart_MainStripMenu.Text = "Restart";
            this.restart_MainStripMenu.Click += new System.EventHandler(this.restart_MainStripMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
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
            // withdraw_toolStripMenuItem
            // 
            this.withdraw_toolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.withdraw_toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWithdraw_toolStripMenuItem,
            this.reloadWithdraw_toolStripMenuItem,
            this.toolStripSeparator6,
            this.checkCsmWithdraw_toolStripMenuItem});
            this.withdraw_toolStripMenuItem.Image = global::ItemChecker.Properties.Resources.down;
            this.withdraw_toolStripMenuItem.Name = "withdraw_toolStripMenuItem";
            this.withdraw_toolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.withdraw_toolStripMenuItem.Text = "Withdraw";
            // 
            // showWithdraw_toolStripMenuItem
            // 
            this.showWithdraw_toolStripMenuItem.Name = "showWithdraw_toolStripMenuItem";
            this.showWithdraw_toolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.showWithdraw_toolStripMenuItem.Text = "Load";
            this.showWithdraw_toolStripMenuItem.Click += new System.EventHandler(this.showWithdraw_toolStripMenuItem_Click);
            // 
            // reloadWithdraw_toolStripMenuItem
            // 
            this.reloadWithdraw_toolStripMenuItem.Enabled = false;
            this.reloadWithdraw_toolStripMenuItem.Name = "reloadWithdraw_toolStripMenuItem";
            this.reloadWithdraw_toolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.reloadWithdraw_toolStripMenuItem.Text = "Reload";
            this.reloadWithdraw_toolStripMenuItem.Click += new System.EventHandler(this.reloadWithdraw_toolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // checkCsmWithdraw_toolStripMenuItem
            // 
            this.checkCsmWithdraw_toolStripMenuItem.Name = "checkCsmWithdraw_toolStripMenuItem";
            this.checkCsmWithdraw_toolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.checkCsmWithdraw_toolStripMenuItem.Text = "Inventory";
            this.checkCsmWithdraw_toolStripMenuItem.Click += new System.EventHandler(this.checkCsmWithdraw_toolStripMenuItem_Click);
            // 
            // tools_MainStripMenu
            // 
            this.tools_MainStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceParserToolStripMenuItem,
            this.tableExtractToolStripMenuItem,
            this.toolStripSeparator4,
            this.buyOrderPush_toolStripMenuItem,
            this.favoriteCheckToolStripMenuItem,
            this.floatCheck_MainStripMenu});
            this.tools_MainStripMenu.Name = "tools_MainStripMenu";
            this.tools_MainStripMenu.Size = new System.Drawing.Size(46, 20);
            this.tools_MainStripMenu.Text = "&Tools";
            // 
            // serviceParserToolStripMenuItem
            // 
            this.serviceParserToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("serviceParserToolStripMenuItem.Image")));
            this.serviceParserToolStripMenuItem.Name = "serviceParserToolStripMenuItem";
            this.serviceParserToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.serviceParserToolStripMenuItem.Text = "ServiceParser";
            this.serviceParserToolStripMenuItem.Click += new System.EventHandler(this.serviceParserToolStripMenuItem_Click);
            // 
            // tableExtractToolStripMenuItem
            // 
            this.tableExtractToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trySkinsTotxtToolStripMenuItem,
            this.buyOrdersTotxtToolStripMenuItem});
            this.tableExtractToolStripMenuItem.Image = global::ItemChecker.Properties.Resources.txt_file;
            this.tableExtractToolStripMenuItem.Name = "tableExtractToolStripMenuItem";
            this.tableExtractToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.tableExtractToolStripMenuItem.Text = "TableExtract";
            // 
            // trySkinsTotxtToolStripMenuItem
            // 
            this.trySkinsTotxtToolStripMenuItem.Name = "trySkinsTotxtToolStripMenuItem";
            this.trySkinsTotxtToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.trySkinsTotxtToolStripMenuItem.Text = "TrySkins to *.txt";
            this.trySkinsTotxtToolStripMenuItem.Click += new System.EventHandler(this.trySkinsTotxtToolStripMenuItem_Click);
            // 
            // buyOrdersTotxtToolStripMenuItem
            // 
            this.buyOrdersTotxtToolStripMenuItem.Name = "buyOrdersTotxtToolStripMenuItem";
            this.buyOrdersTotxtToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.buyOrdersTotxtToolStripMenuItem.Text = "BuyOrders to *.txt";
            this.buyOrdersTotxtToolStripMenuItem.Click += new System.EventHandler(this.buyOrdersTotxtToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(157, 6);
            // 
            // buyOrderPush_toolStripMenuItem
            // 
            this.buyOrderPush_toolStripMenuItem.Name = "buyOrderPush_toolStripMenuItem";
            this.buyOrderPush_toolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.buyOrderPush_toolStripMenuItem.Text = "BuyOrderPusher";
            this.buyOrderPush_toolStripMenuItem.Click += new System.EventHandler(this.buyOrderPush_toolStripMenuItem_Click);
            // 
            // favoriteCheckToolStripMenuItem
            // 
            this.favoriteCheckToolStripMenuItem.Name = "favoriteCheckToolStripMenuItem";
            this.favoriteCheckToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.favoriteCheckToolStripMenuItem.Text = "FavoriteCheck";
            this.favoriteCheckToolStripMenuItem.Click += new System.EventHandler(this.favoriteCheckToolStripMenuItem_Click);
            // 
            // floatCheck_MainStripMenu
            // 
            this.floatCheck_MainStripMenu.Name = "floatCheck_MainStripMenu";
            this.floatCheck_MainStripMenu.Size = new System.Drawing.Size(160, 22);
            this.floatCheck_MainStripMenu.Text = "FloatCheck";
            this.floatCheck_MainStripMenu.Click += new System.EventHandler(this.floatCheck_MainStripMenu_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
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
            this.steamItems_groupBox.Text = "BuyOrders:";
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
            // floatCheck_groupBox
            // 
            this.floatCheck_groupBox.Controls.Add(this.floatItems_label);
            this.floatCheck_groupBox.Controls.Add(this.floatPurchases_label);
            this.floatCheck_groupBox.Controls.Add(this.floatCheck_label);
            this.floatCheck_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.floatCheck_groupBox.Location = new System.Drawing.Point(328, 538);
            this.floatCheck_groupBox.Name = "floatCheck_groupBox";
            this.floatCheck_groupBox.Size = new System.Drawing.Size(259, 40);
            this.floatCheck_groupBox.TabIndex = 10;
            this.floatCheck_groupBox.TabStop = false;
            this.floatCheck_groupBox.Text = "FloatCheck:";
            this.floatCheck_groupBox.Visible = false;
            // 
            // floatItems_label
            // 
            this.floatItems_label.AutoSize = true;
            this.floatItems_label.Location = new System.Drawing.Point(6, 16);
            this.floatItems_label.Name = "floatItems_label";
            this.floatItems_label.Size = new System.Drawing.Size(41, 13);
            this.floatItems_label.TabIndex = 6;
            this.floatItems_label.Text = "Items: -";
            // 
            // floatPurchases_label
            // 
            this.floatPurchases_label.AutoSize = true;
            this.floatPurchases_label.Location = new System.Drawing.Point(134, 16);
            this.floatPurchases_label.Name = "floatPurchases_label";
            this.floatPurchases_label.Size = new System.Drawing.Size(95, 13);
            this.floatPurchases_label.TabIndex = 5;
            this.floatPurchases_label.Text = "Purchases made: -";
            // 
            // floatCheck_label
            // 
            this.floatCheck_label.AutoSize = true;
            this.floatCheck_label.Location = new System.Drawing.Point(68, 16);
            this.floatCheck_label.Name = "floatCheck_label";
            this.floatCheck_label.Size = new System.Drawing.Size(47, 13);
            this.floatCheck_label.TabIndex = 0;
            this.floatCheck_label.Text = "Check: -";
            // 
            // pusherBuyOrder_groupBox
            // 
            this.pusherBuyOrder_groupBox.Controls.Add(this.pusherItems_label);
            this.pusherBuyOrder_groupBox.Controls.Add(this.pusherCancel_label);
            this.pusherBuyOrder_groupBox.Controls.Add(this.pusherPush_label);
            this.pusherBuyOrder_groupBox.Controls.Add(this.pusherCheck_label);
            this.pusherBuyOrder_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pusherBuyOrder_groupBox.Location = new System.Drawing.Point(328, 539);
            this.pusherBuyOrder_groupBox.Name = "pusherBuyOrder_groupBox";
            this.pusherBuyOrder_groupBox.Size = new System.Drawing.Size(259, 40);
            this.pusherBuyOrder_groupBox.TabIndex = 4;
            this.pusherBuyOrder_groupBox.TabStop = false;
            this.pusherBuyOrder_groupBox.Text = "ItemPusher:";
            this.pusherBuyOrder_groupBox.Visible = false;
            // 
            // pusherItems_label
            // 
            this.pusherItems_label.AutoSize = true;
            this.pusherItems_label.Location = new System.Drawing.Point(6, 16);
            this.pusherItems_label.Name = "pusherItems_label";
            this.pusherItems_label.Size = new System.Drawing.Size(41, 13);
            this.pusherItems_label.TabIndex = 9;
            this.pusherItems_label.Text = "Items: -";
            // 
            // pusherCancel_label
            // 
            this.pusherCancel_label.AutoSize = true;
            this.pusherCancel_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pusherCancel_label.Location = new System.Drawing.Point(194, 16);
            this.pusherCancel_label.Name = "pusherCancel_label";
            this.pusherCancel_label.Size = new System.Drawing.Size(49, 13);
            this.pusherCancel_label.TabIndex = 8;
            this.pusherCancel_label.Text = "Cancel: -";
            // 
            // pusherPush_label
            // 
            this.pusherPush_label.AutoSize = true;
            this.pusherPush_label.Location = new System.Drawing.Point(134, 16);
            this.pusherPush_label.Name = "pusherPush_label";
            this.pusherPush_label.Size = new System.Drawing.Size(40, 13);
            this.pusherPush_label.TabIndex = 5;
            this.pusherPush_label.Text = "Push: -";
            // 
            // pusherCheck_label
            // 
            this.pusherCheck_label.AutoSize = true;
            this.pusherCheck_label.Location = new System.Drawing.Point(67, 16);
            this.pusherCheck_label.Name = "pusherCheck_label";
            this.pusherCheck_label.Size = new System.Drawing.Size(47, 13);
            this.pusherCheck_label.TabIndex = 0;
            this.pusherCheck_label.Text = "Check: -";
            // 
            // favoriteCheck_groupBox
            // 
            this.favoriteCheck_groupBox.Controls.Add(this.favoriteItems_label);
            this.favoriteCheck_groupBox.Controls.Add(this.favoriteTrades_label);
            this.favoriteCheck_groupBox.Controls.Add(this.favoriteCheck_label);
            this.favoriteCheck_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.favoriteCheck_groupBox.Location = new System.Drawing.Point(328, 539);
            this.favoriteCheck_groupBox.Name = "favoriteCheck_groupBox";
            this.favoriteCheck_groupBox.Size = new System.Drawing.Size(259, 40);
            this.favoriteCheck_groupBox.TabIndex = 9;
            this.favoriteCheck_groupBox.TabStop = false;
            this.favoriteCheck_groupBox.Text = "CheckFavorite:";
            this.favoriteCheck_groupBox.Visible = false;
            // 
            // favoriteItems_label
            // 
            this.favoriteItems_label.AutoSize = true;
            this.favoriteItems_label.Location = new System.Drawing.Point(6, 16);
            this.favoriteItems_label.Name = "favoriteItems_label";
            this.favoriteItems_label.Size = new System.Drawing.Size(41, 13);
            this.favoriteItems_label.TabIndex = 6;
            this.favoriteItems_label.Text = "Items: -";
            // 
            // favoriteTrades_label
            // 
            this.favoriteTrades_label.AutoSize = true;
            this.favoriteTrades_label.Location = new System.Drawing.Point(134, 16);
            this.favoriteTrades_label.Name = "favoriteTrades_label";
            this.favoriteTrades_label.Size = new System.Drawing.Size(104, 13);
            this.favoriteTrades_label.TabIndex = 5;
            this.favoriteTrades_label.Text = "Successful Trades: -";
            // 
            // favoriteCheck_label
            // 
            this.favoriteCheck_label.AutoSize = true;
            this.favoriteCheck_label.Location = new System.Drawing.Point(68, 16);
            this.favoriteCheck_label.Name = "favoriteCheck_label";
            this.favoriteCheck_label.Size = new System.Drawing.Size(47, 13);
            this.favoriteCheck_label.TabIndex = 0;
            this.favoriteCheck_label.Text = "Check: -";
            // 
            // tryskins_dataGridView
            // 
            this.tryskins_dataGridView.AllowUserToAddRows = false;
            this.tryskins_dataGridView.AllowUserToDeleteRows = false;
            this.tryskins_dataGridView.AllowUserToResizeRows = false;
            this.tryskins_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tryskins_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tryskins_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colorTS_Column,
            this.itemTS_Column,
            this.staTS_Column,
            this.csmTS_Column,
            this.precentTS_Column,
            this.differenceTS_Column});
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
            this.tryskins_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tryskins_dataGridView_ColumnHeaderMouseClick);
            this.tryskins_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tryskins_dataGridView_KeyDown);
            // 
            // colorTS_Column
            // 
            this.colorTS_Column.DataPropertyName = "colorTS_Column";
            this.colorTS_Column.HeaderText = "";
            this.colorTS_Column.Name = "colorTS_Column";
            this.colorTS_Column.ReadOnly = true;
            this.colorTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colorTS_Column.Width = 5;
            // 
            // itemTS_Column
            // 
            this.itemTS_Column.DataPropertyName = "itemTS_Column";
            this.itemTS_Column.HeaderText = "Item (TrySkins)";
            this.itemTS_Column.Name = "itemTS_Column";
            this.itemTS_Column.ReadOnly = true;
            this.itemTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.itemTS_Column.Width = 305;
            // 
            // staTS_Column
            // 
            this.staTS_Column.DataPropertyName = "staTS_Column";
            this.staTS_Column.HeaderText = "ST(A)";
            this.staTS_Column.Name = "staTS_Column";
            this.staTS_Column.ReadOnly = true;
            this.staTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.staTS_Column.Width = 66;
            // 
            // csmTS_Column
            // 
            this.csmTS_Column.DataPropertyName = "csmTS_Column";
            this.csmTS_Column.HeaderText = "CSM";
            this.csmTS_Column.Name = "csmTS_Column";
            this.csmTS_Column.ReadOnly = true;
            this.csmTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.csmTS_Column.Width = 60;
            // 
            // precentTS_Column
            // 
            this.precentTS_Column.DataPropertyName = "precentTS_Column";
            this.precentTS_Column.HeaderText = "Precent";
            this.precentTS_Column.Name = "precentTS_Column";
            this.precentTS_Column.ReadOnly = true;
            this.precentTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.precentTS_Column.Width = 55;
            // 
            // differenceTS_Column
            // 
            this.differenceTS_Column.DataPropertyName = "differenceTS_Column";
            this.differenceTS_Column.HeaderText = "Difference";
            this.differenceTS_Column.Name = "differenceTS_Column";
            this.differenceTS_Column.ReadOnly = true;
            this.differenceTS_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.differenceTS_Column.Width = 64;
            // 
            // buyOrder_dataGridView
            // 
            this.buyOrder_dataGridView.AllowUserToAddRows = false;
            this.buyOrder_dataGridView.AllowUserToDeleteRows = false;
            this.buyOrder_dataGridView.AllowUserToResizeRows = false;
            this.buyOrder_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.buyOrder_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buyOrder_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colorBO_Column,
            this.itemBO_Column,
            this.staBO_Column,
            this.csmBO_Column,
            this.precentBO_Column,
            this.differenceBO_Column});
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
            this.buyOrder_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.buyOrder_dataGridView_ColumnHeaderMouseClick);
            this.buyOrder_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buyOrder_dataGridView_KeyDown);
            // 
            // colorBO_Column
            // 
            this.colorBO_Column.DataPropertyName = "colorBO_Column";
            this.colorBO_Column.HeaderText = "";
            this.colorBO_Column.Name = "colorBO_Column";
            this.colorBO_Column.ReadOnly = true;
            this.colorBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colorBO_Column.Width = 5;
            // 
            // itemBO_Column
            // 
            this.itemBO_Column.DataPropertyName = "itemBO_Column";
            this.itemBO_Column.HeaderText = "Item (BuyOrders)";
            this.itemBO_Column.Name = "itemBO_Column";
            this.itemBO_Column.ReadOnly = true;
            this.itemBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.itemBO_Column.Width = 305;
            // 
            // staBO_Column
            // 
            this.staBO_Column.DataPropertyName = "staBO_Column";
            this.staBO_Column.HeaderText = "ST(A)";
            this.staBO_Column.Name = "staBO_Column";
            this.staBO_Column.ReadOnly = true;
            this.staBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.staBO_Column.Width = 66;
            // 
            // csmBO_Column
            // 
            this.csmBO_Column.DataPropertyName = "csmBO_Column";
            this.csmBO_Column.HeaderText = "CSM";
            this.csmBO_Column.Name = "csmBO_Column";
            this.csmBO_Column.ReadOnly = true;
            this.csmBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.csmBO_Column.Width = 60;
            // 
            // precentBO_Column
            // 
            this.precentBO_Column.DataPropertyName = "precentBO_Column";
            this.precentBO_Column.HeaderText = "Precent";
            this.precentBO_Column.Name = "precentBO_Column";
            this.precentBO_Column.ReadOnly = true;
            this.precentBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.precentBO_Column.Width = 55;
            // 
            // differenceBO_Column
            // 
            this.differenceBO_Column.DataPropertyName = "differenceBO_Column";
            this.differenceBO_Column.HeaderText = "Difference";
            this.differenceBO_Column.Name = "differenceBO_Column";
            this.differenceBO_Column.ReadOnly = true;
            this.differenceBO_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.differenceBO_Column.Width = 64;
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
            this.tree_serviceParserToolStripMenuItem,
            this.toolStripSeparator5,
            this.links_toolStripMenuItem,
            this.toolStripSeparator9,
            this.settings_toolStripMenuItem,
            this.exit_toolStripMenuItem});
            this.taskbar_contextMenuStrip.Name = "taskbar_contextMenuStrip";
            this.taskbar_contextMenuStrip.Size = new System.Drawing.Size(144, 104);
            // 
            // tree_serviceParserToolStripMenuItem
            // 
            this.tree_serviceParserToolStripMenuItem.Image = global::ItemChecker.Properties.Resources.serviceParser;
            this.tree_serviceParserToolStripMenuItem.Name = "tree_serviceParserToolStripMenuItem";
            this.tree_serviceParserToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.tree_serviceParserToolStripMenuItem.Text = "ServiceParser";
            this.tree_serviceParserToolStripMenuItem.Click += new System.EventHandler(this.tree_serviceParserToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
            // 
            // links_toolStripMenuItem
            // 
            this.links_toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tryskinsLink_toolStripMenuItem,
            this.toolStripSeparator10,
            this.transactionsToolStripMenuItem,
            this.csmoneyLink_toolStripMenuItem,
            this.toolStripSeparator11,
            this.inventoryLink_toolStripMenuItem,
            this.marketLink_toolStripMenuItem});
            this.links_toolStripMenuItem.Image = global::ItemChecker.Properties.Resources.link;
            this.links_toolStripMenuItem.Name = "links_toolStripMenuItem";
            this.links_toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.links_toolStripMenuItem.Text = "Links";
            // 
            // tryskinsLink_toolStripMenuItem
            // 
            this.tryskinsLink_toolStripMenuItem.Name = "tryskinsLink_toolStripMenuItem";
            this.tryskinsLink_toolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.tryskinsLink_toolStripMenuItem.Text = "TrySkins";
            this.tryskinsLink_toolStripMenuItem.Click += new System.EventHandler(this.tryskinsLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(141, 6);
            // 
            // transactionsToolStripMenuItem
            // 
            this.transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            this.transactionsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.transactionsToolStripMenuItem.Text = "Transactions";
            this.transactionsToolStripMenuItem.Click += new System.EventHandler(this.transactionsToolStripMenuItem_Click);
            // 
            // csmoneyLink_toolStripMenuItem
            // 
            this.csmoneyLink_toolStripMenuItem.Name = "csmoneyLink_toolStripMenuItem";
            this.csmoneyLink_toolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.csmoneyLink_toolStripMenuItem.Text = "Cs.Money";
            this.csmoneyLink_toolStripMenuItem.Click += new System.EventHandler(this.csmoneyLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(141, 6);
            // 
            // inventoryLink_toolStripMenuItem
            // 
            this.inventoryLink_toolStripMenuItem.Name = "inventoryLink_toolStripMenuItem";
            this.inventoryLink_toolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.inventoryLink_toolStripMenuItem.Text = "Inventory";
            this.inventoryLink_toolStripMenuItem.Click += new System.EventHandler(this.inventoryLink_toolStripMenuItem_Click);
            // 
            // marketLink_toolStripMenuItem
            // 
            this.marketLink_toolStripMenuItem.Name = "marketLink_toolStripMenuItem";
            this.marketLink_toolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.marketLink_toolStripMenuItem.Text = "SteamMarket";
            this.marketLink_toolStripMenuItem.Click += new System.EventHandler(this.marketLink_toolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(140, 6);
            // 
            // settings_toolStripMenuItem
            // 
            this.settings_toolStripMenuItem.Image = global::ItemChecker.Properties.Resources.setting;
            this.settings_toolStripMenuItem.Name = "settings_toolStripMenuItem";
            this.settings_toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.settings_toolStripMenuItem.Text = "Settings";
            this.settings_toolStripMenuItem.Click += new System.EventHandler(this.settings_toolStripMenuItem_Click);
            // 
            // exit_toolStripMenuItem
            // 
            this.exit_toolStripMenuItem.Name = "exit_toolStripMenuItem";
            this.exit_toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exit_toolStripMenuItem.Text = "Exit";
            this.exit_toolStripMenuItem.Click += new System.EventHandler(this.exit_toolStripMenuItem_Click);
            // 
            // withdraw_dataGridView
            // 
            this.withdraw_dataGridView.AllowUserToAddRows = false;
            this.withdraw_dataGridView.AllowUserToDeleteRows = false;
            this.withdraw_dataGridView.AllowUserToResizeRows = false;
            this.withdraw_dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.withdraw_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.withdraw_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colorWD_Column,
            this.itemWD_Column,
            this.csmWD_Column,
            this.staWD_Column,
            this.salesWD_Column,
            this.precentWD_Column});
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
            this.withdraw_dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.withdraw_dataGridView_ColumnHeaderMouseClick);
            // 
            // colorWD_Column
            // 
            this.colorWD_Column.DataPropertyName = "colorWD_Column";
            this.colorWD_Column.HeaderText = "";
            this.colorWD_Column.Name = "colorWD_Column";
            this.colorWD_Column.ReadOnly = true;
            this.colorWD_Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colorWD_Column.Width = 5;
            // 
            // itemWD_Column
            // 
            this.itemWD_Column.DataPropertyName = "itemWD_Column";
            this.itemWD_Column.HeaderText = "Item (Withdraw)";
            this.itemWD_Column.Name = "itemWD_Column";
            this.itemWD_Column.ReadOnly = true;
            this.itemWD_Column.Width = 305;
            // 
            // csmWD_Column
            // 
            this.csmWD_Column.DataPropertyName = "csmWD_Column";
            this.csmWD_Column.HeaderText = "CSM";
            this.csmWD_Column.Name = "csmWD_Column";
            this.csmWD_Column.ReadOnly = true;
            this.csmWD_Column.Width = 66;
            // 
            // staWD_Column
            // 
            this.staWD_Column.DataPropertyName = "staWD_Column";
            this.staWD_Column.HeaderText = "ST";
            this.staWD_Column.Name = "staWD_Column";
            this.staWD_Column.ReadOnly = true;
            this.staWD_Column.Width = 60;
            // 
            // salesWD_Column
            // 
            this.salesWD_Column.DataPropertyName = "salesWD_Column";
            this.salesWD_Column.HeaderText = "Sales";
            this.salesWD_Column.Name = "salesWD_Column";
            this.salesWD_Column.ReadOnly = true;
            this.salesWD_Column.Width = 55;
            // 
            // precentWD_Column
            // 
            this.precentWD_Column.DataPropertyName = "precentWD_Column";
            this.precentWD_Column.HeaderText = "Precent";
            this.precentWD_Column.Name = "precentWD_Column";
            this.precentWD_Column.ReadOnly = true;
            this.precentWD_Column.Width = 64;
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
            // ver_label
            // 
            this.ver_label.AutoSize = true;
            this.ver_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ver_label.Location = new System.Drawing.Point(507, 589);
            this.ver_label.Name = "ver_label";
            this.ver_label.Size = new System.Drawing.Size(81, 13);
            this.ver_label.TabIndex = 1;
            this.ver_label.Text = "Version: 0.0.0.0";
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
            // toolsMessage_groupBox
            // 
            this.toolsMessage_groupBox.Controls.Add(this.toolsMessage_label);
            this.toolsMessage_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolsMessage_groupBox.Location = new System.Drawing.Point(328, 539);
            this.toolsMessage_groupBox.Name = "toolsMessage_groupBox";
            this.toolsMessage_groupBox.Size = new System.Drawing.Size(259, 40);
            this.toolsMessage_groupBox.TabIndex = 11;
            this.toolsMessage_groupBox.TabStop = false;
            this.toolsMessage_groupBox.Text = "Tools:";
            // 
            // toolsMessage_label
            // 
            this.toolsMessage_label.AutoSize = true;
            this.toolsMessage_label.Location = new System.Drawing.Point(44, 16);
            this.toolsMessage_label.Name = "toolsMessage_label";
            this.toolsMessage_label.Size = new System.Drawing.Size(191, 13);
            this.toolsMessage_label.TabIndex = 6;
            this.toolsMessage_label.Text = "Instrument statistics are displayed here.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 611);
            this.Controls.Add(this.loading_panel);
            this.Controls.Add(this.floatCheck_groupBox);
            this.Controls.Add(this.pusherBuyOrder_groupBox);
            this.Controls.Add(this.favoriteCheck_groupBox);
            this.Controls.Add(this.withdraw_dataGridView);
            this.Controls.Add(this.tryskins_dataGridView);
            this.Controls.Add(this.buyOrder_dataGridView);
            this.Controls.Add(this.course_groupBox);
            this.Controls.Add(this.steamItems_groupBox);
            this.Controls.Add(this.queue_groupBox);
            this.Controls.Add(this.tradeOffer_groupBox);
            this.Controls.Add(this.availability_groupBox);
            this.Controls.Add(this.main_statusStrip);
            this.Controls.Add(this.main_menuStrip);
            this.Controls.Add(this.toolsMessage_groupBox);
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
            this.floatCheck_groupBox.ResumeLayout(false);
            this.floatCheck_groupBox.PerformLayout();
            this.pusherBuyOrder_groupBox.ResumeLayout(false);
            this.pusherBuyOrder_groupBox.PerformLayout();
            this.favoriteCheck_groupBox.ResumeLayout(false);
            this.favoriteCheck_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tryskins_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buyOrder_dataGridView)).EndInit();
            this.taskbar_contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.withdraw_dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading_pictureBox)).EndInit();
            this.loading_panel.ResumeLayout(false);
            this.loading_panel.PerformLayout();
            this.toolsMessage_groupBox.ResumeLayout(false);
            this.toolsMessage_groupBox.PerformLayout();
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
        public System.Windows.Forms.ToolStripMenuItem floatCheck_MainStripMenu;
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
        public System.Windows.Forms.GroupBox pusherBuyOrder_groupBox;
        public System.Windows.Forms.GroupBox availability_groupBox;
        public System.Windows.Forms.Label steamMarket_label;
        public System.Windows.Forms.Label tryskins_label;
        public System.Windows.Forms.Label unavailable_label;
        public System.Windows.Forms.Label overstock_label;
        public System.Windows.Forms.LinkLabel tradeOffers_linkLabel;
        public System.Windows.Forms.LinkLabel queue_linkLabel;
        public System.Windows.Forms.Label course_label;
        public System.Windows.Forms.Label available_label;
        public System.Windows.Forms.Label pusherPush_label;
        public System.Windows.Forms.Label pusherCheck_label;
        public System.Windows.Forms.DataGridView tryskins_dataGridView;
        public System.Windows.Forms.DataGridView buyOrder_dataGridView;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        public System.Windows.Forms.ContextMenuStrip taskbar_contextMenuStrip;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        public System.Windows.Forms.ToolStripMenuItem exit_toolStripMenuItem;
        private System.ComponentModel.IContainer components;
        public System.Windows.Forms.DataGridView withdraw_dataGridView;
        private System.Windows.Forms.ToolStripMenuItem updateData_toolStripMenuItem;
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
        public System.Windows.Forms.Label pusherCancel_label;
        private System.Windows.Forms.ToolStripMenuItem settings_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withdraw_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadWithdraw_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem checkCsmWithdraw_toolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem showWithdraw_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
        public System.Windows.Forms.GroupBox favoriteCheck_groupBox;
        public System.Windows.Forms.Label favoriteCheck_label;
        public System.Windows.Forms.Label favoriteItems_label;
        public System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tree_serviceParserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceParserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripMenuItem favoriteCheckToolStripMenuItem;
        public System.Windows.Forms.PictureBox loading_pictureBox;
        public System.Windows.Forms.Label ver_label;
        public System.Windows.Forms.Panel loading_panel;
        private System.Windows.Forms.ToolStripMenuItem tableExtractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trySkinsTotxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyOrdersTotxtToolStripMenuItem;
        public System.Windows.Forms.GroupBox floatCheck_groupBox;
        public System.Windows.Forms.Label floatItems_label;
        public System.Windows.Forms.Label floatPurchases_label;
        public System.Windows.Forms.Label floatCheck_label;
        public System.Windows.Forms.Label pusherItems_label;
        public System.Windows.Forms.ToolStripMenuItem buyOrderPush_toolStripMenuItem;
        private System.Windows.Forms.ToolTip info_toolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn staTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn csmTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn precentTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn differenceTS_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn staBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn csmBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn precentBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn differenceBO_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorWD_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemWD_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn csmWD_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn staWD_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn salesWD_Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn precentWD_Column;
        public System.Windows.Forms.Label favoriteTrades_label;
        public System.Windows.Forms.GroupBox toolsMessage_groupBox;
        public System.Windows.Forms.Label toolsMessage_label;
    }
}

