using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Threading;
using ItemChecker.Support;
using ItemChecker.Net;
using ItemChecker.Settings;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace ItemChecker.Presenter
{
    class ServiceCheckerPresenter
    {
        public static void checkMain(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                Main.loading = true;

                ServiceChecker._clear();
                if (ServiceChecker.service_one < 2 | ServiceChecker.service_two < 2)
                    if (GeneralConfig.Default.proxy & !String.IsNullOrEmpty(Properties.Settings.Default.proxyList))
                        checkMrinkaProxy();
                    else
                        checkMrinka();
                if (ServiceChecker.service_one == 2 | ServiceChecker.service_two == 2)
                    checkLootFarm();

                createList();
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
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                    serviceCheckerForm.ownList_menuStrip.Enabled = true;
                    serviceCheckerForm.quick_button.Enabled = true;
                    serviceCheckerForm.ownList_dataGridView.Sort(serviceCheckerForm.ownList_dataGridView.Columns[7], ListSortDirection.Descending); }));
                MainPresenter.messageBalloonTip();
            }
        }
        private static void checkMrinka()
        {
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                if (!ServiceChecker.checkStop)
                {
                    Tuple<String, Boolean> response = Tuple.Create("", false);
                    do
                    {
                        response = Request.MrinkaRequest(Edit.replaceUrl(Main.checkList[i]));
                        if (!response.Item2)
                        {
                            serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Text = "Check List (429). Please Wait..."; }));
                            Thread.Sleep(30000);
                        }
                    }
                    while (!response.Item2);

                    if (ServiceChecker.service_one == 0)
                    {
                        ServiceChecker.price_one.Add(Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["sellOrder"].ToString()));
                        ServiceChecker.price2_one.Add(Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["buyOrder"].ToString()));
                        ServiceChecker.stUpdated.Add(JObject.Parse(response.Item1)["steam"]["updated"].ToString());
                    }
                    else if (ServiceChecker.service_two == 0)
                    {
                        ServiceChecker.price_two.Add(Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["sellOrder"].ToString()));
                        ServiceChecker.price2_two.Add(Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["buyOrder"].ToString()));
                        ServiceChecker.stUpdated.Add(JObject.Parse(response.Item1)["steam"]["updated"].ToString());
                        ServiceChecker.status.Add("Unknown");
                    }
                    if (ServiceChecker.service_one == 1)
                    {
                        ServiceChecker.price2_one.Add(Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["sell"].ToString()));
                        ServiceChecker.price_one.Add(Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["buy"]["0"].ToString()));
                        ServiceChecker.csmUpdated.Add(JObject.Parse(response.Item1)["csm"]["updated"].ToString());
                    }
                    else if (ServiceChecker.service_two == 1)
                    {
                        ServiceChecker.price2_two.Add(Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["sell"].ToString()));
                        ServiceChecker.price_two.Add(Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["buy"]["0"].ToString()));
                        ServiceChecker.csmUpdated.Add(JObject.Parse(response.Item1)["csm"]["updated"].ToString());
                        if (Main.unavailable.Contains(Main.checkList[i]))
                            ServiceChecker.status.Add("Unavailable");
                        else if (Main.overstock.Contains(Main.checkList[i]))
                            ServiceChecker.status.Add("Overstock");
                        else
                            ServiceChecker.status.Add("Tradable");
                    }
                    serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}"; }));
                }
                else return;
            }
        }
        private static void checkMrinkaProxy()
        {
            int id = 0;
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                try
                {
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + Edit.replaceUrl(Main.checkList[i]);
                    string response = Request.GetRequest(url, Main.proxyList[id]);

                    ServiceChecker.price_one.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                    ServiceChecker.price2_one.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["buyOrder"].ToString()));
                    ServiceChecker.stUpdated.Add(JObject.Parse(response)["steam"]["updated"].ToString());

                    ServiceChecker.price2_two.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["sell"].ToString()));
                    ServiceChecker.price_two.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                    ServiceChecker.csmUpdated.Add(JObject.Parse(response)["csm"]["updated"].ToString());
                    if (Main.unavailable.Contains(Main.checkList[i]))
                        ServiceChecker.status.Add("Unavailable");
                    else if (Main.overstock.Contains(Main.checkList[i]))
                        ServiceChecker.status.Add("Overstock");
                    else
                        ServiceChecker.status.Add("Tradable");
                    serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}"; }));

                    if (!File.Exists("true.txt")) File.WriteAllText("true.txt", Main.proxyList[id] + "\n");
                    else File.WriteAllText("true.txt", string.Format("{0}{1}", Main.proxyList[id] + "\n", File.ReadAllText("true.txt")));
                }
                catch
                {
                    i--;
                    if (Main.proxyList.Count > id)
                        id++;
                    else break;
                }
            }
        }
        private static void checkLootFarm()
        {
            var json = Request.GetRequest("https://loot.farm/fullprice.json");
            JArray jArray = JArray.Parse(json);
             List<string> str = new List<string>();

            for (int i = 0; i < jArray.Count; i++)
            {
                if (!ServiceChecker.checkStop)
                    str.Add(jArray[i]["name"].ToString());
                else
                    return;
            }
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                if (str.Contains(Main.checkList[i]))
                {
                    int id = str.IndexOf(Main.checkList[i]);
                    if (ServiceChecker.service_one == 2)
                    {
                        double price = Convert.ToDouble(jArray[id]["price"]) / 100;
                        ServiceChecker.price_one.Add(price);
                        ServiceChecker.price2_one.Add(Math.Round(price * 0.95, 2));
                    }
                    else if (ServiceChecker.service_two == 2)
                    {
                        double price = Convert.ToDouble(jArray[id]["price"]) / 100;
                        ServiceChecker.price_two.Add(price);
                        ServiceChecker.price2_two.Add(Math.Round(price * 0.95, 2));

                        int have = Convert.ToInt32(jArray[id]["have"]);
                        int max = Convert.ToInt32(jArray[id]["max"]);
                        int count = max - have;
                        if (count > 0)
                            ServiceChecker.status.Add("Tradable");
                        else if (count <= 0)
                            ServiceChecker.status.Add("Overstock");
                    }
                }
                else
                {
                    if (ServiceChecker.service_one == 2)
                    {
                        ServiceChecker.price_one.Add(0);
                        ServiceChecker.price2_one.Add(0);
                    }
                    else if (ServiceChecker.service_two == 2)
                    {
                        ServiceChecker.price_two.Add(0);
                        ServiceChecker.price2_two.Add(0);
                        ServiceChecker.status.Add("Unknown");
                    }
                }
                serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}"; }));
            }
        }

        private static void createList()
        {
            serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                serviceCheckerForm.ownList_dataGridView.Columns[6].ValueType = typeof(Double);
                serviceCheckerForm.ownList_dataGridView.Columns[7].ValueType = typeof(Double);
                serviceCheckerForm.status_toolStripStatusLabel.Text = "Create Table..."; }));

            for (int i = 0; i < Main.checkList.Count; i++)
            {
                double precent = Math.Round(((ServiceChecker.price2_two[i] - ServiceChecker.price_one[i]) / ServiceChecker.price_one[i]) * 100, 2);
                double difference = Math.Round(ServiceChecker.price2_two[i] - ServiceChecker.price_one[i], 2);
                if (ServiceChecker.price_one[i] == 0)
                    precent = 0;
                if (ServiceChecker.service_one == 0) //steam buyorder
                {
                    precent = Math.Round(((ServiceChecker.price2_two[i] - ServiceChecker.price2_one[i]) / ServiceChecker.price2_one[i]) * 100, 2);
                    difference = Math.Round(ServiceChecker.price2_two[i] - ServiceChecker.price2_one[i], 2);
                }

                writeToTable(i, precent, difference);
                serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.ownList_dataGridView.Columns[1].HeaderText = $"Item - {i + 1}"; }));
            }
        }
        private static void writeToTable(int i, double precent, double difference)
        {
            serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                serviceCheckerForm.ownList_dataGridView.Rows.Add();
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[1].Value = Main.checkList[i];
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[2].Value = ServiceChecker.price_one[i] + "$";
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[3].Value = ServiceChecker.price2_one[i] + "$";
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[4].Value = ServiceChecker.price_two[i] + "$";
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[5].Value = ServiceChecker.price2_two[i] + "$";
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[6].Value = precent;
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[7].Value = Edit.removeSymbol(Edit.funcConvert(difference, Main.course));
                serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[8].Value = ServiceChecker.status[i];
            }));

            //color
            serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.LightGray; }));
            serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LightGray; }));
            if (precent >= 35) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.MediumSeaGreen; }));
            if (precent <= 25) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.OrangeRed; }));
            if (precent < 0) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red; }));
            if (precent == 0 | precent == -100) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Gray; }));
            if (ServiceChecker.price2_one[i] > Steam.balance_usd & ServiceChecker.service_one == 0) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Crimson; }));
            if (Main.checkList[i].Contains("Sticker") || Main.checkList[i].Contains("Graffiti")) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue; }));
            if (Main.checkList[i].Contains("Souvenir")) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
            if (Main.checkList[i].Contains("StatTrak")) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
            if (Main.checkList[i].Contains("★")) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
            if (BuyOrder.queue.Contains(Edit.replaceUrl(Main.checkList[i]))) serviceCheckerForm.Invoke(new Action(() => { serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.LimeGreen; serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LimeGreen; }));
            if (BuyOrder.item.Contains(Main.checkList[i]))
            {
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.CornflowerBlue;
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.CornflowerBlue;
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Ordered"; }));
            }
            if (ServiceChecker.status[i].Contains("Overstock"))
            {
                serviceCheckerForm.Invoke(new MethodInvoker(delegate
                {
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.OrangeRed;
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.OrangeRed;
                }));
            }
            if (ServiceChecker.status[i].Contains("Unavailable"))
            {
                serviceCheckerForm.Invoke(new MethodInvoker(delegate
                {
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.Red;
                    serviceCheckerForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.Red;
                }));
            }
        }

        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = serviceCheckerForm.ownList_dataGridView.CurrentCell.RowIndex;
                int cell = serviceCheckerForm.ownList_dataGridView.CurrentCell.ColumnIndex;
                string item = serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[1].Value.ToString();
                double sta = Edit.removeSymbol(serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[3].Value.ToString());

                if (!BuyOrder.item.Contains(item) & cell != 3 & sta <= Steam.balance_usd)
                {
                    if (!BuyOrder.queue.Contains(item))
                    {
                        BuyOrder.queue_rub += Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Add(item);
                        serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub > BuyOrder.available_amount) 
                                mainForm.available_label.ForeColor = Color.Red;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.LimeGreen;
                        }));
                    }
                    else
                    {
                        BuyOrder.queue_rub -= Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Remove(item);
                        serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub <= BuyOrder.available_amount) 
                                mainForm.available_label.ForeColor = Color.Black;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            serviceCheckerForm.ownList_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.White;
                        }));
                    }
                }
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
        }
    }
}