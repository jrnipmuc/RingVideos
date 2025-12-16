using Event_Monitor.Data;
using Event_Monitor.Entities;
using System.ComponentModel;

namespace Event_Monitor.Dialogs
{
    public partial class EnumManagerDialog<T> : Form where T : class, IEnumEntity, new()
    {
        private EnumRepository<T> _repository;
        private BindingList<T> _items;
        private T _selectedItem;
        private readonly string _entityName;

        public EnumManagerDialog(string entityName)
        {
            _entityName = entityName;
            InitializeComponent();
            Text = $"Manage {_entityName}";
            lblTitle.Text = $"{_entityName} List";
            _repository = new EnumRepository<T>();
            SetupDataGridView();
            LoadItems();
        }

        private void SetupDataGridView()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.ReadOnly = true;
            dgvItems.MultiSelect = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(IEnumEntity.Id),
                HeaderText = "ID",
                Name = "Id",
                Width = 60,
                ReadOnly = true
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(IEnumEntity.Description),
                HeaderText = "Description",
                Name = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
        }

        private async void LoadItems()
        {
            try
            {
                var items = await _repository.GetAllAsync();
                _items = new BindingList<T>(items);
                dgvItems.DataSource = _items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {_entityName}: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                _selectedItem = (T)dgvItems.SelectedRows[0].DataBoundItem;
                PopulateDetailPanel();
            }
            else
            {
                _selectedItem = null;
                ClearDetailPanel();
            }
            UpdateButtonStates();
        }

        private void PopulateDetailPanel()
        {
            if (_selectedItem != null)
            {
                txtId.Text = _selectedItem.Id.ToString();
                txtDescription.Text = _selectedItem.Description;
            }
        }

        private void ClearDetailPanel()
        {
            txtId.Clear();
            txtDescription.Clear();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = _selectedItem != null;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvItems.ClearSelection();
            _selectedItem = null;
            txtId.Text = "(New)";
            txtDescription.Clear();
            txtDescription.Focus();
            UpdateButtonStates();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_selectedItem == null)
                {
                    // New item
                    var newItem = new T { Description = txtDescription.Text.Trim() };
                    var added = await _repository.AddAsync(newItem);
                    _items.Add(added);
                    dgvItems.ClearSelection();
                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        if (((T)row.DataBoundItem).Id == added.Id)
                        {
                            row.Selected = true;
                            break;
                        }
                    }
                    MessageBox.Show($"{_entityName} added successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing
                    _selectedItem.Description = txtDescription.Text.Trim();
                    await _repository.UpdateAsync(_selectedItem);
                    dgvItems.Refresh();
                    MessageBox.Show($"{_entityName} updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving {_entityName}: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedItem != null)
            {
                txtDescription.Focus();
                txtDescription.SelectAll();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedItem == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete '{_selectedItem.Description}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _repository.DeleteAsync(_selectedItem.Id);
                    _items.Remove(_selectedItem);
                    ClearDetailPanel();
                    MessageBox.Show($"{_entityName} deleted successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting {_entityName}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _repository?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}