using Event_Monitor.Data;
using Event_Monitor.Entities;

namespace Event_Monitor.Controls.MainTabs
{
    public partial class DevicesTab : TabPage
    {
        private DeviceRepository _deviceRepository;
        private EnumRepository<Site> _siteRepository;
        private Device _selectedDevice;
        private HashSet<TreeNode> _selectedNodes = new HashSet<TreeNode>();
        private TreeNode _lastSelectedNode = null;

        public DevicesTab()
        {
            InitializeComponent();
            this.Name = "DevicesTab";
            
            _deviceRepository = new DeviceRepository();
            _siteRepository = new EnumRepository<Site>();
            
            // Enable drag-drop for tree view
            tvDevices.AllowDrop = true;
            tvDevices.ItemDrag += tvDevices_ItemDrag;
            tvDevices.DragEnter += tvDevices_DragEnter;
            tvDevices.DragOver += tvDevices_DragOver;
            tvDevices.DragDrop += tvDevices_DragDrop;
            
            // Add mouse down handler for multi-select
            tvDevices.MouseDown += tvDevices_MouseDown;
            tvDevices.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tvDevices.DrawNode += tvDevices_DrawNode;
            
            LoadDeviceTree();
        }

        private void tvDevices_MouseDown(object sender, MouseEventArgs e)
        {
            var clickedNode = tvDevices.GetNodeAt(e.X, e.Y);
            
            if (clickedNode == null || !(clickedNode.Tag is Device))
                return;

            bool controlPressed = ModifierKeys.HasFlag(Keys.Control);
            bool shiftPressed = ModifierKeys.HasFlag(Keys.Shift);

            if (controlPressed)
            {
                if (_selectedNodes.Contains(clickedNode))
                {
                    _selectedNodes.Remove(clickedNode);
                }
                else
                {
                    _selectedNodes.Add(clickedNode);
                }
                _lastSelectedNode = clickedNode;
            }
            else if (shiftPressed && _lastSelectedNode != null)
            {
                _selectedNodes.Clear();
                var nodes = GetDeviceNodesBetween(_lastSelectedNode, clickedNode);
                foreach (var node in nodes)
                {
                    _selectedNodes.Add(node);
                }
            }
            else
            {
                _selectedNodes.Clear();
                _selectedNodes.Add(clickedNode);
                _lastSelectedNode = clickedNode;
            }

            if (clickedNode.Tag is Device device)
            {
                _selectedDevice = device;
                _ = LoadDeviceDetailsAsync();
            }

            tvDevices.Invalidate();
        }

        private async Task LoadDeviceDetailsAsync()
        {
            await deviceDetailsPanel.LoadDeviceAsync(_selectedDevice);
            await deviceEventsPanel.LoadEventsAsync(_selectedDevice);
        }

        private List<TreeNode> GetDeviceNodesBetween(TreeNode start, TreeNode end)
        {
            var result = new List<TreeNode>();
            var allDeviceNodes = GetAllDeviceNodes();
            
            int startIndex = allDeviceNodes.IndexOf(start);
            int endIndex = allDeviceNodes.IndexOf(end);
            
            if (startIndex == -1 || endIndex == -1)
                return result;
            
            if (startIndex > endIndex)
            {
                (startIndex, endIndex) = (endIndex, startIndex);
            }
            
            for (int i = startIndex; i <= endIndex; i++)
            {
                result.Add(allDeviceNodes[i]);
            }
            
            return result;
        }

        private List<TreeNode> GetAllDeviceNodes()
        {
            var deviceNodes = new List<TreeNode>();
            
            foreach (TreeNode siteNode in tvDevices.Nodes)
            {
                foreach (TreeNode deviceNode in siteNode.Nodes)
                {
                    if (deviceNode.Tag is Device)
                    {
                        deviceNodes.Add(deviceNode);
                    }
                }
            }
            
            return deviceNodes;
        }

        private void tvDevices_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null)
                return;

            if (!(e.Node.Tag is Device))
            {
                e.DrawDefault = true;
                return;
            }

            var backColor = _selectedNodes.Contains(e.Node) 
                ? SystemColors.Highlight 
                : e.Node.BackColor;
            
            var foreColor = _selectedNodes.Contains(e.Node) 
                ? SystemColors.HighlightText 
                : e.Node.ForeColor;

            using (var brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.Node.Text,
                tvDevices.Font,
                e.Bounds,
                foreColor,
                TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter);
        }

        private async void LoadDeviceTree()
        {
            try
            {
                tvDevices.Nodes.Clear();
                _selectedNodes.Clear();
                _lastSelectedNode = null;

                var devices = await _deviceRepository.GetAllAsync();
                var sites = await _siteRepository.GetAllAsync();

                foreach (var site in sites.OrderBy(s => s.Id))
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

                tvDevices.ExpandAll();
                
                deviceDetailsPanel.Clear();
                deviceEventsPanel.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading devices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tvDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Device device)
            {
                _selectedDevice = device;
                _selectedNodes.Clear();
                _selectedNodes.Add(e.Node);
                _lastSelectedNode = e.Node;
                await LoadDeviceDetailsAsync();
                tvDevices.Invalidate();
            }
            else if (e.Node?.Tag is Site)
            {
                _selectedDevice = null;
                deviceDetailsPanel.Clear();
                deviceEventsPanel.Clear();
            }
        }

        private async void btnSyncDevices_Click(object sender, EventArgs e)
        {
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
                string fullMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    fullMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                MessageBox.Show($"Error syncing devices: {fullMessage}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSyncDevices.Enabled = true;
                btnSyncDevices.Text = "Sync Devices from Ring";
                Cursor = Cursors.Default;
            }
        }

        private async Task SyncDevicesFromRing()
        {
            var currentLogin = AppContext.Current.CurrentLogin;
            
            currentLogin.Decrypt();
            
            var session = await KoenZomers.Ring.Api.Session.GetSessionByRefreshToken(currentLogin.ClearTextRefreshToken);
            
            if (session == null || !session.IsAuthenticated)
            {
                throw new Exception("Failed to create Ring API session. Please re-authenticate.");
            }

            var ringDevices = await session.GetRingDevices();

            int addedCount = 0;
            int updatedCount = 0;

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

        private async Task<bool> UpsertDevice(long ringId, string deviceId, DeviceType deviceType, string kind, string description)
        {
            var existing = await _deviceRepository.GetByRingIdAsync(ringId);
            
            if (existing != null)
            {
                existing.DeviceId = deviceId;
                existing.DeviceTypeId = (int)deviceType;
                existing.Kind = kind;
                existing.Description = description;
                
                await _deviceRepository.UpdateAsync(existing);
                return false;
            }
            else
            {
                var newDevice = new Device
                {
                    RingId = ringId,
                    DeviceId = deviceId,
                    DeviceTypeId = (int)deviceType,
                    DeviceType = deviceType,
                    Kind = kind,
                    Description = description,
                    SiteId = 0
                };
                
                await _deviceRepository.AddAsync(newDevice);
                return true;
            }
        }

        #region Drag and Drop Operations

        private void tvDevices_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode node)
            {
                if (node.Tag is Device && _selectedNodes.Contains(node))
                {
                    DoDragDrop(_selectedNodes.ToList(), DragDropEffects.Move);
                }
                else if (node.Tag is Device)
                {
                    DoDragDrop(new List<TreeNode> { node }, DragDropEffects.Move);
                }
            }
        }

        private void tvDevices_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(List<TreeNode>)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void tvDevices_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = tvDevices.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvDevices.GetNodeAt(targetPoint);

            if (targetNode?.Tag is Site)
            {
                e.Effect = DragDropEffects.Move;
                tvDevices.SelectedNode = targetNode;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void tvDevices_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(List<TreeNode>)))
                return;

            Point targetPoint = tvDevices.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvDevices.GetNodeAt(targetPoint);

            if (targetNode?.Tag is not Site targetSite)
            {
                MessageBox.Show("Please drop devices onto a site node.", "Invalid Drop Target", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var draggedNodes = (List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>));
            var devicesToMove = new List<Device>();

            foreach (var node in draggedNodes)
            {
                if (node?.Tag is Device device)
                {
                    devicesToMove.Add(device);
                }
            }

            if (devicesToMove.Count == 0)
                return;

            var alreadyInSite = devicesToMove.Where(d => d.SiteId == targetSite.Id).ToList();
            if (alreadyInSite.Count == devicesToMove.Count)
            {
                MessageBox.Show("All selected devices are already in this site.", "Same Site", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var deviceList = string.Join("\n", devicesToMove.Take(5).Select(d => $"  • {d.Description}"));
            if (devicesToMove.Count > 5)
            {
                deviceList += $"\n  ... and {devicesToMove.Count - 5} more";
            }

            var message = devicesToMove.Count == 1
                ? $"Move '{devicesToMove[0].Description}' to '{targetSite.Description}'?"
                : $"Move {devicesToMove.Count} devices to '{targetSite.Description}'?\n\n{deviceList}";

            var result = MessageBox.Show(
                message,
                "Confirm Move",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                int movedCount = 0;
                int skippedCount = 0;

                foreach (var device in devicesToMove)
                {
                    if (device.SiteId != targetSite.Id)
                    {
                        device.SiteId = targetSite.Id;
                        await _deviceRepository.UpdateAsync(device);
                        movedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }

                LoadDeviceTree();

                var statusMessage = movedCount == 1
                    ? $"Device moved to '{targetSite.Description}' successfully."
                    : $"{movedCount} device(s) moved to '{targetSite.Description}' successfully.";

                if (skippedCount > 0)
                {
                    statusMessage += $"\n{skippedCount} device(s) were already in the target site.";
                }

                MessageBox.Show(statusMessage, "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error moving devices: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

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
