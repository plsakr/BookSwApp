using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BackEnd.Entities
{
    public class User
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int Reputation { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? CreditCard { get; set; }
        public string Salt { get; set; }

        public User(string name, string email, string password, string salt)
        {
            UserId = null;
            Name = name;
            Reputation = 0;
            Email = email;
            Password = password;
            CreditCard = null;
            Salt = salt;
        }
    }
}