using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Waitlist
    {
        public int? ListID { get; set; }
        public string ISBN { get; set; }

        public Waitlist(string ISBN)
        {
            this.ISBN = ISBN;
            ListID  = null;
        }

    }
}