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
            splitContainerRight = new SplitContainer();
            grpDeviceDetails = new GroupBox();
            deviceDetailsPanel = new DeviceDetailsPanel();
            grpEvents = new GroupBox();
            deviceEventsPanel = new DeviceEventsPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerRight).BeginInit();
            splitContainerRight.Panel1.SuspendLayout();
            splitContainerRight.Panel2.SuspendLayout();
            splitContainerRight.SuspendLayout();
            grpDeviceDetails.SuspendLayout();
            grpEvents.SuspendLayout();
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
            splitContainer1.Panel2.Controls.Add(splitContainerRight);
            splitContainer1.Size = new Size(1200, 700);
            splitContainer1.SplitterDistance = 150;
            splitContainer1.TabIndex = 0;
            // 
            // btnSyncDevices
            // 
            btnSyncDevices.Dock = DockStyle.Top;
            btnSyncDevices.Location = new Point(0, 0);
            btnSyncDevices.Name = "btnSyncDevices";
            btnSyncDevices.Size = new Size(300, 40);
            btnSyncDevices.TabIndex = 0;
            btnSyncDevices.Text = "Sync Devices from Ring";
            btnSyncDevices.UseVisualStyleBackColor = true;
            btnSyncDevices.Click += btnSyncDevices_Click;
            // 
            // tvDevices
            // 
            tvDevices.Dock = DockStyle.Fill;
            tvDevices.Location = new Point(0, 40);
            tvDevices.Name = "tvDevices";
            tvDevices.Size = new Size(300, 660);
            tvDevices.TabIndex = 1;
            tvDevices.AfterSelect += tvDevices_AfterSelect;
            // 
            // splitContainerRight
            // 
            splitContainerRight.Dock = DockStyle.Fill;
            splitContainerRight.Location = new Point(0, 0);
            splitContainerRight.Name = "splitContainerRight";
            splitContainerRight.Orientation = Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            splitContainerRight.Panel1.Controls.Add(grpDeviceDetails);
            // 
            // splitContainerRight.Panel2
            // 
            splitContainerRight.Panel2.Controls.Add(grpEvents);
            splitContainerRight.Size = new Size(896, 700);
            splitContainerRight.SplitterDistance = 60;
            splitContainerRight.TabIndex = 0;
            // 
            // grpDeviceDetails
            // 
            grpDeviceDetails.Controls.Add(deviceDetailsPanel);
            grpDeviceDetails.Dock = DockStyle.Fill;
            grpDeviceDetails.Location = new Point(0, 0);
            grpDeviceDetails.Name = "grpDeviceDetails";
            grpDeviceDetails.Padding = new Padding(10);
            grpDeviceDetails.Size = new Size(896, 200);
            grpDeviceDetails.TabIndex = 0;
            grpDeviceDetails.TabStop = false;
            grpDeviceDetails.Text = "Device Details";
            // 
            // deviceDetailsPanel
            // 
            deviceDetailsPanel.Dock = DockStyle.Fill;
            deviceDetailsPanel.Location = new Point(10, 30);
            deviceDetailsPanel.Name = "deviceDetailsPanel";
            deviceDetailsPanel.Size = new Size(876, 160);
            deviceDetailsPanel.TabIndex = 0;
            // 
            // grpEvents
            // 
            grpEvents.Controls.Add(deviceEventsPanel);
            grpEvents.Dock = DockStyle.Fill;
            grpEvents.Location = new Point(0, 0);
            grpEvents.Name = "grpEvents";
            grpEvents.Padding = new Padding(10);
            grpEvents.Size = new Size(896, 496);
            grpEvents.TabIndex = 0;
            grpEvents.TabStop = false;
            grpEvents.Text = "Events";
            // 
            // deviceEventsPanel
            // 
            deviceEventsPanel.Dock = DockStyle.Fill;
            deviceEventsPanel.Location = new Point(10, 30);
            deviceEventsPanel.Name = "deviceEventsPanel";
            deviceEventsPanel.Size = new Size(876, 456);
            deviceEventsPanel.TabIndex = 0;
            // 
            // DevicesTab
            // 
            Controls.Add(splitContainer1);
            Name = "DevicesTab";
            Text = "Devices";
            Size = new Size(1200, 700);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainerRight.Panel1.ResumeLayout(false);
            splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerRight).EndInit();
            splitContainerRight.ResumeLayout(false);
            grpDeviceDetails.ResumeLayout(false);
            grpEvents.ResumeLayout(false);
            ResumeLayout(false);
        }

        private SplitContainer splitContainer1;
        private Button btnSyncDevices;
        private TreeView tvDevices;
        private SplitContainer splitContainerRight;
        private GroupBox grpDeviceDetails;
        private DeviceDetailsPanel deviceDetailsPanel;
        private GroupBox grpEvents;
        private DeviceEventsPanel deviceEventsPanel;
    }
}
