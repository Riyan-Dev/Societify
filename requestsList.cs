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
    public partial class requestsList : Form
    {
        public requestsList()
        {
            InitializeComponent();
        }

        private void requestsList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            DataTable dtSocieties = GetSocietiesInApprovalRequests();

            if (dtSocieties.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtSocieties;
            }
            else
            {
                MessageBox.Show("No societies found.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adashboard = new admindashboard();
            adashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Retrieve data from the selected row
                int societyID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SocietyID"].Value);


                // Create a new instance of the form you want to open
                this.Hide();
                Form2 newForm = new Form2(societyID);

                // Show the new form
                newForm.Show();
            }
        }
    }
}
