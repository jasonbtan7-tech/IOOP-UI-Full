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
    public partial class RegisterStudentForm : UserControl
    {
        public RegisterStudentForm()
        {
            InitializeComponent();
            // wire up buttons
            this.Register.Click += Register_Click;
            this.Cancel.Click += Cancel_Click;
        }

        private void Module_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            // validate inputs
            var id = textBox1.Text.Trim();
            var name = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Student ID and Name are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ensure unique StudentId
            var exists = StudentStore.GetAll().Find(x => string.Equals(x.StudentId, id, StringComparison.OrdinalIgnoreCase));
            if (exists != null)
            {
                MessageBox.Show("A student with that ID already exists.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var s = new Student
            {
                StudentId = id,
                Name = name,
                Email = textBox3.Text.Trim(),
                Phone = textBox4.Text.Trim(),
                Address = textBox5.Text.Trim(),
                Gender = textBox6.Text.Trim(),
                Module = comboBox1.Text,
                Month = textBox6.Text, // reuse field if applicable
                PaymentStatus = "Unpaid"
            };

            StudentStore.Add(s);
            MessageBox.Show("Student registered.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // clear inputs
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            // clear fields
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
        }
    }
}
