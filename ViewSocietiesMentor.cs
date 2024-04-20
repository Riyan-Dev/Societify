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
    public partial class ViewSocietiesMentor : Form
    {
        public ViewSocietiesMentor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mentordashboard adashboard = new Mentordashboard();
            adashboard.Show();
        }

        private void ViewSocietiesMentor_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private void PopulateDataGridView()
        {
            DataTable dtSocieties = GetSocietyNamesAndIDsForMentor();

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
