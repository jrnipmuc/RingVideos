namespace Event_Monitor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStripMain = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            statusLabelRootDirectory = new ToolStripStatusLabel();
            statusLabelConnection = new ToolStripStatusLabel();
            statusLabelAuthExpires = new ToolStripStatusLabel();
            menuStripMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            devicesToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            sitesToolStripMenuItem1 = new ToolStripMenuItem();
            loginsToolStripMenuItem = new ToolStripMenuItem();
            rootDirectoryToolStripMenuItem = new ToolStripMenuItem();
            tabControlMain = new TabControl();
            statusStripMain.SuspendLayout();
            menuStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripMain
            // 
            statusStripMain.ImageScalingSize = new Size(24, 24);
            statusStripMain.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, statusLabelRootDirectory, statusLabelConnection, statusLabelAuthExpires });
            statusStripMain.Location = new Point(0, 924);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(1574, 22);
            statusStripMain.TabIndex = 0;
            statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 15);
            // 
            // statusLabelRootDirectory
            // 
            statusLabelRootDirectory.Name = "statusLabelRootDirectory";
            statusLabelRootDirectory.Size = new Size(0, 15);
            // 
            // statusLabelConnection
            // 
            statusLabelConnection.Name = "statusLabelConnection";
            statusLabelConnection.Size = new Size(0, 15);
            // 
            // statusLabelAuthExpires
            // 
            statusLabelAuthExpires.Name = "statusLabelAuthExpires";
            statusLabelAuthExpires.Size = new Size(0, 15);
            // 
            // menuStripMain
            // 
            menuStripMain.ImageScalingSize = new Size(24, 24);
            menuStripMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1574, 33);
            menuStripMain.TabIndex = 1;
            menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { devicesToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "&File";
            fileToolStripMenuItem.Click += fileToolStripMenuItem_Click;
            // 
            // devicesToolStripMenuItem
            // 
            devicesToolStripMenuItem.Name = "devicesToolStripMenuItem";
            devicesToolStripMenuItem.Size = new Size(174, 34);
            devicesToolStripMenuItem.Text = "&Devices";
            devicesToolStripMenuItem.Click += devicesToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sitesToolStripMenuItem1, loginsToolStripMenuItem, rootDirectoryToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "&Settings";
            // 
            // sitesToolStripMenuItem1
            // 
            sitesToolStripMenuItem1.Name = "sitesToolStripMenuItem1";
            sitesToolStripMenuItem1.Size = new Size(270, 34);
            sitesToolStripMenuItem1.Text = "&Sites";
            sitesToolStripMenuItem1.Click += sitesToolStripMenuItem1_Click;
            // 
            // loginsToolStripMenuItem
            // 
            loginsToolStripMenuItem.Name = "loginsToolStripMenuItem";
            loginsToolStripMenuItem.Size = new Size(270, 34);
            loginsToolStripMenuItem.Text = "&Logins";
            loginsToolStripMenuItem.Click += loginsToolStripMenuItem_Click;
            // 
            // rootDirectoryToolStripMenuItem
            // 
            rootDirectoryToolStripMenuItem.Name = "rootDirectoryToolStripMenuItem";
            rootDirectoryToolStripMenuItem.Size = new Size(270, 34);
            rootDirectoryToolStripMenuItem.Text = "&Root Directory";
            rootDirectoryToolStripMenuItem.Click += rootDirectoryToolStripMenuItem_Click;
            // 
            // tabControlMain
            // 
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 33);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1574, 891);
            tabControlMain.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1574, 946);
            Controls.Add(tabControlMain);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            Name = "Form1";
            Text = "Ring Event Monitor";
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripMain;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem loginsToolStripMenuItem;
        private ToolStripMenuItem devicesToolStripMenuItem;
        private TabControl tabControlMain;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel statusLabelRootDirectory;
        private ToolStripStatusLabel statusLabelConnection;
        private ToolStripStatusLabel statusLabelAuthExpires;
        private ToolStripMenuItem sitesToolStripMenuItem1;
        private ToolStripMenuItem rootDirectoryToolStripMenuItem;
    }
}
