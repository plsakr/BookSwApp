using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Waiting
    {
        public int UserID { get; set; }
        public int WaitlistID { get; set; }
        public DateTime EntryTime { get; set; }

        public Waiting(int userID, int waitlistID)
        {
            UserID = userID;
            WaitlistID = waitlistID;
        }
    }
}
