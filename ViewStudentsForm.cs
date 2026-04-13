using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class ViewStudentsForm : Form
    {
        private Label lblHeader;
        private DataGridView dgvStudents;
        private Panel pnlTop;

        public ViewStudentsForm()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.dgvStudents = new DataGridView();
            this.pnlTop = new Panel();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 48;
            this.pnlTop.Padding = new Padding(8);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblHeader.Location = new Point(12, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(120, 21);
            this.lblHeader.Text = "Students List";
            // 
            // (close button removed)
            // 
            // dgvStudents
            // 
            this.dgvStudents.Dock = DockStyle.Fill;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // add header controls to panel first
            this.pnlTop.Controls.Add(this.lblHeader);

            // ViewStudentsForm
            // 
            this.ClientSize = new Size(784, 461);
            // add panel first (top) then fill grid
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.dgvStudents);
            this.Name = "ViewStudentsForm";
            this.Text = "View Students";
            this.ResumeLayout(false);
        }

        private void LoadStudents()
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
                // ensure sample appears
                dt.Rows.Add("S001", "Alice Smith", "0123456789", "alice@example.com", "123 Main St", "Female", "Paid");
            }
            else
            {
                foreach (var s in students)
                {
                    dt.Rows.Add(s.StudentId, s.Name, s.Phone, s.Email, s.Address, s.Gender, s.PaymentStatus);
                }
            }

            this.dgvStudents.DataSource = dt;

            if (this.dgvStudents.Columns.Contains("PaymentStatus"))
            {
                this.dgvStudents.Columns["PaymentStatus"].HeaderText = "Payment Status";
            }

            foreach (DataGridViewRow row in this.dgvStudents.Rows)
            {
                var val = row.Cells["PaymentStatus"].Value as string;
                if (string.Equals(val, "Unpaid", StringComparison.OrdinalIgnoreCase))
                {
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else if (string.Equals(val, "Paid", StringComparison.OrdinalIgnoreCase))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
    }
}
