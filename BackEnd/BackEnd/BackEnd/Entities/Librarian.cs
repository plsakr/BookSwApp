using Org.BouncyCastle.Asn1.X509;

namespace BackEnd.Entities
{
    public class Librarian
    {
        public int? StaffID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int BranchID { get; set; }
        public string Salt { get; set; }

        public Librarian(string name, string email, string password, int branchID, string salt)
        {
            StaffID = null;
            Name = name;
            Email = email;
            Password = password;
            BranchID = branchID;
            Salt = salt;
        }
    }
}