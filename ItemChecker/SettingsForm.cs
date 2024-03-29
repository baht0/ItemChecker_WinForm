﻿using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Presenter;

namespace ItemChecker
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(int tabPage = 0)
        {
            InitializeComponent();

            tabControl.SelectedIndex = tabPage;
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //general
            currApiKey_textBox.Text = GeneralConfig.Default.currencyApiKey.Trim();
            wait_numericUpDown.Value = GeneralConfig.Default.wait;
            profile_checkBox.Checked = GeneralConfig.Default.profile;
            exitChrome_checkBox.Checked = GeneralConfig.Default.exitChrome;
            proxy_checkBox.Checked = GeneralConfig.Default.proxy;
            //steam
            steamApiKey_textBox.Text = SteamConfig.Default.steamApiKey.Trim();
            timer_numericUpDown.Value = SteamConfig.Default.timer;
            startupPush_checkBox.Checked = SteamConfig.Default.startupPush;
            updST_checkBox.Checked = SteamConfig.Default.updateST;
            cancelPrecent_numericUpDown.Value = SteamConfig.Default.cancelPrecent;
            cancelBalance_checkBox.Checked = SteamConfig.Default.cancelBalance;
            cancelOverstock_checkBox.Checked = SteamConfig.Default.cancelOverstock;
            //tryskins
            maxPrecent_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrecent;
            minPrecent_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrecent;
            maxPrice_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrice;
            minPrice_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrice;
            sales_numericUpDown.Value = TryskinsConfig.Default.sales;
            compareSt_checkBox.Checked = TryskinsConfig.Default.compareSt;
            dontUpload_checkBox.Checked = TryskinsConfig.Default.dontUpload;
            oldcsm_checkBox.Checked = TryskinsConfig.Default.oldDesign;
            loadData_comboBox.SelectedIndex = TryskinsConfig.Default.loadData;
            //withdraw
            minPrecentW_numericUpDown.Value = WithdrawConfig.Default.minPrecent;
            maxPrecentW_numericUpDown.Value = WithdrawConfig.Default.maxPrecent;
            maxPriceW_numericUpDown.Value = WithdrawConfig.Default.maxPrice;
            minPriceW_numericUpDown.Value = WithdrawConfig.Default.minPrice;
            minSalesW_numericUpDown.Value = WithdrawConfig.Default.minSales;
            souvenir_checkBox.Checked = WithdrawConfig.Default.souvenir;
            sticker_checkBox.Checked = WithdrawConfig.Default.sticker;
            onlySt_checkBox.Checked = WithdrawConfig.Default.onlySticker;
            compareSta_checkBox.Checked = WithdrawConfig.Default.compareSta;
            favoriteTimer_numericUpDown.Value = WithdrawConfig.Default.timer;
            deviation_numericUpDown.Value = WithdrawConfig.Default.deviation;
            //float
            maxPrecentFloat_numericUpDown.Value = FloatConfig.Default.maxFloatPrecent;
            getItems_numericUpDown.Value = FloatConfig.Default.countGetItems;
            floatTimer_numericUpDown.Value = FloatConfig.Default.timer;
            comparePrices_comboBox.SelectedIndex = FloatConfig.Default.priceCompare;

            FN_numericUpDown.Value = FloatConfig.Default.maxFloatValue_FN;
            MW_numericUpDown.Value = FloatConfig.Default.maxFloatValue_MW;
            FT_numericUpDown.Value = FloatConfig.Default.maxFloatValue_FT;
            WW_numericUpDown.Value = FloatConfig.Default.maxFloatValue_WW;
            BS_numericUpDown.Value = FloatConfig.Default.maxFloatValue_BS;
        }
        private void default_button_Click(object sender, EventArgs e)
        {
            //general
            steamApiKey_textBox.Text = " ";
            currApiKey_textBox.Text = "";
            wait_numericUpDown.Value = 15;
            profile_checkBox.Checked = true;
            exitChrome_checkBox.Checked = false;
            proxy_checkBox.Checked = false;
            //steam
            timer_numericUpDown.Value = 10;
            startupPush_checkBox.Checked = false;
            updST_checkBox.Checked = true;
            cancelPrecent_numericUpDown.Value = 10;
            cancelBalance_checkBox.Checked = false;
            cancelOverstock_checkBox.Checked = true;
            //tryskins
            maxPrecent_numericUpDown.Value = 60;
            minPrecent_numericUpDown.Value = 35;
            maxPrice_numericUpDown.Value = 0;
            minPrice_numericUpDown.Value = 0;
            sales_numericUpDown.Value = 0;
            compareSt_checkBox.Checked = false;
            loadData_comboBox.SelectedIndex = 0;
            dontUpload_checkBox.Checked = false;
            oldcsm_checkBox.Checked = true;
            //withdraw
            minPrecentW_numericUpDown.Value = 3;
            maxPrecentW_numericUpDown.Value = 60;
            maxPriceW_numericUpDown.Value = 0;
            minPriceW_numericUpDown.Value = 0;
            minSalesW_numericUpDown.Value = 500;
            souvenir_checkBox.Checked = false;
            sticker_checkBox.Checked = false;
            onlySt_checkBox.Checked = false;
            compareSta_checkBox.Checked = false;
            favoriteTimer_numericUpDown.Value = 15;
            deviation_numericUpDown.Value = 0.01m;
            //float
            maxPrecentFloat_numericUpDown.Value = Convert.ToDecimal(7);
            getItems_numericUpDown.Value = 40;
            floatTimer_numericUpDown.Value = 10;

            FN_numericUpDown.Value = Convert.ToDecimal(0.001);
            MW_numericUpDown.Value = Convert.ToDecimal(0.080);
            FT_numericUpDown.Value = Convert.ToDecimal(0.175);
            WW_numericUpDown.Value = Convert.ToDecimal(0.400);
            BS_numericUpDown.Value = Convert.ToDecimal(0.500);
            comparePrices_comboBox.SelectedIndex = 0;
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                if (!String.IsNullOrEmpty(currApiKey_textBox.Text))
                {
                    //general
                    GeneralConfig.Default.currencyApiKey = currApiKey_textBox.Text;
                    GeneralConfig.Default.wait = Convert.ToInt32(wait_numericUpDown.Value);
                    GeneralConfig.Default.profile = profile_checkBox.Checked;
                    GeneralConfig.Default.exitChrome = exitChrome_checkBox.Checked;
                    GeneralConfig.Default.proxy = proxy_checkBox.Checked;
                    //steam
                    SteamConfig.Default.steamApiKey = steamApiKey_textBox.Text;
                    SteamConfig.Default.timer = Convert.ToInt32(timer_numericUpDown.Value);
                    SteamConfig.Default.startupPush = startupPush_checkBox.Checked;
                    SteamConfig.Default.updateST = updST_checkBox.Checked;
                    SteamConfig.Default.cancelPrecent = Convert.ToInt32(cancelPrecent_numericUpDown.Value);
                    SteamConfig.Default.cancelBalance = cancelBalance_checkBox.Checked;
                    SteamConfig.Default.cancelOverstock = cancelOverstock_checkBox.Checked;
                    //tryskins
                    TryskinsConfig.Default.maxTryskinsPrecent = Convert.ToInt32(maxPrecent_numericUpDown.Value);
                    TryskinsConfig.Default.minTryskinsPrecent = Convert.ToInt32(minPrecent_numericUpDown.Value);
                    TryskinsConfig.Default.maxTryskinsPrice = Convert.ToInt32(maxPrice_numericUpDown.Value);
                    TryskinsConfig.Default.minTryskinsPrice = Convert.ToInt32(minPrice_numericUpDown.Value);
                    TryskinsConfig.Default.sales = Convert.ToInt32(sales_numericUpDown.Value);
                    TryskinsConfig.Default.compareSt = compareSt_checkBox.Checked;
                    TryskinsConfig.Default.dontUpload = dontUpload_checkBox.Checked;
                    TryskinsConfig.Default.loadData = loadData_comboBox.SelectedIndex;
                    TryskinsConfig.Default.oldDesign = oldcsm_checkBox.Checked;
                    //withdraw
                    WithdrawConfig.Default.minPrecent = Convert.ToInt32(minPrecentW_numericUpDown.Value);
                    WithdrawConfig.Default.maxPrecent = Convert.ToInt32(maxPrecentW_numericUpDown.Value);
                    WithdrawConfig.Default.maxPrice = Convert.ToInt32(maxPriceW_numericUpDown.Value);
                    WithdrawConfig.Default.minPrice = Convert.ToInt32(minPriceW_numericUpDown.Value);
                    WithdrawConfig.Default.minSales = Convert.ToInt32(minSalesW_numericUpDown.Value);
                    WithdrawConfig.Default.souvenir = souvenir_checkBox.Checked;
                    WithdrawConfig.Default.sticker = sticker_checkBox.Checked;
                    WithdrawConfig.Default.onlySticker = onlySt_checkBox.Checked;
                    WithdrawConfig.Default.compareSta = compareSta_checkBox.Checked;
                    WithdrawConfig.Default.timer = Convert.ToInt32(favoriteTimer_numericUpDown.Value);
                    WithdrawConfig.Default.deviation = deviation_numericUpDown.Value;
                    //float
                    FloatConfig.Default.maxFloatPrecent = maxPrecentFloat_numericUpDown.Value;
                    FloatConfig.Default.countGetItems = Convert.ToInt32(getItems_numericUpDown.Value);
                    FloatConfig.Default.timer = Convert.ToInt32(floatTimer_numericUpDown.Value);
                    FloatConfig.Default.priceCompare = comparePrices_comboBox.SelectedIndex;

                    FloatConfig.Default.maxFloatValue_FN = FN_numericUpDown.Value;
                    FloatConfig.Default.maxFloatValue_MW = MW_numericUpDown.Value;
                    FloatConfig.Default.maxFloatValue_FT = FT_numericUpDown.Value;
                    FloatConfig.Default.maxFloatValue_WW = WW_numericUpDown.Value;
                    FloatConfig.Default.maxFloatValue_BS = BS_numericUpDown.Value;

                    GeneralConfig.Default.Save();
                    SteamConfig.Default.Save();
                    TryskinsConfig.Default.Save();
                    WithdrawConfig.Default.Save();
                    FloatConfig.Default.Save();

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "The 'Currency Api Key' field must not be empty.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(
                        "Settings cannot be changed during loading.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        //api
        private void getST_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Support.Edit.openUrl("https://steamcommunity.com/dev/apikey#domain");
        }
        private void getCurr_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Support.Edit.openUrl("https://free.currencyconverterapi.com/free-api-key");
            Support.Edit.openUrl("https://openexchangerates.org/signup/free");
        }
        //proxy
        private void proxy_button_Click(object sender, EventArgs e)
        {
            CheckListForm checkList = new CheckListForm("ProxyList");
            checkList.ShowDialog();
        }
        private void proxy_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            proxy_button.Enabled = proxy_checkBox.Checked;
        }
        //withdraw
        private void sticker_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!sticker_checkBox.Checked) onlySt_checkBox.Checked = false;
            onlySt_checkBox.Enabled = sticker_checkBox.Checked;
        }
        private void favoriteList_button_Click(object sender, EventArgs e)
        {
            CheckListForm checkListForm = new("FavoriteList");
            checkListForm.ShowDialog();
        }
        //float
        private void floatList_button_Click(object sender, EventArgs e)
        {
            CheckListForm checkListForm = new("FloatList");
            checkListForm.ShowDialog();
        }

        //config file
        private void upload_button_Click(object sender, EventArgs e)
        {
            var configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath.ToString();
            configPath = configPath.Replace("Roaming", "Local");

            OpenFileDialog dialog = new()
            {
                InitialDirectory = Application.StartupPath,
                RestoreDirectory = true,
                Filter = "user.config | user.config"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                File.Copy(fileName, configPath, true);

                MessageBox.Show("Restart required. The program will be closed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                MainPresenter.Exit();
            }
        }
        private void download_button_Click(object sender, EventArgs e)
        {
            var configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath.ToString();
            configPath = configPath.Replace("Roaming", "Local");

            File.Copy(configPath, Application.StartupPath + "user.config", true);
            MainPresenter.messageBalloonTip("Configuration file downloaded successfully.", ToolTipIcon.Info);
        }
    }
}