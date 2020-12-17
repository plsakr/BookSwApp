using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class RentBookCopyRequest
    {
        [Required] public int CopyID { get; set; }
        

        [Required] public string IsAvailable { get; set; }

        [Required] public string ISBN { get; set; }
        

        [Required] public int OwnerContractID { get; set; }

        [Required] public DateTime EndDate { get; set; }
        
        [Required] public int UserID { get; set; }
    }
}