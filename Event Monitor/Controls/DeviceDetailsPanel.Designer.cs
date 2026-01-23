namespace Event_Monitor.Controls
{
    partial class DeviceDetailsPanel
    {
        private System.ComponentModel.IContainer components = null;


        private void InitializeComponent()
        {
            dgvDeviceDetails = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDeviceDetails).BeginInit();
            SuspendLayout();
            // 
            // dgvDeviceDetails
            // 
            dgvDeviceDetails.AllowUserToAddRows = false;
            dgvDeviceDetails.AllowUserToDeleteRows = false;
            dgvDeviceDetails.AllowUserToResizeRows = false;
            dgvDeviceDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeviceDetails.BackgroundColor = SystemColors.Control;
            dgvDeviceDetails.BorderStyle = BorderStyle.None;
            dgvDeviceDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeviceDetails.Dock = DockStyle.Fill;
            dgvDeviceDetails.Location = new Point(0, 0);
            dgvDeviceDetails.MultiSelect = false;
            dgvDeviceDetails.Name = "dgvDeviceDetails";
            dgvDeviceDetails.ReadOnly = true;
            dgvDeviceDetails.RowHeadersVisible = false;
            dgvDeviceDetails.RowHeadersWidth = 62;
            dgvDeviceDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeviceDetails.Size = new Size(800, 150);
            dgvDeviceDetails.TabIndex = 0;
            // 
            // DeviceDetailsPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvDeviceDetails);
            Name = "DeviceDetailsPanel";
            Size = new Size(800, 150);
            ((System.ComponentModel.ISupportInitialize)dgvDeviceDetails).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvDeviceDetails;
    }
}
