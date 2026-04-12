using System;
using System.Linq;
using System.Windows.Forms;

namespace assignment
{
    public partial class DashboardForm : Form
    {
        // handlers for the four main actions
        internal IViewSchedule ViewScheduleHandler { get; set; }
        internal IRequestCoaching RequestCoachingHandler { get; set; }
        internal IManageRequestsA ManageRequestsHandlerA { get; set; }

        public DashboardForm()
        {
            InitializeComponent();
            // initialize default handlers
            this.ViewScheduleHandler = new DefaultViewSchedule();
            this.RequestCoachingHandler = new DefaultRequestCoaching();
            this.ManageRequestsHandlerA = new DefaultManageRequestsA();
        }

        private void BtnViewSchedule_Click(object sender, EventArgs e)
        {
            this.ViewScheduleHandler?.Execute();
        }

        private void BtnRequestCoaching_Click(object sender, EventArgs e)
        {
            this.RequestCoachingHandler?.Execute();
        }

        private void BtnManageRequests1_Click(object sender, EventArgs e)
        {
            this.ManageRequestsHandlerA?.Execute();
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
            var login = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if (login != null)
            {
                login.Show();
                login.WindowState = FormWindowState.Normal;
                login.BringToFront();
                // clear login inputs
                try { login.ClearInputs(); } catch { }
            }

            // Close all open forms except the login form
            var openForms = Application.OpenForms.Cast<Form>().ToList();
            foreach (var f in openForms)
            {
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
