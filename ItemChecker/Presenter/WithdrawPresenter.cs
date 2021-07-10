using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Threading;
using OpenQA.Selenium;
using ItemChecker.Settings;
using ItemChecker.Support;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ItemChecker.Presenter
{
    public static class WithdrawPresenter
    {
        public static void withdraw(object state)
        {
            try
            {                
                withdrawCheck();
                createDTable();
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.version);
                Exceptions.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
                MainPresenter.messageBalloonTip();
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.progressBar_StripStatus.Visible = false; }));
            }
        }

        public static void withdrawCheck()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check Withdraw...";}));

            Withdraw._clear();
            if (WithdrawConfig.Default.souvenir) Withdraw.souvenir = 1;
            if (WithdrawConfig.Default.sticker) Withdraw.sticker = 1;
            string maxPrice = WithdrawConfig.Default.maxPrice.ToString();
            if (WithdrawConfig.Default.maxPrice == 0) maxPrice = "";
            string onlySticker = "";
            if (WithdrawConfig.Default.onlySticker) onlySticker = "Sticker";
            string service = "steam";
            if (WithdrawConfig.Default.compareSta)
            {
                service = "steama";
                mainForm.withdraw_dataGridView.Columns[3].HeaderText = "STA";
            }
            int page = 1;
            Withdraw.url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=" + Withdraw.souvenir + "&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Bsticker%5D=" + Withdraw.sticker + "&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showcsmoney&ItemsFilter%5Bservice2%5D=show" + service + "&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=0&ItemsFilter%5Bhours2%5D=0&ItemsFilter%5BpriceFrom1%5D=" + WithdrawConfig.Default.minPrice + "&ItemsFilter%5BpriceTo1%5D=" + maxPrice + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=" + WithdrawConfig.Default.minSales + "&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + WithdrawConfig.Default.minPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + WithdrawConfig.Default.maxPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=" + page + "&per-page=30";
            while (true)
            {
                string url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=" + Withdraw.souvenir + "&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Bsticker%5D=" + Withdraw.sticker + "&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showcsmoney&ItemsFilter%5Bservice2%5D=show" + service + "&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=0&ItemsFilter%5Bhours2%5D=0&ItemsFilter%5BpriceFrom1%5D=" + WithdrawConfig.Default.minPrice + "&ItemsFilter%5BpriceTo1%5D=" + maxPrice + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=" + WithdrawConfig.Default.minSales + "&ItemsFilter%5Bname%5D=" + onlySticker + "&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + WithdrawConfig.Default.minPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + WithdrawConfig.Default.maxPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=" + page + "&per-page=30";
                Main.Browser.Navigate().GoToUrl(url);

                List<IWebElement> items = Main.Browser.FindElements(By.XPath("//table[@class='table table-bordered']/tbody/tr")).ToList();
                if (items.Count > 1)
                {
                    checkItems(items);
                    if (items.Count < 30) break;
                    page++;
                }
                else if (items.Count == 1)
                {
                    string[] str = items[0].Text.Split("\n");
                    string item_name = str[0].Trim();

                    if (item_name.Contains("Или криво настроили фильтры") | item_name.Contains("Or poorly configured filters"))
                    {
                        MainPresenter.clearDGV(mainForm.withdraw_dataGridView);
                        MainPresenter.messageBalloonTip("TrySkins returned empty list.", ToolTipIcon.Error);
                    }
                    else checkItems(items);
                    break;
                }
            }
        }
        private static void checkItems(List<IWebElement> items)
        {            
            foreach (IWebElement item in items)
            {
                try
                {
                    string[] str = item.Text.Split("\n");
                    string item_name = str[0].Trim();
                    int sales = Convert.ToInt32(str[2].Trim());

                    double precent = Edit.removeSymbol(str[5].Trim());
                    str[4] = str[4].Replace("★ ", null);
                    str[4] = str[4].Replace("🕐 ", null);
                    string[] prices = str[4].Split(" ");
                    double csm = Edit.removeDol(prices[0].Trim());
                    double st = Edit.removeDol(prices[2].Trim());

                    Withdraw.item.Add(item_name);
                    Withdraw.csm.Add(csm);
                    Withdraw.st.Add(st);
                    Withdraw.sales.Add(sales);
                    Withdraw.precent.Add(precent);
                    mainForm.withdraw_dataGridView.Columns[1].HeaderText = $"Item (Withdraw) - {Withdraw.item.Count}";
                }
                catch
                {
                    continue;
                }                
            }
            
        }
        public static void createDTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Table Withdraw..."; }));
            MainPresenter.clearDGV(mainForm.withdraw_dataGridView);

            DataTable table = new DataTable();
            for (int i = 0; i < mainForm.withdraw_dataGridView.ColumnCount; ++i)
            {
                table.Columns.Add(new DataColumn(mainForm.withdraw_dataGridView.Columns[i].Name));
                mainForm.withdraw_dataGridView.Columns[i].DataPropertyName = mainForm.withdraw_dataGridView.Columns[i].Name;
            }
            table.Columns[4].DataType = typeof(Double);
            table.Columns[5].DataType = typeof(Double);
            for (int i = 0; i < Withdraw.item.Count; i++)
            {
                table.Rows.Add(null,
                        Withdraw.item[i],
                        Withdraw.csm[i] + "$",
                        Withdraw.st[i] + "$",
                        Withdraw.sales[i],
                        Withdraw.precent[i]);
            }
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.withdraw_dataGridView.DataSource = table; }));
            drawDTGView();
        }
        public static void drawDTGView()
        {
            foreach (DataGridViewRow row in mainForm.withdraw_dataGridView.Rows)
            {
                var item = row.Cells[1].Value.ToString();
                var sales = Convert.ToDouble(row.Cells[4].Value.ToString());
                var precent = Edit.removeSymbol(row.Cells[5].Value.ToString());
                if (precent < 5)
                    row.Cells[5].Style.BackColor = Color.LightSalmon;
                if (precent > 10)
                    row.Cells[5].Style.BackColor = Color.PaleGreen;
                if (sales > 1000)
                    row.Cells[4].Style.BackColor = Color.MediumSeaGreen;
                if (item.Contains("Sticker") | item.Contains("Graffiti"))
                    row.Cells[0].Style.BackColor = Color.DeepSkyBlue;
                if (item.Contains("StatTrak"))
                    row.Cells[0].Style.BackColor = Color.Orange;
                if (item.Contains("★"))
                    row.Cells[0].Style.BackColor = Color.DarkViolet;
                row.Cells[2].Style.BackColor = Color.LightGray;
            }
        }
    }
}
