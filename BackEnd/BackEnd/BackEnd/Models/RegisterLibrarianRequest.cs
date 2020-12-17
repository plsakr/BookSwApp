using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class RegisterLibrarianRequest
    {
        [Required] 
        public int BranchId { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    
    }
}