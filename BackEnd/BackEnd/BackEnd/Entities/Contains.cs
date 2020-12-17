using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class Contains
    {
        public int Rental_ContractcontractID { get; set; }
        public int Book_CopycopyID { get; set; }



        public Contains(int rental_ContractcontractID, int book_CopycopyID)
        {
            Rental_ContractcontractID = rental_ContractcontractID;
            Book_CopycopyID = book_CopycopyID;
        }
    }
}