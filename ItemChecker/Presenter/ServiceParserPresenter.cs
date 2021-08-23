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
    class ServiceParserPresenter
    {
        static int i;
        static DateTime start; 
        public static void checkMain(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                ThreadPool.QueueUserWorkItem(TimeLeft);
                if (ServiceParser.service_one < 2 | ServiceParser.service_two < 2)
                    if (GeneralConfig.Default.proxy & !String.IsNullOrEmpty(GeneralConfig.Default.proxyList))
                        checkMrinkaProxy();
                    else
                        checkMrinka();
                if (ServiceParser.service_one == 2 | ServiceParser.service_two == 2)
                    checkLootFarm();
                createDTable();
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
            finally
            {
                if (!ServiceParser.token.IsCancellationRequested)
                {
                    serviceParserForm.Invoke(new MethodInvoker(delegate {
                        serviceParserForm.status_toolStripStatusLabel.Visible = false;
                        serviceParserForm.serviceParser_dataGridView.Enabled = true;
                        serviceParserForm.serviceParser_dataGridView.Sort(serviceParserForm.serviceParser_dataGridView.Columns[6], ListSortDirection.Descending); }));
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
                        response = Get.MrinkaRequest(market_hash_name);
                        if (!response.Item2)
                        {
                            serviceParserForm.Invoke(new MethodInvoker(delegate { serviceParserForm.status_toolStripStatusLabel.Text = "Checking the list (429). Please Wait..."; }));
                            Thread.Sleep(30000);
                        }
                    }
                    while (!response.Item2);
                    serviceParserForm.Invoke(new MethodInvoker(delegate { serviceParserForm.status_toolStripStatusLabel.Text = "Checking the list..."; }));

                    parseJson(response.Item1);
                }
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                else
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        private static void checkMrinkaProxy()
        {
            start = DateTime.Now;
            int id = 0;
            for (i = 0; i < Main.checkList.Count; i++)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                try
                {
                    string market_hash_name = Edit.replaceUrl(Main.checkList[i]);
                    string url = @"http://188.166.72.201:8080/singleitem?i=" + market_hash_name;
                    string response = Get.Request(url, Main.proxyList[id]);
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
            if (ServiceParser.service_one == 0)
            {
                ServiceParser.price_one.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                ServiceParser.price2_one.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["buyOrder"].ToString()));
                ServiceParser.stUpdated.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["updated"].ToString()));
            }
            else if (ServiceParser.service_two == 0)
            {
                ServiceParser.price_two.Add(Convert.ToDecimal(JObject.Parse(response)["steam"]["sellOrder"].ToString()));
                decimal buyOrder = Convert.ToDecimal(JObject.Parse(response)["steam"]["buyOrder"].ToString()) * 0.8696m;
                ServiceParser.price2_two.Add(Math.Round(buyOrder, 2));
                ServiceParser.stUpdated.Add(Convert.ToDouble(JObject.Parse(response)["steam"]["updated"].ToString()));
                ServiceParser.status.Add("Tradable");
            }
            if (ServiceParser.service_one == 1)
            {
                ServiceParser.price2_one.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceParser.price_one.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceParser.csmUpdated.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["updated"].ToString()));
            }
            else if (ServiceParser.service_two == 1)
            {
                ServiceParser.price2_two.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["sell"].ToString()));
                ServiceParser.price_two.Add(Convert.ToDecimal(JObject.Parse(response)["csm"]["buy"]["0"].ToString()));
                ServiceParser.csmUpdated.Add(Convert.ToDouble(JObject.Parse(response)["csm"]["updated"].ToString()));

                if (Main.unavailable.Contains(Main.checkList[i]))
                    ServiceParser.status.Add("Unavailable");
                else if (Main.overstock.Contains(Main.checkList[i]))
                    ServiceParser.status.Add("Overstock");
                else
                    ServiceParser.status.Add("Tradable");
            }
        }
        private static void checkLootFarm()
        {
            start = DateTime.Now;
            var json = Get.Request("https://loot.farm/fullprice.json");
            JArray jArray = JArray.Parse(json);
            List<string> str = new();

            for (int i = 0; i < jArray.Count; i++)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                str.Add(jArray[i]["name"].ToString());
            }
            for (i = 0; i < Main.checkList.Count; i++)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                if (str.Contains(Main.checkList[i]))
                {
                    int id = str.IndexOf(Main.checkList[i]);
                    if (ServiceParser.service_one == 2)
                    {
                        decimal price = Convert.ToDecimal(jArray[id]["price"]) / 100;
                        ServiceParser.price_one.Add(price);
                        ServiceParser.price2_one.Add(Math.Round(price * 0.95m, 2));
                        int have = Convert.ToInt32(jArray[id]["have"]);
                    }
                    else if (ServiceParser.service_two == 2)
                    {
                        decimal price = Convert.ToDecimal(jArray[id]["price"]) / 100;
                        ServiceParser.price_two.Add(price);
                        ServiceParser.price2_two.Add(Math.Round(price * 0.95m, 2));

                        int have = Convert.ToInt32(jArray[id]["have"]);
                        int max = Convert.ToInt32(jArray[id]["max"]);
                        int count = max - have;
                        if (count > 0)
                            ServiceParser.status.Add("Tradable");
                        else if (count <= 0)
                            ServiceParser.status.Add("Overstock");
                    }
                }
                else
                {
                    if (ServiceParser.service_one == 2)
                    {
                        ServiceParser.price_one.Add(0);
                        ServiceParser.price2_one.Add(0);
                    }
                    else if (ServiceParser.service_two == 2)
                    {
                        ServiceParser.price_two.Add(0);
                        ServiceParser.price2_two.Add(0);
                        ServiceParser.status.Add("Unknown");
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
                    serviceParserForm.Invoke(new Action(() => {
                        serviceParserForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{Main.checkList.Count}";
                        serviceParserForm.Text = $"ServiceParser: {Edit.calcTimeLeft(start, Main.checkList.Count, i)}";
                    }));
                    Thread.Sleep(500);
                }
                serviceParserForm.Invoke(new Action(() => {
                    serviceParserForm.count_toolStripStatusLabel.Text = $"Count: {Main.checkList.Count}";
                    serviceParserForm.Text = "ServiceParser";
                }));
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                else
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }            
        }

        private static void createDTable()
        {
            try
            {
                serviceParserForm.Invoke(new MethodInvoker(delegate { serviceParserForm.status_toolStripStatusLabel.Text = "Write to the table..."; }));

                for (int i = 0; i < Main.checkList.Count; i++)
                {
                    if (ServiceParser.service_one == 0) //steam -> (any)
                    {
                        decimal precent = Edit.Precent(ServiceParser.price2_one[i], ServiceParser.price2_two[i]);
                        decimal difference = Edit.Difference(ServiceParser.price2_two[i], ServiceParser.price2_one[i], GeneralConfig.Default.currency);
                        ServiceParser.precent.Add(precent);
                        ServiceParser.difference.Add(difference);
                    }
                    else //(any) -> (any)
                    {
                        decimal precent = Edit.Precent(ServiceParser.price_one[i], ServiceParser.price2_two[i]);
                        decimal difference = Edit.Difference(ServiceParser.price2_two[i], ServiceParser.price_one[i], GeneralConfig.Default.currency);
                        ServiceParser.precent.Add(precent);
                        ServiceParser.difference.Add(difference);
                    }

                    ServiceParser.dataTable.Rows.Add(null,
                        Main.checkList[i],
                        ServiceParser.price_one[i],
                        ServiceParser.price2_one[i],
                        ServiceParser.price_two[i],
                        ServiceParser.price2_two[i],
                        ServiceParser.precent[i],
                        ServiceParser.difference[i],
                        ServiceParser.status[i]);

                    serviceParserForm.Invoke(new MethodInvoker(delegate { serviceParserForm.serviceParser_dataGridView.Columns[1].HeaderText = $"Item - {i + 1}"; }));
                }
                serviceParserForm.Invoke(new MethodInvoker(delegate { serviceParserForm.serviceParser_dataGridView.DataSource = ServiceParser.dataTable; }));
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                else
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        public static void drawDTGView()
        {
            try
            {
                foreach (DataGridViewRow row in serviceParserForm.serviceParser_dataGridView.Rows)
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
                    if (price2_one > Steam.balance_usd & ServiceParser.service_one == 0)
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
                    if (ServiceParser.service_one == 0) //steam -> (any)
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
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                else
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        public static void columnDTable()
        {
            foreach (DataGridViewColumn column in serviceParserForm.serviceParser_dataGridView.Columns)
            {
                if (ServiceParser.dataTable.Columns.Contains(column.Name))
                    break;
                ServiceParser.dataTable.Columns.Add(column.Name);
                if (column.Index >= 2 & column.Index <= 7)
                    ServiceParser.dataTable.Columns[column.Index].DataType = typeof(decimal);
            }
        }

        public static void Filter(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                object[] args = state as object[];
                string str = args[0].ToString();
                DataGridView dataGridView = serviceParserForm.serviceParser_dataGridView;

                DataView dataView = ServiceParser.dataTable.DefaultView;
                dataView.RowFilter = str;
                DataTable dt = dataView.ToTable();
                Filters.filter = str;

                serviceParserForm.Invoke(new MethodInvoker(delegate { 
                    dataGridView.DataSource = dt;
                    dataGridView.Columns[1].HeaderText = $"Item - {dt.Rows.Count}";
                    if (str != string.Empty)
                        dataGridView.Sort(dataGridView.Columns[6], ListSortDirection.Descending); }));
                drawDTGView();
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        public static void ClearAll(bool clearDTable, bool data)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(Filter, new object[] { string.Empty });
                Filters._clearAll();

                if (clearDTable)
                    ServiceParser.dataTable.Clear();
                if (data)
                    ServiceParser._clear();

                serviceParserForm.Invoke(new MethodInvoker(delegate {
                    serviceParserForm.quickCheck_textBox.Clear();
                    serviceParserForm.search_textBox.Clear(); }));
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    return;
                else
                    Exceptions.errorLog(exp, Main.assemblyVersion);
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
                var json = Get.Request("https://loot.farm/fullprice.json");
                JArray fullPriceLF = JArray.Parse(json);

                int count = fullPriceLF.FirstOrDefault(x => x.Value<string>("name") == item).Value<int>("have");
                serviceParserForm.Invoke(new MethodInvoker(delegate {
                    serviceParserForm.availability_toolStripStatusLabel.Text = $"Availability: {count}";
                    if (count > 0) {
                        serviceParserForm.availability_toolStripStatusLabel.ForeColor = Color.Green;
                        serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.MediumSeaGreen;
                    }
                    else {
                        serviceParserForm.availability_toolStripStatusLabel.ForeColor = Color.OrangeRed;
                        serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.LightSalmon;
                    } }));
            }
            catch (Exception exp)
            {
                if (!ServiceParser.token.IsCancellationRequested)
                    Exceptions.errorLog(exp, Main.assemblyVersion);
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
                var json = Get.inventoriesCsMoney(market_hash_name);
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
                serviceParserForm.Invoke(new MethodInvoker(delegate {
                    serviceParserForm.availability_toolStripStatusLabel.Text = $"Availability: {count}";
                    if (count > 0) {
                        serviceParserForm.availability_toolStripStatusLabel.ForeColor = Color.Green;
                        serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.MediumSeaGreen;
                    }
                    else {
                        serviceParserForm.availability_toolStripStatusLabel.ForeColor = Color.OrangeRed;
                        serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[9].Style.BackColor = Color.LightSalmon;
                    } }));
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = serviceParserForm.serviceParser_dataGridView.CurrentCell.RowIndex;
                int cell = serviceParserForm.serviceParser_dataGridView.CurrentCell.ColumnIndex;
                string item = serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[1].Value.ToString();
                decimal sta = Edit.removeSymbol(serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[3].Value.ToString());

                if (!BuyOrder.item.Contains(item) & cell != 3 & sta <= Steam.balance_usd)
                {
                    if (!BuyOrder.queue.Contains(item))
                    {
                        BuyOrder.queue_rub += Math.Round(sta * GeneralConfig.Default.currency, 2);
                        BuyOrder.queue.Add(item);
                        serviceParserForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub > BuyOrder.available_amount) 
                                mainForm.available_label.ForeColor = Color.Red;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.LimeGreen;
                        }));
                    }
                    else
                    {
                        BuyOrder.queue_rub -= Math.Round(sta * GeneralConfig.Default.currency, 2);
                        BuyOrder.queue.Remove(item);
                        serviceParserForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub <= BuyOrder.available_amount) 
                                mainForm.available_label.ForeColor = Color.Black;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LightGray;
                            serviceParserForm.serviceParser_dataGridView.Rows[row].Cells[3].Style.BackColor = Color.LightGray;
                        }));
                    }
                }
            }
            catch (Exception exp)
            {
                if (ServiceParser.token.IsCancellationRequested)
                    Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }

        public static void exportTxt(object state)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            DialogResult result = MessageBox.Show($"Add prices \"{serviceParserForm.serviceParser_dataGridView.Columns[2].HeaderText}\" to the list you create?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;

            serviceParserForm.Invoke(new MethodInvoker(delegate {
                serviceParserForm.status_toolStripStatusLabel.Text = "Export the list to *.txt...";
                serviceParserForm.status_toolStripStatusLabel.Visible = true;
                serviceParserForm.serviceParser_dataGridView.Enabled = false; }));

            DirectoryInfo dirInfo = new("extract");
            if (!dirInfo.Exists)
                dirInfo.Create();
            string str = null;
            foreach (DataGridViewRow row in serviceParserForm.serviceParser_dataGridView.Rows)
            {
                str += row.Cells[1].Value;
                if (result == DialogResult.Yes)
                    str += ";" + (int)(Convert.ToDecimal(row.Cells[2].Value) * 100);
                str += "\r\n";
            }
            str = str.Remove(str.Length - 2);
            File.WriteAllText($"extract/serviceParserList_{DateTime.Now:dd.MM.yyyy_hh.mm}.txt", str);

            serviceParserForm.Invoke(new MethodInvoker(delegate {
                serviceParserForm.status_toolStripStatusLabel.Visible = false;
                serviceParserForm.serviceParser_dataGridView.Enabled = true; }));
        }
        public static void exportCsv(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                if (serviceParserForm.serviceParser_dataGridView.Rows.Count > 0)
                {
                    string csv = null;
                    //services
                    csv += $"{ServiceParser.service_one},{ServiceParser.service_two}\r\n";
                    //Rows
                    foreach (DataGridViewRow row in serviceParserForm.serviceParser_dataGridView.Rows)
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
                    string path = Application.StartupPath + "extract";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    File.WriteAllText(path + $"\\serviceParser_{DateTime.Now.ToString("dd.MM.yyyy_hh.mm")}.csv", Edit.replaceSymbols(csv));
                }
            }
            catch (Exception exp)
            {
                if (!ServiceParser.token.IsCancellationRequested)
                {
                    string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    Exceptions.errorLog(exp, Main.assemblyVersion);
                    Exceptions.errorMessage(exp, currMethodName);
                }
            }
            finally
            {
                if (!ServiceParser.token.IsCancellationRequested)
                {
                    serviceParserForm.Invoke(new MethodInvoker(delegate {
                        serviceParserForm.status_toolStripStatusLabel.Visible = false;
                        serviceParserForm.serviceParser_dataGridView.Enabled = true; }));
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

                ServiceParser.service_one = Convert.ToInt32(service[0]);
                ServiceParser.service_two = Convert.ToInt32(service[1]);
                serviceParserForm.Invoke(new MethodInvoker(delegate {
                    serviceParserForm.firstSer_comboBox.SelectedIndex = ServiceParser.service_one;
                    serviceParserForm.secondSer_comboBox.SelectedIndex = ServiceParser.service_two;
                    serviceParserForm.services_toolStripStatusLabel.Text = $"From {serviceParserForm.firstSer_comboBox.Text} To {serviceParserForm.secondSer_comboBox.Text} ({fileName})";
                    serviceParserForm.services_toolStripStatusLabel.Visible = true;

                    for (int i = 2; i < 6; i++)
                        Filters.prices.Add(serviceParserForm.serviceParser_dataGridView.Columns[i].HeaderText);
                    serviceParserForm.count_toolStripStatusLabel.Text = $"Count: {lines.Length - 1}"; }));

                //rows
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] value = lines[i].Split(',');
                    DataRow row = ServiceParser.dataTable.NewRow();
                    for (int j = 0; j <= 8; j++)
                    {
                        row[j] = value[j].Replace(";", ",");
                        if (j >= 2 & j <= 7)
                            row[j] = Math.Round(Convert.ToDecimal(value[j]) / 100, 2);
                    }
                    ServiceParser.dataTable.Rows.Add(row);
                }
                serviceParserForm.Invoke(new MethodInvoker(delegate {
                    serviceParserForm.status_toolStripStatusLabel.Visible = false;
                    serviceParserForm.serviceParser_dataGridView.Columns[1].HeaderText = $"Item - {ServiceParser.dataTable.Rows.Count}";
                    serviceParserForm.serviceParser_dataGridView.DataSource = ServiceParser.dataTable;
                    serviceParserForm.serviceParser_dataGridView.Sort(serviceParserForm.serviceParser_dataGridView.Columns[6], ListSortDirection.Descending); }));
                drawDTGView();
            }
            catch (Exception exp)
            {
                if (!ServiceParser.token.IsCancellationRequested)
                {
                    string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    Exceptions.errorLog(exp, Main.assemblyVersion);
                    Exceptions.errorMessage(exp, currMethodName);
                }
            }
            finally
            {
                if (!ServiceParser.token.IsCancellationRequested)
                {
                    MainPresenter.messageBalloonTip("Importing was completed.");
                    Main.loading = false;
                }
            }
        }
    }
}