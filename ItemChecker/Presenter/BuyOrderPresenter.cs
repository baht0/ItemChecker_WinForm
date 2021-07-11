using System;
using System.Timers;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json.Linq;
using static ItemChecker.Program;
using ItemChecker.Support;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.Net;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

namespace ItemChecker.Presenter
{
    public class BuyOrderPresenter
    {
        public static void getSteamlist()
        {
            BuyOrder._clear();
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check Steam..."; }));

            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");

            int table_index = 1;
            IWebElement table = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table']/h3/span[1]")));
            if (table.Text == "My listings awaiting confirmation") table_index = 2;

            List<IWebElement> items = Main.Browser.FindElements(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table'][" + table_index + "]/div[@class='market_listing_row market_recent_listing_row']")).ToList();
            int i = 2;
            foreach (IWebElement item in items)
            {
                string[] str = item.Text.Split("\n");
                IWebElement id = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table'][" + table_index + "]/div[" + i + "]")));

                BuyOrder.item.Add(str[2].Trim());
                BuyOrder.url.Add(Edit.replaceUrl(str[2].Trim()));
                BuyOrder.price.Add(Edit.removeRub(str[0].Trim()));
                BuyOrder.id.Add(Edit.buyOrderId(id.GetAttribute("id"))); i++;
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}"; }));
            }
            availableAmount();
            MainPresenter.progressInvoke();
        }
        public static void availableAmount()
        {
            BuyOrder.sum = 0;
            BuyOrder.available_amount = 0;
            foreach (double item_price in BuyOrder.price) BuyOrder.sum += item_price;
            BuyOrder.available_amount = Math.Round(Steam.balance * 10 - BuyOrder.sum, 2);
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.available_label.Text = "Available: " + BuyOrder.available_amount.ToString() + "₽"; }));
            if (BuyOrder.available_amount < 1000)
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.available_label.ForeColor = Color.OrangeRed; }));
                MessageBox.Show(
                    "Little available amount.\nCancellation of orders is possible.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else mainForm.Invoke(new MethodInvoker(delegate { mainForm.available_label.ForeColor = Color.Black; }));
        }
        public static void checkOrders()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Calculate Steam..."; }));

            for (int i = 0; i < BuyOrder.item.Count; i++)
            {
                Tuple<String, Boolean> response = Tuple.Create("", false);
                do
                {
                    response = Request.MrinkaRequest(BuyOrder.url[i]);
                    if (!response.Item2)
                    {
                        mainForm.Invoke(new MethodInvoker(delegate
                        {
                            mainForm.status_StripStatus.Text = "Calculate Steam (429). Please Wait...";
                            mainForm.timer_StripStatus.Text = "Updating (429). Please Wait...";
                        }));
                        Thread.Sleep(30000);
                    }
                }
                while (!response.Item2);

                parseOrder(response.Item1, i);
            }
            MainPresenter.progressInvoke();
        }
        public static void checkOrdersProxy()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Calculate Steam..."; }));
            int id = 0;

            for (int i = 0; i < BuyOrder.item.Count; i++)
            {
                try
                {
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + BuyOrder.url[i];
                    var response = Request.GetRequest(url, Main.proxyList[id]);

                    parseOrder(response, i);
                }
                catch
                {
                    i--;
                    if (Main.proxyList.Count > id)
                        id++;
                    else break;
                }
            }
            MainPresenter.progressInvoke();
        }
        private static void parseOrder(string response, int i)
        {
            double my_order = Convert.ToDouble(BuyOrder.price[i]);
            var buy_order = Math.Round(my_order / Main.course, 2);
            var csm_sell = Convert.ToDouble(JObject.Parse(response)["csm"]["sell"].ToString());
            var precent = Edit.Precent(buy_order, csm_sell);
            var different = Edit.Difference(csm_sell * Main.course, my_order);

            BuyOrder.csm_price.Add(csm_sell);
            BuyOrder.precent.Add(precent);
            BuyOrder.difference.Add(different);
        }
        public static void createDTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Write Steam..."; }));
            MainPresenter.clearDGVRows(mainForm.buyOrder_dataGridView);

            DataTable table = new DataTable();
            for (int i = 0; i < mainForm.buyOrder_dataGridView.ColumnCount; ++i)
            {
                table.Columns.Add(new DataColumn(mainForm.buyOrder_dataGridView.Columns[i].Name));
                mainForm.buyOrder_dataGridView.Columns[i].DataPropertyName = mainForm.buyOrder_dataGridView.Columns[i].Name;
            }
            table.Columns[4].DataType = typeof(Double);
            table.Columns[5].DataType = typeof(Double);
            for (int i = 0; i < BuyOrder.item.Count; i++)
            {
                table.Rows.Add(null,
                        BuyOrder.item[i],
                        BuyOrder.price[i] + "₽",
                        BuyOrder.csm_price[i] + "$",
                        BuyOrder.precent[i],
                        BuyOrder.difference[i] );

                mainForm.Invoke(new MethodInvoker(delegate { mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}"; }));
            }
            mainForm.Invoke(new MethodInvoker(delegate { 
                mainForm.buyOrder_dataGridView.DataSource = table;
                mainForm.buyOrder_dataGridView.Sort(mainForm.buyOrder_dataGridView.Columns[5], ListSortDirection.Ascending); }));
            drawDTGView();
        }
        public static void drawDTGView()
        {
            foreach (DataGridViewRow row in mainForm.buyOrder_dataGridView.Rows)
            {
                var item = row.Cells[1].Value.ToString();
                var price = Edit.removeSymbol(row.Cells[2].Value.ToString());
                var precent = Edit.removeDol(row.Cells[4].Value.ToString());
                if (precent < 25)
                    row.Cells[4].Style.BackColor = Color.OrangeRed;
                if (precent > 35)
                    row.Cells[4].Style.BackColor = Color.MediumSeaGreen;
                if (price > Steam.balance)
                    row.Cells[2].Style.BackColor = Color.Crimson;
                if (item.Contains("Sticker") | item.Contains("Graffiti"))
                    row.Cells[0].Style.BackColor = Color.DeepSkyBlue;
                if (item.Contains("Souvenir"))
                    row.Cells[0].Style.BackColor = Color.Yellow;
                if (item.Contains("StatTrak"))
                    row.Cells[0].Style.BackColor = Color.Orange;
                if (item.Contains("★"))
                    row.Cells[0].Style.BackColor = Color.DarkViolet;
                if (precent <= SteamConfig.Default.autoDelete & precent != -100)
                {
                    int id = BuyOrder.item.IndexOf(item);
                    Main.Browser.ExecuteJavaScript(Request.CancelBuyOrder(BuyOrder.id[id], Main.sessionid));
                    BuyOrder.removeAtItem(id);
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[2].Value = "Cancel";
                    mainForm.Invoke(new Action(() => {
                        mainForm.cancel_label.Text = "Cancel: " + BuyOrder.int_cancel++.ToString();
                        mainForm.cancel_label.ForeColor = Color.OrangeRed; }));
                }
            }
        }

        //place order
        public static void placeOrder(object state)
        {
            try
            {
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Text = "Place Order...";
                    mainForm.status_StripStatus.Visible = true;
                    mainForm.progressBar_StripStatus.Maximum = BuyOrder.queue.Count;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Visible = true; }));
                createOrder();
                MainPresenter.messageBalloonTip("The creation of orders has been completed.");
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                Main.loading = false;
                Thread.Sleep(1000);
                BuyOrder._clearQueue();
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.queue_label.Text = "Queue: -";
                    mainForm.queue_linkLabel.Text = "Place order: -";
                    mainForm.buyOrdersReload_MainStripMenu.PerformClick(); }));
            }
        }
        private static void createOrder()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            for (int i = 0; i < BuyOrder.queue.Count; i++)
            {
                try
                {
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/" + Edit.replaceUrl(BuyOrder.queue[i]));
                    Thread.Sleep(500);
                    var ItemNameId = Request.ItemNameId(Edit.replaceUrl(BuyOrder.queue[i]));
                    var highest_buy_order = Request.ItemOrdersHistogram(ItemNameId);

                    if (Steam.balance > highest_buy_order) 
                        Main.Browser.ExecuteJavaScript(Request.CreateBuyOrder(BuyOrder.queue[i], highest_buy_order, Main.sessionid));
                }
                catch (Exception exp)
                {
                    Exceptions.errorLog(exp, Main.version);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }
        }
        //delete order
        public static void CancelOrder(object state)
        {
            try
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");
                int row = Convert.ToInt32(mainForm.buyOrder_dataGridView.CurrentCell.RowIndex.ToString());
                string item = mainForm.buyOrder_dataGridView.CurrentCell.Value.ToString();

                int index = BuyOrder.item.IndexOf(item);
                Main.Browser.ExecuteJavaScript(Request.CancelBuyOrder(BuyOrder.id[index], Main.sessionid));

                BuyOrder.removeAtItem(index);
                availableAmount();

                mainForm.Invoke(new Action(() => {
                    mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}";
                    mainForm.buyOrder_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.Red;
                    mainForm.buyOrder_dataGridView.Rows[row].Cells[2].Value = "Cancel"; }));
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
            }
        }

        //push...
        public static void pushStart()
        {
            if (BuyOrder.item.Count > 0 & !Main.loading)
            {
                if (mainForm.push_linkLabel.Text == "Push...")
                {
                    BuyOrder.tick = SteamConfig.Default.timer * 60;

                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Visible = true;
                        mainForm.push_linkLabel.Text = "Stop..."; }));

                    Main.timer.Start();
                }
                else
                {
                    if (BuyOrder.tick > 1) MainPresenter.stopPush();
                }
            }
        }
        public void timerTick(Object sender, ElapsedEventArgs e)
        {
            BuyOrder.tick--;
            TimeSpan time = TimeSpan.FromSeconds(BuyOrder.tick);
            mainForm.timer_StripStatus.Text = "Next check: " + time.ToString("mm':'ss");
            if (BuyOrder.tick <= 0)
            {
                if (!Main.loading)
                {
                    Main.timer.Stop();
                    mainForm.timer_StripStatus.Text = "Pushing...";
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(push);
                }
                else MainPresenter.stopPush();
            }
        }
        public void push(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                SteamPresenter.getBalance();
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value = 0; }));
                getSteamlist();

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Maximum = BuyOrder.item.Count;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Visible = true; }));
                pushItem();
                if (SteamConfig.Default.updateST)
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Text = "Updating...";
                        mainForm.progressBar_StripStatus.Value = 0;
                        mainForm.progressBar_StripStatus.Maximum = 3; }));
                    SteamPresenter.getBalance();
                    MainPresenter.loadDataSteam();
                }
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                BuyOrder.tick = SteamConfig.Default.timer * 60;
                Main.timer.Start();
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Visible = false; }));
            }
        }
        private void pushItem()
        {
            for (int i = 0; i < BuyOrder.item.Count; i++)
            {
                try
                {
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/" + BuyOrder.url[i]);
                    Thread.Sleep(500);
                    var ItemNameId = Request.ItemNameId(BuyOrder.url[i]);
                    var highest_buy_order = Request.ItemOrdersHistogram(ItemNameId);

                    double my_order = BuyOrder.price[i];

                    if (highest_buy_order > my_order & Steam.balance >= highest_buy_order & (highest_buy_order - my_order) <= BuyOrder.available_amount)
                    {
                        Main.Browser.ExecuteJavaScript(Request.CancelBuyOrder(BuyOrder.id[i], Main.sessionid));
                        Thread.Sleep(2000);
                        Main.Browser.ExecuteJavaScript(Request.CreateBuyOrder(BuyOrder.url[i], highest_buy_order, Main.sessionid));

                        BuyOrder.int_push++;
                        mainForm.push_label.Invoke(new MethodInvoker(() => mainForm.push_label.Text = "Push: " + Convert.ToString(BuyOrder.int_push)));
                        Thread.Sleep(1500);
                    }
                    else if (Steam.balance < highest_buy_order | (highest_buy_order - my_order) >= BuyOrder.available_amount)
                    {
                        if (SteamConfig.Default.cancelOrder)
                        {
                            Main.Browser.ExecuteJavaScript(Request.CancelBuyOrder(BuyOrder.id[i], Main.sessionid));

                            BuyOrder.removeAtItem(i);
                            availableAmount();

                            mainForm.Invoke(new MethodInvoker(delegate {
                                mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}";
                                mainForm.cancel_label.Text = "Cancel: " + BuyOrder.int_cancel++.ToString();
                                mainForm.cancel_label.ForeColor = Color.OrangeRed; }));
                        }

                        MainPresenter.messageBalloonTip($"Not enough balance for item:\n{BuyOrder.item[i]}", ToolTipIcon.Warning);
                    }
                }
                catch (Exception exp)
                {
                    Exceptions.errorLog(exp, Main.version);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }
            BuyOrder.int_check++;
            mainForm.check_label.Invoke(new MethodInvoker(() => mainForm.check_label.Text = "Check: " + BuyOrder.int_check.ToString()));
        }
    }
}