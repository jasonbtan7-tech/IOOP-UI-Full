using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class SendFeedbackForm : Form
    {
        private Label lblHeader;
        private TextBox txtFeedback;
        private Button btnSubmit;

        public SendFeedbackForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.txtFeedback = new TextBox();
            this.btnSubmit = new Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblHeader.Location = new Point(12, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(110, 21);
            this.lblHeader.Text = "Send Feedback";
            // 
            // txtFeedback
            // 
            this.txtFeedback.Location = new Point(16, 40);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new Size(420, 180);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new Point(260, 230);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(80, 30);
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new EventHandler(this.BtnSubmit_Click);
            // 
            // 
            // SendFeedbackForm
            // 
            this.ClientSize = new Size(460, 280);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.btnSubmit);
            this.Name = "SendFeedbackForm";
            this.Text = "Send Feedback";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var text = txtFeedback.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Please enter feedback before submitting.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var f = new Feedback
            {
                Id = Guid.NewGuid(),
                Text = text,
                SubmittedAt = DateTime.Now
            };

            FeedbackStore.Add(f);

            MessageBox.Show("Feedback submitted. Thank you.", "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
