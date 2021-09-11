using System;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using ItemChecker.Model;
using ItemChecker.Presenter;
using ItemChecker.Settings;

namespace ItemChecker
{
    public partial class StartingForm : Form
    {
        public StartingForm()
        {
            InitializeComponent();            
        }
        private void StartingForm_Shown(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Main.loading = true;

            version_StripStatus.Text = "Version: " + Main.assemblyVersion;

            BuyOrderPresenter buyOrderPresenter = new();
            WithdrawPresenter withdrawPresenter = new();
            FloatPresenter floatPresenter = new();
            BuyOrder.timer.Elapsed += new ElapsedEventHandler(buyOrderPresenter.timerTick);
            Withdraw.timer.Elapsed += new ElapsedEventHandler(withdrawPresenter.timerTick);
            Float.timer.Elapsed += new ElapsedEventHandler(floatPresenter.timerTick);
            BuyOrder.timer.Interval = Withdraw.timer.Interval = Float.timer.Interval = 1000;

            Main.proxyList.AddRange(GeneralConfig.Default.proxyList.Split("\n"));
            Withdraw.favoriteList.AddRange(WithdrawConfig.Default.favoriteList.Split("\n"));
            Float.floatList.AddRange(FloatConfig.Default.floatList.Split("\n"));

            ThreadPool.QueueUserWorkItem(StartingPresenter.Start);
        }
    }
}
