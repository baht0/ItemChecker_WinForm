using System;
using System.Timers;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json.Linq;
using static ItemChecker.Program;
using ItemChecker.General;
using ItemChecker.Model;
using ItemChecker.Settings;
using ItemChecker.NET;

namespace ItemChecker.Presenter
{
    public class BuyOrderPresenter
    {
        public static void getSteamlist()
        {
            BuyOrder._clear();
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Check Steam..."; }));

            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/");

            int table_index = 1;
            IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table']/h3/span[1]")));
            if (table.Text == "My listings awaiting confirmation") table_index = 2;

            for (int i = 0; i < BuyOrder.count; i++)
            {
                IWebElement list = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table'][" + table_index + "]/div[" + (i + 2) + "]/div[4]/span[1]/a")));
                IWebElement price = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table'][" + table_index + "]/div[" + (i + 2) + "]/div[2]/span/span")));
                IWebElement id = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='my_listing_section market_content_block market_home_listing_table'][" + table_index + "]/div[" + (i + 2) + "]")));

                BuyOrder.item.Add(list.Text);
                BuyOrder.url.Add(Edit.replaceUrl(list.Text));
                BuyOrder.price.Add(Edit.removeRub(price.Text));
                BuyOrder.id.Add(Edit.buyOrderId(id.GetAttribute("id")));
            }

            availableAmount();
            MainPresenter.progressInvoke();
        }
        public static void availableAmount()
        {
            BuyOrder.sum = 0;
            BuyOrder.available_amount = 0;
            foreach (double item in BuyOrder.price) BuyOrder.sum += item;
            BuyOrder.available_amount = Math.Round(Steam.balance * 10 - BuyOrder.sum, 2);
            mainForm.Invoke((Action)(() => Edit.invokeLabel(mainForm.available_label, "Available: " + BuyOrder.available_amount.ToString() + "₽")));
        }
        public static void precentSteam()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Calculate Steam..."; }));

            for (int i = 0; i < BuyOrder.count; i++)
            {
                try
                {
                    double my_order = Convert.ToDouble(BuyOrder.price[i]);
                    var buy_order = Math.Round(my_order / Main.course, 2);

                    var json = Request.mrinkaRequest(BuyOrder.url[i]);

                    var csm_sell = Convert.ToDouble(JObject.Parse(json)["csm"]["sell"].ToString());
                    var prec = Math.Round(((csm_sell - buy_order) / buy_order) * 100, 2);
                    var diff = Math.Round((csm_sell * Main.course) - my_order, 2);

                    BuyOrder.csm_price.Add(csm_sell);
                    BuyOrder.precent.Add(prec);
                    BuyOrder.difference.Add(diff);
                }
                catch
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.status_StripStatus.Text = "Calculate Steam (429). Wait 2 min...";
                        mainForm.timer_StripStatus.Text = "Updating (429). Wait 2 min...";
                    }));
                    i--;
                    Thread.Sleep(30000);
                    continue;
                }
            }
            MainPresenter.progressInvoke();
        }
        public static void createSteamTable()
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Steam Table...";
                mainForm.buyOrder_dataGridView.Rows.Clear();
            }));
            int s = 0;

            for (int i = 0; i < BuyOrder.count; i++)
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.buyOrder_dataGridView.Rows.Add(); }));
                if (!Main.overstock.Contains(BuyOrder.item[i]) & !Main.unavailable.Contains(BuyOrder.item[i]))
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[1].Value = BuyOrder.item[i];
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Value = BuyOrder.price[i] + "₽";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[3].Value = BuyOrder.csm_price[i] + "$";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Value = BuyOrder.precent[i];
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[5].Value = BuyOrder.difference[i];
                    }));

                    if (BuyOrder.precent[i] < 25) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.OrangeRed; }));
                    if (BuyOrder.precent[i] > 35) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.MediumSeaGreen; }));
                    if (Convert.ToDouble(BuyOrder.price[i]) > Steam.balance) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.Crimson; }));
                    if (BuyOrder.item[i].Contains("Souvenir")) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                    if (BuyOrder.item[i].Contains("StatTrak")) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                    if (BuyOrder.item[i].Contains("★")) mainForm.buyOrder_dataGridView.Invoke(new Action(() => { mainForm.buyOrder_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                    if (BuyOrder.precent[i] < SteamConfig.Default.autoDelete & BuyOrder.precent[i] != -100)
                    {
                        Main.Browser.ExecuteJavaScript(Request.cancelBuyOrder(BuyOrder.id[i], Main.sessionid));
                        mainForm.Invoke(new Action(() => {
                            mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.Red;
                            mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Value = "Deleted";
                        }));
                        continue;
                    }
                }
                else if (Main.overstock.Contains(BuyOrder.item[i]))
                {
                    s++;
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.steamMarket_label.ForeColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[1].Value = BuyOrder.item[i];
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Value = BuyOrder.price[i] + "₽";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[3].Value = "Overstock";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Value = BuyOrder.precent[i];
                    }));
                }
                else if (Main.unavailable.Contains(BuyOrder.item[i]))
                {
                    s++;
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.steamMarket_label.ForeColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[1].Value = BuyOrder.item[i];
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[2].Value = BuyOrder.price[i] + "₽";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[3].Value = "Unavailable";
                        mainForm.buyOrder_dataGridView.Rows[i].Cells[4].Value = BuyOrder.precent[i];
                    }));
                }
            }
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.buyOrder_dataGridView.Columns[4].ValueType = mainForm.buyOrder_dataGridView.Columns[5].ValueType = typeof(Double);
                mainForm.buyOrder_dataGridView.Sort(mainForm.buyOrder_dataGridView.Columns[4], ListSortDirection.Ascending);
            }));
            mainForm.steamMarket_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.steamMarket_label, "SteamMarket: " + Convert.ToString(s))));

            MainPresenter.progressInvoke();
        }

        //place order
        public static void createOrder()
        {
            if (!BuyOrder.queue.Contains("") & BuyOrder.queue_count > 0)
            {
                mainForm.queue_linkLabel.Enabled = false;
                mainForm.progressBar_StripStatus.Visible = true;
                mainForm.progressBar_StripStatus.Maximum = BuyOrder.queue_count;
                mainForm.progressBar_StripStatus.Value = 0;
                Main.loading = true;
                ThreadPool.QueueUserWorkItem(placeOrder);
            }
        }
        private static void placeOrder(object state)
        {
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.status_StripStatus.Text = "Place Order...";
                mainForm.status_StripStatus.Visible = true;
            }));
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));

            for (int i = 0; i < BuyOrder.queue_count; i++)
            {
                try
                {
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + BuyOrder.queue[i]);
                    Thread.Sleep(1000);

                    IWebElement last = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='market_commodity_buyrequests']/span[2]")));
                    double last_order = Edit.removeRub(last.Text);

                    if (Steam.balance > last_order)
                    {
                        Main.Browser.ExecuteJavaScript(Request.createBuyOrder(BuyOrder.queue[i], last_order, Main.sessionid));
                    }
                    BuyOrder.ordered.Add(Edit.inverReplaceUrl(BuyOrder.queue[i]));
                }
                catch (Exception exp)
                {
                    string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    Edit.errorMessage(exp, currMethodName);
                    Edit.errorLog(exp, Main.version);
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                }
            }
            Thread.Sleep(2000);
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.progressBar_StripStatus.Maximum = 7;
            }));
            Main.reload = 0;
            ThreadPool.QueueUserWorkItem(MainPresenter._reload);
        }
        //delete order
        public static void deleteOrder(object state)
        {
            try
            {
                Main.loading = true;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));

                if (mainForm.buyOrder_dataGridView.CurrentCell.ColumnIndex == 1)
                {
                    int row = Convert.ToInt32(mainForm.buyOrder_dataGridView.CurrentCell.RowIndex.ToString());
                    string iname = mainForm.buyOrder_dataGridView.CurrentCell.Value.ToString();
                    string price = mainForm.buyOrder_dataGridView.Rows[row].Cells[2].Value.ToString();

                    string url = Edit.replaceUrl(iname);
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + url);

                    IWebElement buy_orderid = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='tabContentsMyListings']/div/div[2]")));

                    Main.Browser.ExecuteJavaScript(Request.cancelBuyOrder(Edit.buyOrderId(buy_orderid.GetAttribute("id")), Main.sessionid));

                    BuyOrder.available_amount = Math.Round(BuyOrder.available_amount + Edit.removeSymbol(price), 2);
                    BuyOrder.sum = Math.Round(BuyOrder.sum - Edit.removeSymbol(price), 2);
                    mainForm.available_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.available_label, "Available: " + Convert.ToString(BuyOrder.available_amount) + "₽")));
                    mainForm.quantity_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.quantity_label, "Quantity: " + Convert.ToString(BuyOrder.count--))));
                    mainForm.Invoke(new Action(() => {
                        mainForm.buyOrder_dataGridView.Rows[row].Cells[2].Style.BackColor = Color.Red;
                        mainForm.buyOrder_dataGridView.Rows[row].Cells[2].Value = "Deleted";
                    }));
                }
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                Main.loading = false;
            }
        }

        //push...
        public void timerTick(Object sender, ElapsedEventArgs e)
        {
            BuyOrder.tick--;
            TimeSpan time = TimeSpan.FromSeconds(BuyOrder.tick);
            mainForm.timer_StripStatus.Text = "Next check: " + time.ToString("mm':'ss");
            if (BuyOrder.tick <= 0)
            {
                Main.timer.Stop();
                mainForm.timer_StripStatus.Text = "Pushing...";
                Main.loading = true;
                ThreadPool.QueueUserWorkItem(push);
            }
        }
        public void push(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                SteamPresenter.getBalance();

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.progressBar_StripStatus.Visible = true;
                    mainForm.progressBar_StripStatus.Value = 0;
                    mainForm.progressBar_StripStatus.Maximum = BuyOrder.count;
                }));

                getSteamlist();
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Value = 0; }));

                pushItem();
                if (SteamConfig.Default.updateST)
                {
                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Text = "Updating...";
                        mainForm.progressBar_StripStatus.Value = 0;
                        mainForm.progressBar_StripStatus.Maximum = 3;
                    }));
                    SteamPresenter.getBalance();
                    MainPresenter.loadDataSteam();
                }
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
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
            WebDriverWait wait = new WebDriverWait(Main.Browser, TimeSpan.FromSeconds(GeneralConfig.Default.wait));
            for (int i = 0; i < BuyOrder.count; i++)
            {
                try
                {
                    Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market/listings/730/" + BuyOrder.url[i]);
                    Thread.Sleep(2000);

                    IWebElement my_last = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='tabContentsMyListings']/div/div[2]/div[2]/span/span")));
                    double my_order = Edit.removeRub(my_last.Text);

                    IWebElement last_price = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='market_commodity_buyrequests']/span[2]")));
                    double last_order = Edit.removeRub(last_price.Text);

                    if (Steam.balance > last_order & last_order > my_order)
                    {
                        Main.Browser.ExecuteJavaScript(Request.cancelBuyOrder(BuyOrder.id[i], Main.sessionid));
                        Thread.Sleep(2000);
                        Main.Browser.ExecuteJavaScript(Request.createBuyOrder(BuyOrder.url[i], last_order, Main.sessionid));

                        BuyOrder.int_push++;
                        mainForm.push_label.Invoke(new MethodInvoker(() => mainForm.push_label.Text = "Push: " + Convert.ToString(BuyOrder.int_push)));
                        Thread.Sleep(1500);
                    }
                    else if (Steam.balance < last_order)
                    {
                        DialogResult result = MessageBox.Show(
                            $"There is not enough balance for a purchase requisition.\n{Edit.inverReplaceUrl(BuyOrder.url[i])}\nHigher order: {last_order}₽\n\nRemove buy order?",
                            "Warning",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Main.Browser.ExecuteJavaScript(Request.cancelBuyOrder(BuyOrder.id[i], Main.sessionid));

                            BuyOrder.available_amount = Math.Round(BuyOrder.available_amount + BuyOrder.price[i], 2);
                            BuyOrder.sum = Math.Round(BuyOrder.sum - BuyOrder.price[i], 2);
                            mainForm.available_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.available_label, "Available: " + BuyOrder.available_amount.ToString() + "₽")));
                            mainForm.quantity_label.Invoke((Action)(() => Edit.invokeLabel(mainForm.quantity_label, "Quantity: " + Convert.ToString(BuyOrder.count--))));
                        }
                    }
                }
                catch (Exception exp)
                {
                    string catch_item = Edit.inverReplaceUrl(BuyOrder.url[i]) + "\n";
                    string catch_item_url = "https://steamcommunity.com/market/listings/730/" + BuyOrder.url[i] + "\n";
                    Edit.errorLog(exp, Main.version);

                    BuyOrder.int_catch++;
                    mainForm.catch_label.Invoke(new MethodInvoker(() => mainForm.catch_label.Text = "Catch: " + Convert.ToString(BuyOrder.int_catch)));
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