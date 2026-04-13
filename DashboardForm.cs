using System;
using System.Windows.Forms;

namespace assignment
{
    public partial class DashboardForm : Form
    {
        // handlers for the four main actions (simple delegates — no interfaces)
        internal Action ViewScheduleHandler { get; set; }
        internal Action RequestCoachingHandler { get; set; }
        internal Action ManageRequestsHandlerA { get; set; }

        public DashboardForm()
        {
            InitializeComponent();
            // initialize default handlers (use simple actions instead of interface implementations)
            this.ViewScheduleHandler = () =>
            {
                var f = new ViewScheduleForm();
                try { f.SetStudentId(Session.CurrentUsername); } catch { }
                f.Show();
            };

            this.RequestCoachingHandler = () =>
            {
                var f = new RequestCoachingForm();
                f.Show();
            };

            this.ManageRequestsHandlerA = () =>
            {
                var f = new ManageRequestsFormA();
                f.Show();
            };
        }

        private void BtnViewSchedule_Click(object sender, EventArgs e)
        {
            if (this.ViewScheduleHandler != null) this.ViewScheduleHandler();
        }

        private void BtnRequestCoaching_Click(object sender, EventArgs e)
        {
            if (this.RequestCoachingHandler != null) this.RequestCoachingHandler();
        }

        private void BtnManageRequests1_Click(object sender, EventArgs e)
        {
            if (this.ManageRequestsHandlerA != null) this.ManageRequestsHandlerA();
        }

        private void BtnMakePayment_Click(object sender, EventArgs e)
        {
            // Open a simple MakePayment form (placeholder)
            var f = new MakePaymentForm();
            f.Show();
        }

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            var f = new UpdateProfileForm();
            f.Show();
        }



        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Show the login form (Form1) if it exists, then close all other open forms
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
                login.BringToFront();
                // clear login inputs
                try { login.ClearInputs(); } catch { }
            }

            // Collect open forms and close them (except login)
            var toClose = new System.Collections.Generic.List<Form>();
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                toClose.Add(Application.OpenForms[i]);
            }

            for (int i = 0; i < toClose.Count; i++)
            {
                var f = toClose[i];
                if (login != null && object.ReferenceEquals(f, login))
                    continue;

                try
                {
                    f.Close();
                }
                catch
                {
                    // ignore any errors while closing forms
                }
            }
        }

        private void DashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Do not exit application here; main form (login) controls app lifetime.
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
