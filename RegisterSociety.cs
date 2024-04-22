﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static Societify.DBHandler;
using static Societify.Constants;

namespace Societify
{
    public partial class RegisterSociety : Form
    {
        public RegisterSociety()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

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
            Console.WriteLine(checkBox1.Checked);
            if (checkBox1.Checked)
            {
                int id = InsertIntoSociety(textBox1.Text, null, DateTime.Now, user.UserID, false, textBox4.Text);
                InsertIntoSocietyApprovalRequests(id, textBox4.Text, textBox3.Text, textBox2.Text, textBox5.Text, textBox6.Text);
                Console.WriteLine("Request Sent");
            } else
            {
                MessageBox.Show("Accept the terms and conditions", "Error");
                return;
            }
            this.Close();
            Form1 sdashboard = new Form1();
            sdashboard.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 sdashboard = new Form1();
            sdashboard.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Event handler for the Terms and Conditions button
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }

}
