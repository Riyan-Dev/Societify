﻿
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
    public partial class MentorEventsRequestPanel : Form
    {
        int SocietyID = 0;
        int eventID = 0;
        public MentorEventsRequestPanel(int ID, int evID)
        {
            InitializeComponent();
            this.SocietyID = ID;
            this.eventID = evID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MentorEventsRequestList newWindow = new MentorEventsRequestList();
            newWindow.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable dtReqDetails = GetSocietyEventsApprovalDetails(SocietyID, eventID);
            DataRow row = dtReqDetails.Rows[0];
            label7.Text = row["reqID"].ToString();
            label14.Text = row["societyID"].ToString();
            label13.Text = row["eventName"].ToString();
            label10.Text = row["Date"].ToString();
            label11.Text = row["registrationFee"].ToString();
            label12.Text = row["Description"].ToString();
        }

        private void registerbutton_Click(object sender, EventArgs e)
        {
            UpdateSocietyEventsVerification(SocietyID, eventID);
            this.Hide();
            MentorEventsRequestList newWindow = new MentorEventsRequestList();
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteSocietyEventVerification(SocietyID, eventID);
            this.Hide();
            MentorEventsRequestList newWindow = new MentorEventsRequestList();
            newWindow.Show();


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
