using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Event_Monitor.Entities;

namespace Event_Monitor.Controls
{
    public partial class DeviceEventsPanel : UserControl
    {
        private Device _currentDevice;

        public DeviceEventsPanel()
        {
            InitializeComponent();
        }

        public async Task LoadEventsAsync(Device device)
        {
            _currentDevice = device;
            
            if (device == null)
            {
                Clear();
                return;
            }

            // TODO: Implement event loading and display
            lblPlaceholder.Text = $"Events for {device.Description} will be displayed here";
        }

        public void Clear()
        {
            _currentDevice = null;
            lblPlaceholder.Text = "Select a device to view events";
        }
    }
}
