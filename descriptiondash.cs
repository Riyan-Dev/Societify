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
    public partial class descriptiondash : Form
    {
        private string descriptionText;

        public descriptiondash(string description)
        {
            InitializeComponent();
            descriptionText = description;
            textBox1.ReadOnly = true;
        }

        private void descriptiondash_Load(object sender, EventArgs e)
        {
            textBox1.Text = descriptionText;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            // This event handler is not needed for displaying the description
        }

        
    }

}