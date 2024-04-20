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
    public partial class Mentordashboard : Form
    {
        public Mentordashboard()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //ViewSocietiesAdmin newWindow = new ViewSocietiesAdmin();
            //newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorSocietyRequestList newWindow = new MentorSocietyRequestList();
            newWindow.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ViewSocietiesMentor newWindow = new ViewSocietiesMentor();
            newWindow.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorEventsRequestList newWindow = new MentorEventsRequestList();
            newWindow.Show();
        }

    }
}
