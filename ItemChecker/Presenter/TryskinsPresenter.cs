using System;
using System.Threading;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Drawing;
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
using System.ComponentModel;

namespace ItemChecker.Presenter
{
    public class TryskinsPresenter
    {
        public static void Tryskins(bool dontUpload)
        {
            if (dontUpload)
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - Setting: 'Don't upload'."; }));
                return;
            }

            TrySkins._clear();
            loginTryskins();

            List<IWebElement> items = getItemsTryskins();

            items = checkItemsList(items);

            if (items.Any())
            {
                if (TryskinsConfig.Default.loadData == 0)
                    checkItems(items);
                else if (TryskinsConfig.Default.loadData == 1)
                {
                    if (!GeneralConfig.Default.proxy)
                        checkItemsMrinka(items);
                    else
                        checkItemsMrinkaProxy(items);
                }
            }

            if (TrySkins.item.Any())
                TryskinsPresenter.createDTable();
        }
        public static void loginTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Tryskins..."; }));

            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/site/items");
            if (Main.Browser.Url.Contains("items"))
            {
                MainPresenter.progressInvoke();
                return;
            }

            Main.Browser.Navigate().GoToUrl("https://table.altskins.com/login/steam");
            IWebElement account = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='OpenID_loggedInAccount']")));
            if (account.Text == SteamConfig.Default.login | account.Text == Steam.login)
            {
                IWebElement signins = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                signins.Click();
                Thread.Sleep(300);

                MainPresenter.progressInvoke();
            }
            else throw new InvalidOperationException("Login Tryskins");
        }
        private static List<IWebElement> getItemsTryskins()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check Tryskins..."; }));

            decimal min_sta = 2;
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
            decimal max_sta = Steam.balance_usd;
            if (TryskinsConfig.Default.maxTryskinsPrice != 0)
                max_sta = TryskinsConfig.Default.maxTryskinsPrice;
            string service = "steama";
            if (TryskinsConfig.Default.compareSt)
            {
                service = "steam";
                mainForm.tryskins_dataGridView.Columns[2].HeaderText = "ST";
            }

            List<IWebElement> items = new();
            TrySkins.url = "https://table.altskins.com/site/items?ItemsFilter%5Bknife%5D=0&ItemsFilter%5Bknife%5D=1&ItemsFilter%5Bstattrak%5D=0&ItemsFilter%5Bstattrak%5D=1&ItemsFilter%5Bsouvenir%5D=0&ItemsFilter%5Bsouvenir%5D=1&ItemsFilter%5Bsticker%5D=0&ItemsFilter%5Btype%5D=1&ItemsFilter%5Bservice1%5D=show" + service + "&ItemsFilter%5Bservice2%5D=showcsmoney&ItemsFilter%5Bunstable1%5D=1&ItemsFilter%5Bunstable2%5D=1&ItemsFilter%5Bhours1%5D=192&ItemsFilter%5Bhours2%5D=192&ItemsFilter%5BpriceFrom1%5D=" + min_sta + "&ItemsFilter%5BpriceTo1%5D=" + max_sta + "&ItemsFilter%5BpriceFrom2%5D=&ItemsFilter%5BpriceTo2%5D=&ItemsFilter%5BsalesBS%5D=&ItemsFilter%5BsalesTM%5D=&ItemsFilter%5BsalesST%5D=" + TryskinsConfig.Default.sales + "&ItemsFilter%5Bname%5D=&ItemsFilter%5Bservice1Minutes%5D=&ItemsFilter%5Bservice2Minutes%5D=&ItemsFilter%5BpercentFrom1%5D=" + TryskinsConfig.Default.minTryskinsPrecent + "&ItemsFilter%5BpercentFrom2%5D=&ItemsFilter%5Btimeout%5D=5&ItemsFilter%5Bservice1CountFrom%5D=1&ItemsFilter%5Bservice1CountTo%5D=&ItemsFilter%5Bservice2CountFrom%5D=1&ItemsFilter%5Bservice2CountTo%5D=&ItemsFilter%5BpercentTo1%5D=" + TryskinsConfig.Default.maxTryskinsPrecent + "&ItemsFilter%5BpercentTo2%5D=&page=1&per-page=30";
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

            MainPresenter.progressInvoke();
            return items;
        }
        private static List<IWebElement> checkItemsList(List<IWebElement> items)
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
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.tryskins_label.Text = $"TrySkins: {TrySkins.t}"; }));
            return items;
        }
        private static void checkItems(List<IWebElement> items)
        {
            foreach (IWebElement element in items)
            {
                string[] values = element.Text.Split("\n");
                string item_name = values[0].Trim();
                string[] prices;
                decimal precent = 0;
                decimal sta = 0;
                decimal csm = 0;

                for (int i = 1; i < values.Length; i++)
                {
                    if (!values[i].Contains("$"))
                        continue;

                    prices = values[i].Split(" ");
                    precent = Edit.removeSymbol(values[i + 1].Trim());
                    sta = Edit.removeDol(prices[0].Trim());
                    if (prices[1].Contains("$"))
                        csm = Edit.removeDol(prices[1].Trim());
                    else
                        csm = Edit.removeDol(prices[2].Trim());
                    break;
                }

                TrySkins.item.Add(item_name);
                TrySkins.sta.Add(sta);
                TrySkins.csm.Add(csm);
                TrySkins.precent.Add(precent);
                TrySkins.difference.Add(Edit.Difference(csm, sta, GeneralConfig.Default.currency));
                mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - {TrySkins.item.Count}/{items.Count}";
            }
            mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) - {TrySkins.item.Count}";
            MainPresenter.progressInvoke();
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
                    response = Get.MrinkaRequest(Edit.replaceUrl(item_name));
                    if (!response.Item2)
                    {
                        mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check TrySkins (429). Please Wait..."; }));
                        Thread.Sleep(30000);
                    }
                }
                while (!response.Item2);

                parseJson(response.Item1, item_name);
                mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}/{items.Count}";
            }
            mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}";
            MainPresenter.progressInvoke();
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
                    string response = Get.Request(url, Main.proxyList[id]);

                    parseJson(response, item_name);
                    mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}/{items.Count}";
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
            mainForm.tryskins_dataGridView.Columns[1].HeaderText = $"Item (TrySkins) [Accurate] - {TrySkins.item.Count}";
            MainPresenter.progressInvoke();
        }
        private static void parseJson(string response, string item_name)
        {
            decimal steam_price = Convert.ToDecimal(JObject.Parse(response)["steam"]["buyOrder"].ToString());
            if (TryskinsConfig.Default.compareSt)
                steam_price = Convert.ToDecimal(JObject.Parse(response)["steam"]["sellOrder"].ToString());
            decimal csm_sell = Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString());
            decimal precent = Edit.Precent(steam_price, csm_sell);

            if (precent > 0)
            {
                TrySkins.item.Add(item_name);
                TrySkins.sta.Add(steam_price);
                TrySkins.csm.Add(csm_sell);
                TrySkins.precent.Add(precent);
                TrySkins.difference.Add(Edit.Difference(csm_sell, steam_price, GeneralConfig.Default.currency));
            }
        }
        private static void createDTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Write Tryskins...";}));

            MainPresenter.clearDTableRows(mainForm.tryskins_dataGridView);
            DataTable table = new();

            foreach (DataGridViewColumn column in mainForm.tryskins_dataGridView.Columns)
                table.Columns.Add(new DataColumn(column.Name));

            table.Columns[4].DataType = typeof(decimal);
            table.Columns[5].DataType = typeof(decimal);
            for (int i = 0; i < TrySkins.item.Count; i++)
            {
                table.Rows.Add(null,
                        TrySkins.item[i],
                        TrySkins.sta[i] + "$",
                        TrySkins.csm[i] + "$",
                        TrySkins.precent[i],
                        TrySkins.difference[i]);
            }
            mainForm.Invoke(new MethodInvoker(delegate { 
                mainForm.tryskins_dataGridView.DataSource = table;
                mainForm.tryskins_dataGridView.Sort(mainForm.tryskins_dataGridView.Columns[5], ListSortDirection.Descending); }));
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
                if (BuyOrder.queue.Contains(item))
                {
                    row.Cells[1].Style.BackColor = Color.LimeGreen;
                    row.Cells[2].Style.BackColor = Color.LimeGreen;
                }
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
    }
}