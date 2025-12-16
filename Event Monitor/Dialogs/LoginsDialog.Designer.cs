namespace Event_Monitor.Dialogs
{
    partial class LoginsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _repository?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ssLoginsDialog = new StatusStrip();
            splitContainer1 = new SplitContainer();
            lbConnectionList = new ListBox();
            btnDeleteLogin = new Button();
            btnSaveLogin = new Button();
            btnSetAsActiveLogin = new Button();
            tbPassword = new TextBox();
            tbUsername = new TextBox();
            cbIsActive = new CheckBox();
            lbllIsActive = new Label();
            lblPassword = new Label();
            lblUsername = new Label();
            menuStrip1 = new MenuStrip();
            newToolStripMenuItem = new ToolStripMenuItem();
            btnAuthenticate = new Button();
            lblTokenStatus = new Label();
            lblTokenStatusValue = new Label();
            lblTokenExpires = new Label();
            lblTokenExpiresValue = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ssLoginsDialog
            // 
            ssLoginsDialog.ImageScalingSize = new Size(24, 24);
            ssLoginsDialog.Location = new Point(0, 428);
            ssLoginsDialog.Name = "ssLoginsDialog";
            ssLoginsDialog.Size = new Size(730, 22);
            ssLoginsDialog.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbConnectionList);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnAuthenticate);
            splitContainer1.Panel2.Controls.Add(btnDeleteLogin);
            splitContainer1.Panel2.Controls.Add(btnSaveLogin);
            splitContainer1.Panel2.Controls.Add(btnSetAsActiveLogin);
            splitContainer1.Panel2.Controls.Add(tbPassword);
            splitContainer1.Panel2.Controls.Add(tbUsername);
            splitContainer1.Panel2.Controls.Add(cbIsActive);
            splitContainer1.Panel2.Controls.Add(lbllIsActive);
            splitContainer1.Panel2.Controls.Add(lblPassword);
            splitContainer1.Panel2.Controls.Add(lblUsername);
            splitContainer1.Panel2.Controls.Add(lblTokenStatus);
            splitContainer1.Panel2.Controls.Add(lblTokenStatusValue);
            splitContainer1.Panel2.Controls.Add(lblTokenExpires);
            splitContainer1.Panel2.Controls.Add(lblTokenExpiresValue);
            splitContainer1.Size = new Size(730, 395);
            splitContainer1.SplitterDistance = 256;
            splitContainer1.TabIndex = 2;
            // 
            // lbConnectionList
            // 
            lbConnectionList.Dock = DockStyle.Fill;
            lbConnectionList.FormattingEnabled = true;
            lbConnectionList.ItemHeight = 25;
            lbConnectionList.Location = new Point(0, 0);
            lbConnectionList.Name = "lbConnectionList";
            lbConnectionList.Size = new Size(256, 395);
            lbConnectionList.TabIndex = 0;
            lbConnectionList.SelectedIndexChanged += lbConnectionList_SelectedIndexChanged;
            // 
            // btnDeleteLogin
            // 
            btnDeleteLogin.Location = new Point(319, 345);
            btnDeleteLogin.Name = "btnDeleteLogin";
            btnDeleteLogin.Size = new Size(112, 34);
            btnDeleteLogin.TabIndex = 8;
            btnDeleteLogin.Text = "Delete";
            btnDeleteLogin.UseVisualStyleBackColor = true;
            btnDeleteLogin.Click += btnDeleteLogin_Click;
            // 
            // btnSaveLogin
            // 
            btnSaveLogin.Location = new Point(188, 345);
            btnSaveLogin.Name = "btnSaveLogin";
            btnSaveLogin.Size = new Size(112, 34);
            btnSaveLogin.TabIndex = 7;
            btnSaveLogin.Text = "Save";
            btnSaveLogin.UseVisualStyleBackColor = true;
            btnSaveLogin.Click += btnSaveLogin_Click;
            // 
            // btnSetAsActiveLogin
            // 
            btnSetAsActiveLogin.Location = new Point(27, 345);
            btnSetAsActiveLogin.Name = "btnSetAsActiveLogin";
            btnSetAsActiveLogin.Size = new Size(137, 34);
            btnSetAsActiveLogin.TabIndex = 6;
            btnSetAsActiveLogin.Text = "Set As Active";
            btnSetAsActiveLogin.UseVisualStyleBackColor = true;
            btnSetAsActiveLogin.Click += btnSetAsActiveLogin_Click;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(142, 58);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(289, 31);
            tbPassword.TabIndex = 5;
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(142, 16);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(289, 31);
            tbUsername.TabIndex = 4;
            // 
            // cbIsActive
            // 
            cbIsActive.AutoSize = true;
            cbIsActive.Location = new Point(142, 103);
            cbIsActive.Name = "cbIsActive";
            cbIsActive.Size = new Size(22, 21);
            cbIsActive.TabIndex = 3;
            cbIsActive.UseVisualStyleBackColor = true;
            // 
            // lbllIsActive
            // 
            lbllIsActive.AutoSize = true;
            lbllIsActive.Location = new Point(35, 99);
            lbllIsActive.Name = "lbllIsActive";
            lbllIsActive.Size = new Size(78, 25);
            lbllIsActive.TabIndex = 2;
            lbllIsActive.Text = "Is Active";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(31, 58);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(87, 25);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(31, 19);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(91, 25);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(730, 33);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(63, 29);
            newToolStripMenuItem.Text = "&New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // btnAuthenticate
            // 
            btnAuthenticate.Location = new Point(27, 299);
            btnAuthenticate.Name = "btnAuthenticate";
            btnAuthenticate.Size = new Size(404, 34);
            btnAuthenticate.TabIndex = 9;
            btnAuthenticate.Text = "Authenticate";
            btnAuthenticate.UseVisualStyleBackColor = true;
            btnAuthenticate.Click += btnAuthenticate_Click;
            // 
            // lblTokenStatus
            // 
            lblTokenStatus.AutoSize = true;
            lblTokenStatus.Location = new Point(35, 140);
            lblTokenStatus.Name = "lblTokenStatus";
            lblTokenStatus.Size = new Size(110, 25);
            lblTokenStatus.TabIndex = 10;
            lblTokenStatus.Text = "Token Status:";
            // 
            // lblTokenStatusValue
            // 
            lblTokenStatusValue.AutoSize = true;
            lblTokenStatusValue.Location = new Point(155, 140);
            lblTokenStatusValue.Name = "lblTokenStatusValue";
            lblTokenStatusValue.Size = new Size(15, 25);
            lblTokenStatusValue.TabIndex = 11;
            lblTokenStatusValue.Text = "-";
            // 
            // lblTokenExpires
            // 
            lblTokenExpires.AutoSize = true;
            lblTokenExpires.Location = new Point(35, 170);
            lblTokenExpires.Name = "lblTokenExpires";
            lblTokenExpires.Size = new Size(120, 25);
            lblTokenExpires.TabIndex = 12;
            lblTokenExpires.Text = "Token Expires:";
            // 
            // lblTokenExpiresValue
            // 
            lblTokenExpiresValue.AutoSize = true;
            lblTokenExpiresValue.Location = new Point(155, 170);
            lblTokenExpiresValue.Name = "lblTokenExpiresValue";
            lblTokenExpiresValue.Size = new Size(15, 25);
            lblTokenExpiresValue.TabIndex = 13;
            lblTokenExpiresValue.Text = "-";
            // 
            // LoginsDialog
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 450);
            Controls.Add(splitContainer1);
            Controls.Add(ssLoginsDialog);
            Controls.Add(menuStrip1);
            Name = "LoginsDialog";
            Text = "Logins";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip ssLoginsDialog;
        private SplitContainer splitContainer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem newToolStripMenuItem;
        private Label lblPassword;
        private Label lblUsername;
        private TextBox tbPassword;
        private TextBox tbUsername;
        private CheckBox cbIsActive;
        private Label lbllIsActive;
        private Button btnSaveLogin;
        private Button btnSetAsActiveLogin;
        private Button btnDeleteLogin;
        private ListBox lbConnectionList;
        private Button btnAuthenticate;
        private Label lblTokenStatus;
        private Label lblTokenStatusValue;
        private Label lblTokenExpires;
        private Label lblTokenExpiresValue;
    }
}