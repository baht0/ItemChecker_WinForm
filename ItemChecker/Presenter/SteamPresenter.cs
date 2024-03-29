﻿using System;
using System.Windows.Forms;
using ItemChecker.Model;
using static ItemChecker.Program;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ItemChecker.Support;
using ItemChecker.Settings;

namespace ItemChecker.Presenter
{
    public class SteamPresenter
    {
        public static void getBalance()
        {
            try
            {
                Main.Browser.Navigate().GoToUrl("https://steamcommunity.com/market");
                IWebElement count = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@id='my_market_buylistings_number']")));
                IWebElement balance = Main.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='header_wallet_balance']")));

                Steam.balance = Edit.removeRub(balance.Text);
                BuyOrder.my_buy_orders = Convert.ToInt32(count.Text);
                Steam.balance_usd = Math.Round(Steam.balance / GeneralConfig.Default.currency, 2);

                mainForm.Invoke(new MethodInvoker(delegate {
                    mainForm.available_label.Text = $"Available: {Math.Round(Steam.balance * 10, 2)}₽"; }));
            }
            catch (Exception exp)
            {
                Exceptions.errorLog(exp, Main.assemblyVersion);
            }
        }
    }
}