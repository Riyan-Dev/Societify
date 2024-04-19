using System;
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
using static Societify.User;
using static Societify.Constants;



namespace Societify
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginbutton_Click(object sender, EventArgs e)
        {



            user = DBHandler.Login(textBox1.Text, textBox2.Text);

            if (user is Student)
            {
                // Assuming your User object has a property called UserId which stores the user ID
                this.Hide();
                Form1 sdashboard = new Form1();
                sdashboard.Show();
                
            } else if (user is Mentor)
            {

            } else if (user is Admin)
            {
                this.Hide();
                admindashboard adashboard = new admindashboard();
                adashboard.Show();
            } else
            {
                MessageBox.Show("User not Found, Kindly enter valid credentials", "Login unsuccessful");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            register newWindow = new register();
            newWindow.Show();
        }

        private void checkBoxshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxshowpassword.Checked)
            {
                textBox2.PasswordChar = '\0';
            } else
            {
                textBox2.PasswordChar = '*';

            }
        }
    }
}
