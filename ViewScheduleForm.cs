using System;
using System.Windows.Forms;

namespace assignment
{
    public class ViewScheduleForm : Form
    {
        private Label lblStudentId;
        private ListView lvSchedule;
        private ColumnHeader colId;
        private ColumnHeader colName;
        private ColumnHeader colTime;
        private ColumnHeader colLecture;

        public ViewScheduleForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblStudentId = new System.Windows.Forms.Label();
            this.lvSchedule = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLecture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Location = new System.Drawing.Point(12, 9);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(71, 16);
            this.lblStudentId.TabIndex = 1;
            this.lblStudentId.Text = "Student ID:";
            // 
            // lvSchedule
            // 
            this.lvSchedule.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colName,
            this.colTime,
            this.colLecture});
            this.lvSchedule.FullRowSelect = true;
            this.lvSchedule.GridLines = true;
            this.lvSchedule.HideSelection = false;
            this.lvSchedule.Location = new System.Drawing.Point(12, 35);
            this.lvSchedule.Name = "lvSchedule";
            this.lvSchedule.Size = new System.Drawing.Size(376, 253);
            this.lvSchedule.TabIndex = 2;
            this.lvSchedule.UseCompatibleStateImageBehavior = false;
            this.lvSchedule.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 80;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 120;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 80;
            // 
            // colLecture
            // 
            this.colLecture.Text = "Lecture";
            this.colLecture.Width = 90;
            // 
            // ViewScheduleForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lvSchedule);
            this.Controls.Add(this.lblStudentId);
            this.Name = "ViewScheduleForm";
            this.Text = "View My Schedule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Public helpers to update the UI
        public void SetStudentId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.lblStudentId.Text = "Student ID:";
            }
            else
            {
                this.lblStudentId.Text = "Student ID: " + id;
            }
        }

        public void AddScheduleRow(string id, string name, string time, string lecture)
        {
            var item = new ListViewItem(new[] { id ?? string.Empty, name ?? string.Empty, time ?? string.Empty, lecture ?? string.Empty });
            this.lvSchedule.Items.Add(item);
        }

        public void ClearSchedule()
        {
            this.lvSchedule.Items.Clear();
        }
    }
}
