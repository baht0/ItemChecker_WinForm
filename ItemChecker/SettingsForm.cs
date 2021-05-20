using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.General;

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
            //steam
            timer_numericUpDown.Value = SteamConfig.Default.timer;
            updST_checkBox.Checked = SteamConfig.Default.updateST;
            autoRemove_numericUpDown.Value = SteamConfig.Default.autoDelete;
            //tryskins
            maxPrecent_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrecent;
            minPrecent_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrecent;
            maxPrice_numericUpDown.Value = TryskinsConfig.Default.maxTryskinsPrice;
            minPrice_numericUpDown.Value = TryskinsConfig.Default.minTryskinsPrice;
            minPrecentW_numericUpDown.Value = TryskinsConfig.Default.minPrecentW;
            maxPrecentW_numericUpDown.Value = TryskinsConfig.Default.maxPrecentW;
            minSalesW_numericUpDown.Value = TryskinsConfig.Default.minSalesW;
            souvenir_checkBox.Checked = TryskinsConfig.Default.souvenirW;
            sticker_checkBox.Checked = TryskinsConfig.Default.stickerW;

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
            //steam
            timer_numericUpDown.Value = 10;
            updST_checkBox.Checked = true;
            autoRemove_numericUpDown.Value = 0;
            //tryskins
            maxPrecent_numericUpDown.Value = 60;
            minPrecent_numericUpDown.Value = 27;
            maxPrice_numericUpDown.Value = 0;
            minPrice_numericUpDown.Value = 0;

            minPrecentW_numericUpDown.Value = 3;
            maxPrecentW_numericUpDown.Value = 60;
            minSalesW_numericUpDown.Value = 500;
            souvenir_checkBox.Checked = false;
            sticker_checkBox.Checked = false;

            fast_radioButton.Checked = true;
            long_radioButton.Checked = false;
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
            if (currApiKey_textBox.Text != "" & steamApiKey_textBox.Text != "")
            {
                //general
                GeneralConfig.Default.steamApiKey = steamApiKey_textBox.Text;
                GeneralConfig.Default.currencyApiKey = currApiKey_textBox.Text;
                GeneralConfig.Default.wait = Convert.ToInt32(wait_numericUpDown.Value);
                //steam
                SteamConfig.Default.timer = Convert.ToInt32(timer_numericUpDown.Value);
                SteamConfig.Default.updateST = updST_checkBox.Checked;
                SteamConfig.Default.autoDelete = Convert.ToInt32(autoRemove_numericUpDown.Value);
                //tryskins
                TryskinsConfig.Default.maxTryskinsPrecent = Convert.ToInt32(maxPrecent_numericUpDown.Value);
                TryskinsConfig.Default.minTryskinsPrecent = Convert.ToInt32(minPrecent_numericUpDown.Value);
                TryskinsConfig.Default.maxTryskinsPrice = Convert.ToInt32(maxPrice_numericUpDown.Value);
                TryskinsConfig.Default.minTryskinsPrice = Convert.ToInt32(minPrice_numericUpDown.Value);
                TryskinsConfig.Default.minPrecentW = Convert.ToInt32(minPrecentW_numericUpDown.Value);
                TryskinsConfig.Default.maxPrecentW = Convert.ToInt32(maxPrecentW_numericUpDown.Value);
                TryskinsConfig.Default.minSalesW = Convert.ToInt32(minSalesW_numericUpDown.Value);
                TryskinsConfig.Default.souvenirW = souvenir_checkBox.Checked;
                TryskinsConfig.Default.stickerW = sticker_checkBox.Checked;
                TryskinsConfig.Default.fastTime = fast_radioButton.Checked;
                TryskinsConfig.Default.longTime = long_radioButton.Checked;
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
                FloatConfig.Default.Save();

                MessageBox.Show(
                    "Some changes may require restarting the program.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            else
            {
                MessageBox.Show(
                    "Not all fields are filled.",
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
                File.WriteAllText("extract/steamList_" + DateTime.Now.ToString("yyyy.MM.dd_hh.mm") + ".txt", str);
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
                File.WriteAllText("extract/tryskinsList_" + DateTime.Now.ToString("yyyy.MM.dd_hh.mm") + ".txt", str);
            }
        }
        private void openFolder_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Console.WriteLine(Application.StartupPath);
            Process.Start(new ProcessStartInfo("cmd", $"/c start {Application.StartupPath}"));
        }

        //api
        private void getST_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {"https://steamcommunity.com/dev/apikey#domain"}"));
        }
        private void getCurr_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {"https://free.currencyconverterapi.com/free-api-key"}"));
        }
    }
}
