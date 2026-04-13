using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class ViewFeedbackForm : Form
    {
        private Label lblHeader;
        private DataGridView dgvFeedbacks;
        private Panel pnlTop;

        public ViewFeedbackForm()
        {
            InitializeComponent();
            LoadFeedbacks();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.dgvFeedbacks = new DataGridView();
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
            this.lblHeader.Size = new Size(140, 21);
            this.lblHeader.Text = "Trainer Feedbacks";
            // 
            // dgvFeedbacks
            // 
            this.dgvFeedbacks.Dock = DockStyle.Fill;
            this.dgvFeedbacks.Name = "dgvFeedbacks";
            this.dgvFeedbacks.ReadOnly = true;
            this.dgvFeedbacks.AllowUserToAddRows = false;
            this.dgvFeedbacks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvFeedbacks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // compose form
            // 
            this.pnlTop.Controls.Add(this.lblHeader);
            this.ClientSize = new Size(700, 460);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.dgvFeedbacks);
            this.Name = "ViewFeedbackForm";
            this.Text = "View Feedback";
            this.ResumeLayout(false);
        }

        private void LoadFeedbacks()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("SubmittedAt", typeof(DateTime));

            var all = FeedbackStore.GetAll();
            // sort by SubmittedAt descending without LINQ
            for (int i = 0; i < all.Count; i++)
            {
                for (int j = i + 1; j < all.Count; j++)
                {
                    if (all[j].SubmittedAt > all[i].SubmittedAt)
                    {
                        var tmp = all[i];
                        all[i] = all[j];
                        all[j] = tmp;
                    }
                }
            }

            for (int i = 0; i < all.Count; i++)
            {
                var f = all[i];
                dt.Rows.Add(f.Id.ToString(), f.Text, f.SubmittedAt);
            }

            this.dgvFeedbacks.DataSource = dt;
            if (this.dgvFeedbacks.Columns.Contains("Id"))
                this.dgvFeedbacks.Columns["Id"].Visible = false;
        }
    }
}
