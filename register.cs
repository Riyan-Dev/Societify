using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Societify.DBHandler;

namespace Societify
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Create label and text box for Department
                Label labelDepartment = new Label();
                labelDepartment.Name = "labelDepartment"; // Set the Name property
                labelDepartment.Text = "Department:";
                labelDepartment.Location = new Point(radioButton1.Location.X, radioButton1.Location.Y + radioButton1.Height + 10);
                labelDepartment.BackColor = Color.Transparent;
                this.Controls.Add(labelDepartment);

                TextBox textBoxDepartment = new TextBox();
                textBoxDepartment.Name = "textBoxDepartment"; // Set the Name property
                textBoxDepartment.Location = new Point(labelDepartment.Location.X + labelDepartment.Width + 5, labelDepartment.Location.Y);
                this.Controls.Add(textBoxDepartment);

                // Create label and text box for Batch
                Label labelBatch = new Label();
                labelBatch.Name = "labelBatch"; // Set the Name property
                labelBatch.Text = "Batch:";
                labelBatch.Location = new Point(radioButton1.Location.X, labelDepartment.Location.Y + labelDepartment.Height + 10);
                labelBatch.BackColor = Color.Transparent;
                this.Controls.Add(labelBatch);

                TextBox textBoxBatch = new TextBox();
                textBoxBatch.Name = "textBoxBatch"; // Set the Name property
                textBoxBatch.Location = new Point(labelBatch.Location.X + labelBatch.Width + 5, labelBatch.Location.Y);
                this.Controls.Add(textBoxBatch);

            }
            else
            {
                // Remove labels and text boxes if they exist
                Control[] controlsToDelete = {
                    Controls.Find("labelDepartment", true).FirstOrDefault(),
                    Controls.Find("textBoxDepartment", true).FirstOrDefault(),
                    Controls.Find("labelBatch", true).FirstOrDefault(),
                    Controls.Find("textBoxBatch", true).FirstOrDefault()
                };

            
                foreach (Control control in controlsToDelete)
                {
                    if (control != null)
                    {
                        Controls.Remove(control);
                        control.Dispose();
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login loginPage = new Login();
            loginPage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Kindly Enter All Relevant info", "Incomplete Info");
                return;
            }

            DataTable dt = GetUserByEmailOrUserID(textBox1.Text, textBox4.Text);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("User Already Exists with that Email or User ID", "User Exists");
                return;
            }

            // Validate email format
            if (!IsValidEmail(textBox4.Text))
            {
                MessageBox.Show("Please enter a valid email address", "Invalid Email");
                return;
            }

            // Validate password requirements
            if (!IsValidPassword(textBox3.Text))
            {
                MessageBox.Show("Password must be at least 8 characters long and contain at least one uppercase letter, one number, and one special character.", "Invalid Password");
                return;
            }
          

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Kindly Enter Correct Confirm Password", "Password Mismatch");
                return;
            }

            if (radioButton1.Checked)
            {
                TextBox textBoxBatch = Controls.Find("textBoxBatch", true).FirstOrDefault() as TextBox;
                TextBox textBoxDepartment = Controls.Find("textBoxDepartment", true).FirstOrDefault() as TextBox;
                if (textBoxBatch.Text == "" || textBoxDepartment.Text == "")
                {
                    MessageBox.Show("Kindly Enter batch and Department", "Incomplete Info");
                    return;
                }
                DateTime dob = dateTimePicker1.Value;
                InsertUser(textBox1.Text, textBox6.Text, textBox2.Text, textBox4.Text, dob);
                InsertStudent(textBox1.Text, textBoxDepartment.Text, textBoxBatch.Text);
                loginFun();

            }
            else if (radioButton2.Checked)
            {
                TextBox textBoxDepartment = Controls.Find("textBoxDepartment", true).FirstOrDefault() as TextBox;
                if (textBoxDepartment.Text == "")
                {
                    MessageBox.Show("Kindly Enter Department", "Incomplete Info");
                    return;
                }
                DateTime dob = dateTimePicker1.Value;
                InsertUser(textBox1.Text, textBox6.Text, textBox2.Text, textBox4.Text, dob);
                InsertMentor(textBox1.Text, textBoxDepartment.Text);
                loginFun();
            } else
            {
                MessageBox.Show("Kindly Select User Type", "Incomplete Info");
            }
        }

        // Method to validate email format using regex
        private bool IsValidEmail(string email)
        {
            string emailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, emailRegex);
        }

        // Method to validate password format
        private bool IsValidPassword(string password)
        {
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
            return Regex.IsMatch(password, passwordRegex);
        }

        private void loginFun()
        {
            User user = DBHandler.Login(textBox1.Text, textBox2.Text);

            if (user is Student)
            {
                this.Hide();
                Form1 sdashboard = new Form1();
                sdashboard.Show();

            }
            else if (user is Mentor)
            {

            }
            else if (user is Admin)
            {
                this.Hide();
                admindashboard adashboard = new admindashboard();
                adashboard.Show();
            }
            else
            {
                MessageBox.Show("User not Found, Kindly enter valid credentials", "Login unsuccessful");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                // Create label and text box for Department
                Label labelDepartment = new Label();
                labelDepartment.Name = "labelDepartment"; // Set the Name property
                labelDepartment.Text = "Department:";
                labelDepartment.Location = new Point(radioButton1.Location.X, radioButton1.Location.Y + radioButton1.Height + 10);
                labelDepartment.BackColor = Color.Transparent;
                this.Controls.Add(labelDepartment);

                TextBox textBoxDepartment = new TextBox();
                textBoxDepartment.Name = "textBoxDepartment"; // Set the Name property
                textBoxDepartment.Location = new Point(labelDepartment.Location.X + labelDepartment.Width + 5, labelDepartment.Location.Y);
                this.Controls.Add(textBoxDepartment);

               

            }
            else
            {
                // Remove labels and text boxes if they exist
                Control[] controlsToDelete = {
                    Controls.Find("labelDepartment", true).FirstOrDefault(),
                    Controls.Find("textBoxDepartment", true).FirstOrDefault(),
                  
                };


                foreach (Control control in controlsToDelete)
                {
                    if (control != null)
                    {
                        Controls.Remove(control);
                        control.Dispose();
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
