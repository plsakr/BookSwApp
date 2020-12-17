using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class RentBookCopyRequest
    {
        [Required] public int CopyID { get; set; }

    }
}