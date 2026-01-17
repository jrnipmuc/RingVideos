using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Monitor.Dialogs
{
    internal class PromptDialog : Form
    {
        public string ResponseText => txtResponse.Text.Trim();

        private PromptDialog(string title, string message, string defaultValue = "")
        {
            InitializeComponent();
            Text = title;
            lblMessage.Text = message;
            txtResponse.Text = defaultValue;
        }

        /// <summary>
        /// Shows a prompt dialog and returns the user's input, or null if cancelled
        /// </summary>
        public static string Show(string title, string message, string defaultValue = "")
        {
            using (var dialog = new PromptDialog(title, message, defaultValue))
            {
                return dialog.ShowDialog() == DialogResult.OK ? dialog.ResponseText : null;
            }
        }

        /// <summary>
        /// Shows a prompt dialog with a parent window and returns the user's input, or null if cancelled
        /// </summary>
        public static string Show(IWin32Window owner, string title, string message, string defaultValue = "")
        {
            using (var dialog = new PromptDialog(title, message, defaultValue))
            {
                return dialog.ShowDialog(owner) == DialogResult.OK ? dialog.ResponseText : null;
            }
        }

        private void InitializeComponent()
        {
            lblMessage = new Label();
            txtResponse = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // lblMessage
            lblMessage.AutoSize = false;
            lblMessage.Location = new Point(15, 15);
            lblMessage.Size = new Size(350, 60);
            lblMessage.Text = "Message";

            // txtResponse
            txtResponse.Location = new Point(15, 80);
            txtResponse.Size = new Size(350, 27);

            // btnOK
            btnOK.Location = new Point(170, 120);
            btnOK.Size = new Size(90, 30);
            btnOK.Text = "OK";
            btnOK.DialogResult = DialogResult.OK;

            // btnCancel
            btnCancel.Location = new Point(270, 120);
            btnCancel.Size = new Size(90, 30);
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;

            // PromptDialog
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(384, 171);
            Controls.Add(lblMessage);
            Controls.Add(txtResponse);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PromptDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input";

            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblMessage;
        private TextBox txtResponse;
        private Button btnOK;
        private Button btnCancel;
    }
}
