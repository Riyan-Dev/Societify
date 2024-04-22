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
            LoadSocietyEvents(Constants.user.UserID);

        }

        private void LoadSocietyEvents(string userID)
        {
            // Retrieve events for the specified society ID
            DataTable dtSocietyEvents = GetSocietyEventsForStudent(userID); // Implement this method according to your database structure and logic
            dataGridView2.DataSource = dtSocietyEvents;

            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(190, 210, 240);
            

            AdjustDataGridView(dataGridView2);
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


        private void AdjustDataGridView(DataGridView dataGridView)
        {
            // Auto resize all columns to fit their contents
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Calculate the total width of all columns
            int totalWidth = dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);

            // Adjust the width of the DataGridView to fit its content without horizontal scrollbar
            dataGridView.Width = totalWidth + SystemInformation.VerticalScrollBarWidth;
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
            dtJoinedSocieties.Merge(GetApprovalPendingRequests(user.UserID));
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
           




        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Retrieve data from the selected row
                int societyID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SocietyID"].Value);
                string approval = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Status"].Value);


                // Create a new instance of the form you want to open
                if (approval == "Approved")
                {
                    this.Hide();
                    SocietyDetails newForm = new SocietyDetails(societyID);

                    // Show the new form
                    newForm.Show();
                }
                else
                {
                    MessageBox.Show("Approval Pending or Rejected, No Access to this society", "Error");
                }
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Check if the clicked cell is the Description cell
            if (e.ColumnIndex == dataGridView2.Columns["Description"].Index && e.RowIndex >= 0)
            {
                // Get the description from the clicked cell
                string description = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Open a new form to display the description
                descriptiondash descriptionForm = new descriptiondash(description);
                descriptionForm.Show();
            }

        }




    

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
