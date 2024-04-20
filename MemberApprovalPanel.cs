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
    public partial class MemberApprovalPanel : Form
    {
        
        MemberApprovalRequest request = null;
        string[] roles = {"", "Secretary", "Deputy Secretary", "Member" };
        string[] teams = { "", "Logistics", "Media", "Operations", "Finance" };

        public MemberApprovalPanel(int reqID)
        {
            InitializeComponent();
            request = GetApprovalRequestByReqID(reqID);
        }

        private void MemberApprovalPanel_Load(object sender, EventArgs e)
        {
            label7.Text = teams[request.TeamID];
            label14.Text = roles[request.RollID];
            label13.Text = request.Purpose;
            label10.Text = request.Motivation;
            label11.Text = request.AboutYou;
            label12.Text = request.PastExp;
        }

        private void registerbutton_Click(object sender, EventArgs e)
        {
            DeleteMemberApprovalRequest(request.ReqID);
            AddSocietyMember(request.SocietyID, request.UserID, request.RollID, request.TeamID, true);
            if (request.RollID == 3 && GetMemberCount(request.SocietyID, request.RollID, request.TeamID) == 3)
            {
                DeleteMemberApprovalRequests(request.SocietyID, request.RollID, request.TeamID);
            } else
            {
                DeleteMemberApprovalRequests(request.SocietyID, request.RollID, request.TeamID);

            }
            this.Close();
            memberRequestsList newWindow = new memberRequestsList(request.SocietyID);
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteMemberApprovalRequest(request.ReqID);
            this.Close();
            memberRequestsList newWindow = new memberRequestsList(request.SocietyID);
            newWindow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            memberRequestsList newWindow = new memberRequestsList(request.SocietyID);
            newWindow.Show();
        }
    }
}
