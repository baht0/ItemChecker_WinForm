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
        private int checkCount = 1;
        private int pushCount = 1;
        static private int cancelCount = 1;
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
                BuyOrder.price.Add(Edit.removeRub(str[0].Trim()));
                BuyOrder.order_id.Add(Edit.buyOrderId(id.GetAttribute("id"))); i++;
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}"; }));
            }
            availableAmount();
            MainPresenter.progressInvoke();
        }
        public static void availableAmount()
        {
            BuyOrder.sum = 0;
            BuyOrder.available_amount = 0;
            foreach (decimal item_price in BuyOrder.price) BuyOrder.sum += item_price;
            BuyOrder.available_amount = Math.Round(Steam.balance * 10 - BuyOrder.sum, 2);
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.available_label.Text = "Available: " + BuyOrder.available_amount.ToString() + "₽"; }));
            if (BuyOrder.available_amount < 1000)
            {
                MainPresenter.messageBalloonTip("Little available amount.\nCancellation of orders is possible.", ToolTipIcon.Warning);
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.available_label.ForeColor = Color.OrangeRed; }));
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
                    var market_hash_name = Edit.replaceUrl(BuyOrder.item[i]);
                    response = Get.MrinkaRequest(market_hash_name);
                    if (!response.Item2)
                    {
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.status_StripStatus.Text = "Calculate Steam (429). Please Wait...";
                            mainForm.timer_StripStatus.Text = "Updating (429). Please Wait..."; }));
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
                    var market_hash_name = Edit.replaceUrl(BuyOrder.item[i]);
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + market_hash_name;
                    var response = Get.Request(url, Main.proxyList[id]);

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
            decimal my_order = Convert.ToDecimal(BuyOrder.price[i]);
            var buy_order = Math.Round(my_order / GeneralConfig.Default.currency, 2);
            var csm_sell = Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString());
            var precent = Edit.Precent(buy_order, csm_sell);
            var different = Edit.Difference(csm_sell * GeneralConfig.Default.currency, my_order);

            BuyOrder.csm_price.Add(csm_sell);
            BuyOrder.precent.Add(precent);
            BuyOrder.difference.Add(different);
        }
        public static void createDTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Write Steam..."; }));
            MainPresenter.clearDTableRows(mainForm.buyOrder_dataGridView);

            DataTable table = new();

            foreach (DataGridViewColumn column in mainForm.buyOrder_dataGridView.Columns)
                table.Columns.Add(new DataColumn(column.Name));

            table.Columns[4].DataType = typeof(decimal);
            table.Columns[5].DataType = typeof(decimal);
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
                int id = BuyOrder.item.IndexOf(item);
                var price = Edit.removeSymbol(row.Cells[2].Value.ToString());
                var precent = Edit.removeDol(row.Cells[4].Value.ToString());

                if (precent < 25)
                    row.Cells[4].Style.BackColor = Color.OrangeRed;
                if (precent > 35)
                    row.Cells[4].Style.BackColor = Color.MediumSeaGreen;
                if (price > Steam.balance)
                {
                    row.Cells[2].Style.BackColor = Color.Crimson;
                    if (SteamConfig.Default.cancelBalance)
                    {
                        CancelOrder(id);
                        row.Cells[2].Style.BackColor = Color.Red;
                        row.Cells[2].Value = "Cancel";;
                    }
                    MainPresenter.messageBalloonTip($"Not enough balance for item:\n{item}", ToolTipIcon.Warning);
                }
                if (precent <= SteamConfig.Default.cancelPrecent & precent != -100)
                {
                    CancelOrder(id);
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[2].Value = "Cancel";
                }
                if (Main.overstock.Contains(item))
                {
                    row.Cells[3].Value = "Overstock";
                    row.Cells[3].Style.BackColor = Color.OrangeRed;
                    if (SteamConfig.Default.cancelOverstock)
                    {
                        CancelOrder(id);
                        row.Cells[2].Style.BackColor = Color.Red;
                        row.Cells[2].Value = "Cancel"; ;
                    }
                    MainPresenter.messageBalloonTip("Buy orders contains inaccessible items!", ToolTipIcon.Warning);
                }
                if (Main.unavailable.Contains(item))
                {
                    row.Cells[3].Style.BackColor = Color.Red;
                    row.Cells[3].Value = "Unavailable";
                    CancelOrder(id);
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[2].Value = "Cancel"; ;
                    MainPresenter.messageBalloonTip("Buy orders contains inaccessible items!", ToolTipIcon.Warning);
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
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
                Exceptions.errorLog(exp, Main.assemblyVersion);
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
                MainPresenter.messageBalloonTip("The creation of orders has been completed.");
            }
        }
        private static void createOrder()
        {
            foreach (string queue in BuyOrder.queue)
            {
                try
                {
                    var market_hash_name = Edit.replaceUrl(queue);
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/" + market_hash_name);
                    var item_nameid = Get.ItemNameId(market_hash_name);
                    var highest_buy_order = Get.ItemOrdersHistogram(item_nameid);

                    if (Steam.balance > highest_buy_order) 
                        Main.Browser.ExecuteJavaScript(Post.CreateBuyOrder(market_hash_name, highest_buy_order, Main.sessionid));
                }
                catch (Exception exp)
                {
                    Exceptions.errorLog(exp, Main.assemblyVersion);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }
        }
        //delete order
        public static void CancelOrder(int id)
        {
            try
            {
                Main.Browser.ExecuteJavaScript(Post.CancelBuyOrder(BuyOrder.order_id[id], Main.sessionid));
                BuyOrder.removeAtItem(id);
                availableAmount();

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.buyOrder_dataGridView.Columns[1].HeaderText = $"Item (BuyOrders) - {BuyOrder.item.Count}";
                    mainForm.pusherCancel_label.Text = $"Cancel: {cancelCount++}";
                    mainForm.pusherCancel_label.ForeColor = Color.OrangeRed;
                }));
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorMessage(exp, currMethodName);
            }
        }

        //push...
        public static void stopBuyOrderPusher()
        {
            BuyOrder.timer.Enabled = false;
            BuyOrder.tick = 1;
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.timer_StripStatus.Visible = false;
                mainForm.buyOrderPush_toolStripMenuItem.ForeColor = Color.Black; }));
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
                    BuyOrder.timer.Enabled = false;
                    mainForm.timer_StripStatus.Text = "Pushing...";
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(push);
                }
                else
                    BuyOrderPresenter.stopBuyOrderPusher();
            }
        }
        private void push(object state)
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
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
            finally
            {
                Main.loading = false;
                mainForm.Invoke(new MethodInvoker(delegate { 
                    mainForm.progressBar_StripStatus.Visible = false;
                    mainForm.timer_StripStatus.Text = "Next check: 00:00";
                    mainForm.pusherCheck_label.Text = $"Check: {checkCount++}";
                    mainForm.pusherItems_label.Text = $"Items: {BuyOrder.item.Count}";
                }));
                BuyOrder.tick = SteamConfig.Default.timer * 60;
                BuyOrder.timer.Enabled = true;
            }
        }
        private void pushItem()
        {
            for (int i = 0; i < BuyOrder.item.Count; i++)
            {
                try
                {
                    var market_hash_name = Edit.replaceUrl(BuyOrder.item[i]);
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/" + market_hash_name);
                    Thread.Sleep(500);
                    var item_nameid = Get.ItemNameId(market_hash_name);
                    decimal highest_buy_order = Get.ItemOrdersHistogram(item_nameid);

                    decimal my_order = BuyOrder.price[i];

                    if (highest_buy_order > my_order & Steam.balance >= highest_buy_order & (highest_buy_order - my_order) <= BuyOrder.available_amount)
                    {
                        Main.Browser.ExecuteJavaScript(Post.CancelBuyOrder(BuyOrder.order_id[i], Main.sessionid));
                        Thread.Sleep(2000);
                        Main.Browser.ExecuteJavaScript(Post.CreateBuyOrder(market_hash_name, highest_buy_order, Main.sessionid));

                        mainForm.pusherPush_label.Invoke(new MethodInvoker(() => mainForm.pusherPush_label.Text = $"Push: {pushCount++}"));
                        Thread.Sleep(1500);
                    }
                    else if (Steam.balance < highest_buy_order)
                    {
                        if (SteamConfig.Default.cancelBalance)
                            CancelOrder(i);
                        MainPresenter.messageBalloonTip($"Not enough balance for item:\n{BuyOrder.item[i]}", ToolTipIcon.Warning);
                    }
                }
                catch (Exception exp)
                {
                    Exceptions.errorLog(exp, Main.assemblyVersion);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }
        }
    }
}