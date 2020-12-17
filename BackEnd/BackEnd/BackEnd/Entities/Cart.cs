namespace BackEnd.Entities
{
    public class Cart
    {
        public int UserId { get; set; }
        public int BookCopyId { get; set; }

        public Cart(int userId, int bookCopyId)
        {
            UserId = userId;
            BookCopyId = bookCopyId;
        }
    }
}