using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Support;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ItemChecker
{
    public partial class FiltersForm : Form
    {
        public FiltersForm()
        {
            InitializeComponent();
        }
        private void FiltersForm_Shown(object sender, EventArgs e)
        {
            //general
            foreach (int index in Filters.category)
                category_checkedListBox.SetItemCheckState(index, CheckState.Checked);
            foreach (int index in Filters.status)
                status_checkedListBox.SetItemCheckState(index, CheckState.Checked);
            foreach (int index in Filters.exterior)
                exterior_checkedListBox.SetItemCheckState(index, CheckState.Checked);
            foreach (int index in Filters.types)
                types_checkedListBox.SetItemCheckState(index, CheckState.Checked);

            hide_checkedListBox.SetItemCheckState(0, Filters.hide_category);
            hide_checkedListBox.SetItemCheckState(1, Filters.hide_status);
            hide_checkedListBox.SetItemCheckState(2, Filters.hide_exterior);
            hide_checkedListBox.SetItemCheckState(3, Filters.hide_types);

            //price
            if (Filters.prices.Any())
            {
                Price1_checkBox.Text = Filters.prices[0];
                Price2_checkBox.Text = Filters.prices[1];
                Price3_checkBox.Text = Filters.prices[2];
                Price4_checkBox.Text = Filters.prices[3];
            }

            Price1_checkBox.Checked = Filters.price_1;
            Price2_checkBox.Checked = Filters.price_2;
            Price3_checkBox.Checked = Filters.price_3;
            Price4_checkBox.Checked = Filters.price_4;

            price1From_numericUpDown.Value = Filters.price_1From;
            price1To_numericUpDown.Value = Filters.price_1To;
            price2From_numericUpDown.Value = Filters.price_2From;
            price2To_numericUpDown.Value = Filters.price_2To;
            price3From_numericUpDown.Value = Filters.price_3From;
            price3To_numericUpDown.Value = Filters.price_3To;
            price4From_numericUpDown.Value = Filters.price_4From;
            price4To_numericUpDown.Value = Filters.price_4To;
            //other
            precentFrom_numericUpDown.Value = Filters.precentFrom;
            precentTo_numericUpDown.Value = Filters.precentTo;

            diffFrom_numericUpDown.Value = Filters.diffFrom;
            diffTo_numericUpDown.Value = Filters.diffTo;

            hide0_checkBox.Checked = Filters.hide_0;
            hide100_checkBox.Checked = Filters.hide_100;

            close_checkBox.Checked = Filters.close;
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            if (!Main.loading & ServiceChecker.dataTable != null)
            {
                ServiceCheckerPresenter.ClearAll(false, false);
                clear_form();
            }

        }
        private void clear_form()
        {
            foreach (int index in category_checkedListBox.CheckedIndices)
                category_checkedListBox.SetItemCheckState(index, CheckState.Unchecked);
            foreach (int index in status_checkedListBox.CheckedIndices)
                status_checkedListBox.SetItemCheckState(index, CheckState.Unchecked);
            foreach (int index in exterior_checkedListBox.CheckedIndices)
                exterior_checkedListBox.SetItemCheckState(index, CheckState.Unchecked);
            foreach (int index in types_checkedListBox.CheckedIndices)
                types_checkedListBox.SetItemCheckState(index, CheckState.Unchecked);

            hide_checkedListBox.SetItemCheckState(0, Filters.hide_category);
            hide_checkedListBox.SetItemCheckState(1, Filters.hide_status);
            hide_checkedListBox.SetItemCheckState(2, Filters.hide_exterior);
            hide_checkedListBox.SetItemCheckState(3, Filters.hide_types);

            if (Filters.prices.Any())
            {
                Price1_checkBox.Text = Filters.prices[0];
                Price2_checkBox.Text = Filters.prices[1];
                Price3_checkBox.Text = Filters.prices[2];
                Price4_checkBox.Text = Filters.prices[3];
            }
            else
            {
                Price1_checkBox.Text = "Price_1";
                Price2_checkBox.Text = "Price_2";
                Price3_checkBox.Text = "Price_3";
                Price4_checkBox.Text = "Price_4";
            }

            Price1_checkBox.Checked = Filters.price_1;
            Price2_checkBox.Checked = Filters.price_2;
            Price3_checkBox.Checked = Filters.price_3;
            Price4_checkBox.Checked = Filters.price_4;

            price1From_numericUpDown.Value = Filters.price_1From;
            price1To_numericUpDown.Value = Filters.price_1To;
            price2From_numericUpDown.Value = Filters.price_2From;
            price2To_numericUpDown.Value = Filters.price_2To;
            price3From_numericUpDown.Value = Filters.price_3From;
            price3To_numericUpDown.Value = Filters.price_3To;
            price4From_numericUpDown.Value = Filters.price_4From;
            price4To_numericUpDown.Value = Filters.price_4To;
            //other
            precentFrom_numericUpDown.Value = Filters.precentFrom;
            precentTo_numericUpDown.Value = Filters.precentTo;

            diffFrom_numericUpDown.Value = Filters.diffFrom;
            diffTo_numericUpDown.Value = Filters.diffTo;

            hide0_checkBox.Checked = Filters.hide_0;
            hide100_checkBox.Checked = Filters.hide_100;

            category_checkedListBox.ClearSelected();
            status_checkedListBox.ClearSelected();
            exterior_checkedListBox.ClearSelected();
            types_checkedListBox.ClearSelected();
            hide_checkedListBox.ClearSelected();
        }
        private void close_button_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void apply_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Main.loading & ServiceChecker.dataTable.Rows.Count > 1)
                {
                    Filters.filter = string.Empty;
                    //category
                    if (category_checkedListBox.CheckedItems.Count > 0)
                    {
                        if (category_checkedListBox.GetItemCheckState(0) == CheckState.Unchecked)
                        {
                            string not = null;
                            if (hide_checkedListBox.GetItemCheckState(0) == CheckState.Checked)
                                not = "NOT";
                            Filters.filter += createFilter(category_checkedListBox, "item_Column", not);
                        }
                        else if (category_checkedListBox.GetItemCheckState(0) == CheckState.Checked)
                        {
                            for (int i = 1; i < 5; i++)
                                Filters.filter += $"AND item_Column NOT LIKE '%{category_checkedListBox.Items[i]}%'";
                            for (int i = 2; i < 12; i++)
                                Filters.filter += $"AND item_Column NOT LIKE '%{types_checkedListBox.Items[i]}%'";
                        }
                    }
                    //status
                    if (status_checkedListBox.CheckedItems.Count > 0)
                    {
                        string not = null;
                        if (hide_checkedListBox.GetItemCheckState(1) == CheckState.Checked)
                            not = "NOT";
                        Filters.filter += createFilter(status_checkedListBox, "status_Column", not);
                    }
                    //exterior
                    if (exterior_checkedListBox.CheckedItems.Count > 0)
                    {
                        string not = null;
                        if (hide_checkedListBox.GetItemCheckState(2) == CheckState.Checked)
                            not = "NOT";
                        Filters.filter += createFilter(exterior_checkedListBox, "item_Column", not);
                    }
                    //types
                    if (types_checkedListBox.CheckedItems.Count > 0)
                    {
                        if (types_checkedListBox.GetItemCheckState(0) == CheckState.Unchecked)
                        {
                            string not = null;
                            if (hide_checkedListBox.GetItemCheckState(3) == CheckState.Checked)
                                not = "NOT";
                            Filters.filter += createFilter(types_checkedListBox, "item_Column", not);
                        }
                        else if (types_checkedListBox.GetItemCheckState(0) == CheckState.Checked)
                            for (int i = 2; i < 12; i++)
                                Filters.filter += $"AND item_Column NOT LIKE '%{types_checkedListBox.Items[i]}%'";
                    }
                    //price
                    if (Price1_checkBox.Checked)
                        Filters.filter += createFilter(price1From_numericUpDown, price1To_numericUpDown, "price1_Column");
                    if (Price2_checkBox.Checked)
                        Filters.filter += createFilter(price2From_numericUpDown, price2To_numericUpDown, "price2_Column");
                    if (Price3_checkBox.Checked)
                        Filters.filter += createFilter(price3From_numericUpDown, price3To_numericUpDown, "price3_Column");
                    if (Price4_checkBox.Checked)
                        Filters.filter += createFilter(price4From_numericUpDown, price4To_numericUpDown, "price4_Column");
                    //other
                    if (precentFrom_numericUpDown.Value != 0)
                        Filters.filter += $"AND precent_Column > {precentFrom_numericUpDown.Value}";
                    if (precentTo_numericUpDown.Value != 0)
                        Filters.filter += $"AND precent_Column < {precentTo_numericUpDown.Value}";
                    if (diffFrom_numericUpDown.Value != 0)
                        Filters.filter += $"AND difference_Column > {diffFrom_numericUpDown.Value}";
                    if (diffTo_numericUpDown.Value != 0)
                        Filters.filter += $"AND difference_Column < {diffTo_numericUpDown.Value}";
                    if (hide100_checkBox.Checked)
                        Filters.filter += $"AND precent_Column <> -100";
                    if (hide0_checkBox.Checked)
                        Filters.filter += $"AND precent_Column <> 0";

                    if (Filters.filter != string.Empty)
                        ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { Filters.filter.Remove(0, 4) });
                    else
                        ThreadPool.QueueUserWorkItem(ServiceCheckerPresenter.Filter, new object[] { string.Empty });

                    saveFilters();
                    if (!close_checkBox.Checked)
                        this.Close();
                }
            }
            catch (Exception exp)
            {
                string currMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Exceptions.errorLog(exp, Main.version);
                Exceptions.errorMessage(exp, currMethodName);
            }
        }
        private void saveFilters()
        {
            //general
            Filters._clear();
            foreach (int index in category_checkedListBox.CheckedIndices)
                Filters.category.Add(index);
            foreach (int index in status_checkedListBox.CheckedIndices)
                Filters.status.Add(index);
            foreach (int index in exterior_checkedListBox.CheckedIndices)
                Filters.exterior.Add(index);
            foreach (int index in types_checkedListBox.CheckedIndices)
                Filters.types.Add(index);

            Filters.hide_category = hide_checkedListBox.GetItemCheckState(0);
            Filters.hide_status = hide_checkedListBox.GetItemCheckState(1);
            Filters.hide_exterior = hide_checkedListBox.GetItemCheckState(2);
            Filters.hide_types = hide_checkedListBox.GetItemCheckState(3);

            //price
            Filters.price_1 = Price1_checkBox.Checked;
            Filters.price_2 = Price2_checkBox.Checked;
            Filters.price_3 = Price3_checkBox.Checked;
            Filters.price_4 = Price4_checkBox.Checked;

            Filters.price_1From = price1From_numericUpDown.Value;
            Filters.price_1To = price1To_numericUpDown.Value;
            Filters.price_2From = price2From_numericUpDown.Value;
            Filters.price_2To = price2To_numericUpDown.Value;
            Filters.price_3From = price3From_numericUpDown.Value;
            Filters.price_3To = price3To_numericUpDown.Value;
            Filters.price_4From = price4From_numericUpDown.Value;
            Filters.price_4To = price4To_numericUpDown.Value;
            //precent
            Filters.precentFrom = precentFrom_numericUpDown.Value;
            Filters.precentTo = precentTo_numericUpDown.Value;

            Filters.diffFrom = diffFrom_numericUpDown.Value;
            Filters.diffTo = diffTo_numericUpDown.Value;

            Filters.hide_0 = hide0_checkBox.Checked;
            Filters.hide_100 = hide100_checkBox.Checked;

            Filters.close = close_checkBox.Checked;

            category_checkedListBox.ClearSelected();
            status_checkedListBox.ClearSelected();
            exterior_checkedListBox.ClearSelected();
            types_checkedListBox.ClearSelected();
            hide_checkedListBox.ClearSelected();
        }
        private String createFilter(CheckedListBox clb, string column, string not)
        {
            string filter = null;
            string log_element = "OR";
            int index = 3;

            if (not != null)
            {
                log_element = "AND";
                index = 4;
            }
            foreach (var item in clb.CheckedItems)
                filter += $"{log_element} {column} {not} LIKE '%{item}%'";

            return $"AND ({filter.Remove(0, index)})";
        }
        private String createFilter(NumericUpDown nudFrom, NumericUpDown nudTo, string column)
        {
            string filter = null;
            filter += $"AND {column} > {nudFrom.Value}";
            if (nudTo.Value != 0)
                filter += $"AND {column} < {nudTo.Value}";
            return filter;
        }

        //price
        private void Price1_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            price1From_numericUpDown.Enabled = Price1_checkBox.Checked;
            price1To_numericUpDown.Enabled = Price1_checkBox.Checked;
        }
        private void Price2_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            price2From_numericUpDown.Enabled = Price2_checkBox.Checked;
            price2To_numericUpDown.Enabled = Price2_checkBox.Checked;
        }
        private void Price3_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            price3From_numericUpDown.Enabled = Price3_checkBox.Checked;
            price3To_numericUpDown.Enabled = Price3_checkBox.Checked;
        }
        private void Price4_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            price4From_numericUpDown.Enabled = Price4_checkBox.Checked;
            price4To_numericUpDown.Enabled = Price4_checkBox.Checked;
        }

        //checkedListBox
        private void category_checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int index in category_checkedListBox.CheckedIndices)
                if (index != 0)
                    category_checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
                else
                    for (int i = 1; i < 5; i++)
                        category_checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }
        private void types_checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int index in types_checkedListBox.CheckedIndices)
                if (index != 0)
                    types_checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
                else
                    for (int i = 1; i < 12; i++)
                        types_checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }
    }
}
