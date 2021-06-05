using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Settings;
using ItemChecker.Support;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace ItemChecker.Presenter
{
    public static class WithdrawPresenter
    {
        public static void withdraw(object state)
        {
            try
            {
                withdrawCheck();
                createWithdraw();
                Main.loading = false;
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorLog(exp, Main.version);
                Edit.errorMessage(exp, currMethodName);
            }
        }

        public static void withdrawCheck()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Check Withdraw...";
                mainForm.status_StripStatus.Visible = true;
                mainForm.withdraw_dataGridView.Rows.Clear();
            }));

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
                    getWithdrawTryskins(items.Count);
                    if (items.Count < 30) break;
                    page++;
                }
                else if (items.Count == 1)
                {
                    try
                    {
                        getWithdrawTryskins(items.Count);
                        break;
                    }
                    catch
                    {
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.withdraw_dataGridView.Rows.Add();
                            mainForm.withdraw_dataGridView.Rows[0].Cells[1].Value = "TrySkins return empty list.";
                        }));
                        break;
                    }
                }
            }            
        }
        private static void getWithdrawTryskins(int count)
        {
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            for (int i = 0; i < count; i++)
            {
                mainForm.withdraw_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - {Withdraw.item.Count}";
                IWebElement name_value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + (i + 1) + "]/td[1]")));
                IWebElement sales_value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + (i + 1) + "]/td[4]/a/div")));
                IWebElement csm_value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + (i + 1) + "]/td[7]/span[1]")));
                IWebElement st_value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + (i + 1) + "]/td[8]/span[1]")));
                IWebElement precent_value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + (i + 1) + "]/td[9]")));

                Withdraw.item.Add(name_value.Text);
                Withdraw.sales.Add(Convert.ToInt32(sales_value.Text));
                Withdraw.csm.Add(csm_value.Text);
                Withdraw.st.Add(st_value.Text);
                Withdraw.precent.Add(Edit.removeSymbol(precent_value.Text));
            }
        }
        public static void createWithdraw()
        {
            try
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Text = "Table Withdraw...";
                    mainForm.withdraw_dataGridView.Columns[4].ValueType = typeof(Int32);
                    mainForm.withdraw_dataGridView.Columns[5].ValueType = typeof(Double);
                }));
                for (int i = 0; i < Withdraw.item.Count(); i++)
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.withdraw_dataGridView.Rows.Add();
                        mainForm.withdraw_dataGridView.Rows[i].Cells[1].Value = Withdraw.item[i];
                        mainForm.withdraw_dataGridView.Rows[i].Cells[2].Value = Withdraw.csm[i] + "$";
                        mainForm.withdraw_dataGridView.Rows[i].Cells[3].Value = Withdraw.st[i] + "$";
                        mainForm.withdraw_dataGridView.Rows[i].Cells[4].Value = Withdraw.sales[i];
                        mainForm.withdraw_dataGridView.Rows[i].Cells[5].Value = Withdraw.precent[i];
                    }));

                    if (Withdraw.precent[i] < 5) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[5].Style.BackColor = Color.LightSalmon; }));
                    if (Withdraw.precent[i] > 10) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[5].Style.BackColor = Color.PaleGreen; }));
                    if (Withdraw.sales[i] > 1000) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.MediumSeaGreen; }));
                    if (Withdraw.item[i].Contains("Sticker") || Withdraw.item[i].Contains("Graffiti")) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue; }));
                    if (Withdraw.item[i].Contains("StatTrak")) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                    if (Withdraw.item[i].Contains("★")) mainForm.withdraw_dataGridView.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                    mainForm.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.LightGray; }));
                }
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
            }
            finally
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.withdraw_dataGridView.Sort(mainForm.withdraw_dataGridView.Columns[4], ListSortDirection.Descending);
                    mainForm.status_StripStatus.Visible = false;
                }));
            }
        }
    }
}
