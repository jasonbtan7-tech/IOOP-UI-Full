using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class AddTrainerForm : Form
    {
        private Label lblId, lblName, lblGender, lblEmail, lblPhone, lblUsername, lblPassword;
        private TextBox txtId, txtName, txtEmail, txtPhone, txtUsername, txtPassword;
        private ComboBox comboGender;
        private Button btnOk;

        public Trainer CreatedTrainer { get; private set; }

        public AddTrainerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblId = new Label();
            this.lblName = new Label();
            this.lblGender = new Label();
            this.lblEmail = new Label();
            this.lblPhone = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();

            this.txtId = new TextBox();
            this.txtName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPhone = new TextBox();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();

            this.comboGender = new ComboBox();

            this.btnOk = new Button();

            this.SuspendLayout();

            // Row 1 - Id
            this.lblId.AutoSize = true;
            this.lblId.Location = new Point(12, 12);
            this.lblId.Text = "Id:";
            this.txtId.Location = new Point(120, 9);
            this.txtId.Size = new Size(200, 23);

            // Row 2 - Name
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(12, 42);
            this.lblName.Text = "Name:";
            this.txtName.Location = new Point(120, 39);
            this.txtName.Size = new Size(200, 23);

            // Row 3 - Gender
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new Point(12, 72);
            this.lblGender.Text = "Gender:";
            this.comboGender.Location = new Point(120, 69);
            this.comboGender.Size = new Size(200, 23);
            this.comboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

            // Row 4 - Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(12, 102);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new Point(120, 99);
            this.txtEmail.Size = new Size(200, 23);

            // Row 5 - Phone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new Point(12, 132);
            this.lblPhone.Text = "Phone:";
            this.txtPhone.Location = new Point(120, 129);
            this.txtPhone.Size = new Size(200, 23);

            // Row 6 - Username
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(12, 162);
            this.lblUsername.Text = "Username:";
            this.txtUsername.Location = new Point(120, 159);
            this.txtUsername.Size = new Size(200, 23);

            // Row 7 - Password
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(12, 192);
            this.lblPassword.Text = "Password:";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Location = new Point(120, 189);
            this.txtPassword.Size = new Size(200, 23);

            // OK button
            this.btnOk.Location = new Point(120, 228);
            this.btnOk.Size = new Size(90, 30);
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.BtnOk_Click);

            this.ClientSize = new Size(360, 280);
            this.Controls.AddRange(new Control[] {
                lblId, txtId,
                lblName, txtName,
                lblGender, comboGender,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                lblUsername, txtUsername,
                lblPassword, txtPassword,
                btnOk
            });
            this.Name = "AddTrainerForm";
            this.Text = "Add Trainer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void OnOk()
        {
            // simple validation
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill required fields: Id, Name, Username, Password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CreatedTrainer = new Trainer
            {
                Id = txtId.Text.Trim(),
                Name = txtName.Text.Trim(),
                Gender = comboGender.SelectedItem as string ?? string.Empty,
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            this.DialogResult = DialogResult.OK;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            OnOk();
        }
    }
}
