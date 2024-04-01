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
    public partial class AddEvent : Form
    {
        int Sid;
        public AddEvent(int societyID)
        {
            InitializeComponent();
            Sid = societyID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox3.Text == "" || textBox2.Text == "")
            {

                MessageBox.Show("Information Incomplete, Kindly Fill all required fields ", "Error");
            } else { 
                DateTime dt = dateTimePicker1.Value;
                InsertSocietyEvent(Sid, textBox1.Text, dt.ToShortDateString(), textBox3.Text, textBox2.Text);

                this.Close();
                SocietyDetails newWindow = new SocietyDetails(Sid);
                newWindow.Show();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            SocietyDetails newWindow = new SocietyDetails(Sid);
            newWindow.Show();
        }
    }
}
