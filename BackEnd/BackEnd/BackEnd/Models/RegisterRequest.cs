﻿using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}