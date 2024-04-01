using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                labelDepartment.Text = "Department:";
                labelDepartment.Location = new Point(radioButton1.Location.X, radioButton1.Location.Y + radioButton1.Height + 10);
                this.Controls.Add(labelDepartment);

                TextBox textBoxDepartment = new TextBox();
                textBoxDepartment.Location = new Point(labelDepartment.Location.X + labelDepartment.Width + 5, labelDepartment.Location.Y);
                this.Controls.Add(textBoxDepartment);

                // Create label and text box for Batch
                Label labelBatch = new Label();
                labelBatch.Text = "Batch:";
                labelBatch.Location = new Point(radioButton1.Location.X, labelDepartment.Location.Y + labelDepartment.Height + 10);
                this.Controls.Add(labelBatch);

                TextBox textBoxBatch = new TextBox();
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

        }
    }
}
