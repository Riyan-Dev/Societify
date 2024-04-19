using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Societify
{
    class MemberApprovalRequest
    {
        public int ReqID { get; set; }
        public int SocietyID { get; set; }
        public string UserID { get; set; }
        public int RollID { get; set; }
        public int TeamID { get; set; }
        public string Purpose { get; set; }
        public string Motivation { get; set; }
        public string AboutYou { get; set; }
        public string PastExp { get; set; }
    }

}
