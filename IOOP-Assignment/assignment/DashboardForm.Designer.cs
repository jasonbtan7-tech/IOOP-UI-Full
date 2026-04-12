using System;
using System.Windows.Forms;

namespace assignment
{
    partial class DashboardForm
    {
        private Label lblWelcome;
        private Button btnViewSchedule;
        private Button btnRequestCoaching;
        private Button btnManageRequests1;
        private Button btnMakePayment;
        private Button btnUpdateProfile;
        private Button btnLogout;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnViewSchedule = new System.Windows.Forms.Button();
            this.btnRequestCoaching = new System.Windows.Forms.Button();
            this.btnManageRequests1 = new System.Windows.Forms.Button();
            this.btnMakePayment = new System.Windows.Forms.Button();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(12, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(278, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome to dashboard";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // btnViewSchedule
            // 
            this.btnViewSchedule.Location = new System.Drawing.Point(35, 75);
            this.btnViewSchedule.Name = "btnViewSchedule";
            this.btnViewSchedule.Size = new System.Drawing.Size(200, 40);
            this.btnViewSchedule.TabIndex = 1;
            this.btnViewSchedule.Text = "View My Schedule";
            this.btnViewSchedule.UseVisualStyleBackColor = true;
            this.btnViewSchedule.Click += new System.EventHandler(this.BtnViewSchedule_Click);
            // 
            // btnRequestCoaching
            // 
            this.btnRequestCoaching.Location = new System.Drawing.Point(35, 125);
            this.btnRequestCoaching.Name = "btnRequestCoaching";
            this.btnRequestCoaching.Size = new System.Drawing.Size(200, 40);
            this.btnRequestCoaching.TabIndex = 2;
            this.btnRequestCoaching.Text = "Request Coaching";
            this.btnRequestCoaching.UseVisualStyleBackColor = true;
            this.btnRequestCoaching.Click += new System.EventHandler(this.BtnRequestCoaching_Click);
            // 
            // btnManageRequests1
            // 
            this.btnManageRequests1.Location = new System.Drawing.Point(35, 175);
            this.btnManageRequests1.Name = "btnManageRequests1";
            this.btnManageRequests1.Size = new System.Drawing.Size(200, 40);
            this.btnManageRequests1.TabIndex = 3;
            this.btnManageRequests1.Text = "Manage Requests";
            this.btnManageRequests1.UseVisualStyleBackColor = true;
            this.btnManageRequests1.Click += new System.EventHandler(this.BtnManageRequests1_Click);
            // 
            // btnMakePayment
            // 
            this.btnMakePayment.Location = new System.Drawing.Point(35, 225);
            this.btnMakePayment.Name = "btnMakePayment";
            this.btnMakePayment.Size = new System.Drawing.Size(200, 40);
            this.btnMakePayment.TabIndex = 4;
            this.btnMakePayment.Text = "Make Payment";
            this.btnMakePayment.UseVisualStyleBackColor = true;
            this.btnMakePayment.Click += new System.EventHandler(this.BtnMakePayment_Click);
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Location = new System.Drawing.Point(35, 275);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(200, 40);
            this.btnUpdateProfile.TabIndex = 5;
            this.btnUpdateProfile.Text = "Update Profile";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.BtnUpdateProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(226, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(60, 24);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // DashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(298, 350);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnMakePayment);
            this.Controls.Add(this.btnManageRequests1);
            this.Controls.Add(this.btnRequestCoaching);
            this.Controls.Add(this.btnViewSchedule);
            this.Controls.Add(this.btnUpdateProfile);
            this.Controls.Add(this.lblWelcome);
            this.Name = "DashboardForm";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
