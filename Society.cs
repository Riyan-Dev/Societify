using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Societify
{
    class Society
    {
        public int SocietyID { get; set; }
        public string SocietyName { get; set; }
        public string MentorID { get; set; }
        public DateTime CreationDate { get; set; }
        public string President_ID { get; set; }
        public bool Approved { get; set; }
        public string Description { get; set; }

        // Constructor
        public Society()
        {
            // Default constructor
        }

        // Parameterized constructor
        public Society(int societyID, string societyName, string mentorID, DateTime creationDate, string president_ID, bool approved, string description)
        {
            SocietyID = societyID;
            SocietyName = societyName;
            MentorID = mentorID;
            CreationDate = creationDate;
            President_ID = president_ID;
            Approved = approved;
            Description = description;
        }
    }
}
