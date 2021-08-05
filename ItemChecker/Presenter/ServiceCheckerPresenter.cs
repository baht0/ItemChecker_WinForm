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
using System.IO;
using System.Linq;

namespace ItemChecker.Presenter
{
    class ServiceCheckerPresenter
    {
        static int i;
        static DateTime start; 
        public static void checkMain(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                ClearAll(true, true);

                ThreadPool.QueueUserWorkItem(TimeLeft);
                if (ServiceChecker.service_one < 2 | ServiceChecker.service_two < 2)
                    if (GeneralConfig.Default.proxy & !String.IsNullOrEmpty(GeneralConfig.Default.proxyList))
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
                    serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                        serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                        serviceCheckerForm.servChecker_dataGridView.Enabled = true;
                        serviceCheckerForm.servChecker_dataGridView.Sort(serviceCheckerForm.servChecker_dataGridView.Columns[6], ListSortDirection.Descending); }));
                    drawDTGView();
                    MainPresenter.messageBalloonTip();
                    Main.loading = false;
                }
            }
        }
        private static void checkMrinka()
        {
            try
            {
                start = DateTime.Now;
                for (i = 0; i < Main.checkList.Count; i++)
                {
                    
                    string market_hash_name = Edit.replaceUrl(Main.checkList[i]);
                    Tuple<String, Boolean> response = Tuple.Create("", false);
                    do
                    {
                        response = Request.MrinkaRequest(market_hash_name);
                        if (!response.Item2)
                        {
                            serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Text = "Checking the list (429). Please Wait..."; }));
                            Thread.Sleep(30000);
                        }
                    }
                    while (!response.Item2);
                    serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Text = "Checking the list..."; }));

                    parseJson(response.Item1);
                }
            }
            catch (Exception exp)
            {
                if (ServiceChecker.checkStop)
                    return;
                else
                    Exceptions.errorLog(exp, Main.version);
            }
        }
        private static void checkMrinkaProxy()
        {
            start = DateTime.Now;
            int id = 0;
            for (i = 0; i < Main.checkList.Count; i++)
            {
                if (ServiceChecker.checkStop)
                    return;
                try
                {
                    string market_hash_name = Edit.replaceUrl(Main.checkList[i]);
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + market_hash_name;
                    string response = Request.GetRequest(url, Main.proxyList[id]);
                    parseJson(response);
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
        private static void parseJson(string response)
        {
            if (ServiceChecker.service_one == 0)
            {
                ServiceChecker.price_one.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                ServiceChecker.price2_one.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["buyOrder"].ToString()));
                ServiceChecker.stUpdated.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["updated"].ToString()));
            }
            else if (ServiceChecker.service_two == 0)
            {
                ServiceChecker.price_two.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                decimal buyOrder = Convert.ToDecimal(JObject.Parse(response)["steam"]["buyOrder"].ToString()) * 0.8696m;
                ServiceChecker.price2_two.Add(Math.Round(buyOrder, 2));
                ServiceChecker.stUpdated.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["updated"].ToString()));
                ServiceChecker.status.Add("Tradable");
            }
            if (ServiceChecker.service_one == 1)
            {
                ServiceChecker.price2_one.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceChecker.price_one.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceChecker.csmUpdated.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["updated"].ToString()));
            }
            else if (ServiceChecker.service_two == 1)
            {
                ServiceChecker.price2_two.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceChecker.price_two.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceChecker.csmUpdated.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["updated"].ToString()));

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
            start = DateTime.Now;
            var json = Request.GetRequest("https://loot.farm/fullprice.json");
            JArray jArray = JArray.Parse(json);
            List<string> str = new();

            for (int i = 0; i < jArray.Count; i++)
            {
                if (ServiceChecker.checkStop)
                    return;
                str.Add(jArray[i]["name"].ToString());
            }
            for (i = 0; i < Main.checkList.Count; i++)
            {
                if (ServiceChecker.checkStop)
                    return;
                if (str.Contains(Main.checkList[i]))
                {
                    int id = str.IndexOf(Main.checkList[i]);
                    if (ServiceChecker.service_one == 2)
                    {
                        decimal price = Convert.ToDecimal(jArray[id]["price"]) / 100;
                        ServiceChecker.price_one.Add(price);
                        ServiceChecker.price2_one.Add(Math.Round(price * 0.95m, 2));
                        int have = Convert.ToInt32(jArray[id]["have"]);
                    }
                    else if (ServiceChecker.service_two == 2)
                    {
                        decimal price = Convert.ToDecimal(jArray[id]["price"]) / 100;
                        ServiceChecker.price_two.Add(price);
                        ServiceChecker.price2_two.Add(Math.Round(price * 0.95m, 2));

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
            }
        }       
        private static void TimeLeft(object state)
        {
            try
            {
                while (i < Main.checkList.Count)
                {
                    serviceCheckerForm.Invoke(new Action(() => {
                        serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}";
                        serviceCheckerForm.Text = $"ServiceChecker: {Edit.calcTimeLeft(start, Main.checkList.Count, i)}";
                    }));
                    Thread.Sleep(500);
                }
                serviceCheckerForm.Invoke(new Action(() => {
                    serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {Main.checkList.Count}";
                    serviceCheckerForm.Text = "ServiceChecker";
                }));
            }
            catch (Exception exp)
            {
                if (ServiceChecker.checkStop)
                    return;
                else
                    Exceptions.errorLog(exp, Main.version);
            }            
        }

        private static void createDTable()
        {
            try
            {
                serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.status_toolStripStatusLabel.Text = "Write to the table..."; }));

                for (int i = 0; i < Main.checkList.Count; i++)
                {
                    if (ServiceChecker.service_one == 0) //steam -> (any)
                    {
                        decimal precent = Edit.Precent(ServiceChecker.price2_one[i], ServiceChecker.price2_two[i]);
                        decimal difference = Edit.Difference(ServiceChecker.price2_two[i], ServiceChecker.price2_one[i], Main.course);
                        ServiceChecker.precent.Add(precent);
                        ServiceChecker.difference.Add(difference);
                    }
                    else //(any) -> (any)
                    {
                        decimal precent = Edit.Precent(ServiceChecker.price_one[i], ServiceChecker.price2_two[i]);
                        decimal difference = Edit.Difference(ServiceChecker.price2_two[i], ServiceChecker.price_one[i], Main.course);
                        ServiceChecker.precent.Add(precent);
                        ServiceChecker.difference.Add(difference);
                    }

                    ServiceChecker.dataTable.Rows.Add(null,
                        Main.checkList[i],
                        ServiceChecker.price_one[i],
                        ServiceChecker.price2_one[i],
                        ServiceChecker.price_two[i],
                        ServiceChecker.price2_two[i],
                        ServiceChecker.precent[i],
                        ServiceChecker.difference[i],
                        ServiceChecker.status[i]);

                    serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.servChecker_dataGridView.Columns[1].HeaderText = $"Item - {i + 1}"; }));
                }
                serviceCheckerForm.Invoke(new MethodInvoker(delegate { serviceCheckerForm.servChecker_dataGridView.DataSource = ServiceChecker.dataTable; }));
            }
            catch (Exception exp)
            {
                if (ServiceChecker.checkStop)
                    return;
                else
                    Exceptions.errorLog(exp, Main.version);
            }
        }
        public static void drawDTGView()
        {
            try
            {
                foreach (DataGridViewRow row in serviceCheckerForm.servChecker_dataGridView.Rows)
                {
                    var item = row.Cells[1].Value.ToString();
                    var price2_one = Edit.removeDol(row.Cells[3].Value.ToString());
                    var precent = Convert.ToDecimal(row.Cells[6].Value.ToString());
                    var status = row.Cells[8].Value.ToString();
                    if (precent >= 20)
                        row.Cells[6].Style.BackColor = Color.MediumSeaGreen;
                    if (precent <= 0)
                        row.Cells[6].Style.BackColor = Color.OrangeRed;
                    if (precent < -15)
                        row.Cells[6].Style.BackColor = Color.Red;
                    if (precent == 0 | precent == -100)
                        row.Cells[6].Style.BackColor = Color.Gray;
                    if (price2_one > Steam.balance_usd & ServiceChecker.service_one == 0)
                        row.Cells[3].Style.BackColor = Color.Crimson;
                    if (BuyOrder.queue.Contains(item))
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
                    row.Cells[5].Style.BackColor = Color.LightGray;
                    if (ServiceChecker.service_one == 0) //steam -> (any)
                    {
                        row.Cells[3].Style.BackColor = Color.LightGray;
                        row.Cells[2].Style.BackColor = Color.White;
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
            catch (Exception exp)
            {
                if (ServiceChecker.checkStop)
                    return;
                else
                    Exceptions.errorLog(exp, Main.version);
            }
        }
        public static void columnDTable()
        {
            foreach (DataGridViewColumn column in serviceCheckerForm.servChecker_dataGridView.Columns)
            {
                if (ServiceChecker.dataTable.Columns.Contains(column.Name))
                    break;
                ServiceChecker.dataTable.Columns.Add(column.Name);
                if (column.Index >= 2 & column.Index <= 7)
                    ServiceChecker.dataTable.Columns[column.Index].DataType = typeof(decimal);
            }
        }

        public static void Filter(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                object[] args = state as object[];
                string str = args[0].ToString();
                DataGridView dataGridView = serviceCheckerForm.servChecker_dataGridView;

                DataView dataView = ServiceChecker.dataTable.DefaultView;
                dataView.RowFilter = str;
                DataTable dt = dataView.ToTable();
                Filters.filter = str;

                serviceCheckerForm.Invoke(new MethodInvoker(delegate { 
                    dataGridView.DataSource = dt;
                    dataGridView.Columns[1].HeaderText = $"Item - {dt.Rows.Count}";
                    if (str != string.Empty)
                        dataGridView.Sort(dataGridView.Columns[6], ListSortDirection.Descending); }));
                drawDTGView();
            }
            catch (Exception exp)
            {
                if(!ServiceChecker.checkStop)
                    Exceptions.errorLog(exp, Main.version);
            }
        }
        public static void ClearAll(bool clearDTable, bool data)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(Filter, new object[] { string.Empty });
                Filters._clearAll();

                if (clearDTable)
                    MainPresenter.clearDTableRows(serviceCheckerForm.servChecker_dataGridView);
                if (data)
                    ServiceChecker._clear();

                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.quickCheck_textBox.Clear();
                    serviceCheckerForm.search_textBox.Clear(); }));
            }
            catch (Exception exp)
            {
                if (ServiceChecker.checkStop)
                    return;
                else
                    Exceptions.errorLog(exp, Main.version);
            }
        }

        //other
        public static void checkLootFarmItem(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                object[] args = state as object[];
                string item = args[0].ToString();
                int row = Convert.ToInt32(args[1]);
                var json = Request.GetRequest("https://loot.farm/fullprice.json");
                JArray fullPriceLF = JArray.Parse(json);

                int count = fullPriceLF.FirstOrDefault(x => x.Value<string>("name") == item).Value<int>("have");
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.availability_toolStripStatusLabel.Text = $"Availability: {count}";
                    if (count > 0) {
                        serviceCheckerForm.availability_toolStripStatusLabel.ForeColor = Color.Green;
                        serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.MediumSeaGreen;
                    }
                    else {
                        serviceCheckerForm.availability_toolStripStatusLabel.ForeColor = Color.OrangeRed;
                        serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.LightSalmon;
                    } }));
            }
            catch (Exception exp)
            {
                if (!ServiceChecker.checkStop)
                    Exceptions.errorLog(exp, Main.version);
            }            
        }
        public static void checkCsmItem(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                object[] args = state as object[];
                string item_name = args[0].ToString();
                int row = Convert.ToInt32(args[1]);

                string market_hash_name = Edit.replaceUrl(item_name);
                var json = Request.inventoriesCsMoney(market_hash_name);
                int count = 0;

                if (!json.Contains("error"))
                {
                    json = JObject.Parse(json)["items"].ToString();
                    JArray items = JArray.Parse(json);
                    foreach (JObject item in items)
                    {
                        if ((string)item["fullName"] != item_name)
                            continue;
                        if (item.ContainsKey("stackSize"))
                            count += Convert.ToInt32(item["stackSize"].ToString());
                        else
                            count++;
                    }
                }
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.availability_toolStripStatusLabel.Text = $"Availability: {count}";
                    if (count > 0) {
                        serviceCheckerForm.availability_toolStripStatusLabel.ForeColor = Color.Green;
                        serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.MediumSeaGreen;
                    }
                    else {
                        serviceCheckerForm.availability_toolStripStatusLabel.ForeColor = Color.OrangeRed;
                        serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.LightSalmon;
                    } }));
            }
            catch (Exception exp)
            {
                if (!ServiceChecker.checkStop)
                    Exceptions.errorLog(exp, Main.version);
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
                decimal sta = Edit.removeSymbol(serviceCheckerForm.servChecker_dataGridView.Rows[row].Cells[3].Value.ToString());

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
                if(ServiceChecker.checkStop)
                    Exceptions.errorLog(exp, Main.version);
            }
        }

        public static void exportTxt(object state)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            DialogResult result = MessageBox.Show($"Add prices \"{serviceCheckerForm.servChecker_dataGridView.Columns[2].HeaderText}\" to the list you create?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;

            serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                serviceCheckerForm.status_toolStripStatusLabel.Text = "Export the list to *.txt...";
                serviceCheckerForm.status_toolStripStatusLabel.Visible = true;
                serviceCheckerForm.servChecker_dataGridView.Enabled = false; }));

            DirectoryInfo dirInfo = new("extract");
            if (!dirInfo.Exists)
                dirInfo.Create();
            string str = null;
            foreach (DataGridViewRow row in serviceCheckerForm.servChecker_dataGridView.Rows)
            {
                str += row.Cells[1].Value;
                if (result == DialogResult.Yes)
                    str += ";" + (int)(Convert.ToDecimal(row.Cells[2].Value) * 100);
                str += "\r\n";
            }
            str = str.Remove(str.Length - 2);
            File.WriteAllText($"extract/serviceCheckerList_{DateTime.Now:dd.MM.yyyy_hh.mm}.txt", str);

            serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                serviceCheckerForm.servChecker_dataGridView.Enabled = true; }));
        }
        public static void exportCsv(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                if (serviceCheckerForm.servChecker_dataGridView.Rows.Count > 0)
                {
                    string csv = null;
                    //services
                    csv += $"{ServiceChecker.service_one},{ServiceChecker.service_two}\r\n";
                    //Rows
                    foreach (DataGridViewRow row in serviceCheckerForm.servChecker_dataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var value = cell.Value;
                            if (value.ToString().Contains(","))
                            {
                                value = value.ToString().Replace(",", ";");
                            }
                            if(cell.ColumnIndex >= 2 & cell.ColumnIndex <= 7)
                            {
                                value = (int)(Convert.ToDecimal(value) * 100);
                            }
                            csv += value;
                            csv += ",";
                        }
                        csv = csv.Remove(csv.Length - 2);
                        csv += "\r\n";
                    }
                    File.WriteAllText(Application.StartupPath.Replace(@"\", @"\\") + $"extract\\serviceChecker_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.csv", Edit.replaceSymbols(csv));
                }
            }
            catch (Exception exp)
            {
                if (!ServiceChecker.checkStop)
                {
                    string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    Exceptions.errorLog(exp, Main.version);
                    Exceptions.errorMessage(exp, currMethodName);
                }
            }
            finally
            {
                if (!ServiceChecker.checkStop)
                {
                    serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                        serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                        serviceCheckerForm.servChecker_dataGridView.Enabled = true; }));
                    MainPresenter.messageBalloonTip("Extraction was completed.");
                    Main.loading = false;
                }
            }
        }
        public static void importCsv(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                object[] args = state as object[];
                string filePath = args[0].ToString();
                string fileName = args[1].ToString();
                string[] lines = File.ReadAllLines(filePath);
                string[] service = lines[0].Split(',');
                ClearAll(true, true);

                ServiceChecker.service_one = Convert.ToInt32(service[0]);
                ServiceChecker.service_two = Convert.ToInt32(service[1]);
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.firstSer_comboBox.SelectedIndex = ServiceChecker.service_one;
                    serviceCheckerForm.secondSer_comboBox.SelectedIndex = ServiceChecker.service_two;
                    serviceCheckerForm.services_toolStripStatusLabel.Text = $"From {serviceCheckerForm.firstSer_comboBox.Text} To {serviceCheckerForm.secondSer_comboBox.Text} ({fileName})";
                    serviceCheckerForm.services_toolStripStatusLabel.Visible = true;

                    for (int i = 2; i < 6; i++)
                        Filters.prices.Add(serviceCheckerForm.servChecker_dataGridView.Columns[i].HeaderText);
                    serviceCheckerForm.count_toolStripStatusLabel.Text = $"Count: {lines.Length - 1}"; }));

                //rows
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] value = lines[i].Split(',');
                    DataRow row = ServiceChecker.dataTable.NewRow();
                    for (int j = 0; j <= 8; j++)
                    {
                        row[j] = value[j].Replace(";", ",");
                        if (j >= 2 & j <= 7)
                            row[j] = Math.Round(Convert.ToDecimal(value[j]) / 100, 2);
                    }
                    ServiceChecker.dataTable.Rows.Add(row);
                }
                serviceCheckerForm.Invoke(new MethodInvoker(delegate {
                    serviceCheckerForm.status_toolStripStatusLabel.Visible = false;
                    serviceCheckerForm.servChecker_dataGridView.Columns[1].HeaderText = $"Item - {ServiceChecker.dataTable.Rows.Count}";
                    serviceCheckerForm.servChecker_dataGridView.DataSource = ServiceChecker.dataTable;
                    serviceCheckerForm.servChecker_dataGridView.Sort(serviceCheckerForm.servChecker_dataGridView.Columns[6], ListSortDirection.Descending); }));
                drawDTGView();
            }
            catch (Exception exp)
            {
                if (!ServiceChecker.checkStop)
                {
                    string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    Exceptions.errorLog(exp, Main.version);
                    Exceptions.errorMessage(exp, currMethodName);
                }
            }
            finally
            {
                if (!ServiceChecker.checkStop)
                {
                    MainPresenter.messageBalloonTip("Importing was completed.");
                    Main.loading = false;
                }
            }
        }
    }
}