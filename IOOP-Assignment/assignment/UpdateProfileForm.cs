using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class UpdateProfileForm : Form
    {
        private Label lblHeader;
        private Label lblCurrent;
        private TextBox txtCurrent;
        private Label lblNew;
        private TextBox txtNew;
        private Label lblConfirm;
        private TextBox txtConfirm;
        private Button btnSave;
        private Button btnCancel;

        public UpdateProfileForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.lblCurrent = new Label();
            this.txtCurrent = new TextBox();
            this.lblNew = new Label();
            this.txtNew = new TextBox();
            this.lblConfirm = new Label();
            this.txtConfirm = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblHeader.Location = new Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(140, 21);
            this.lblHeader.Text = "Change Password";
            // 
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new Point(12, 45);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new Size(90, 13);
            this.lblCurrent.Text = "Current Password";
            // 
            // txtCurrent
            // 
            this.txtCurrent.Location = new Point(120, 42);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new Size(180, 23);
            this.txtCurrent.PasswordChar = '*';
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new Point(12, 78);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new Size(77, 13);
            this.lblNew.Text = "New Password";
            // 
            // txtNew
            // 
            this.txtNew.Location = new Point(120, 75);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new Size(180, 23);
            this.txtNew.PasswordChar = '*';
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Location = new Point(12, 111);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new Size(89, 13);
            this.lblConfirm.Text = "Confirm Password";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new Point(120, 108);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new Size(180, 23);
            this.txtConfirm.PasswordChar = '*';
            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(120, 145);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(75, 25);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(210, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 25);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);
            // 
            // UpdateProfileForm
            // 
            this.ClientSize = new Size(320, 200);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.txtCurrent);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.lblHeader);
            this.Name = "UpdateProfileForm";
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var current = txtCurrent.Text;
            var nw = txtNew.Text;
            var conf = txtConfirm.Text;

            if (string.IsNullOrEmpty(nw) || string.IsNullOrEmpty(conf))
            {
                MessageBox.Show("Please enter and confirm the new password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nw != conf)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var role = Session.CurrentRole ?? string.Empty;
            var username = Session.CurrentUsername ?? string.Empty;

            var ok = AuthManager.UpdatePasswordForCurrent(role, username, current, nw);
            if (!ok)
            {
                MessageBox.Show("Current password incorrect or cannot update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Password updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
