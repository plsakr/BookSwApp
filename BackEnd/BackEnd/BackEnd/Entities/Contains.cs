using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Contains
    {
        public int RentalContractID { get; set; }
        public int BookCopyId { get; set; }



        public Contains(int rentalContractId, int bookCopyId)
        {
            RentalContractID = rentalContractId;
            BookCopyId = bookCopyId;
        }
    }
}