using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace BackEnd.Models
{
    public class AddBookRequest
    {
        [Required] public string Isbn { get; set; }
        
        [Required] public string Name { get; set; }

        [Required] public string Author { get; set; }

        [Required] public string Genre { get; set; }

        [Required] public DateTime ReleaseDate { get; set; }

        [Required] public string Publisher { get; set; }
    }
}