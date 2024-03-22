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
    public partial class ViewSocietiesAdmin : Form
    {
        public ViewSocietiesAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindashboard adashboard = new admindashboard();
            adashboard.Show();
        }

        private void ViewSocietiesAdmin_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            DataTable dtSocieties = GetSocietyNamesAndIDsForAdmin();

            if (dtSocieties.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtSocieties;
            }
            else
            {
                MessageBox.Show("No societies found.");
            }
        }
    }
}
