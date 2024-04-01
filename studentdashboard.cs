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
using static Societify.Constants; 

namespace Societify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadJoinedSocieties();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterSociety newWindow = new RegisterSociety();
            newWindow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            JoinSociety newWindow = new JoinSociety();
            newWindow.Show();
        }


        private void AdjustDataGridViewHeight(DataGridView dataGridView)
        {
            // Calculate the height required to display all rows without a scrollbar
            int totalHeight = dataGridView.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                totalHeight += row.Height;
            }

            // Adjust the height of the DataGridView
            dataGridView.Height = totalHeight + 2; // Add a small buffer for aesthetics
        }


        private void LoadJoinedSocieties()
        {
            // Assuming you have a method to retrieve joined societies for the current student
            DataTable dtJoinedSocieties = GetJoinedSocietiesForStudent(user.UserID); // Implement this method according to your database structure and logic
            dtJoinedSocieties.Merge( GetApprovalPendingRequests(user.UserID));
            dtJoinedSocieties.Merge(GetYourSocietiesForStudent(user.UserID));

            if (dtJoinedSocieties != null && dtJoinedSocieties.Rows.Count > 0)
            {
                // Remove the "SocietyID" column from the DataGridView

                // Bind the modified DataTable to the DataGridView
                dataGridView1.DataSource = dtJoinedSocieties;
                // Optionally, you can adjust column widths, headers, etc.
                // For example:
                dataGridView1.Columns["SocietyName"].HeaderText = "Society Name";
                dataGridView1.Columns["rollName"].HeaderText = "Role";

                // Add a new column named "Status" with the value "Approved" to each row
               
                dataGridView1.Columns["SocietyID"].Visible = false;

                AdjustDataGridViewHeight(dataGridView1);
            }
            else
            {
                MessageBox.Show("No joined societies found.");
            }

            

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
