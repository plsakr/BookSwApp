using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class BookCopy
    {
        public int CopyID { get; set; }
        public string CurrentState { get; set; }
        public string IsAvailable { get; set; }
        public string ISBN { get; set; }
        public int ShelfID { get; set; }
        public int OwnerContractID { get; set; }

    

        public BookCopy(int copyID, string currentState, string isAvailable, string Isbn, int shelfID, int ownerContractID)
        {
            CopyID = copyID;
            CurrentState = currentState;
            IsAvailable = isAvailable;
            ISBN = Isbn;
            ShelfID = shelfID;
            OwnerContractID = ownerContractID;
        } }

}