using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class WaitOnBookRequest
    {
        [Required] public int waitlistID { get; set; }

        [Required] public string ISBN { get; set; }
        
        [Required] public int UserID { get; set; }
    }
}