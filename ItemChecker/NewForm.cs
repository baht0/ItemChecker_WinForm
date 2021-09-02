using System.Windows.Forms;
using ItemChecker.Model;

namespace ItemChecker
{
    public partial class NewForm : Form
    {
        int i = 10;
        public NewForm()
        {
            InitializeComponent();

            this.Text = $"What'is new? Version: {Main.assemblyVersion}";
            if (Properties.Settings.Default.whatIsNew)
            {
                close_button.Text = $"Close ({i--})";
                timer.Start();
            }
            else
                close_button.Enabled = true;
        }
        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i >= 0 & Properties.Settings.Default.whatIsNew)
                e.Cancel = true;
        }
        private void timer_Tick(object sender, System.EventArgs e)
        {
            close_button.Text = $"Close ({i--})";
            if (i < 0)
            {
                close_button.Text = $"Close";
                close_button.Enabled = true;
                timer.Stop();
            }
        }
        private void close_button_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}