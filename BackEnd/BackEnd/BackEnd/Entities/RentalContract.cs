using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class RentalContract
    {
        public int? RentalContractID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }


        public RentalContract(int userID, DateTime endDate)
        {
            RentalContractID = null;
            StartDate = DateTime.Today;
            EndDate = endDate;
            UserID = userID;
        }
    }
}