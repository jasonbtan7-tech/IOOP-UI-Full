using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class MakePaymentForm : Form
    {
        private Label lblGrade;
        private ComboBox comboGrades;
        private Label lblFinalPayment;
        private Button btnPay;
        private Button btnReturn;
        private Label lblStatus;

        // static flag to remember payment status across instances
        public static bool IsPaid { get; private set; }

        public MakePaymentForm()
        {
            InitializeComponent();
            UpdateUiForPaymentState();
        }

        private void InitializeComponent()
        {
            this.lblGrade = new Label();
            this.comboGrades = new ComboBox();
            this.lblFinalPayment = new Label();
            this.btnPay = new Button();
            this.btnReturn = new Button();
            this.lblStatus = new Label();

            this.SuspendLayout();

            // lblGrade
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new Point(20, 20);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new Size(39, 13);
            this.lblGrade.TabIndex = 0;
            this.lblGrade.Text = "Level:";

            // comboGrades
            this.comboGrades.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboGrades.Location = new Point(120, 16);
            this.comboGrades.Name = "comboGrades";
            this.comboGrades.Size = new Size(200, 21);
            this.comboGrades.TabIndex = 1;
            this.comboGrades.Items.AddRange(new object[] { "Foundation", "Diploma", "Degree" });
            this.comboGrades.SelectedIndexChanged += ComboGrades_SelectedIndexChanged;

            // lblFinalPayment
            this.lblFinalPayment.AutoSize = true;
            this.lblFinalPayment.Location = new Point(20, 60);
            this.lblFinalPayment.Name = "lblFinalPayment";
            this.lblFinalPayment.Size = new Size(100, 13);
            this.lblFinalPayment.TabIndex = 2;
            this.lblFinalPayment.Text = "Final payment: $0";

            // btnPay
            this.btnPay.Location = new Point(120, 92);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new Size(90, 30);
            this.btnPay.TabIndex = 3;
            this.btnPay.Text = "Pay";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += BtnPay_Click;

            // btnReturn
            this.btnReturn.Location = new Point(230, 92);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new Size(90, 30);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += BtnReturn_Click;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblStatus.Location = new Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(150, 21);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Payment successful";
            this.lblStatus.Visible = false;

            // MakePaymentForm
            this.ClientSize = new Size(400, 140);
            this.Controls.Add(this.lblGrade);
            this.Controls.Add(this.comboGrades);
            this.Controls.Add(this.lblFinalPayment);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lblStatus);
            this.Name = "MakePaymentForm";
            this.Text = "Make Payment";
            this.Load += MakePaymentForm_Load;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ComboGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFinalPaymentLabel();
        }

        private void UpdateFinalPaymentLabel()
        {
            var amount = GetAmountForLevel(comboGrades.SelectedItem as string);
            lblFinalPayment.Text = $"Final payment: ${amount}";
        }

        private int GetAmountForLevel(string level)
        {
            if (string.IsNullOrEmpty(level)) return 0;
            switch (level)
            {
                case "Foundation": return 100;
                case "Diploma": return 200;
                case "Degree": return 300;
                default: return 0;
            }
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (IsPaid)
            {
                MessageBox.Show("Payment has already been completed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboGrades.SelectedItem == null)
            {
                MessageBox.Show("Please select a level before paying.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // simulate payment processing
            IsPaid = true;

            // hide inputs and show success message
            lblGrade.Visible = false;
            comboGrades.Visible = false;
            lblFinalPayment.Visible = false;
            btnPay.Visible = false;
            btnReturn.Visible = false;

            lblStatus.Text = "Payment successful";
            lblStatus.Visible = true;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateUiForPaymentState()
        {
            if (IsPaid)
            {
                // show only status
                lblGrade.Visible = false;
                comboGrades.Visible = false;
                lblFinalPayment.Visible = false;
                btnPay.Visible = false;
                btnReturn.Visible = true; // allow return

                lblStatus.Text = "Payment already done";
                lblStatus.Visible = true;
            }
            else
            {
                lblStatus.Visible = false;
                lblGrade.Visible = true;
                comboGrades.Visible = true;
                lblFinalPayment.Visible = true;
                btnPay.Visible = true;
                btnReturn.Visible = true;
            }
        }

        private void MakePaymentForm_Load(object sender, EventArgs e)
        {
            // ensure status label is hidden until user pays
            if (!IsPaid)
            {
                lblStatus.Visible = false;
            }
        }
    }
}
