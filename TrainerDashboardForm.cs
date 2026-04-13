using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class TrainerDashboardForm : Form
    {
        private Label lblInfo;
        private Button btnLogout;
        private Button btnUpdateProfile;
        private Button btnClass;
        private Button btnViewStudent;
        private Button btnSendFeedback;

        public TrainerDashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblInfo = new Label();
            this.btnLogout = new Button();
            this.btnUpdateProfile = new Button();
            this.btnClass = new Button();
            this.btnViewStudent = new Button();
            this.btnSendFeedback = new Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(100, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Trainer Dashboard";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new Point(12, 35);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(75, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new EventHandler(this.BtnLogout_Click);
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Location = new Point(100, 35);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new Size(100, 23);
            this.btnUpdateProfile.TabIndex = 2;
            this.btnUpdateProfile.Text = "Update Profile";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new EventHandler(this.BtnUpdateProfile_Click);
            // 
            // btnClass
            // 
            this.btnClass.Location = new Point(12, 70);
            this.btnClass.Name = "btnClass";
            this.btnClass.Size = new Size(188, 30);
            this.btnClass.TabIndex = 3;
            this.btnClass.Text = "Class";
            this.btnClass.UseVisualStyleBackColor = true;
            this.btnClass.Click += new EventHandler(this.BtnClass_Click);
            // 
            // btnViewStudent
            // 
            this.btnViewStudent.Location = new Point(12, 110);
            this.btnViewStudent.Name = "btnViewStudent";
            this.btnViewStudent.Size = new Size(188, 30);
            this.btnViewStudent.TabIndex = 4;
            this.btnViewStudent.Text = "View Student";
            this.btnViewStudent.UseVisualStyleBackColor = true;
            this.btnViewStudent.Click += new EventHandler(this.BtnViewStudent_Click);
            // 
            // btnSendFeedback
            // 
            this.btnSendFeedback.Location = new Point(12, 150);
            this.btnSendFeedback.Name = "btnSendFeedback";
            this.btnSendFeedback.Size = new Size(188, 30);
            this.btnSendFeedback.TabIndex = 5;
            this.btnSendFeedback.Text = "Send Feedback";
            this.btnSendFeedback.UseVisualStyleBackColor = true;
            this.btnSendFeedback.Click += new EventHandler(this.BtnSendFeedback_Click);
            // 
            // TrainerDashboardForm
            // 
            this.ClientSize = new Size(220, 220);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUpdateProfile);
            this.Controls.Add(this.btnSendFeedback);
            this.Controls.Add(this.btnViewStudent);
            this.Controls.Add(this.btnClass);
            this.Controls.Add(this.lblInfo);
            this.Name = "TrainerDashboardForm";
            this.Text = "Trainer Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var login = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if (login != null)
            {
                login.Show();
                login.WindowState = FormWindowState.Normal;
                try { login.ClearInputs(); } catch { }
            }

            // Close this trainer dashboard
            this.Close();
        }

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            var f = new UpdateProfileForm();
            f.Show();
        }

        private void BtnClass_Click(object sender, EventArgs e)
        {
            var f = new ClassForm();
            f.Show();
        }

        private void BtnViewStudent_Click(object sender, EventArgs e)
        {
            var f = new ViewStudentsForm();
            f.Show();
        }

        private void BtnSendFeedback_Click(object sender, EventArgs e)
        {
            var f = new SendFeedbackForm();
            f.Show();
        }
    }
}
