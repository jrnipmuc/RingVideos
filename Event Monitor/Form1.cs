using Event_Monitor.Dialogs;
using Event_Monitor.Entities;

namespace Event_Monitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppContext.ContextChanged += AppContext_ContextChanged;
            UpdateStatusLabels();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.LoginsDialog logins = new Dialogs.LoginsDialog();
            logins.FormClosed += (s, args) => UpdateStatusLabels(); // Update when dialog closes
            logins.Show(this);
        }

        private void sitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void devicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectSingletonTab(new Controls.MainTabs.DevicesTab());
        }

        private void SelectSingletonTab(TabPage tab)
        {
            if (tabControlMain.TabPages.ContainsKey(tab.Name))
            {
                tabControlMain.SelectedTab = tabControlMain.TabPages[tab.Name];
            }
            else
            {
                tabControlMain.TabPages.Add(tab);
                tabControlMain.SelectedTab = tab;
            }
        }

        private void AppContext_ContextChanged(object sender, EventArgs e)
        {
            // Update status labels when AppContext changes
            UpdateStatusLabels();
        }

        private void UpdateStatusLabels()
        {
            // Update Connection status label
            if (AppContext.Current?.CurrentLogin != null)
            {
                statusLabelConnection.Text = $"Connection: {AppContext.Current.CurrentLogin.Username}";
                statusLabelConnection.ForeColor = Color.Green;
            }
            else
            {
                statusLabelConnection.Text = "Connection: (none)";
                statusLabelConnection.ForeColor = Color.Red;
            }

            // Update Root Directory status label
            if (!string.IsNullOrWhiteSpace(AppContext.Current?.RootDirectory))
            {
                statusLabelRootDirectory.Text = $"Root Directory: {AppContext.Current.RootDirectory}";
            }
            else
            {
                statusLabelRootDirectory.Text = "Root Directory: (not set)";
            }

            // Update Auth Expiration status label
            UpdateAuthExpirationLabel();
        }

        private void UpdateAuthExpirationLabel()
        {
            var currentLogin = AppContext.Current?.CurrentLogin;

            if (currentLogin == null)
            {
                statusLabelAuthExpires.Text = "";
                statusLabelAuthExpires.ForeColor = SystemColors.ControlText;
                return;
            }

            if (string.IsNullOrWhiteSpace(currentLogin.RefreshToken))
            {
                statusLabelAuthExpires.Text = "Auth: Not authenticated";
                statusLabelAuthExpires.ForeColor = Color.Gray;
                return;
            }

            if (!currentLogin.TokenExpirationDate.HasValue)
            {
                statusLabelAuthExpires.Text = "Auth: Unknown expiry";
                statusLabelAuthExpires.ForeColor = Color.Orange;
                return;
            }

            var timeRemaining = currentLogin.TokenExpirationDate.Value - DateTime.Now;

            if (timeRemaining.TotalSeconds <= 0)
            {
                statusLabelAuthExpires.Text = "Auth: EXPIRED - Re-authenticate required";
                statusLabelAuthExpires.ForeColor = Color.Red;
                statusLabelAuthExpires.Font = new Font(statusLabelAuthExpires.Font, FontStyle.Bold);
            }
            else if (timeRemaining.TotalDays <= 3)
            {
                var days = (int)Math.Ceiling(timeRemaining.TotalDays);
                statusLabelAuthExpires.Text = $"Auth: Expires in {days} day{(days != 1 ? "s" : "")}";
                statusLabelAuthExpires.ForeColor = Color.Orange;
                statusLabelAuthExpires.Font = new Font(statusLabelAuthExpires.Font, FontStyle.Bold);
            }
            else if (timeRemaining.TotalDays <= 7)
            {
                var days = (int)Math.Ceiling(timeRemaining.TotalDays);
                statusLabelAuthExpires.Text = $"Auth: Expires in {days} days";
                statusLabelAuthExpires.ForeColor = Color.DarkOrange;
            }
            else
            {
                var days = (int)Math.Ceiling(timeRemaining.TotalDays);
                statusLabelAuthExpires.Text = $"Auth: Valid for {days} days";
                statusLabelAuthExpires.ForeColor = Color.Green;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from event to prevent memory leaks
                AppContext.ContextChanged -= AppContext_ContextChanged;
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void rootDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newRootDir = PromptDialog.Show(
                this,
                "Root Directory",
                "Select a root directory to store the videos:",
                AppContext.Current.RootDirectory);

            if (!string.IsNullOrWhiteSpace(newRootDir))
            {
                AppContext.Current.RootDirectory = newRootDir;
            }
        }

        private void sitesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dialog = new EnumManagerDialog<Site>("SItes");
            dialog.ShowDialog();

        }
        
        private void deviceTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
