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

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await CheckAuthenticationAsync();
            SelectSingletonTab(new Controls.MainTabs.DevicesTab());
        }

        private async Task CheckAuthenticationAsync()
        {
            // Check if we have an active connection
            if (AppContext.Current?.CurrentLogin == null)
            {
                var result = MessageBox.Show(
                    "No active connection configured.\n\nWould you like to configure a login now?",
                    "No Connection",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ShowLoginsDialog();
                }
                return;
            }

            // Check if token is expired
            if (!AppContext.Current.CurrentLogin.HasValidToken)
            {
                var daysExpired = AppContext.Current.CurrentLogin.TokenExpirationDate.HasValue
                    ? (int)(DateTime.Now - AppContext.Current.CurrentLogin.TokenExpirationDate.Value).TotalDays
                    : 0;

                var message = daysExpired > 0
                    ? $"Your authentication token expired {daysExpired} day(s) ago.\n\nYou must re-authenticate to use the application."
                    : "Your authentication token has expired.\n\nYou must re-authenticate to use the application.";

                var result = MessageBox.Show(
                    message,
                    "Token Expired",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    ShowLoginsDialog();
                }
            }
            else if (AppContext.Current.CurrentLogin.TokenExpirationDate.HasValue)
            {
                // Check if token is expiring soon (within 3 days)
                var daysRemaining = (AppContext.Current.CurrentLogin.TokenExpirationDate.Value - DateTime.Now).TotalDays;

                if (daysRemaining <= 3 && daysRemaining > 0)
                {
                    var result = MessageBox.Show(
                        $"Your authentication token will expire in {(int)Math.Ceiling(daysRemaining)} day(s).\n\nWould you like to re-authenticate now?",
                        "Token Expiring Soon",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        ShowLoginsDialog();
                    }
                }
            }
        }

        private void ShowLoginsDialog()
        {
            var logins = new Dialogs.LoginsDialog();
            logins.FormClosed += (s, args) => UpdateStatusLabels();
            logins.ShowDialog(this); // Use ShowDialog for modal behavior on startup
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLoginsDialog();
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

        }
    }
}
