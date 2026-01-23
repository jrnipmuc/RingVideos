using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Event_Monitor.Data;
using Event_Monitor.Entities;

namespace Event_Monitor.Controls
{
    public partial class DeviceDetailsPanel : UserControl
    {
        private EnumRepository<Site> _siteRepository;

        public DeviceDetailsPanel()
        {
            InitializeComponent();
            _siteRepository = new EnumRepository<Site>();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            dgvDeviceDetails.Columns.Clear();
            dgvDeviceDetails.AutoGenerateColumns = false;
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                DataPropertyName = "Description",
                Width = 200,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RingId",
                HeaderText = "Ring ID",
                DataPropertyName = "RingId",
                Width = 100,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DeviceId",
                HeaderText = "Device ID",
                DataPropertyName = "DeviceId",
                Width = 150,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kind",
                HeaderText = "Kind",
                DataPropertyName = "Kind",
                Width = 120,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DeviceType",
                HeaderText = "Device Type",
                DataPropertyName = "DeviceType",
                Width = 120,
                ReadOnly = true
            });
            
            dgvDeviceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SiteDescription",
                HeaderText = "Site",
                Width = 150,
                ReadOnly = true
            });
            
            dgvDeviceDetails.ColumnHeadersVisible = true;
            dgvDeviceDetails.RowHeadersVisible = false;
            dgvDeviceDetails.AllowUserToResizeColumns = true;
            dgvDeviceDetails.AllowUserToResizeRows = false;
            dgvDeviceDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public async Task LoadDeviceAsync(Device device)
        {
            if (device == null)
            {
                Clear();
                return;
            }

            dgvDeviceDetails.Rows.Clear();
            
            var sites = await _siteRepository.GetAllAsync();
            var site = sites.FirstOrDefault(s => s.Id == device.SiteId);
            var siteDescription = site?.Description ?? "Unknown";
            
            var rowIndex = dgvDeviceDetails.Rows.Add();
            var row = dgvDeviceDetails.Rows[rowIndex];
            
            row.Cells["Id"].Value = device.Id;
            row.Cells["Description"].Value = device.Description;
            row.Cells["RingId"].Value = device.RingId;
            row.Cells["DeviceId"].Value = device.DeviceId;
            row.Cells["Kind"].Value = device.Kind;
            row.Cells["DeviceType"].Value = device.DeviceType.ToString();
            row.Cells["SiteDescription"].Value = siteDescription;
        }

        public void Clear()
        {
            dgvDeviceDetails.Rows.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _siteRepository?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
