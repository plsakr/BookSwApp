using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Waiting
    {
        public int UserUserID { get; set; }
        public int WaitlistListID { get; set; }
        public DateTime EntryDate { get; set; }

        public Waiting(int userUserID, int waitlistListID)
        {
            UserUserID = userUserID;
            WaitlistListID = waitlistListID;
        }
        
    }
}
