using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class WaitOnBookRequest
    {

        [Required] public string ISBN { get; set; }
        
    }
}