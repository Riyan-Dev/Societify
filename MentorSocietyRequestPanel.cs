using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Societify.DBHandler;

namespace Societify
{
    public partial class MentorSocietyRequestPanel : Form
    {
        int SocietyID = 0;
        public MentorSocietyRequestPanel(int ID)
        {
            InitializeComponent();
            this.SocietyID = ID;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorSocietyRequestList newWindow = new MentorSocietyRequestList();
            newWindow.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dtReqDetails = GetSocietyApprovalRequestDetails(SocietyID);
            DataRow row = dtReqDetails.Rows[0];
            label7.Text = row["SocietyName"].ToString();
            label14.Text = row["purpose"].ToString();
            label13.Text = row["motivation"].ToString();
            label10.Text = row["AboutYou"].ToString();
            label11.Text = row["PastExp"].ToString();
            label12.Text = row["PlannedEvent"].ToString();
        }

        private void registerbutton_Click(object sender, EventArgs e)
        {
            UpdateSocietyVerification(SocietyID, true);
            this.Hide();
            MentorSocietyRequestList newWindow = new MentorSocietyRequestList();
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSocietyVerificationMentor(SocietyID);
            this.Hide();
            MentorSocietyRequestList newWindow = new MentorSocietyRequestList();
            newWindow.Show();


        }

    }
}



