using System;
using System.IO;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Settings;

namespace ItemChecker
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            //general
            steamApiKey_textBox.Text = GeneralConfig.Default.steamApiKey.Trim();
            currApiKey_textBox.Text = GeneralConfig.Default.currencyApiKey.Trim();
            wait_numericUpDown.Value = GeneralConfig.Default.wait;
            profile_checkBox.Checked = GeneralConfig.Default.profile;
            proxy_checkBox.Checked = GeneralConfig.Default.proxy;
            //steam
            timer_numericUpDown.Value = SteamConfig.Default.timer;
            updST_checkBox.Checked = SteamConfig.Default.updateST;
            autoRemove_numericUpDown.Value = SteamConfig.Default.autoDelete;
            cancelOrder_checkBox.Checked = SteamConfig.Default.cancelOrder;
            startupPush_checkBox.Checked = SteamConfig.Default.startupPush;
            //tryskins
            maxPrecent_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrecent;
            minPrecent_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrecent;
            maxPrice_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrice;
            minPrice_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrice;
            oldcsm_checkBox.Checked = TryskinsConfig.Default.oldDesign;
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

            fast_radioButton.Checked = TryskinsConfig.Default.fastTime;
            long_radioButton.Checked = TryskinsConfig.Default.longTime;
            //float
            maxPrecentFloat_numericUpDown.Value = FloatConfig.Default.maxFloatPrecent;
            getItems_numericUpDown.Value = FloatConfig.Default.countGetItems;
            if (FloatConfig.Default.priceCompare == 0) lowest_radioButton.Checked = true;
            if (FloatConfig.Default.priceCompare == 1) median_radioButton.Checked = true;
            if (FloatConfig.Default.priceCompare == 2) csm_radioButton.Checked = true;

            FN_numericUpDown.Value = FloatConfig.Default.maxFloatValue_FN;
            MW_numericUpDown.Value = FloatConfig.Default.maxFloatValue_MW;
            FT_numericUpDown.Value = FloatConfig.Default.maxFloatValue_FT;
            WW_numericUpDown.Value = FloatConfig.Default.maxFloatValue_WW;
            BS_numericUpDown.Value = FloatConfig.Default.maxFloatValue_BS;

            version_label.Text = "Version: " + Main.version;
        }
        private void default_button_Click(object sender, EventArgs e)
        {
            //general
            steamApiKey_textBox.Text = " ";
            currApiKey_textBox.Text = "";
            wait_numericUpDown.Value = 15;
            profile_checkBox.Checked = true;
            proxy_checkBox.Checked = false;
            //steam
            timer_numericUpDown.Value = 10;
            updST_checkBox.Checked = true;
            autoRemove_numericUpDown.Value = 0;
            cancelOrder_checkBox.Checked = false;
            startupPush_checkBox.Checked = false;
            //tryskins
            fast_radioButton.Checked = true;
            long_radioButton.Checked = false;
            maxPrecent_numericUpDown.Value = 60;
            minPrecent_numericUpDown.Value = 27;
            maxPrice_numericUpDown.Value = 0;
            minPrice_numericUpDown.Value = 0;
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

            //float
            maxPrecentFloat_numericUpDown.Value = Convert.ToDecimal(7);
            getItems_numericUpDown.Value = 40;

            FN_numericUpDown.Value = Convert.ToDecimal(0.001);
            MW_numericUpDown.Value = Convert.ToDecimal(0.080);
            FT_numericUpDown.Value = Convert.ToDecimal(0.175);
            WW_numericUpDown.Value = Convert.ToDecimal(0.400);
            BS_numericUpDown.Value = Convert.ToDecimal(0.500);
            lowest_radioButton.Checked = true;
            median_radioButton.Checked = false;
            csm_radioButton.Checked = false;
        }
        private void ok_button_Click(object sender, EventArgs e)
        {
            if (!Main.loading)
            {
                if (!String.IsNullOrEmpty(currApiKey_textBox.Text))
                {
                    //general
                    GeneralConfig.Default.steamApiKey = steamApiKey_textBox.Text;
                    GeneralConfig.Default.currencyApiKey = currApiKey_textBox.Text;
                    GeneralConfig.Default.wait = Convert.ToInt32(wait_numericUpDown.Value);
                    GeneralConfig.Default.profile = profile_checkBox.Checked;
                    GeneralConfig.Default.proxy = proxy_checkBox.Checked;
                    //steam
                    SteamConfig.Default.timer = Convert.ToInt32(timer_numericUpDown.Value);
                    SteamConfig.Default.updateST = updST_checkBox.Checked;
                    SteamConfig.Default.autoDelete = Convert.ToInt32(autoRemove_numericUpDown.Value);
                    SteamConfig.Default.cancelOrder = cancelOrder_checkBox.Checked;
                    SteamConfig.Default.startupPush = startupPush_checkBox.Checked;
                    //tryskins
                    TryskinsConfig.Default.maxTryskinsPrecent = Convert.ToInt32(maxPrecent_numericUpDown.Value);
                    TryskinsConfig.Default.minTryskinsPrecent = Convert.ToInt32(minPrecent_numericUpDown.Value);
                    TryskinsConfig.Default.maxTryskinsPrice = Convert.ToInt32(maxPrice_numericUpDown.Value);
                    TryskinsConfig.Default.minTryskinsPrice = Convert.ToInt32(minPrice_numericUpDown.Value);
                    TryskinsConfig.Default.fastTime = fast_radioButton.Checked;
                    TryskinsConfig.Default.longTime = long_radioButton.Checked;
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
                    //float
                    FloatConfig.Default.maxFloatPrecent = maxPrecentFloat_numericUpDown.Value;
                    FloatConfig.Default.countGetItems = Convert.ToInt32(getItems_numericUpDown.Value);
                    if (lowest_radioButton.Checked == true) FloatConfig.Default.priceCompare = 0;
                    if (median_radioButton.Checked == true) FloatConfig.Default.priceCompare = 1;
                    if (csm_radioButton.Checked == true) FloatConfig.Default.priceCompare = 2;

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

        //extract
        private void extractST_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (BuyOrder.item != null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo("extract");
                if (!dirInfo.Exists) dirInfo.Create();
                string str = "";
                foreach (string i in BuyOrder.item)
                {
                    str += i + "\r\n";
                }
                File.WriteAllText($"extract/steamList_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.txt", str);
            }
        }
        private void extractTry_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (TrySkins.item != null)
            {
                DirectoryInfo dirInfo = new DirectoryInfo("extract");
                if (!dirInfo.Exists) dirInfo.Create();
                string str = "";
                foreach (string i in TrySkins.item)
                {
                    str += i + "\r\n";
                }
                File.WriteAllText($"extract/tryskinsList_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.txt", str);
            }
        }
        private void openFolder_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Support.Edit.openUrl(Application.StartupPath);
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

        //withdraw
        private void sticker_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!sticker_checkBox.Checked) onlySt_checkBox.Checked = false;
            onlySt_checkBox.Enabled = sticker_checkBox.Checked;
        }
        private void favoriteItems_button_Click(object sender, EventArgs e)
        {
            CheckListForm checkListForm = new("FavoriteList");
            checkListForm.ShowDialog();
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
    }
}