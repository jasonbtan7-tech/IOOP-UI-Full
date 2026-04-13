using System;
using System.Drawing;
using System.Windows.Forms;

namespace assignment
{
    public class ManageRequestsFormA : Form
    {
        private ListView listRequests;

        public ManageRequestsFormA()
        {
            InitializeComponent();
            RequestStore.RequestsChanged += RequestStore_RequestsChanged;
            LoadRequests();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            RequestStore.RequestsChanged -= RequestStore_RequestsChanged;
        }

        private void InitializeComponent()
        {
            this.listRequests = new ListView();
            this.SuspendLayout();

            // listRequests
            this.listRequests.Dock = DockStyle.Fill;
            this.listRequests.View = View.Details;
            this.listRequests.FullRowSelect = true;
            this.listRequests.Columns.Add("Grade", 100);
            this.listRequests.Columns.Add("Lecturer", 150);
            this.listRequests.Columns.Add("Status", 80);
            this.listRequests.Columns.Add("RequestedAt", 150);

            // ManageRequestsFormA
            this.ClientSize = new Size(500, 300);
            this.Controls.Add(this.listRequests);
            this.Name = "ManageRequestsFormA";
            this.Text = "Manage Requests";

            this.ResumeLayout(false);
        }

        private void RequestStore_RequestsChanged()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)LoadRequests);
            }
            else
            {
                LoadRequests();
            }
        }

        private void LoadRequests()
        {
            listRequests.Items.Clear();
            var all = RequestStore.GetAll();
            for (int i = 0; i < all.Count; i++)
            {
                var r = all[i];
                if (string.Equals(r.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                {
                    var item = new ListViewItem(new string[] { r.Grade, r.Lecturer, r.Status, r.RequestedAt.ToString("g") }) { Tag = r };
                    listRequests.Items.Add(item);
                }
            }
        }
    }
}
