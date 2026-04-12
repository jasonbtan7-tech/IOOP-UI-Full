using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = txtUsername.Text.Trim();
            var pass = txtPassword.Text;
            // Use AuthManager for validation
            if (AuthManager.ValidateAdmin(user, pass))
            {
                Session.CurrentRole = "admin";
                Session.CurrentUsername = user;
                var admin = new AdminDashboardForm();
                admin.Show();
                this.Hide();
                return;
            }

            if (AuthManager.ValidateStudent(user, pass))
            {
                Session.CurrentRole = "student";
                Session.CurrentUsername = user;
                var dash = new DashboardForm();
                dash.Show();
                this.Hide();
                return;
            }

            if (AuthManager.ValidateTrainer(user, pass))
            {
                Session.CurrentRole = "trainer";
                Session.CurrentUsername = user;
                var trainerDash = new TrainerDashboardForm();
                trainerDash.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Clear username and password inputs and set focus to username
        public void ClearInputs()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}
