using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Waiting
    {
        public int UserID { get; set; }
        public int WaitlistID { get; set; }
        public DateTime EntryTime { get; set; }

        public Waiting(int userId, int waitlistId)
        {
            UserID = userId;
            WaitlistID = waitlistId;
        }
    }
}
