using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class OwnerContract
    {
        public int? ContractID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }



        public OwnerContract(int userID, int branchID, DateTime endDate)
        {
            StartDate = DateTime.Today;
            EndDate = endDate;
            BranchID = branchID;
            UserID = userID;

        }

    }
    
}