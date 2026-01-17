namespace Event_Monitor.Controls.MainTabs
{
    partial class DevicesTab
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            btnSyncDevices = new Button();
            tvDevices = new TreeView();
            grpDeviceDetails = new GroupBox();
            cmbSite = new ComboBox();
            lblSite = new Label();
            cmbDeviceType = new ComboBox();
            lblDeviceType = new Label();
            txtKind = new TextBox();
            lblKind = new Label();
            txtDeviceId = new TextBox();
            lblDeviceId = new Label();
            txtRingId = new TextBox();
            lblRingId = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtId = new TextBox();
            lblId = new Label();
            btnSave = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            grpDeviceDetails.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tvDevices);
            splitContainer1.Panel1.Controls.Add(btnSyncDevices);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnRefresh);
            splitContainer1.Panel2.Controls.Add(btnSave);
            splitContainer1.Panel2.Controls.Add(grpDeviceDetails);
            splitContainer1.Size = new Size(900, 600);
            splitContainer1.SplitterDistance = 300;
            splitContainer1.TabIndex = 0;
            // 
            // btnSyncDevices
            // 
            btnSyncDevices.Dock = DockStyle.Top;
            btnSyncDevices.Location = new Point(0, 0);
            btnSyncDevices.Name = "btnSyncDevices";
            btnSyncDevices.Size = new Size(300, 40);
            btnSyncDevices.TabIndex = 0;
            btnSyncDevices.Text = "Sync from Ring";
            btnSyncDevices.UseVisualStyleBackColor = true;
            btnSyncDevices.Click += btnSyncDevices_Click;
            // 
            // tvDevices
            // 
            tvDevices.Dock = DockStyle.Fill;
            tvDevices.Location = new Point(0, 40);
            tvDevices.Name = "tvDevices";
            tvDevices.Size = new Size(300, 560);
            tvDevices.TabIndex = 1;
            tvDevices.AfterSelect += tvDevices_AfterSelect;
            // 
            // grpDeviceDetails
            // 
            grpDeviceDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDeviceDetails.Controls.Add(cmbSite);
            grpDeviceDetails.Controls.Add(lblSite);
            grpDeviceDetails.Controls.Add(cmbDeviceType);
            grpDeviceDetails.Controls.Add(lblDeviceType);
            grpDeviceDetails.Controls.Add(txtKind);
            grpDeviceDetails.Controls.Add(lblKind);
            grpDeviceDetails.Controls.Add(txtDeviceId);
            grpDeviceDetails.Controls.Add(lblDeviceId);
            grpDeviceDetails.Controls.Add(txtRingId);
            grpDeviceDetails.Controls.Add(lblRingId);
            grpDeviceDetails.Controls.Add(txtDescription);
            grpDeviceDetails.Controls.Add(lblDescription);
            grpDeviceDetails.Controls.Add(txtId);
            grpDeviceDetails.Controls.Add(lblId);
            grpDeviceDetails.Location = new Point(15, 15);
            grpDeviceDetails.Name = "grpDeviceDetails";
            grpDeviceDetails.Size = new Size(560, 500);
            grpDeviceDetails.TabIndex = 0;
            grpDeviceDetails.TabStop = false;
            grpDeviceDetails.Text = "Device Details";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 35);
            lblId.Name = "lblId";
            lblId.Size = new Size(28, 25);
            lblId.TabIndex = 0;
            lblId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.Location = new Point(150, 32);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(100, 31);
            txtId.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 75);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(106, 25);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(150, 72);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(385, 31);
            txtDescription.TabIndex = 3;
            // 
            // lblRingId
            // 
            lblRingId.AutoSize = true;
            lblRingId.Location = new Point(20, 115);
            lblRingId.Name = "lblRingId";
            lblRingId.Size = new Size(73, 25);
            lblRingId.TabIndex = 4;
            lblRingId.Text = "Ring ID:";
            // 
            // txtRingId
            // 
            txtRingId.Location = new Point(150, 112);
            txtRingId.Name = "txtRingId";
            txtRingId.ReadOnly = true;
            txtRingId.Size = new Size(200, 31);
            txtRingId.TabIndex = 5;
            // 
            // lblDeviceId
            // 
            lblDeviceId.AutoSize = true;
            lblDeviceId.Location = new Point(20, 155);
            lblDeviceId.Name = "lblDeviceId";
            lblDeviceId.Size = new Size(90, 25);
            lblDeviceId.TabIndex = 6;
            lblDeviceId.Text = "Device ID:";
            // 
            // txtDeviceId
            // 
            txtDeviceId.Location = new Point(150, 152);
            txtDeviceId.Name = "txtDeviceId";
            txtDeviceId.ReadOnly = true;
            txtDeviceId.Size = new Size(385, 31);
            txtDeviceId.TabIndex = 7;
            // 
            // lblKind
            // 
            lblKind.AutoSize = true;
            lblKind.Location = new Point(20, 195);
            lblKind.Name = "lblKind";
            lblKind.Size = new Size(51, 25);
            lblKind.TabIndex = 8;
            lblKind.Text = "Kind:";
            // 
            // txtKind
            // 
            txtKind.Location = new Point(150, 192);
            txtKind.Name = "txtKind";
            txtKind.ReadOnly = true;
            txtKind.Size = new Size(200, 31);
            txtKind.TabIndex = 9;
            // 
            // lblDeviceType
            // 
            lblDeviceType.AutoSize = true;
            lblDeviceType.Location = new Point(20, 235);
            lblDeviceType.Name = "lblDeviceType";
            lblDeviceType.Size = new Size(107, 25);
            lblDeviceType.TabIndex = 10;
            lblDeviceType.Text = "Device Type:";
            // 
            // cmbDeviceType
            // 
            cmbDeviceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeviceType.FormattingEnabled = true;
            cmbDeviceType.Location = new Point(150, 232);
            cmbDeviceType.Name = "cmbDeviceType";
            cmbDeviceType.Size = new Size(200, 33);
            cmbDeviceType.TabIndex = 11;
            // 
            // lblSite
            // 
            lblSite.AutoSize = true;
            lblSite.Location = new Point(20, 275);
            lblSite.Name = "lblSite";
            lblSite.Size = new Size(44, 25);
            lblSite.TabIndex = 12;
            lblSite.Text = "Site:";
            // 
            // cmbSite
            // 
            cmbSite.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSite.FormattingEnabled = true;
            cmbSite.Location = new Point(150, 272);
            cmbSite.Name = "cmbSite";
            cmbSite.Size = new Size(385, 33);
            cmbSite.TabIndex = 13;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(15, 530);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(145, 530);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // DevicesTab
            // 
            Controls.Add(splitContainer1);
            Name = "DevicesTab";
            Text = "Devices";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            grpDeviceDetails.ResumeLayout(false);
            grpDeviceDetails.PerformLayout();
            ResumeLayout(false);
        }

        private SplitContainer splitContainer1;
        private Button btnSyncDevices;
        private TreeView tvDevices;
        private GroupBox grpDeviceDetails;
        private Label lblId;
        private TextBox txtId;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblRingId;
        private TextBox txtRingId;
        private Label lblDeviceId;
        private TextBox txtDeviceId;
        private Label lblKind;
        private TextBox txtKind;
        private Label lblDeviceType;
        private ComboBox cmbDeviceType;
        private Label lblSite;
        private ComboBox cmbSite;
        private Button btnSave;
        private Button btnRefresh;
    }
}
