using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class OwnerContract
    {
        public int? OwnerContractID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BranchID { get; set; }
        public int UserID { get; set; }

        public string ISBN { get; set; }


        public OwnerContract(int userID, string isbn, int branchId, DateTime endDate)
        {
            StartDate = DateTime.Today;
            EndDate = endDate;
            BranchID = branchId;
            UserID = userID;
            ISBN = isbn;

        }

    }
    
}