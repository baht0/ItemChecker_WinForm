using System;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using static ItemChecker.Program;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Net;

namespace ItemChecker.Presenter
{
    public class TryskinsPresenter
    {
        static bool pages = true;
        public static void checkTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Check Tryskins...";
                mainForm.tryskins_dataGridView.Rows.Clear(); }));

            TrySkins._clear();
            double min_sta = 2;
            if (TryskinsConfig.Default.minTryskinsPrice == 0)
            {
                int j = 15;
                do
                {
                    j += 15;
                    min_sta += 5;
                }
                while (j < Steam.balance_usd);
                min_sta -= 2;
            }
            else min_sta = TryskinsConfig.Default.minTryskinsPrice;

            double max_sta = Steam.balance_usd;
            if (TryskinsConfig.Default.maxTryskinsPrice != 0) max_sta = TryskinsConfig.Default.maxTryskinsPrice;

            int page = 1;
            TrySkins.url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=1&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showsteama&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=" + min_sta + "&ItemsFilter%5BpriceTo1%5D=" + max_sta + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + TryskinsConfig.Default.minTryskinsPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + TryskinsConfig.Default.maxTryskinsPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=" + page + "&per-page=30";
            while (pages)
            {
                string url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=1&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showsteama&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=" + min_sta + "&ItemsFilter%5BpriceTo1%5D=" + max_sta + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + TryskinsConfig.Default.minTryskinsPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + TryskinsConfig.Default.maxTryskinsPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=" + page + "&per-page=30";
                Main.Browser.Navigate().GoToUrl(url);

                List<IWebElement> items = Main.Browser.FindElements(By.XPath("//table[@class='table table-bordered']/tbody/tr")).ToList();

                if (items.Count > 1)
                {
                    getTryskins(items);
                    if (items.Count < 30) break;
                    page++;
                }
                else if (items.Count == 1)
                {
                    try
                    {
                        getTryskins(items);
                        break;
                    }
                    catch
                    {
                        TrySkins._clear();
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.tryskins_dataGridView.Rows.Add();
                            mainForm.tryskins_dataGridView.Rows[0].Cells[1].Value = "TrySkins return empty list."; }));
                        break;
                    }
                }
            }
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_label.Text = "TrySkins: " + TrySkins.t.ToString(); }));
            MainPresenter.progressInvoke();
        }
        public static void getTryskins(List<IWebElement> items)
        {
            foreach (IWebElement item in items)
            {
                string[] str = item.Text.Split("\n");
                string item_name = str[0].Trim();

                if (TrySkins.item.Contains(item_name))
                {
                    pages = false;
                    break;
                }
                else if (Main.overstock.Contains(item_name) || Main.unavailable.Contains(item_name))
                {
                    TrySkins.t++;
                    continue;
                }
                else if (BuyOrder.item.Contains(item_name)) continue;

                //fast
                else if (TryskinsConfig.Default.fastTime)
                {
                    string[] prices = str[4].Split(" ");
                    double sta = Edit.removeDol(prices[0].Trim());
                    double csm = Edit.removeDol(prices[1].Trim());
                    double precent = Edit.removeSymbol(str[5].Trim());

                    TrySkins.item.Add(item_name);
                    TrySkins.sta.Add(sta);
                    TrySkins.csm.Add(csm);
                    TrySkins.precent.Add(precent);
                    TrySkins.difference.Add(Edit.difference(csm, sta, Main.course));
                    mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - {TrySkins.item.Count}";
                }
                //long
                else if (TryskinsConfig.Default.longTime)
                {
                    Tuple<String, Boolean> response = Tuple.Create("", false);
                    do
                    {
                        response = Request.MrinkaRequest(Edit.replaceUrl(item_name));
                        if (!response.Item2)
                        {
                            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check TrySkins (429). Please Wait..."; }));
                            Thread.Sleep(30000);
                        }
                    }
                    while (!response.Item2);

                    var highest_buy_order = Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["buyOrder"].ToString());
                    var csm_sell = Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["sell"].ToString());
                    var precent = Math.Round(((csm_sell - highest_buy_order) / highest_buy_order) * 100, 2);

                    if (precent > 0)
                    {
                        TrySkins.item.Add(item_name);
                        TrySkins.sta.Add(highest_buy_order);
                        TrySkins.csm.Add(csm_sell);
                        TrySkins.precent.Add(precent);
                        TrySkins.difference.Add(Edit.difference(csm_sell, highest_buy_order, Main.course));
                    }
                    mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}";
                }
            }
        }
        public static void createTryTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Write Tryskins..."; }));
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
                    if (TrySkins.precent[i] < 30) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.OrangeRed; }));
                    if (TrySkins.precent[i] >= 35) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.MediumSeaGreen; }));
                    if (TrySkins.sta[i] > Steam.balance_usd) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.Crimson; }));
                    if (TrySkins.item[i].Contains("Sticker") || TrySkins.item[i].Contains("Graffiti")) mainForm.Invoke(new Action(() => { mainForm.withdraw_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue; }));
                    if (TrySkins.item[i].Contains("Souvenir")) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                    if (TrySkins.item[i].Contains("StatTrak")) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                    if (TrySkins.item[i].Contains("★")) mainForm.Invoke(new Action(() => { mainForm.tryskins_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                }
            }
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.tryskins_dataGridView.Columns[4].ValueType = typeof(Double);
                mainForm.tryskins_dataGridView.Columns[5].ValueType = typeof(Double);
                mainForm.tryskins_dataGridView.Sort(mainForm.tryskins_dataGridView.Columns[5], ListSortDirection.Descending);
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
                string item = mainForm.tryskins_dataGridView.Rows[row].Cells[1].Value.ToString();
                double sta = Edit.removeSymbol(mainForm.tryskins_dataGridView.Rows[row].Cells[2].Value.ToString());

                if (!BuyOrder.item.Contains(item) & cell != 2 & sta <= Steam.balance_usd)
                {
                    if (!BuyOrder.queue.Contains(item))
                    {
                        BuyOrder.queue_rub += Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Add(item);
                        mainForm.Invoke(new MethodInvoker(delegate
                        {
                            if (BuyOrder.queue_rub > BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Red;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.LimeGreen; }));
                    }
                    else
                    {
                        BuyOrder.queue_rub -= Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Remove(item);
                        mainForm.Invoke(new MethodInvoker(delegate
                        {
                            if (BuyOrder.queue_rub < BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Black;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            mainForm.tryskins_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.White; }));
                    }
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
                Exceptions.errorLog(exp, Main.version);
            }
        }
    }
}
