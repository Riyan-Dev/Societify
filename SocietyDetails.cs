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
    public partial class SocietyDetails : Form
    {
        Society society = null;
        public SocietyDetails(int societyID)
        {
            InitializeComponent();
            society = GetSocietyById(societyID);

            if (society.President_ID == user.UserID)
            {
                addEvent.Visible = true;
            } else
            {
                addEvent.Visible = false;
            }

            label4.Text = society.SocietyName;
            label1.Text = society.Description;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addEvent_Click(object sender, EventArgs e)
        {
            this.Close();
            AddEvent newWindow = new AddEvent(society.SocietyID);
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 sdashboard = new Form1();
            sdashboard.Show();
        }

        private void SocietyDetails_Load(object sender, EventArgs e)
        {
            DataTable dtEvents = GetSocietyEventsWithStatus(society.SocietyID);

            if (dtEvents != null && dtEvents.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtEvents;
            }

        }
    }
}
