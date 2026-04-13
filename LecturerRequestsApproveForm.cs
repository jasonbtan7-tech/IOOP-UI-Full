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
    public partial class LecturerApproveRequestsForm : Form
    {
        public LecturerApproveRequestsForm()
        {
            InitializeComponent();
            LoadRequests();
            this.button1.Click += Button1_Click; // Approve
            this.button2.Click += Button2_Click; // Reject
            this.button3.Click += Button3_Click; // Cancel
            RequestStore.RequestsChanged += RequestStore_RequestsChanged;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RequestStore_RequestsChanged()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(LoadRequests));
            }
            else
            {
                LoadRequests();
            }
        }

        private void LoadRequests()
        {
            listView1.Items.Clear();
            var all = RequestStore.GetAll();
            foreach (var r in all)
            {
                var item = new ListViewItem(new string[] { r.Id.ToString(), r.Grade, r.Lecturer, r.Status });
                listView1.Items.Add(item);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Approve selected
            if (listView1.SelectedItems.Count == 0) return;
            var idStr = listView1.SelectedItems[0].Text;
            Guid id;
            if (!Guid.TryParse(idStr, out id)) return;
            var req = RequestStore.GetAll().Find(x => x.Id == id);
            if (req == null) return;
            RequestStore.UpdateStatus(id, "Approved");
            MessageBox.Show("Request approved.", "Approved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Reject selected
            if (listView1.SelectedItems.Count == 0) return;
            var idStr = listView1.SelectedItems[0].Text;
            Guid id;
            if (!Guid.TryParse(idStr, out id)) return;
            var req = RequestStore.GetAll().Find(x => x.Id == id);
            if (req == null) return;
            RequestStore.UpdateStatus(id, "Rejected");
            MessageBox.Show("Request rejected.", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
