using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Societify
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
    }

    public class Student : User
    {
        public string Department { get; set; }
        public string Batch { get; set; }
    }

    public class Mentor : User
    {
        public string Department { get; set; }
    }

    public class Admin : User
    {
        // No additional properties needed for Admin
    }

}
