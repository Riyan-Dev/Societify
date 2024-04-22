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
    public partial class JoinSociety : Form
    {
        private int selectedSociety = 0;
        private int selectedTeam = 0;
        private int selectedRole = 0;

        public JoinSociety()
        {
            InitializeComponent();
            JoinSociety_Load();
        }

        private void getAvailableroles()
        {
            Console.WriteLine(selectedTeam);
            if (selectedSociety != 0 && selectedTeam != 0)
            {
                DataTable dtRoles = GetRemainingSlots(selectedTeam, selectedSociety);

                if (dtRoles != null && dtRoles.Rows.Count > 0)
                {
                    comboBox3.Items.Clear();
                    foreach (DataRow row in dtRoles.Rows)
                    {
                        int roleID = Convert.ToInt32(row["rollID"]);
                        string roleName = row["rollName"].ToString();
                        Console.WriteLine($"Society ID: {roleID}, Society Name: {roleName}");
                        comboBox3.Items.Add(new KeyValuePair<int, string>(roleID, roleName));
                    }

                    // Set the dropdown's display member and value member properties
                    comboBox3.DisplayMember = "Value";
                    comboBox3.ValueMember = "Key";
                }
                else
                {
                    Console.WriteLine("No roles found.");
                }
            } else
            {
                comboBox3.DisplayMember = "Select Team";
            }
        }

        private void JoinSociety_Load()
        {
            // Fetch society names and IDs from the database
            DataTable dtSocieties = GetSocietyNamesAndIDs(user.UserID);

            if (dtSocieties != null && dtSocieties.Rows.Count > 0)
            {
                foreach (DataRow row in dtSocieties.Rows)
                {
                    int societyID = Convert.ToInt32(row["SocietyID"]);
                    string societyName = row["SocietyName"].ToString();
                    Console.WriteLine($"Society ID: {societyID}, Society Name: {societyName}");
                    comboBox2.Items.Add(new KeyValuePair<int, string>(societyID, societyName));
                }

                // Set the dropdown's display member and value member properties
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
            }
            else
            {
                Console.WriteLine("No societies found.");
            }

            DataTable dtTeams = GetTeams();

            if (dtTeams != null && dtTeams.Rows.Count > 0)
            {
                foreach (DataRow row in dtTeams.Rows)
                {
                    int teamID = Convert.ToInt32(row["TeamID"]);
                    string teamName = row["teamName"].ToString();
                    Console.WriteLine($"Society ID: {teamID}, Society Name: {teamName}");
                    comboBox4.Items.Add(new KeyValuePair<int, string>(teamID, teamName));
                }

                // Set the dropdown's display member and value member properties
                comboBox4.DisplayMember = "Value";
                comboBox4.ValueMember = "Key";
            }
            else
            {
                Console.WriteLine("No teams found.");
            }
        }
        bool AreTextboxesNotEmpty()
        {
            return Controls.OfType<TextBox>().All(textBox => !string.IsNullOrWhiteSpace(textBox.Text));
        }
        private void registerbutton_Click(object sender, EventArgs e)
        {
            if (!AreTextboxesNotEmpty())
            {
                MessageBox.Show("Information Incomplete, all field are required", "Error");
                return;
            }

            if (selectedSociety == 0 || selectedRole == 0 || selectedTeam == 0)
            {
                MessageBox.Show("Information Incomplete, all field are required", "Error");
                return;
            }
            Console.WriteLine(checkBox1.Checked);
            if (checkBox1.Checked)
            {
                InsertMemberApprovalRequest(selectedSociety, user.UserID, selectedRole, selectedTeam, textBox2.Text, textBox5.Text, textBox6.Text, textBox1.Text);
                Console.WriteLine("Request Sent");
            }
            else
            {
                MessageBox.Show("Accept the terms and conditions", "Error");
                return;
            }
            this.Close();
            Form1 sdashboard = new Form1();
            sdashboard.Show();
        }



        private void label6_Click(object sender, EventArgs e)
        {
            // Your event handler logic here
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Your event handler logic here
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Your event handler logic here
        }


        private void termsButton_Click(object sender, EventArgs e)
        {
            // Define the terms and conditions text
            string termsAndConditionsText =
                "1. You must follow all society rules and regulations.\n" +
                "2. Participation in events is voluntary but encouraged.\n" +
                "3. Respect other members and society property.\n" +
                "4. No discrimination or harassment will be tolerated.\n" +
                "5. The society reserves the right to update these terms.";

            // Display the terms and conditions in a message box
            MessageBox.Show(termsAndConditionsText, "Terms and Conditions");
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> selectedPair = (KeyValuePair<int, string>)comboBox2.SelectedItem;
            selectedSociety = selectedPair.Key;
            // Assuming comboBoxTeams is your ComboBox control
            getAvailableroles();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> selectedPair = (KeyValuePair<int, string>)comboBox4.SelectedItem;
            selectedTeam = selectedPair.Key;
            getAvailableroles();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> selectedPair = (KeyValuePair<int, string>)comboBox3.SelectedItem;
            selectedRole = selectedPair.Key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 sdashboard = new Form1();
            sdashboard.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Define the terms and conditions text
            string termsAndConditionsText =
                "1. You must follow all society rules and regulations.\n" +
                "2. Participation in events is voluntary but encouraged.\n" +
                "3. Respect other members and society property.\n" +
                "4. No discrimination or harassment will be tolerated.\n" +
                "5. The society reserves the right to update these terms.";

            // Display the terms and conditions in a message box
            MessageBox.Show(termsAndConditionsText, "Terms and Conditions");
        }
    }
}
