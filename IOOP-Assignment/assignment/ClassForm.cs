using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace assignment
{
    public class ClassForm : Form
    {
        private Label lblHeader;
        private ListBox lstClasses;
        private TextBox txtClassName;
        private Label lblName;
        private Button btnAddClass;
        private Button btnUpdateClass;
        private Button btnDeleteClass;

        // Simple in-memory store of class names for this example
        private List<string> classes = new List<string>();

        public ClassForm()
        {
            InitializeComponent();
            LoadSampleData();
            RefreshList();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.lstClasses = new ListBox();
            this.txtClassName = new TextBox();
            this.lblName = new Label();
            this.btnAddClass = new Button();
            this.btnUpdateClass = new Button();
            this.btnDeleteClass = new Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblHeader.Location = new Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(86, 21);
            this.lblHeader.Text = "Manage Class";
            // 
            // lstClasses
            // 
            this.lstClasses.Location = new Point(16, 40);
            this.lstClasses.Name = "lstClasses";
            this.lstClasses.Size = new Size(300, 160);
            this.lstClasses.SelectedIndexChanged += new EventHandler(this.LstClasses_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(16, 210);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(67, 13);
            this.lblName.Text = "Class Name";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new Point(90, 206);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new Size(226, 23);
            // 
            // btnAddClass
            // 
            this.btnAddClass.Location = new Point(16, 240);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new Size(95, 30);
            this.btnAddClass.Text = "Add Class";
            this.btnAddClass.UseVisualStyleBackColor = true;
            this.btnAddClass.Click += new EventHandler(this.BtnAddClass_Click);
            // 
            // btnUpdateClass
            // 
            this.btnUpdateClass.Location = new Point(120, 240);
            this.btnUpdateClass.Name = "btnUpdateClass";
            this.btnUpdateClass.Size = new Size(95, 30);
            this.btnUpdateClass.Text = "Update Class";
            this.btnUpdateClass.UseVisualStyleBackColor = true;
            this.btnUpdateClass.Click += new EventHandler(this.BtnUpdateClass_Click);
            // 
            // btnDeleteClass
            // 
            this.btnDeleteClass.Location = new Point(221, 240);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new Size(95, 30);
            this.btnDeleteClass.Text = "Delete Class";
            this.btnDeleteClass.UseVisualStyleBackColor = true;
            this.btnDeleteClass.Click += new EventHandler(this.BtnDeleteClass_Click);
            // 
            // ClassForm
            // 
            this.ClientSize = new Size(340, 290);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lstClasses);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.btnAddClass);
            this.Controls.Add(this.btnUpdateClass);
            this.Controls.Add(this.btnDeleteClass);
            this.Name = "ClassForm";
            this.Text = "Class Management";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadSampleData()
        {
            // Add some sample items so the UI isn't empty. In a real app, load from DB.
            classes.AddRange(new[] { "Math 101", "Programming 201", "Physics 105" });
        }

        private void RefreshList()
        {
            this.lstClasses.Items.Clear();
            foreach (var c in classes)
            {
                this.lstClasses.Items.Add(c);
            }
        }

        private void BtnAddClass_Click(object sender, EventArgs e)
        {
            var name = txtClassName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a class name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            classes.Add(name);
            RefreshList();
            txtClassName.Clear();
        }

        private void BtnUpdateClass_Click(object sender, EventArgs e)
        {
            if (lstClasses.SelectedIndex < 0)
            {
                MessageBox.Show("Select a class to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = txtClassName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a class name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            classes[lstClasses.SelectedIndex] = name;
            RefreshList();
            txtClassName.Clear();
        }

        private void BtnDeleteClass_Click(object sender, EventArgs e)
        {
            if (lstClasses.SelectedIndex < 0)
            {
                MessageBox.Show("Select a class to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idx = lstClasses.SelectedIndex;
            var name = classes[idx];
            var ok = MessageBox.Show(string.Format("Delete '{0}'?", name), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                classes.RemoveAt(idx);
                RefreshList();
                txtClassName.Clear();
            }
        }

        private void LstClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClasses.SelectedIndex >= 0)
            {
                txtClassName.Text = lstClasses.SelectedItem.ToString();
            }
        }
    }
}
