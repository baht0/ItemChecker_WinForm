using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using static ItemChecker.Program;
using ItemChecker.General;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.NET;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace ItemChecker.Presenter
{
    public class TryskinsPresenter
    {
        public static void checkTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Check Tryskins...";
                mainForm.tryskins_dataGridView.Rows.Clear();
            }));

            TrySkins._clear();

            double min_sta = 2;
            int j = 15;
            do
            {
                j += 15;
                min_sta += 5;
            }
            while (j < Steam.balance_usd); min_sta -= 2;
            double max_sta = Steam.balance_usd;
            if (TryskinsConfig.Default.maxTryskinsPrice != 0) max_sta = TryskinsConfig.Default.maxTryskinsPrice;
            if (TryskinsConfig.Default.minTryskinsPrice != 0) min_sta = TryskinsConfig.Default.minTryskinsPrice;

            int page = 1;
            while (true)
            {
                TrySkins.url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showsteama&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=" + min_sta + "&ItemsFilter%5BpriceTo1%5D=" + max_sta + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + TryskinsConfig.Default.minTryskinsPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + TryskinsConfig.Default.maxTryskinsPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=" + page + "&per-page=30";
                Main.Browser.Navigate().GoToUrl(TrySkins.url);
                Thread.Sleep(500);

                List<IWebElement> items = Main.Browser.FindElements(By.XPath("//table[@class='table table-bordered']/tbody/tr")).ToList();
                TrySkins.count += items.Count;

                if (items.Count > 1)
                {
                    getTryskins(items.Count);
                    if (items.Count < 30) break;
                    page++;
                }
                else if (items.Count == 1)
                {
                    try
                    {
                        getTryskins(items.Count);
                        break;
                    }
                    catch
                    {
                        TrySkins.count = 0;
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.tryskins_dataGridView.Rows.Add();
                            mainForm.tryskins_dataGridView.Rows[0].Cells[1].Value = "TrySkins return empty list.";
                        }));
                        break;
                    }
                }
            }
            mainForm.tryskins_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.tryskins_label, "TrySkins: " + TrySkins.t.ToString())));
            MainPresenter.progressInvoke();
        }
        public static void getTryskins(int count)
        {
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            for (int i = 1; i <= count; i++)
            {
                IWebElement name = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[1]/span")));
                if (BuyOrder.item.Contains(name.Text)) continue;
                if (Main.overstock.Contains(name.Text) || Main.unavailable.Contains(name.Text))
                {
                    TrySkins.t++;
                    continue;
                }
                if (TrySkins.item.Contains(name.Text)) break;

                //fast
                if (TryskinsConfig.Default.fastTime)
                {
                    mainForm.tryskins_dataGridView.Columns[1].HeaderText = "Item (TrySkins) [FAST]";
                    IWebElement steama = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[7]/span")));
                    IWebElement csmoney = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[8]/span")));
                    IWebElement prec = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[" + i + "]/td[9]/div")));

                    TrySkins.item.Add(name.Text);
                    TrySkins.sta.Add(Convert.ToDouble(steama.Text));
                    TrySkins.csm.Add(Convert.ToDouble(csmoney.Text));
                    TrySkins.precent.Add(Edit.removeSymbol(prec.Text));
                    TrySkins.difference.Add(Edit.difference(Convert.ToDouble(csmoney.Text), Convert.ToDouble(steama.Text), Main.course));
                }
                //long
                if (TryskinsConfig.Default.longTime)
                {
                    try
                    {
                        mainForm.tryskins_dataGridView.Columns[1].HeaderText = "Item (TrySkins) [LONG]";
                        var json = Request.mrinkaRequest(Edit.replaceUrl(name.Text));
                        var buy_order = Convert.ToDouble(JObject.Parse(json)["steam"]["buyOrder"].ToString());
                        var csm_sell = Convert.ToDouble(JObject.Parse(json)["csm"]["sell"].ToString());
                        var prec = Math.Round(((csm_sell - buy_order) / buy_order) * 100, 2);

                        if (prec > 0)
                        {
                            TrySkins.item.Add(name.Text);
                            TrySkins.sta.Add(buy_order);
                            TrySkins.csm.Add(csm_sell);
                            TrySkins.precent.Add(prec);
                            TrySkins.difference.Add(Edit.difference(csm_sell, buy_order, Main.course));
                        }
                    }
                    catch
                    {
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.status_StripStatus.Text = "Check TrySkins (429). Wait 2 min...";
                            mainForm.timer_StripStatus.Text = "Updating (429). Wait 2 min...";
                        }));
                        i--;
                        Thread.Sleep(30000);
                        continue;
                    }
                }
            }
        }
        public static void createTryTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Table Tryskins..."; }));
            for (int i = 0; i < TrySkins.item.Count; i++)
            {
                if (!Main.overstock.Contains(TrySkins.item[i]) & !Main.unavailable.Contains(TrySkins.item[i]) & !BuyOrder.item.Contains(TrySkins.item[i]))
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.tryskins_dataGridView.Rows.Add();
                        mainForm.tryskins_dataGridView.Rows[i].Cells[1].Value = TrySkins.item[i];
                        mainForm.tryskins_dataGridView.Rows[i].Cells[2].Value = TrySkins.sta[i] + "$";
                        mainForm.tryskins_dataGridView.Rows[i].Cells[3].Value = TrySkins.csm[i] + "$";
                        mainForm.tryskins_dataGridView.Rows[i].Cells[4].Value = TrySkins.precent[i];
                        mainForm.tryskins_dataGridView.Rows[i].Cells[5].Value = TrySkins.difference[i];
                    }));
                    //color
                    if (TrySkins.precent[i] < 30) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.OrangeRed; }));
                    if (TrySkins.precent[i] >= 35) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.MediumSeaGreen; }));
                    if (TrySkins.precent[i] >= 40) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Green; }));
                    if (TrySkins.item[i].Contains("Souvenir")) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                    if (TrySkins.item[i].Contains("StatTrak")) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                    if (TrySkins.item[i].Contains("★")) mainForm.tryskins_dataGridView.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                }
            }
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.tryskins_dataGridView.Columns[4].ValueType = typeof(Double);
                mainForm.tryskins_dataGridView.Columns[5].ValueType = typeof(Double);
                mainForm.tryskins_dataGridView.Sort(mainForm.tryskins_dataGridView.Columns[4], ListSortDirection.Descending);
            }));
            MainPresenter.progressInvoke();
        }

        //order
        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = mainForm.tryskins_dataGridView.CurrentCell.RowIndex;
                int cell = mainForm.tryskins_dataGridView.CurrentCell.ColumnIndex;
                string iname = mainForm.tryskins_dataGridView.Rows[row].Cells[1].Value.ToString();
                double buyord = Convert.ToDouble(Edit.removeSymbol(mainForm.tryskins_dataGridView.Rows[row].Cells[2].Value.ToString()));

                if (!BuyOrder.ordered.Contains(iname) & cell != 2)
                {
                    string url = Edit.replaceUrl(iname);

                    if (!BuyOrder.queue.Contains(url))
                    {
                        BuyOrder.order_dol += buyord;
                        BuyOrder.order_rub += Math.Round(buyord * Main.course, 2);
                        BuyOrder.queue.Add(url);
                        BuyOrder.queue_count++;
                        mainForm.Invoke(new MethodInvoker(delegate
                        {
                            if (BuyOrder.order_rub > BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Red;
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue_count;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.LimeGreen;
                        }));
                    }
                    else
                    {
                        BuyOrder.order_dol -= buyord;
                        BuyOrder.order_rub -= Math.Round(buyord * Main.course, 2);
                        BuyOrder.queue.Remove(url);
                        BuyOrder.queue_count--;
                        mainForm.Invoke(new MethodInvoker(delegate
                        {
                            if (BuyOrder.order_rub < BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Black;
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue_count;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.White;
                        }));
                    }
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
                Edit.errorLog(exp, Main.version);
            }
        }
    }
}
