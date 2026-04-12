using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class RequestCoachingForm : Form
    {
        private Label lblGrade;
        private ComboBox comboGrades;
        private Label lblLecturer;
        private ComboBox comboLecturers;
        private Button btnConfirm;
        private Button btnReturn;

        public RequestCoachingForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblGrade = new Label();
            this.comboGrades = new ComboBox();
            this.lblLecturer = new Label();
            this.comboLecturers = new ComboBox();

            this.SuspendLayout();

            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new Point(20, 20);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new Size(39, 13);
            this.lblGrade.TabIndex = 0;
            this.lblGrade.Text = "Grade:";

            // 
            // comboGrades
            // 
            this.comboGrades.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboGrades.Location = new Point(120, 16);
            this.comboGrades.Name = "comboGrades";
            this.comboGrades.Size = new Size(200, 21);
            this.comboGrades.TabIndex = 1;
            this.comboGrades.Items.AddRange(new object[] { "Foundation", "Diploma", "Degree" });

            // 
            // lblLecturer
            // 
            this.lblLecturer.AutoSize = true;
            this.lblLecturer.Location = new Point(20, 60);
            this.lblLecturer.Name = "lblLecturer";
            this.lblLecturer.Size = new Size(55, 13);
            this.lblLecturer.TabIndex = 2;
            this.lblLecturer.Text = "Lecturer:";

            // 
            // comboLecturers
            // 
            this.comboLecturers.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboLecturers.Location = new Point(120, 56);
            this.comboLecturers.Name = "comboLecturers";
            this.comboLecturers.Size = new Size(200, 21);
            this.comboLecturers.TabIndex = 3;
            // leave items empty for now

            // 
            // btnConfirm
            // 
            this.btnConfirm = new Button();
            this.btnConfirm.Location = new Point(120, 96);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new Size(90, 30);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new EventHandler(this.BtnConfirm_Click);

            // 
            // btnReturn
            // 
            this.btnReturn = new Button();
            this.btnReturn.Location = new Point(230, 96);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new Size(90, 30);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new EventHandler(this.BtnReturn_Click);

            // 
            // RequestCoachingForm
            // 
            this.ClientSize = new Size(400, 150);
            this.Controls.Add(this.lblGrade);
            this.Controls.Add(this.comboGrades);
            this.Controls.Add(this.lblLecturer);
            this.Controls.Add(this.comboLecturers);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnReturn);
            this.Name = "RequestCoachingForm";
            this.Text = "Request Coaching";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (comboGrades.SelectedItem == null)
            {
                MessageBox.Show("Please select a grade.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboLecturers.SelectedItem == null)
            {
                MessageBox.Show("Please select a lecturer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var grade = comboGrades.SelectedItem.ToString();
            var lecturer = comboLecturers.SelectedItem.ToString();

            var req = new CoachingRequest
            {
                Id = Guid.NewGuid(),
                Grade = grade,
                Lecturer = lecturer,
                Status = "Pending",
                RequestedAt = DateTime.Now
            };

            RequestStore.Add(req);

            MessageBox.Show($"Request submitted and pending:\nGrade: {grade}\nLecturer: {lecturer}", "Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
