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

    

        public BookCopy(string currentState, bool isAvailable, string Isbn, int shelfID, int ownerContractId)
        {
            CopyID = null;
            CurrentState = currentState;
            IsAvailable = isAvailable;
            ISBN = Isbn;
            ShelfID = shelfID;
            OwnerContractID = ownerContractId;
        }

        
    }

}