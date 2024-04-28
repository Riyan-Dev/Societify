using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Societify.DBHandler;

namespace Societify
{
    public partial class MentorEventsRequestList : Form
    {
        public MentorEventsRequestList()
        {
            InitializeComponent();
        }

        private void MentorEventsRequestsList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            DataTable dtSocieties = GetSocietiesInEventsVerificationRequests();

            if (dtSocieties.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtSocieties;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentordashboard adashboard = new Mentordashboard();
            adashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Retrieve data from the selected row
                int societyID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["societyID"].Value);
                int eventID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["reqID"].Value);

                // Create a new instance of the form you want to open
                this.Hide();
                MentorEventsRequestPanel newForm = new MentorEventsRequestPanel(societyID, eventID);

                // Show the new form
                newForm.Show();
            }
        }
    }
}
