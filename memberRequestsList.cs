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
    public partial class memberRequestsList : Form
    {
        int societyID;
        public memberRequestsList(int Sid)
        {
            InitializeComponent();
            societyID = Sid;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Retrieve data from the selected row
                int reqID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["reqID"].Value);



                // Create a new instance of the form you want to open
                this.Hide();
                MemberApprovalPanel newForm = new MemberApprovalPanel(reqID);

                // Show the new form
                newForm.Show();
            }
        }

        private void memberRequestsList_Load(object sender, EventArgs e)
        {
            DataTable dtSocieties = GetMemberApprovalRequests(societyID);

            if (dtSocieties.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtSocieties;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            SocietyDetails newWindow = new SocietyDetails(societyID);
            newWindow.Show();
        }
    }
}
