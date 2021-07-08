using System;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using static ItemChecker.Program;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Net;
using System.Data;
using OpenQA.Selenium.Support.UI;

namespace ItemChecker.Presenter
{
    public class TryskinsPresenter
    {
        public static void getItemsTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check Tryskins..."; }));
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

            List<IWebElement> items = new List<IWebElement>();
            TrySkins.url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=1&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=showsteama&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=" + min_sta + "&ItemsFilter%5BpriceTo1%5D=" + max_sta + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + TryskinsConfig.Default.minTryskinsPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + TryskinsConfig.Default.maxTryskinsPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=1&per-page=30";
            Main.Browser.Navigate().GoToUrl(TrySkins.url);
            int last;
            do
            {
                last = items.Count();
                IWebElement element = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//table[@class='table table-bordered']/tbody/tr[last()]")));
                Thread.Sleep(1000);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)Main.Browser;
                jse.ExecuteScript("arguments[0].scrollIntoView(true); window.scrollBy(0, -window.innerHeight / 4);", element);
                Thread.Sleep(1000);
                items = Main.Browser.FindElements(By.XPath("//table[@class='table table-bordered']/tbody/tr")).ToList();
            } while (items.Count > last);

            items = checkItem(items);

            if (items.Any())
            {
                if (TryskinsConfig.Default.fastTime)
                    checkItems(items);
                else if (TryskinsConfig.Default.longTime)
                {
                    if (!GeneralConfig.Default.proxy)
                        checkItemsMrinka(items);
                    else
                        checkItemsMrinkaProxy(items);
                }
            }
            MainPresenter.progressInvoke();
        }
        private static List<IWebElement> checkItem(List<IWebElement> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                string[] str = items[i].Text.Split("\n");
                string item_name = str[0].Trim();

                if (item_name.Contains("Или криво настроили фильтры") | item_name.Contains("Or poorly configured filters"))
                {
                    items.Clear();
                    mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_dataGridView.Rows.Add(null, "TrySkins returned empty list."); }));
                }
                else if (TrySkins.item.Contains(item_name) | BuyOrder.item.Contains(item_name))
                {
                    items.RemoveAt(i);
                }
                else if (Main.overstock.Contains(item_name) | Main.unavailable.Contains(item_name))
                {
                    TrySkins.t++;
                    items.RemoveAt(i);
                }
            }
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_label.Text = "TrySkins: " + TrySkins.t.ToString(); }));
            return items;
        }
        private static void checkItems(List<IWebElement> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                string[] str = items[i].Text.Split("\n");
                string item_name = str[0].Trim();
                string[] prices; double precent;

                if (str[1].Contains("$"))
                {
                    prices = str[1].Split(" ");
                    precent = Edit.removeSymbol(str[2].Trim());
                }
                else
                {
                    prices = str[4].Split(" ");
                    precent = Edit.removeSymbol(str[5].Trim());
                }
                double sta = Edit.removeDol(prices[0].Trim());
                double csm = Edit.removeDol(prices[1].Trim());

                TrySkins.item.Add(item_name);
                TrySkins.sta.Add(sta);
                TrySkins.csm.Add(csm);
                TrySkins.precent.Add(precent);
                TrySkins.difference.Add(Edit.difference(csm, sta, Main.course));
                mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - {TrySkins.item.Count}";
            }
        }
        private static void checkItemsMrinka(List<IWebElement> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                string[] str = items[i].Text.Split("\n");
                string item_name = str[0].Trim();

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

                    mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}";
                }
            }
        } 
        private static void checkItemsMrinkaProxy(List<IWebElement> items)
        {
            int id = 0;
            for (int i = 0; i < items.Count; i++)
            {
                string[] str = items[i].Text.Split("\n");
                string item_name = str[0].Trim();

                try
                {
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + Edit.replaceUrl(Main.checkList[i]);
                    string response = Request.GetRequest(url, Main.proxyList[id]);

                    var highest_buy_order = Convert.ToDouble(JObject.Parse(response)["steam"]["buyOrder"].ToString());
                    var csm_sell = Convert.ToDouble(JObject.Parse(response)["csm"]["sell"].ToString());
                    var precent = Math.Round(((csm_sell - highest_buy_order) / highest_buy_order) * 100, 2);

                    if (precent > 0)
                    {
                        TrySkins.item.Add(item_name);
                        TrySkins.sta.Add(highest_buy_order);
                        TrySkins.csm.Add(csm_sell);
                        TrySkins.precent.Add(precent);
                        TrySkins.difference.Add(Edit.difference(csm_sell, highest_buy_order, Main.course));

                        mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}";
                    }
                }
                catch
                {
                    i--;
                    if (Main.proxyList.Count > id)
                        id++;
                    else
                        id = 0;
                }
            }
        }
        public static void createDTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Write Tryskins...";}));

            MainPresenter.clearDTGView(mainForm.tryskins_dataGridView);
            DataTable table = new DataTable();
            for (int i = 0; i < mainForm.tryskins_dataGridView.ColumnCount; ++i)
            {
                table.Columns.Add(new DataColumn( mainForm.tryskins_dataGridView.Columns[i].Name ));
                mainForm.tryskins_dataGridView.Columns[i].DataPropertyName = mainForm.tryskins_dataGridView.Columns[i].Name;
            }
            table.Columns[4].DataType = typeof(Double);
            table.Columns[5].DataType = typeof(Double);
            for (int i = 0; i < TrySkins.item.Count; i++)
            {
                table.Rows.Add(null,
                        TrySkins.item[i],
                        TrySkins.sta[i] + "$",
                        TrySkins.csm[i] + "$",
                        TrySkins.precent[i],
                        TrySkins.difference[i]);
            }
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_dataGridView.DataSource = table; }));
            drawDTGView();
            MainPresenter.progressInvoke();
        }
        public static void drawDTGView()
        {
            foreach (DataGridViewRow row in mainForm.tryskins_dataGridView.Rows)
            {
                var item = row.Cells[1].Value.ToString();
                var sta = Edit.removeDol(row.Cells[2].Value.ToString());
                var precent = Edit.removeDol(row.Cells[4].Value.ToString());
                if (precent < 30)
                    row.Cells[4].Style.BackColor = Color.OrangeRed;
                if (precent >= 35)
                    row.Cells[4].Style.BackColor = Color.MediumSeaGreen;
                if (sta > Steam.balance_usd)
                    row.Cells[2].Style.BackColor = Color.Crimson;
                if (item.Contains("Sticker") | item.Contains("Graffiti"))
                    row.Cells[0].Style.BackColor = Color.DeepSkyBlue;
                if (item.Contains("Souvenir"))
                    row.Cells[0].Style.BackColor = Color.Yellow;
                if (item.Contains("StatTrak"))
                    row.Cells[0].Style.BackColor = Color.Orange;
                if (item.Contains("★"))
                    row.Cells[0].Style.BackColor = Color.DarkViolet;
            }
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
