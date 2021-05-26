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
    public class LootFarmPresenter
    {
        public static void checkList(object state)
        {
            try
            {
                LootFarm.item.Clear();
                LootFarm.price.Clear();
                LootFarm.price_st.Clear();
                LootFarm.buy_order.Clear();
                LootFarm.get_price.Clear();
                LootFarm.precent.Clear();
                LootFarm.difference.Clear();
                LootFarm.status.Clear();

                getLootFarm();
                create();
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
            }
        }

        private static void getLootFarm()
        {
            var json = Request.getRequest("https://loot.farm/fullprice.json");
            JArray array = JArray.Parse(json);

            for (int i = 0; i < array.Count; i++)
            {
                try
                {
                    string str = array[i]["name"].ToString();
                    if (Main.checkList.Contains(str))
                    {
                        json = Request.mrinkaRequest(Edit.replaceUrl(str));

                        LootFarm.item.Add(str);
                        double price_lf = Convert.ToDouble(array[i]["price"]) / 100;
                        LootFarm.price.Add(price_lf);

                        LootFarm.price_st.Add(Convert.ToDouble(JObject.Parse(json)["steam"]["sellOrder"].ToString()));
                        LootFarm.get_price.Add(Math.Round(Convert.ToDouble(array[i]["price"]) / 100 * 0.97, 2));
                        double buy_order_st = Convert.ToDouble(JObject.Parse(json)["steam"]["buyOrder"].ToString());
                        LootFarm.buy_order.Add(buy_order_st);

                        LootFarm.precent.Add(Math.Round(((price_lf - buy_order_st) / buy_order_st) * 100, 2));
                        LootFarm.difference.Add(Math.Round(price_lf - buy_order_st, 2));

                        int have = Convert.ToInt32(array[i]["have"]);
                        int max = Convert.ToInt32(array[i]["max"]);
                        int count = max - have;
                        if (count > 0) LootFarm.status.Add("Tradable");
                        else if (count <= 0) LootFarm.status.Add("Overstock");
                    }
                }
                catch
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate { checkOwnListForm.status_toolStripStatusLabel.Text = "Check List (429). Wait 2 min..."; }));
                    i--;
                    Thread.Sleep(30000);
                    continue;
                }
            }
        }
        static void create()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            checkOwnListForm.Invoke(new MethodInvoker(delegate {
                checkOwnListForm.ownList_dataGridView.Rows.Clear();
                checkOwnListForm.ownList_dataGridView.Columns[6].ValueType = typeof(Double);
                checkOwnListForm.ownList_dataGridView.Columns[7].ValueType = typeof(Double);
                checkOwnListForm.status_toolStripStatusLabel.Text = "Create Table...";
            }));

            for (int i = 0; i < LootFarm.item.Count; i++)
            {
                checkOwnListForm.Invoke(new MethodInvoker(delegate {
                    checkOwnListForm.ownList_dataGridView.Rows.Add();
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Value = LootFarm.item[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[2].Value = LootFarm.price_st[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[3].Value = LootFarm.price[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Value = LootFarm.buy_order[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[5].Value = LootFarm.get_price[i] + "$";
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Value = LootFarm.precent[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[7].Value = LootFarm.difference[i];
                    checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = LootFarm.status[i];
                }));

                //color
                if (LootFarm.precent[i] >= 35) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.MediumSeaGreen; }));
                if (LootFarm.precent[i] <= 25) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.OrangeRed; }));
                if (LootFarm.precent[i] < 0) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red; }));
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[2].Style.BackColor = Color.LightGray; }));
                checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LightGray; }));
                if (LootFarm.buy_order[i] > Steam.balance_usd) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.Crimson; }));
                if (LootFarm.item[i].Contains("Souvenir")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Yellow; }));
                if (LootFarm.item[i].Contains("StatTrak")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Orange; }));
                if (LootFarm.item[i].Contains("★")) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DarkViolet; }));
                if (BuyOrder.queue.Contains(Edit.replaceUrl(LootFarm.item[i]))) checkOwnListForm.Invoke(new Action(() => { checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.LimeGreen; checkOwnListForm.ownList_dataGridView.Rows[i].Cells[4].Style.BackColor = Color.LimeGreen; }));
                if (BuyOrder.item.Contains(LootFarm.item[i]) || BuyOrder.ordered.Contains(LootFarm.item[i]))
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate {
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.CornflowerBlue;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.CornflowerBlue;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Value = "Ordered";
                    }));
                }
                if (LootFarm.status[i] == "Overstock")
                {
                    checkOwnListForm.Invoke(new MethodInvoker(delegate {
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[1].Style.BackColor = Color.OrangeRed;
                        checkOwnListForm.ownList_dataGridView.Rows[i].Cells[8].Style.BackColor = Color.OrangeRed;
                    }));
                }
                checkOwnListForm.ownList_statusStrip.Invoke(new Action(() => { checkOwnListForm.count_toolStripStatusLabel.Text = $"Count: {i + 1}/{LootFarm.item.Count}"; }));
            }
        }
    }
}
