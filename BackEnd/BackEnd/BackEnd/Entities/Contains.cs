using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Contains
    {
        public int RentalContractID { get; set; }
        public int BookCopyID { get; set; }



        public Contains(int rentalContractID, int bookCopyID)
        {
            RentalContractID = rentalContractID;
            BookCopyID = bookCopyID;
        }
    }
}