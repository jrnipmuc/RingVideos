using Event_Monitor.Data;
using Event_Monitor.Entities;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Event_Monitor.Dialogs
{
    public partial class LoginsDialog : Form
    {
        private ConnectionRepository _repository;
        private BindingList<Connection> _connections;
        private Connection _selectedConnection;

        public LoginsDialog()
        {
            InitializeComponent();
            _repository = new ConnectionRepository();
            SetupListBoxConnections();
            LoadConnections();
            if (lbConnectionList.Items?.Count > 0)
            {
                lbConnectionList.SelectedIndex = 0;
            }
        }

        private void SetupListBoxConnections()
        {
            lbConnectionList.DisplayMember = nameof(Connection.Username);
            lbConnectionList.ValueMember = nameof(Connection.Id);
            lbConnectionList.SelectionMode = SelectionMode.One;
            lbConnectionList.DrawMode = DrawMode.OwnerDrawFixed;
            lbConnectionList.ItemHeight = 30;
            lbConnectionList.DrawItem += LbConnectionList_DrawItem;
        }

        private void LbConnectionList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            var connection = _connections[e.Index];
            var isActiveLogin = connection.Id == AppContext.Current?.CurrentLogin?.Id;

            // Draw active indicator (green circle for active connection)
            if (isActiveLogin)
            {
                using (var brush = new SolidBrush(Color.Green))
                {
                    // Draw a filled circle
                    e.Graphics.FillEllipse(brush, e.Bounds.Left + 5, e.Bounds.Top + 8, 14, 14);
                }
            }

            // Draw the username text
            using (var brush = new SolidBrush(e.ForeColor))
            {
                var textRect = new Rectangle(e.Bounds.Left + 25, e.Bounds.Top + 5, e.Bounds.Width - 25, e.Bounds.Height);
                e.Graphics.DrawString(connection.Username, e.Font, brush, textRect, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private async void LoadConnections()
        {
            try
            {
                var connections = await _repository.GetAllAsync();
                _connections = new BindingList<Connection>(connections);
                lbConnectionList.DataSource = _connections;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading connections: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDetailPanel()
        {
            if (_selectedConnection != null)
            {
                tbUsername.Text = _selectedConnection.Username;
                tbPassword.Text = _selectedConnection.ClearTextPassword;
                cbIsActive.Checked = _selectedConnection.IsActive;

                UpdateTokenStatusDisplay();
            }
            UpdateButtonStates();
        }

        private void UpdateTokenStatusDisplay()
        {
            if (_selectedConnection == null)
            {
                lblTokenStatusValue.Text = "-";
                lblTokenExpiresValue.Text = "-";
                lblTokenStatusValue.ForeColor = SystemColors.ControlText;
                return;
            }

            // Display token status
            lblTokenStatusValue.Text = _selectedConnection.TokenStatus;

            // Color code based on validity
            if (_selectedConnection.HasValidToken)
            {
                lblTokenStatusValue.ForeColor = Color.Green;
            }
            else if (string.IsNullOrWhiteSpace(_selectedConnection.RefreshToken))
            {
                lblTokenStatusValue.ForeColor = Color.Gray;
            }
            else
            {
                lblTokenStatusValue.ForeColor = Color.Red;
            }

            // Display expiration date
            if (_selectedConnection.TokenExpirationDate.HasValue)
            {
                lblTokenExpiresValue.Text = _selectedConnection.TokenExpirationDate.Value.ToString("MMM dd, yyyy h:mm tt");
            }
            else
            {
                lblTokenExpiresValue.Text = string.IsNullOrWhiteSpace(_selectedConnection.RefreshToken) ? "-" : "Unknown";
            }
        }

        private void ClearDetailPanel()
        {
            tbUsername.Clear();
            tbPassword.Clear();
            cbIsActive.Checked = true;
            lblTokenStatusValue.Text = "-";
            lblTokenExpiresValue.Text = "-";
            lblTokenStatusValue.ForeColor = SystemColors.ControlText;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _selectedConnection != null;
            bool isAlreadyActive = _selectedConnection?.Id == AppContext.Current?.CurrentLogin?.Id;

            btnSaveLogin.Enabled = true;
            btnDeleteLogin.Enabled = hasSelection && !isAlreadyActive;
            btnSetAsActiveLogin.Enabled = hasSelection && !isAlreadyActive;
            btnAuthenticate.Enabled = hasSelection;
        }

        private void lbConnectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbConnectionList.SelectedItem is Connection connection)
            {
                _selectedConnection = connection;
                PopulateDetailPanel();
            }
            else
            {
                _selectedConnection = null;
                ClearDetailPanel();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbConnectionList.ClearSelected();
            _selectedConnection = null;
            ClearDetailPanel();
            tbUsername.Focus();
            UpdateButtonStates();
        }

        private async void btnSetAsActiveLogin_Click(object sender, EventArgs e)
        {
            if (_selectedConnection == null) return;

            AppContext.Current.CurrentLogin = _selectedConnection;

            try
            {
                await AppContext.SaveToDatabaseAsync();

                lbConnectionList.Invalidate();
                UpdateButtonStates();

                MessageBox.Show($"'{_selectedConnection.Username}' is now the active login.",
                    "Active Login Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving active connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSaveLogin_Click(object sender, EventArgs e)
        {
            await SaveConnection();
        }

        private async void btnDeleteLogin_Click(object sender, EventArgs e)
        {
            await DeleteConnection();
        }

        private async Task DeleteConnection()
        {
            if (MessageBox.Show("Are you sure you want to delete this connection?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (_selectedConnection != null)
                    {
                        await _repository.DeleteAsync(_selectedConnection.Id);
                        _connections.Remove(_selectedConnection);
                        ClearDetailPanel();
                        _selectedConnection = null;
                        UpdateButtonStates();
                        MessageBox.Show("Connection deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task SaveConnection()
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_selectedConnection == null)
                {
                    // New connection
                    var newConnection = new Connection
                    {
                        Username = tbUsername.Text.Trim(),
                        ClearTextPassword = tbPassword.Text,
                        IsActive = cbIsActive.Checked
                    };

                    var added = await _repository.AddAsync(newConnection);
                    _connections.Add(added);
                    lbConnectionList.SelectedItem = added;
                    MessageBox.Show("Connection added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing connection
                    _selectedConnection.Username = tbUsername.Text.Trim();
                    _selectedConnection.ClearTextPassword = tbPassword.Text;
                    _selectedConnection.IsActive = cbIsActive.Checked;

                    await _repository.UpdateAsync(_selectedConnection);

                    MessageBox.Show("Connection updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Refresh the ListBox display
                var selectedIndex = lbConnectionList.SelectedIndex;
                lbConnectionList.DataSource = null;
                lbConnectionList.DataSource = _connections;
                lbConnectionList.SelectedIndex = selectedIndex;
                SetupListBoxConnections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAuthenticate_Click(object sender, EventArgs e)
        {
            if (_selectedConnection == null)
            {
                MessageBox.Show("Please select a connection to authenticate.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_selectedConnection.ClearTextPassword))
            {
                MessageBox.Show("Password is required for authentication.", "Missing Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnAuthenticate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                KoenZomers.Ring.Api.Session session = null;

                try
                {
                    session = new KoenZomers.Ring.Api.Session(_selectedConnection.Username, _selectedConnection.ClearTextPassword);
                    await session.Authenticate();

                    MessageBox.Show("Authentication successful!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (KoenZomers.Ring.Api.Exceptions.TwoFactorAuthenticationRequiredException)
                {
                    string code = PromptDialog.Show(
                        this,
                        "Two-Factor Authentication",
                        "Two-factor authentication is enabled.\nPlease enter the verification code sent to your phone:");

                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        try
                        {
                            await session.Authenticate(twoFactorAuthCode: code);
                            MessageBox.Show("Authentication successful!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (KoenZomers.Ring.Api.Exceptions.TwoFactorAuthenticationIncorrectException)
                        {
                            MessageBox.Show("The verification code was incorrect. Please try again.", "Incorrect Code",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Authentication cancelled.", "Cancelled",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (KoenZomers.Ring.Api.Exceptions.ThrottledException ex)
                {
                    MessageBox.Show($"Too many authentication attempts. Please wait and try again.\n\n{ex.Message}",
                        "Throttled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                catch (KoenZomers.Ring.Api.Exceptions.AuthenticationFailedException ex)
                {
                    MessageBox.Show($"Authentication failed. Please check your credentials.\n\n{ex.Message}",
                        "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (System.Net.WebException ex)
                {
                    MessageBox.Show($"Connection failed. Please check your internet connection.\n\n{ex.Message}",
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Save the refresh token if authentication was successful
                if (session?.OAuthToken != null)
                {
                    _selectedConnection.ClearTextRefreshToken = session.OAuthToken.RefreshToken;
                    _selectedConnection.TokenObtainedDate = DateTime.Now;
                    _selectedConnection.TokenExpirationDate = DateTime.Now.AddDays(30); // Ring tokens typically expire in 30 days
                    _selectedConnection.LastAuthenticatedDate = DateTime.Now;

                    await _repository.UpdateAsync(_selectedConnection);

                    UpdateAppContext();


                    // Update the display
                    UpdateTokenStatusDisplay();

                    MessageBox.Show("Authentication tokens saved successfully!", "Tokens Saved",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred:\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAuthenticate.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void UpdateAppContext()
        {
            if (AppContext.Current != null
                && AppContext.Current.CurrentLogin != null
                && AppContext.Current.CurrentLogin.Username == _selectedConnection.Username)
            {
                AppContext.Current.CurrentLogin = _selectedConnection;
            }
        }
    }
}
