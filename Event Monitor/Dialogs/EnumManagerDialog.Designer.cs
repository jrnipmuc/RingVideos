namespace Event_Monitor.Dialogs
{
    partial class EnumManagerDialog<T> where T : class, Entities.IEnumEntity, new()
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            lblTitle = new Label();
            dgvItems = new DataGridView();
            grpDetails = new GroupBox();
            txtDescription = new TextBox();
            txtId = new TextBox();
            lblDescription = new Label();
            lblId = new Label();
            btnClose = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnSave = new Button();
            btnNew = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            grpDetails.SuspendLayout();
            SuspendLayout();
            
            // splitContainer1
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Panel1.Controls.Add(dgvItems);
            splitContainer1.Panel2.Controls.Add(btnClose);
            splitContainer1.Panel2.Controls.Add(btnDelete);
            splitContainer1.Panel2.Controls.Add(btnEdit);
            splitContainer1.Panel2.Controls.Add(btnSave);
            splitContainer1.Panel2.Controls.Add(btnNew);
            splitContainer1.Panel2.Controls.Add(grpDetails);
            splitContainer1.Size = new Size(600, 500);
            splitContainer1.SplitterDistance = 300;
            splitContainer1.TabIndex = 0;
            
            // dgvItems
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.Location = new Point(0, 40);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersWidth = 62;
            dgvItems.Size = new Size(600, 260);
            dgvItems.TabIndex = 1;
            
            // grpDetails
            grpDetails.Controls.Add(txtDescription);
            grpDetails.Controls.Add(txtId);
            grpDetails.Controls.Add(lblDescription);
            grpDetails.Controls.Add(lblId);
            grpDetails.Location = new Point(15, 10);
            grpDetails.Name = "grpDetails";
            grpDetails.Size = new Size(570, 120);
            grpDetails.TabIndex = 0;
            grpDetails.TabStop = false;
            grpDetails.Text = "Details";
            
            // lblId
            lblId.AutoSize = true;
            lblId.Location = new Point(20, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(28, 25);
            lblId.TabIndex = 0;
            lblId.Text = "ID:";
            
            // txtId
            txtId.Location = new Point(120, 27);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(100, 31);
            txtId.TabIndex = 1;
            
            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 70);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(106, 25);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";
            
            // txtDescription
            txtDescription.Location = new Point(120, 67);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(430, 31);
            txtDescription.TabIndex = 3;
            
            // btnNew
            btnNew.Location = new Point(15, 145);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(90, 35);
            btnNew.TabIndex = 1;
            btnNew.Text = "New";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            
            // btnSave
            btnSave.Location = new Point(115, 145);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 35);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            
            // btnEdit
            btnEdit.Location = new Point(215, 145);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 35);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            
            // btnDelete
            btnDelete.Location = new Point(315, 145);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 35);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            
            // btnClose
            btnClose.Location = new Point(495, 145);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 35);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            
            // EnumManagerDialog
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 500);
            Controls.Add(splitContainer1);
            Name = "EnumManagerDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Enum Manager";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            ResumeLayout(false);
        }

        private SplitContainer splitContainer1;
        private Label lblTitle;
        private DataGridView dgvItems;
        private GroupBox grpDetails;
        private Label lblId;
        private TextBox txtId;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnNew;
        private Button btnSave;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;
    }
}