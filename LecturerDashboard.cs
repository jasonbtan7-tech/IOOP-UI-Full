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
    public partial class LecturerDashboard : UserControl
    {
        public LecturerDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // RegisterStudentForm is a UserControl; host it inside a Form when opened standalone
            var uc = new RegisterStudentForm();
            var host = new Form();
            host.Text = "Register Student";
            host.StartPosition = FormStartPosition.CenterParent;
            host.ClientSize = uc.Size;
            uc.Dock = DockStyle.Fill;
            host.Controls.Add(uc);
            host.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var f = new LecturerUpdateProfileForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var f = new LecturerApproveRequestsForm();
            f.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new LecturerViewStudentsForm();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Log out: show login form and close parent form if applicable
            var f = new Form1();
            f.Show();
            var parentForm = this.FindForm();
            if (parentForm != null) parentForm.Close();
        }
    }
}
