namespace Event_Monitor.Controls
{
    partial class DeviceEventsPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblPlaceholder = new Label();
            SuspendLayout();
            // 
            // lblPlaceholder
            // 
            lblPlaceholder.Dock = DockStyle.Fill;
            lblPlaceholder.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblPlaceholder.ForeColor = SystemColors.GrayText;
            lblPlaceholder.Location = new Point(0, 0);
            lblPlaceholder.Name = "lblPlaceholder";
            lblPlaceholder.Size = new Size(800, 400);
            lblPlaceholder.TabIndex = 0;
            lblPlaceholder.Text = "Select a device to view events";
            lblPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DeviceEventsPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblPlaceholder);
            Name = "DeviceEventsPanel";
            Size = new Size(800, 400);
            ResumeLayout(false);
        }

        private Label lblPlaceholder;
    }
}
