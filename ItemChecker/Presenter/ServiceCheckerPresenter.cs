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
using System.Data;

namespace ItemChecker.Presenter
{
    class ServiceCheckerPresenter
    {
        public static void checkMain(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                ServiceChecker._clear();
                if (ServiceChecker.service_one < 2 | ServiceChecker.service_two < 2)
                    if (GeneralConfig.Default.proxy & !String.IsNullOrEmpty(Properties.Settings.Default.proxyList))
                        checkMrinkaProxy();
                    else
                        checkMrinka();
                if (ServiceChecker.service_one == 2 | ServiceChecker.service_two == 2)
                    checkLootFarm();

                createDTable();
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                if (!ServiceChecker.checkStop)
                {
                    Main.loading = false;
                    serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                        serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                        serviceCheckerForm.servChecker_menuStrip.Enabled = true;
                        serviceCheckerForm.quick_button.Enabled = true;
                        serviceCheckerForm.servChecker_dataGridView.Sort(serviceCheckerForm.servChecker_dataGridView.Columns[7], ListSortDirection.Descending); }));
                    drawDTGView();
                    MainPresenter.messageBalloonTip();
                }
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
                    parseMrinka(i, response.Item1);

                    serviceCheckerForm.Invoke(new Action(() => { 
                        serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}";
                        serviceCheckerForm.status_toolStripStatusLabel.Text = "Check List..."; }));
                }
                else break;
            }
        }
        private static void checkMrinkaProxy()
        {
            int id = 0;
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                if (!ServiceChecker.checkStop)
                {
                    try
                    {
                        string url = @"http://188.166.72.201:8080/singleitem?i=" + Edit.replaceUrl(Main.checkList[i]);
                        string response = Request.GetRequest(url, Main.proxyList[id]);

                        parseMrinka(i, response);
                        serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}"; }));
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
                else break;
            }
        }
        private static void parseMrinka(int i, string response)
        {
            if (ServiceChecker.service_one == 0)
            {
                ServiceChecker.price_one.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                ServiceChecker.price2_one.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["buyOrder"].ToString()));
                ServiceChecker.stUpdated.Add(JObject.Parse(response)["steam"]["updated"].ToString());
            }
            else if (ServiceChecker.service_two == 0)
            {
                ServiceChecker.price_two.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                ServiceChecker.price2_two.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["buyOrder"].ToString()));
                ServiceChecker.stUpdated.Add(JObject.Parse(response)["steam"]["updated"].ToString());
                ServiceChecker.status.Add("Unknown");
            }
            if (ServiceChecker.service_one == 1)
            {
                ServiceChecker.price2_one.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceChecker.price_one.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceChecker.csmUpdated.Add(JObject.Parse(response)["csm"]["updated"].ToString());
            }
            else if (ServiceChecker.service_two == 1)
            {
                ServiceChecker.price2_two.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceChecker.price_two.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceChecker.csmUpdated.Add(JObject.Parse(response)["csm"]["updated"].ToString());
                if (Main.unavailable.Contains(Main.checkList[i]))
                    ServiceChecker.status.Add("Unavailable");
                else if (Main.overstock.Contains(Main.checkList[i]))
                    ServiceChecker.status.Add("Overstock");
                else
                    ServiceChecker.status.Add("Tradable");
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
                else break;
            }
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                if (!ServiceChecker.checkStop)
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
                else break;
            }
        }

        private static void createDTable()
        {
            serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Text = "Write to the table..."; }));

            DataTable table = new DataTable();
            for (int i = 0; i < serviceCheckerForm.servChecker_dataGridView.ColumnCount; ++i)
            {
                table.Columns.Add(new DataColumn(serviceCheckerForm.servChecker_dataGridView.Columns[i].Name));
                serviceCheckerForm.servChecker_dataGridView.Columns[i].DataPropertyName = serviceCheckerForm.servChecker_dataGridView.Columns[i].Name;
            }
            table.Columns[6].DataType = typeof(Double);
            table.Columns[7].DataType = typeof(Double);
            for (int i = 0; i < Main.checkList.Count; i++)
            {
                if (!ServiceChecker.checkStop)
                {
                    ServiceChecker.precent.Add(Math.Round(((ServiceChecker.price2_two[i] - ServiceChecker.price_one[i]) / ServiceChecker.price_one[i]) * 100, 2));
                    double difference = Math.Round(ServiceChecker.price2_two[i] - ServiceChecker.price_one[i], 2);
                    ServiceChecker.difference.Add(Edit.removeSymbol(Edit.funcConvert(difference, Main.course)));

                    if (ServiceChecker.price_one[i] == 0)
                        ServiceChecker.precent.Add(0);
                    if (ServiceChecker.service_one == 0) //steam buyorder
                    {
                        ServiceChecker.precent.Add(Math.Round(((ServiceChecker.price2_two[i] - ServiceChecker.price2_one[i]) / ServiceChecker.price2_one[i]) * 100, 2));
                        ServiceChecker.difference.Add(Math.Round(ServiceChecker.price2_two[i] - ServiceChecker.price2_one[i], 2));
                    }
                    table.Rows.Add(null,
                        Main.checkList[i],
                        ServiceChecker.price_one[i] + "$",
                        ServiceChecker.price2_one[i] + "$",
                        ServiceChecker.price_two[i] + "$",
                        ServiceChecker.price2_two[i] + "$",
                        ServiceChecker.precent[i],
                        ServiceChecker.difference[i],
                        ServiceChecker.status[i]);

                    serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.servChecker_dataGridView.Columns[1].HeaderText = $"Item - {i + 1}"; }));
                }
                else break;
            }
            serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.servChecker_dataGridView.DataSource = table; }));
        }
        public static void drawDTGView()
        {
            foreach (DataGridViewRow row in serviceCheckerForm.servChecker_dataGridView.Rows)
            {
                var item = row.Cells[1].Value.ToString();
                var price2_one = Edit.removeDol(row.Cells[3].Value.ToString());
                var precent = Convert.ToDouble(row.Cells[6].Value.ToString());
                var status = row.Cells[8].Value.ToString();
                if (precent >= 35)
                    row.Cells[6].Style.BackColor = Color.MediumSeaGreen;
                if (precent <= 25)
                    row.Cells[6].Style.BackColor = Color.OrangeRed;
                if (precent < 0)
                    row.Cells[6].Style.BackColor = Color.Red;
                if (precent == 0 | precent == -100)
                    row.Cells[6].Style.BackColor = Color.Gray;
                if (price2_one > Steam.balance_usd & ServiceChecker.service_one == 0)
                    row.Cells[4].Style.BackColor = Color.Crimson;
                if (item.Contains("Sticker") | item.Contains("Graffiti"))
                    row.Cells[0].Style.BackColor = Color.DeepSkyBlue;
                if (item.Contains("Souvenir"))
                    row.Cells[0].Style.BackColor = Color.Yellow;
                if (item.Contains("StatTrak"))
                    row.Cells[0].Style.BackColor = Color.Orange;
                if (item.Contains("★"))
                    row.Cells[0].Style.BackColor = Color.DarkViolet;
                if (BuyOrder.queue.Contains(Edit.replaceUrl(item)))
                {
                    row.Cells[1].Style.BackColor = Color.LimeGreen;
                    row.Cells[3].Style.BackColor = Color.LimeGreen;
                }
                if (BuyOrder.item.Contains(item))
                {
                    row.Cells[1].Style.BackColor = Color.CornflowerBlue;
                    row.Cells[8].Style.BackColor = Color.CornflowerBlue;
                    row.Cells[8].Value = "Ordered";
                }
                if (status.Contains("Overstock"))
                {
                    row.Cells[1].Style.BackColor = Color.OrangeRed;
                    row.Cells[8].Style.BackColor = Color.OrangeRed;
                }
                if (status.Contains("Unavailable"))
                {
                    row.Cells[1].Style.BackColor = Color.Red;
                    row.Cells[8].Style.BackColor = Color.Red;
                }
                row.Cells[2].Style.BackColor = Color.LightGray;
                row.Cells[4].Style.BackColor = Color.LightGray;
            }
        }

        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = serviceCheckerForm.servChecker_dataGridView.CurrentCell.RowIndex;
                int cell = serviceCheckerForm.servChecker_dataGridView.CurrentCell.ColumnIndex;
                string item = serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[1].Value.ToString();
                double sta = Edit.removeSymbol(serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[3].Value.ToString());

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
                            serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.LimeGreen;
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
                            serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.White;
                        }));
                    }
                }
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
        }
        public static void extractCsv(object state)
        {
            try
            {
                if (serviceCheckerForm.servChecker_dataGridView.Rows.Count > 0)
                {

                    string csv = null;
                    foreach (DataGridViewColumn column in serviceCheckerForm.servChecker_dataGridView.Columns)
                    {
                        csv += column.HeaderText + ',';
                    }
                    csv += "\r\n";
                    foreach (DataGridViewRow row in serviceCheckerForm.servChecker_dataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            csv += cell.Value;
                            csv += ",";
                        }
                        csv += "\r\n";
                    }
                    System.IO.File.WriteAllText(Application.StartupPath.Replace(@"\", @"\\") + $"extract\\serviceChecker_{DateTime.Now.ToString("yyyy.MM.dd_hh.mm")}.csv", Edit.replaceSymbols(csv));
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
                if (!ServiceChecker.checkStop)
                {
                    serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Visible = false; }));
                    MainPresenter.messageBalloonTip("Extraction was completed.");
                }
            }
        }
    }
}