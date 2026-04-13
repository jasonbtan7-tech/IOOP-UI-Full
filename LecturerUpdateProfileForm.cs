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
    public partial class LecturerUpdateProfileForm : Form
    {
        public LecturerUpdateProfileForm()
        {
            InitializeComponent();
            this.button1.Click += Button1_Click; // Save
            this.button2.Click += Button2_Click; // Cancel

            // load current lecturer profile into fields
            LoadProfile();
        }

        private void StudentID_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ContactNumber_Click(object sender, EventArgs e)
        {

        }

        private void StudentEmail_Click(object sender, EventArgs e)
        {

        }

        private void StudentName_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadProfile()
        {
            try
            {
                var username = Session.CurrentUsername;
                if (string.IsNullOrEmpty(username)) return;
                var tr = TrainerStore.GetAll().Find(t => string.Equals(t.Username, username, StringComparison.OrdinalIgnoreCase));
                if (tr == null) return;

                // map fields: textBox1 = Email, textBox2 = ContactNumber (Phone), textBox3 = NewPassword, textBox4 = Confirm Password
                textBox1.Text = tr.Email ?? string.Empty;
                textBox2.Text = tr.Phone ?? string.Empty;
                // don't load existing password into UI
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
            }
            catch { }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Save
            try
            {
                var username = Session.CurrentUsername;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("No lecturer session found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var tr = TrainerStore.GetAll().Find(t => string.Equals(t.Username, username, StringComparison.OrdinalIgnoreCase));
                if (tr == null)
                {
                    MessageBox.Show("Lecturer record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // update email and phone
                tr.Email = textBox1.Text.Trim();
                tr.Phone = textBox2.Text.Trim();

                // update password if provided and confirmed
                var newPwd = textBox3.Text;
                var confirm = textBox4.Text;
                if (!string.IsNullOrEmpty(newPwd) || !string.IsNullOrEmpty(confirm))
                {
                    if (newPwd != confirm)
                    {
                        MessageBox.Show("New password and confirm password do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    tr.Password = newPwd;
                }

                // persist
                try { TrainerStore.SaveToDisk(); } catch { }

                MessageBox.Show("Profile updated.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
