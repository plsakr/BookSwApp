using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class BookCopyRequest
    {
        [Required] public int CopyID { get; set; }
        
        [Required] public string CurrentState { get; set; }

        [Required] public string IsAvailable { get; set; }

        [Required] public string ISBN { get; set; }

        [Required] public int ShelfID { get; set; }

        [Required] public int OwnerContractID { get; set; }
    }
}