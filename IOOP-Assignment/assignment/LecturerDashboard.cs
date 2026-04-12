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

            var f = new RegisterStudentForm();
            f.Show();

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
            var f = new Form1();
            f.Show();
        }
    }
}
