using System;
using System.Windows.Forms;
using static ItemChecker.Program;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using ItemChecker.Settings;
using ItemChecker.Model;
using ItemChecker.Support;
using ItemChecker.Net;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Timers;

namespace ItemChecker.Presenter
{
    public class WithdrawPresenter
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
                MainPresenter.messageBalloonTip();
            }
        }

        public static void withdrawCheck()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

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
                        MainPresenter.clearDTableRows(mainForm.withdraw_dataGridView);
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
            MainPresenter.clearDTableRows(mainForm.withdraw_dataGridView);

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
                if (Withdraw.favoriteList.Contains(item))
                {
                    row.Cells[1].Style.BackColor = Color.LightGray;
                    row.Cells[2].Style.BackColor = Color.LightGray;
                    row.Cells[3].Style.BackColor = Color.LightGray;
                    row.Cells[4].Style.BackColor = Color.LightGray;
                    row.Cells[5].Style.BackColor = Color.LightGray;
                }
                row.Cells[2].Style.BackColor = Color.LightGray;
            }
        }

        //inventory
        public static void inventoryCsm(object state)
        {
            try
            {
                loginCsm();

                JArray items = getItems(checkInventory());
                if(items.Count > 0)
                {
                    DialogResult result = MessageBox.Show($"There are '{items.Count}' items to withdraw.\nDo you want to withdraw?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        mainForm.Invoke(new MethodInvoker(delegate {
                            mainForm.progressBar_StripStatus.Maximum = items.Count;
                            mainForm.progressBar_StripStatus.Value = 0;
                            mainForm.progressBar_StripStatus.Visible = true; }));
                        withdrawItems(items);
                    }
                }
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
                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.status_StripStatus.Visible = false;
                    mainForm.progressBar_StripStatus.Visible = false; }));
                MainPresenter.messageBalloonTip("Checking inventory cs.money is completed.");
            }
        }
        private static void loginCsm()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Login Cs.Money..."; }));
            Main.Browser.Navigate().GoToUrl("https://cs.money/pending-trades");
            IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
            string json = html.Text;
            if (json.Contains("error"))
            {
                var code_error = JObject.Parse(json)["error"].ToString();
                if (code_error == "6")
                {
                    string signIn_url = "https://auth.dota.trade/login?redirectUrl=https://cs.money/&callbackUrl=https://cs.money/login";
                    Main.Browser.Navigate().GoToUrl(signIn_url);

                    IWebElement signins = Main.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@class='btn_green_white_innerfade']")));
                    signins.Click();
                    Thread.Sleep(300);
                }
            }
        }
        private static JArray checkInventory()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Getting inventory items..."; }));
            int offset = 0;
            JArray inventory = new();
            while (true)
            {
                if (inventory.Count < offset)
                    break;
                Main.Browser.Navigate().GoToUrl("https://cs.money/3.0/load_user_inventory/730?limit=60&noCache=true&offset=" + offset + "&order=desc&sort=price");
                IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
                string json = JObject.Parse(html.Text)["items"].ToString();
                inventory.Merge(JArray.Parse(json));
                offset += 60;
            }
            JArray items = new();
            if (inventory.Count > 0)
            {
                foreach (JObject item in inventory)
                {
                    if (!item.ContainsKey("tradeLock"))
                    {
                        if (item.ContainsKey("isVirtual"))
                            items.Add(item);
                    }
                }
            }
            return items;
        }
        private static JArray getItems(JArray inventory)
        {
            JArray items = new();
            if (inventory.Count > 0)
            {
                foreach (JObject item in inventory)
                {
                    if (item.ContainsKey("stackSize"))
                    {
                        Main.Browser.Navigate().GoToUrl("https://cs.money/2.0/get_user_stack/730/" + item["stackId"].ToString());
                        IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
                        JArray stack = JArray.Parse(html.Text);

                        foreach (JObject stack_item in stack)
                        {
                            JObject item_copy = item;
                            item_copy["id"] = Convert.ToInt64(stack_item["id"].ToString());
                            if (item.ContainsKey("float"))
                                item_copy["float"] = stack_item["float"].ToString();
                            if (item.ContainsKey("pattern"))
                                item_copy["pattern"] = Convert.ToInt32(stack_item["pattern"].ToString());
                            items.Add(item_copy);
                        }
                    }
                    else
                        items.Add(item);
                }
            }            
            return items;
        }
        private static void withdrawItems(JArray items)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.status_StripStatus.Text = "Withdraw items..."; }));

            foreach (JObject item in items)
            {
                try
                {
                    JObject json =
                       new JObject(
                           new JProperty("skins",
                               new JObject(
                                   new JProperty("bot", new JArray(item)),
                                   new JProperty("user", new JArray()))));
                    string body = json.ToString(Formatting.None);
                    Main.Browser.ExecuteJavaScript(Request.PostRequest("application/json", body, "https://cs.money/2.0/withdraw_skins"));
                }
                catch
                {
                    continue;
                }
                finally
                {
                    MainPresenter.progressInvoke();
                    Thread.Sleep(500);
                }
            }
        }

        //CheckFavorite
        public static void stopCheckFavorite()
        {
            Withdraw.timer.Enabled = false;
            Withdraw.tick = 0;
            mainForm.Invoke(new MethodInvoker(delegate {
                mainForm.timer_StripStatus.Visible = false;
                mainForm.checkFavorite_ToolStripMenuItem.ForeColor = Color.Black; }));
        }
        public static void checkStart()
        {
            if (Withdraw.favoriteList.Count > 0 & !Main.loading)
            {
                if (!BuyOrder.timer.Enabled & !Withdraw.timer.Enabled)
                {
                    Withdraw.tick = 90;

                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Visible = true;
                        mainForm.checkFavorite_ToolStripMenuItem.ForeColor = Color.OrangeRed; }));

                    Withdraw.timer.Enabled = true;
                }
                else if (Withdraw.timer.Enabled & Withdraw.tick > 1)
                    WithdrawPresenter.stopCheckFavorite();
            }
        }
        public void timerTick(Object sender, ElapsedEventArgs e)
        {
            Withdraw.tick--;
            TimeSpan time = TimeSpan.FromSeconds(Withdraw.tick);
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Next check: " + time.ToString("mm':'ss"); }));
            if (Withdraw.tick <= 0)
            {
                if (!Main.loading)
                {
                    Withdraw.timer.Enabled = false;

                    mainForm.Invoke(new MethodInvoker(delegate {
                        mainForm.timer_StripStatus.Text = "Checking...";
                        mainForm.progressBar_StripStatus.Maximum = Withdraw.favoriteList.Count;
                        mainForm.progressBar_StripStatus.Value = 0;
                        mainForm.progressBar_StripStatus.Visible = true; }));
                    Main.loading = true;
                    ThreadPool.QueueUserWorkItem(checkFavorite);
                }
                else
                    WithdrawPresenter.stopCheckFavorite();
            }
        }
        private void checkFavorite(object state)
        {
            try
            {
                foreach (string item_name in Withdraw.favoriteList)
                {
                    mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Checking..."; }));
                    var json = Request.inventoriesCsMoney(Edit.replaceUrl(item_name));
                    if (!json.Contains("error"))
                    {
                        JArray items = new();
                        JArray inventory = JArray.Parse(JObject.Parse(json)["items"].ToString());
                        foreach (JObject item in inventory)
                        {
                            if (item.ContainsKey("stackSize"))
                            {
                                Main.Browser.Navigate().GoToUrl("https://inventories.cs.money/4.0/get_bot_stack/730/" + item["stackId"].ToString());
                                IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
                                JArray stack = JArray.Parse(html.Text);
                                foreach (JObject stack_item in stack)
                                    items.Add(getStackItems(item, stack_item));
                            }
                            else
                                items.Add(item);
                        }
                        addCart(items);
                        pendingTrades();
                    }
                    MainPresenter.progressInvoke();
                }
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                mainForm.Invoke(new MethodInvoker(delegate { mainForm.progressBar_StripStatus.Visible = false; }));
                Main.loading = false;
                Withdraw.tick = 90;
                Withdraw.timer.Enabled = true;
            }
        }
        private JObject getStackItems(JObject item, JObject stack_item)
        {
            JObject item_copy = item;
            if (item.ContainsKey("3d"))
                item_copy["3d"] = stack_item["3d"].ToString();
            if (item.ContainsKey("float"))
                item_copy["float"] = stack_item["float"].ToString();
            if (item.ContainsKey("id"))
                item_copy["id"] = Convert.ToInt64(stack_item["id"].ToString());
            if (item.ContainsKey("img"))
                item_copy["img"] = stack_item["img"].ToString();
            if (item.ContainsKey("pattern"))
                item_copy["pattern"] = Convert.ToInt32(stack_item["pattern"].ToString());
            if (item.ContainsKey("preview"))
                item_copy["preview"] = stack_item["preview"].ToString();
            if (item.ContainsKey("screenshot"))
                item_copy["screenshot"] = stack_item["screenshot"].ToString();
            if (item.ContainsKey("steamId"))
                item_copy["steamId"] = stack_item["steamId"].ToString();

            return item_copy;
        }
        private void addCart(JArray items)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Add Cart..."; }));
            clearCart();
            Main.Browser.Navigate().GoToUrl("https://cs.money/csgo/trade/");
            decimal sum = 0;
            foreach (JObject item in items)
            {
                try {
                    JObject json = new JObject(
                           new JProperty("type", 2),
                           new JProperty("item", new JObject(item)));
                    string body = json.ToString(Formatting.None);
                    Main.Browser.ExecuteJavaScript(Request.PostRequest("application/json", body, "https://cs.money/add_cart"));
                    sum += Convert.ToDecimal(item["price"].ToString());
                }
                catch {
                    items.Remove(item);
                    continue;
                }
                finally {
                    Thread.Sleep(500);
                }
            }
            sum *= -1;
            sendOffer(items, sum);
        }
        private void clearCart()
        {
            Main.Browser.ExecuteJavaScript(Request.PostRequest("application/json", "{\"type\":2}", "https://cs.money/clear_cart"));
        }
        private void sendOffer(JArray items, decimal sum)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Send Offer..."; }));

            JObject json = new JObject(
                        new JProperty("skins",
                            new JObject(
                                new JProperty("user", new JArray()),
                                new JProperty("bot", new JArray(items)))),
                        new JProperty("balance", sum),
                            new JProperty("games", new JObject()),
                            new JProperty("isVirtual", false));
            string body = json.ToString(Formatting.None);
            Main.Browser.ExecuteJavaScript(Request.PostRequest("application/json", body, "https://cs.money/2.0/send_offer"));
        }
        private void pendingTrades()
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Pending Trades..."; }));

            Main.Browser.Navigate().GoToUrl("https://cs.money/pending-trades");
            IWebElement html = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//pre")));
            JArray trades = JArray.Parse(html.Text);
            foreach (JObject trade in trades)
            {
                int status = Convert.ToInt32(trade["status"].ToString());
                if (status == 0)
                {
                    Int64 id = Convert.ToInt64(trade["id"].ToString());
                    confirmVirtualOffer(id);
                }
            }
        }
        private void confirmVirtualOffer(Int64 id)
        {
            mainForm.Invoke(new MethodInvoker(delegate { mainForm.timer_StripStatus.Text = "Confirm Virtual Offer..."; }));

            JObject json = new JObject(
                        new JProperty("offer_id", id),
                        new JProperty("action", "confirm"));
            string body = json.ToString(Formatting.None);
            Main.Browser.ExecuteJavaScript(Request.PostRequest("application/json", body, "https://cs.money/confirm_virtual_offer"));
        }
    }
}