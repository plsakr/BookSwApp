using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class BookCopy
    {
        public int? CopyID { get; set; }
        public string CurrentState { get; set; }
        public bool IsAvailable { get; set; }
        public string ISBN { get; set; }
        public int ShelfID { get; set; }
        public int OwnerContractID { get; set; }

    

        public BookCopy(string currentState, bool isAvailable, string ISBN, int shelfID, int ownerContractID)
        {
            CopyID = null;
            CurrentState = currentState;
            IsAvailable = isAvailable;
            this.ISBN = ISBN;
            ShelfID = shelfID;
            OwnerContractID = ownerContractID;
        }


    }

}
