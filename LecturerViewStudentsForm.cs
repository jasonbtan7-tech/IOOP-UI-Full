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
    public partial class LecturerViewStudentsForm : Form
    {
        public LecturerViewStudentsForm()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadStudents()
        {
            // If designer grid exists, bind to StudentStore
            try
            {
                if (this.Controls.ContainsKey("dataGridView1"))
                {
                    var grid = this.Controls["dataGridView1"] as DataGridView;
                    if (grid != null)
                    {
                        var dt = new DataTable();
                        dt.Columns.Add("StudentId", typeof(string));
                        dt.Columns.Add("Name", typeof(string));
                        dt.Columns.Add("Phone", typeof(string));
                        dt.Columns.Add("Email", typeof(string));
                        dt.Columns.Add("Address", typeof(string));
                        dt.Columns.Add("Gender", typeof(string));
                        dt.Columns.Add("PaymentStatus", typeof(string));
                        var students = StudentStore.GetAll();
                        if (students.Count == 0)
                        {
                            dt.Rows.Add("S001", "Alice Smith", "0123456789", "alice@example.com", "123 Main St", "Female", "Paid");
                        }
                        else
                        {
                            foreach (var s in students)
                            {
                                dt.Rows.Add(s.StudentId, s.Name, s.Phone, s.Email, s.Address, s.Gender, s.PaymentStatus);
                            }
                        }
                        grid.DataSource = dt;
                    }
                }
            }
            catch { }
        }
    }
}
