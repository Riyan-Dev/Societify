﻿using System;
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
    public partial class admindashboard : Form
    {
        public admindashboard()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewSocietiesAdmin newWindow = new ViewSocietiesAdmin();
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminrequestsList newWindow = new AdminrequestsList();
            newWindow.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            eventsRequestList newWindow = new eventsRequestList();
            newWindow.Show();
        }
    }
}
