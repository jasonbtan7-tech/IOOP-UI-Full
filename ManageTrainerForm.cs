using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace assignment
{
    public class ManageTrainerForm : Form
    {
        private Label lblHeader;
        private DataGridView dgvTrainers;
        private Button btnAdd;
        private Button btnDelete;
        private Panel pnlTop;

        public ManageTrainerForm()
        {
            InitializeComponent();
            LoadTrainers();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new Label();
            this.dgvTrainers = new DataGridView();
            this.btnAdd = new Button();
            this.btnDelete = new Button();
            this.pnlTop = new Panel();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 48;
            this.pnlTop.Padding = new Padding(8);

            // lblHeader
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblHeader.Location = new Point(12, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(140, 21);
            this.lblHeader.Text = "Manage Trainers";

            // buttons
            this.btnAdd.Location = new Point(12, 60);
            this.btnAdd.Size = new Size(120, 28);
            this.btnAdd.Text = "Add Trainer...";
            this.btnAdd.Click += new EventHandler(this.BtnAdd_Click);

            this.btnDelete.Location = new Point(140, 60);
            this.btnDelete.Size = new Size(120, 28);
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.Click += new EventHandler(this.BtnDelete_Click);

            // view feedback button
            var btnViewFb = new Button();
            btnViewFb.Location = new Point(268, 60);
            btnViewFb.Size = new Size(120, 28);
            btnViewFb.Text = "View Feedback";
            btnViewFb.Click += (s, e) => ViewFeedbackForSelected();
            this.Controls.Add(btnViewFb);

            // dgvTrainers
            this.dgvTrainers.Dock = DockStyle.Bottom;
            this.dgvTrainers.Height = 300;
            this.dgvTrainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrainers.ReadOnly = true;
            this.dgvTrainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrainers.AllowUserToAddRows = false;

            // compose
            this.pnlTop.Controls.Add(this.lblHeader);
            this.ClientSize = new Size(700, 400);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvTrainers);
            this.Name = "ManageTrainerForm";
            this.Text = "Manage Trainers";
            this.ResumeLayout(false);
        }

        private void LoadTrainers()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Username", typeof(string));

            foreach (var t in TrainerStore.GetAll())
            {
                dt.Rows.Add(t.Id, t.Name, t.Gender, t.Email, t.Phone, t.Username);
            }

            this.dgvTrainers.DataSource = dt;
            if (this.dgvTrainers.Columns.Contains("Id"))
                this.dgvTrainers.Columns["Id"].Visible = false;
        }

        private void OpenAddDialog()
        {
            var dlg = new AddTrainerForm();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                TrainerStore.Add(dlg.CreatedTrainer);
                LoadTrainers();
            }
        }

        private void ViewFeedbackForSelected()
        {
            if (dgvTrainers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a trainer to view feedback.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = dgvTrainers.SelectedRows[0].Cells["Id"].Value as string;
            if (string.IsNullOrEmpty(id)) return;
            var f = new ViewFeedbackForm(id);
            f.Show();
        }

        private void DeleteSelected()
        {
            if (dgvTrainers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a trainer to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = dgvTrainers.SelectedRows[0].Cells["Id"].Value as string;
            if (string.IsNullOrEmpty(id)) return;

            var ok = MessageBox.Show("Delete selected trainer?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes)
            {
                TrainerStore.RemoveById(id);
                LoadTrainers();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OpenAddDialog();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }
    }
}
