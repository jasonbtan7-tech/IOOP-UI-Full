using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class AdminDashboardForm : Form
    {
        private Label lblInfo;
        private Button btnLogout;
        private Button btnViewFeedback;
        private Button btnViewIncomeReport;
        private Button btnManageTrainer;
        private Button btnUpdateProfile;

        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnViewFeedback = new System.Windows.Forms.Button();
            this.btnViewIncomeReport = new System.Windows.Forms.Button();
            this.btnManageTrainer = new System.Windows.Forms.Button();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(116, 16);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Admin Dashboard";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(117, 28);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // btnViewFeedback
            // 
            this.btnViewFeedback.Location = new System.Drawing.Point(24, 73);
            this.btnViewFeedback.Name = "btnViewFeedback";
            this.btnViewFeedback.Size = new System.Drawing.Size(150, 30);
            this.btnViewFeedback.TabIndex = 2;
            this.btnViewFeedback.Text = "View Feedback";
            this.btnViewFeedback.UseVisualStyleBackColor = true;
            this.btnViewFeedback.Click += new System.EventHandler(this.BtnViewFeedback_Click);
            // 
            // btnViewIncomeReport
            // 
            this.btnViewIncomeReport.Location = new System.Drawing.Point(24, 113);
            this.btnViewIncomeReport.Name = "btnViewIncomeReport";
            this.btnViewIncomeReport.Size = new System.Drawing.Size(150, 30);
            this.btnViewIncomeReport.TabIndex = 3;
            this.btnViewIncomeReport.Text = "View Income Report";
            this.btnViewIncomeReport.UseVisualStyleBackColor = true;
            this.btnViewIncomeReport.Click += new System.EventHandler(this.BtnViewIncomeReport_Click);
            // 
            // btnManageTrainer
            // 
            this.btnManageTrainer.Location = new System.Drawing.Point(24, 153);
            this.btnManageTrainer.Name = "btnManageTrainer";
            this.btnManageTrainer.Size = new System.Drawing.Size(150, 30);
            this.btnManageTrainer.TabIndex = 4;
            this.btnManageTrainer.Text = "Manage Trainer";
            this.btnManageTrainer.UseVisualStyleBackColor = true;
            this.btnManageTrainer.Click += new System.EventHandler(this.BtnManageTrainer_Click);
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Location = new System.Drawing.Point(24, 193);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(150, 30);
            this.btnUpdateProfile.TabIndex = 5;
            this.btnUpdateProfile.Text = "Update Profile";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.BtnUpdateProfile_Click);
            // 
            // AdminDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(202, 260);
            this.Controls.Add(this.btnUpdateProfile);
            this.Controls.Add(this.btnManageTrainer);
            this.Controls.Add(this.btnViewIncomeReport);
            this.Controls.Add(this.btnViewFeedback);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblInfo);
            this.Name = "AdminDashboardForm";
            this.Text = "Admin Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = null;
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                var f = Application.OpenForms[i];
                if (f is Form1)
                {
                    login = (Form1)f;
                    break;
                }
            }

            if (login != null)
            {
                login.Show();
                login.WindowState = FormWindowState.Normal;
                try { login.ClearInputs(); } catch { }
            }

            this.Close();
        }

        private void BtnViewFeedback_Click(object sender, EventArgs e)
        {
            var f = new ViewFeedbackForm();
            f.Show();
        }

        private void BtnViewIncomeReport_Click(object sender, EventArgs e)
        {
            var f = new ViewIncomeReportForm();
            f.Show();
        }

            
        private void BtnManageTrainer_Click(object sender, EventArgs e)
        {
            var f = new ManageTrainerForm();
            f.Show();
        }

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            var f = new UpdateProfileForm();
            f.Show();
        }
    }
}
