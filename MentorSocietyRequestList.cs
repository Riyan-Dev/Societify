using Societify;
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
    public partial class MentorSocietyRequestList : Form
    {

        public MentorSocietyRequestList()
        {
            InitializeComponent();
        }

        private void requestsList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            DataTable dtSocieties = GetSocietiesVerificationRequests();

            if (dtSocieties.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtSocieties;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentordashboard mdashboard = new Mentordashboard();
            mdashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Retrieve data from the selected row
                int societyID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SocietyID"].Value);


                // Create a new instance of the form you want to open
                this.Hide();
                MentorSocietyRequestPanel newForm = new MentorSocietyRequestPanel(societyID);

                // Show the new form
                newForm.Show();
            }
        }
    }
}

