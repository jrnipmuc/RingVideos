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
using Event_Monitor.Services;

namespace Event_Monitor.Controls.MainTabs
{
    public partial class DevicesTab : TabPage
    {
        private DeviceRepository _deviceRepository;
        private EnumRepository<Site> _siteRepository;
        private Device _selectedDevice;

        public DevicesTab()
        {
            InitializeComponent();
            this.Name = "DevicesTab";
            
            _deviceRepository = new DeviceRepository();
            _siteRepository = new EnumRepository<Site>();
            
            LoadDropdowns();
            LoadDeviceTree();
        }

        private async void LoadDropdowns()
        {
            try
            {
                // Load Device Types
                var deviceTypes = EnumService.GetEnumList<DeviceType>();
                cmbDeviceType.DisplayMember = "Description";
                cmbDeviceType.ValueMember = "Id";
                cmbDeviceType.DataSource = deviceTypes;

                // Load Sites (with Unassociated option)
                var sites = await _siteRepository.GetAllAsync();
                var siteList = new List<object> { new { Id = 0L, Name = "Unassociated" } };
                siteList.AddRange(sites.Select(s => new { Id = s.Id, Name = s.Description }));
                
                cmbSite.DisplayMember = "Name";
                cmbSite.ValueMember = "Id";
                cmbSite.DataSource = siteList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dropdowns: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadDeviceTree()
        {
            try
            {
                tvDevices.Nodes.Clear();

                // Get all devices and sites
                var devices = await _deviceRepository.GetAllAsync();
                var sites = await _siteRepository.GetAllAsync();

                // Create "Unassociated" node
                var unassociatedNode = new TreeNode("Unassociated") { Tag = null };
                var unassociatedDevices = devices.Where(d => d.SiteId == 0).ToList();
                
                foreach (var device in unassociatedDevices)
                {
                    var deviceNode = new TreeNode(device.Description) { Tag = device };
                    unassociatedNode.Nodes.Add(deviceNode);
                }
                
                tvDevices.Nodes.Add(unassociatedNode);

                // Create site nodes
                foreach (var site in sites.OrderBy(s => s.Description))
                {
                    var siteNode = new TreeNode(site.Description) { Tag = site };
                    var siteDevices = devices.Where(d => d.SiteId == site.Id).ToList();
                    
                    foreach (var device in siteDevices)
                    {
                        var deviceNode = new TreeNode(device.Description) { Tag = device };
                        siteNode.Nodes.Add(deviceNode);
                    }
                    
                    tvDevices.Nodes.Add(siteNode);
                }

                // Expand all nodes
                tvDevices.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading devices: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tvDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Device device)
            {
                _selectedDevice = device;
                PopulateDeviceDetails();
            }
            else
            {
                _selectedDevice = null;
                ClearDeviceDetails();
            }
        }

        private void PopulateDeviceDetails()
        {
            if (_selectedDevice == null) return;

            txtId.Text = _selectedDevice.Id.ToString();
            txtDescription.Text = _selectedDevice.Description;
            txtRingId.Text = _selectedDevice.RingId.ToString();
            txtDeviceId.Text = _selectedDevice.DeviceId;
            txtKind.Text = _selectedDevice.Kind;
            cmbDeviceType.SelectedValue = _selectedDevice.DeviceTypeId;
            cmbSite.SelectedValue = _selectedDevice.SiteId;
        }

        private void ClearDeviceDetails()
        {
            txtId.Clear();
            txtDescription.Clear();
            txtRingId.Clear();
            txtDeviceId.Clear();
            txtKind.Clear();
            cmbDeviceType.SelectedIndex = -1;
            cmbSite.SelectedIndex = 0; // Unassociated
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedDevice == null)
            {
                MessageBox.Show("No device selected.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _selectedDevice.Description = txtDescription.Text.Trim();
                _selectedDevice.DeviceTypeId = (long)cmbDeviceType.SelectedValue;
                _selectedDevice.SiteId = (long)cmbSite.SelectedValue;

                await _deviceRepository.UpdateAsync(_selectedDevice);
                
                LoadDeviceTree(); // Refresh tree (device may have moved sites)
                
                MessageBox.Show("Device updated successfully.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving device: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDeviceTree();
        }

        private async void btnSyncDevices_Click(object sender, EventArgs e)
        {
            // Check if we have an active connection
            if (AppContext.Current?.CurrentLogin == null)
            {
                MessageBox.Show("No active connection. Please configure a login first.", 
                    "No Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!AppContext.Current.CurrentLogin.HasValidToken)
            {
                MessageBox.Show("Authentication token is expired. Please re-authenticate.", 
                    "Token Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSyncDevices.Enabled = false;
            btnSyncDevices.Text = "Syncing...";
            Cursor = Cursors.WaitCursor;

            try
            {
                await SyncDevicesFromRing();
                LoadDeviceTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error syncing devices: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSyncDevices.Enabled = true;
                btnSyncDevices.Text = "Sync from Ring";
                Cursor = Cursors.Default;
            }
        }

        private async Task SyncDevicesFromRing()
        {
            // Get a session using the current login's refresh token
            var currentLogin = AppContext.Current.CurrentLogin;
            
            KoenZomers.Ring.Api.Session session;
            
            // Decrypt the refresh token
            currentLogin.Decrypt();
            
            // Create session from refresh token
            session = await KoenZomers.Ring.Api.Session.GetSessionByRefreshToken(currentLogin.ClearTextRefreshToken);
            
            if (session == null || !session.IsAuthenticated)
            {
                throw new Exception("Failed to create Ring API session. Please re-authenticate.");
            }

            // Fetch all Ring devices
            var ringDevices = await session.GetRingDevices();

            int addedCount = 0;
            int updatedCount = 0;

            // Process Stickup Cams
            if (ringDevices.StickupCams != null)
            {
                foreach (var cam in ringDevices.StickupCams)
                {
                    if (cam.Id != null)
                    {
                        var result = await UpsertDevice((long)cam.Id, cam.DeviceId, DeviceType.StickupCam, cam.Kind, cam.Description);
                        if (result) addedCount++; else updatedCount++;
                    }
                }
            }

            // Process Chimes
            if (ringDevices.Chimes != null)
            {
                foreach (var chime in ringDevices.Chimes)
                {
                    var result = await UpsertDevice(chime.Id, chime.DeviceId, DeviceType.Chime, chime.Kind, chime.Description);
                    if (result) addedCount++; else updatedCount++;
                }
            }

            MessageBox.Show($"Sync complete!\nAdded: {addedCount}\nUpdated: {updatedCount}", 
                "Sync Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Inserts or updates a device from Ring data
        /// </summary>
        /// <returns>True if added (new), False if updated (existing)</returns>
        private async Task<bool> UpsertDevice(long ringId, string deviceId, DeviceType deviceType, string kind, string description)
        {
            var existing = await _deviceRepository.GetByRingIdAsync(ringId);
            
            if (existing != null)
            {
                // Update existing device
                existing.DeviceId = deviceId;
                existing.DeviceTypeId = (int)deviceType;
                existing.Kind = kind;
                existing.Description = description;
                
                await _deviceRepository.UpdateAsync(existing);
                return false; // Updated
            }
            else
            {
                // Add new device (unassociated by default)
                var newDevice = new Device
                {
                    RingId = ringId,
                    DeviceId = deviceId,
                    DeviceTypeId = (int)deviceType,
                    DeviceType = deviceType,
                    Kind = kind,
                    Description = description,
                    SiteId = 0 // Unassociated
                };
                
                await _deviceRepository.AddAsync(newDevice);
                return true; // Added
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _deviceRepository?.Dispose();
                _siteRepository?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
