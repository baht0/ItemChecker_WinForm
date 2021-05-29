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
using System.ComponentModel;

namespace ItemChecker.Presenter
{
    public class MrinkaPresenter
    {
        public static void checkList(object state)
        {
            try
            {
                for (int i = 0; i < Main.checkList.Count; i++)
                {
                    try
                    {
                        var json = Request.mrinkaRequest(Edit.replaceUrl(Main.checkList[i]));

                        Mrinka.sellOrder.Add(Convert.ToDouble(JObject.Parse(json)["steam"]["sellOrder"].ToString()));
                        double buyorder = Convert.ToDouble(JObject.Parse(json)["steam"]["buyOrder"].ToString());
                        Mrinka.stUpdated.Add(JObject.Parse(json)["steam"]["updated"].ToString());
                        Mrinka.buyOrder.Add(buyorder);

                        double csmsell = Convert.ToDouble(JObject.Parse(json)["csm"]["sell"].ToString());
                        Mrinka.csmBuy.Add(JObject.Parse(json)["csm"]["buy"]["0"].ToString());
                        Mrinka.csmUpdated.Add(JObject.Parse(json)["csm"]["updated"].ToString());
                        Mrinka.csmSell.Add(csmsell);

                        Mrinka.precent.Add(Math.Round(((csmsell - buyorder) / buyorder) * 100, 2));
                        Mrinka.difference.Add(Math.Round(csmsell - buyorder, 2));
                    }
                    catch
                    {
                        checkOwnListForm.Invoke(new MethodInvoker(delegate { checkOwnListForm.status_toolStripStatusLabel.Text = "Check List (429). Wait 2 min..."; }));
                        i--;
                        Thread.Sleep(30000);
                        continue;
                    }
                }
                createList();
            }
            catch (Exception exp)
            {
                Edit.errorLog(exp, Main.version);
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
            }
            finally
            {
                checkOwnListForm.Invoke(new MethodInvoker(delegate {
                    checkOwnListForm.status_toolStripStatusLabel.Visible = false;
                    checkOwnListForm.ownList_menuStrip.Enabled = true;
                    checkOwnListForm.quick_button.Enabled = true;
                    checkOwnListForm.ownList_dataGridView.Sort(checkOwnListForm.ownList_dataGridView.Columns[6], ListSortDirection.Descending);
                }));
                Main.loading = false;
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
                if (Mrinka.precent[i] >= 35) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.MediumSeaGreen; }));
                if (Mrinka.precent[i] <= 25) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.OrangeRed; }));
                if (Mrinka.precent[i] < 0) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red; }));
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.LightGray; }));
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LightGray; }));
                if (Mrinka.buyOrder[i] > Steam.balance_usd) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Crimson; }));
                if (Main.checkList[i].Contains("Souvenir")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                if (Main.checkList[i].Contains("StatTrak")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                if (Main.checkList[i].Contains("★")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                if (BuyOrder.queue.Contains(Edit.replaceUrl(Main.checkList[i]))) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.LimeGreen; checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LimeGreen; }));
                if (BuyOrder.item.Contains(Main.checkList[i]) || BuyOrder.ordered.Contains(Main.checkList[i]))
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
                checkOwnListForm.ownList_statusStrip.Invoke(new Action(() => { checkOwnListForm.count_toolStripStatusLabel.Text = $"Count: {i+1}/{Main.checkList.Count}"; }));
            }
        }

        public static void addQueue(object state)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                int row = checkOwnListForm.ownList_dataGridView.CurrentCell.RowIndex;
                int cell = checkOwnListForm.ownList_dataGridView.CurrentCell.ColumnIndex;
                string iname = checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Value.ToString();
                double buyord = Convert.ToDouble(Edit.removeSymbol(checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Value.ToString()));

                if (!BuyOrder.item.Contains(iname) & !BuyOrder.ordered.Contains(iname) & cell != 4 & buyord <= Steam.balance_usd)
                {
                    string url = Edit.replaceUrl(iname);

                    if (!BuyOrder.queue.Contains(url))
                    {
                        BuyOrder.order_dol += buyord;
                        BuyOrder.order_rub += Math.Round(buyord * Main.course, 2);
                        BuyOrder.queue.Add(url);
                        BuyOrder.queue_count++;
                        checkOwnListForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_count > 0) checkOwnListForm.queue_toolStripStatusLabel.Visible = true;
                            if (BuyOrder.order_rub > BuyOrder.available_amount)
                            {
                                mainForm.available_label.ForeColor = Color.Red;
                                checkOwnListForm.queue_toolStripStatusLabel.ForeColor = Color.Red;
                            }
                            checkOwnListForm.queue_toolStripStatusLabel.Text = $"BuyOrder: {BuyOrder.order_dol}$ | {BuyOrder.order_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue_count;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.LimeGreen;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Style.BackColor = Color.LimeGreen;
                        }));
                    }
                    else
                    {
                        BuyOrder.order_dol -= buyord;
                        BuyOrder.order_rub -= Math.Round(buyord * Main.course, 2);
                        BuyOrder.queue.Remove(url);
                        BuyOrder.queue_count--;
                        checkOwnListForm.Invoke(new MethodInvoker(delegate {
                            if (BuyOrder.queue_count <= 0) checkOwnListForm.queue_toolStripStatusLabel.Visible = false;
                            if (BuyOrder.order_rub < BuyOrder.available_amount)
                            {
                                mainForm.available_label.ForeColor = Color.Black;
                                checkOwnListForm.queue_toolStripStatusLabel.ForeColor = Color.Black;
                            }
                            checkOwnListForm.queue_toolStripStatusLabel.Text = $"BuyOrder: {BuyOrder.order_dol}$ | {BuyOrder.order_rub}₽";
                            mainForm.queue_linkLabel.Text = "Place order: " + BuyOrder.queue_count;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[1].Style.BackColor = Color.White;
                            checkOwnListForm.ownList_dataGridView.Rows[row].Cells[4].Style.BackColor = Color.LightGray;
                        }));
                    }
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Edit.errorMessage(exp, currMethodName);
                Edit.errorLog(exp, Main.version);
            }
        }
    }
}