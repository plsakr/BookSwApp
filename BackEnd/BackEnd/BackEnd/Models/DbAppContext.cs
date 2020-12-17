using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class DbAppContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<OwnerContract> OwnerContracts { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<Contains> Contain { get; set; }
        public DbSet<Waitlist> Waitlists { get; set; }
        public DbSet<Waiting> WaitingPeople { get; set; }
        public DbSet<TaggedWith> Tags { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Librarian> Librarians { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("BOOK").HasKey(x => new {x.ISBN});
            modelBuilder.Entity<User>().ToTable("USER");
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Tags)
                .WithOne(t => t.Book)
                .HasForeignKey(x => x.BookISBN);
            modelBuilder.Entity<BookCopy>().ToTable("BOOK_COPY").HasKey(x => new {x.CopyID});
            modelBuilder.Entity<TaggedWith>().ToTable("IS_TAGGED_WITH").HasKey(x => new {x.BookISBN, x.TagName});
            modelBuilder.Entity<OwnerContract>().ToTable("OWNER_CONTRACT").HasKey(x => x.ContractID);
            modelBuilder.Entity<RentalContract>().ToTable("RENTAL_CONTRACT").HasKey(x => x.RentalContractID);
            modelBuilder.Entity<Contains>().ToTable("CONTAINS").HasKey(x => new {x.RentalContractID, x.BookCopyID});
            modelBuilder.Entity<Cart>().ToTable("CART").HasKey(x => new {x.UserId,x.BookCopyId});
            modelBuilder.Entity<Waitlist>().ToTable("WAITLIST").HasKey(x => x.WaitlistID);
            modelBuilder.Entity<Waiting>().ToTable("IS_WAITING").HasKey(x => new {x.UserID, x.WaitlistID});
            modelBuilder.Entity<Librarian>().ToTable("LIBRARIAN").HasKey(x => x.StaffID);
            modelBuilder.Entity<Branch>().ToTable("BRANCH");
        }
    }
}
