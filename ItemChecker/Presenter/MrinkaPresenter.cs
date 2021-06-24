using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using System.Threading;
using ItemChecker.Support;
using ItemChecker.Net;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace ItemChecker.Presenter
{
    public class MrinkaPresenter
    {
        public static void checkList()
        {
            try
            {
                Mrinka._clear();
                for (int i = 0; i < Main.checkList.Count; i++)
                {
                    if (!CheckOwnListForm.checkStop)
                    {
                        Tuple<String, Boolean> response = Tuple.Create("", false);
                        do
                        {
                            response = Request.MrinkaRequest(Edit.replaceUrl(Main.checkList[i]));
                            if (!response.Item2)
                            {
                                checkOwnListForm.Invoke(new MethodInvoker(delegate { checkOwnListForm.status_toolStripStatusLabel.Text = "Check List (429). Please Wait..."; }));
                                Thread.Sleep(30000);
                            }
                        }
                        while (!response.Item2);

                        Mrinka.sellOrder.Add(Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["sellOrder"].ToString()));
                        double buyorder = Convert.ToDouble(JObject.Parse(response.Item1)["steam"]["buyOrder"].ToString());
                        Mrinka.stUpdated.Add(JObject.Parse(response.Item1)["steam"]["updated"].ToString());
                        Mrinka.buyOrder.Add(buyorder);

                        double csmsell = Convert.ToDouble(JObject.Parse(response.Item1)["csm"]["sell"].ToString());
                        Mrinka.csmBuy.Add(JObject.Parse(response.Item1)["csm"]["buy"]["0"].ToString());
                        Mrinka.csmUpdated.Add(JObject.Parse(response.Item1)["csm"]["updated"].ToString());
                        Mrinka.csmSell.Add(csmsell);

                        Mrinka.precent.Add(Math.Round(((csmsell - buyorder) / buyorder) * 100, 2));
                        Mrinka.difference.Add(Math.Round(csmsell - buyorder, 2));

                        checkOwnListForm.Invoke(new MethodInvoker(delegate { checkOwnListForm.ownList_dataGridView.Columns[1].HeaderText = $"Item - {i+1}"; }));
                    }
                    else return;
                }
                createList();
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.version);
            }
            finally
            {
                MainPresenter.messageBalloonTip();
            }
        }
        private static void createList()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            checkOwnListForm.Invoke(new MethodInvoker(delegate {
                checkOwnListForm.ownList_dataGridView.Rows.Clear();
                checkOwnListForm.ownList_dataGridView.Columns[6].ValueType = typeof(Double);
                checkOwnListForm.ownList_dataGridView.Columns[7].ValueType = typeof(Double);
                checkOwnListForm.status_toolStripStatusLabel.Text = "Create Table...";
            }));

            for (int i = 0; i < Main.checkList.Count; i++)
            {
                checkOwnListForm.Invoke(new MethodInvoker(delegate {
                    checkOwnListForm.ownList_dataGridView.Rows.Add();
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Value = Main.checkList[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[2].Value = Mrinka.sellOrder[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[3].Value = Mrinka.csmBuy[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Value = Mrinka.buyOrder[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[5].Value = Mrinka.csmSell[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Value = Mrinka.precent[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[7].Value = Mrinka.difference[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Tradable";
                }));

                //color
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.LightGray; }));
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LightGray; }));
                if (Mrinka.precent[i] >= 35) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.MediumSeaGreen; }));
                if (Mrinka.precent[i] <= 25) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.OrangeRed; }));
                if (Mrinka.precent[i] < 0) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red; }));
                if (Mrinka.buyOrder[i] > Steam.balance_usd) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Crimson; }));
                if (Main.checkList[i].Contains("Sticker") || Main.checkList[i].Contains("Graffiti")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue; }));
                if (Main.checkList[i].Contains("Souvenir")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                if (Main.checkList[i].Contains("StatTrak")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                if (Main.checkList[i].Contains("★")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                if (BuyOrder.queue.Contains(Edit.replaceUrl(Main.checkList[i]))) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.LimeGreen; checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LimeGreen; }));
                if (BuyOrder.item.Contains(Main.checkList[i]))
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate {
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.CornflowerBlue;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.CornflowerBlue;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Ordered";
                    }));
                }
                if (Main.overstock.Contains(Main.checkList[i]))
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate {
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.OrangeRed;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.OrangeRed;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Overstock";
                    }));
                }
                if (Main.unavailable.Contains(Main.checkList[i]))
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate {
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.Red;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Unavailable";
                    }));
                }
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.count_toolStripStatusLabel.Text = $"Count: {i+1}/{Main.checkList.Count}"; }));
            }
        }

        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = checkOwnListForm.ownList_dataGridView.CurrentCell.RowIndex;
                int cell = checkOwnListForm.ownList_dataGridView.CurrentCell.ColumnIndex;
                string item = checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Value.ToString();
                double sta = Edit.removeSymbol(checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Value.ToString());

                if (!BuyOrder.item.Contains(item) & cell != 4 & sta <= Steam.balance_usd)
                {
                    if (!BuyOrder.queue.Contains(item))
                    {
                        BuyOrder.queue_rub += Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Add(item);
                        checkOwnListForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub > BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Red;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Style.BackColor = Color.LimeGreen; }));
                    }
                    else
                    {
                        BuyOrder.queue_rub -= Math.Round(sta * Main.course, 2);
                        BuyOrder.queue.Remove(item);
                        checkOwnListForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_rub < BuyOrder.available_amount) mainForm.available_label.ForeColor = Color.Black;
                            mainForm.queue_label.Text = $"Queue: {BuyOrder.queue_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue.Count;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Style.BackColor = Color.LightGray; }));
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