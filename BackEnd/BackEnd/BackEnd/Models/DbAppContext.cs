using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class DbAppContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DBSet<BookCopy> BookCopies { get; set; }
        public DbSet<OwnerContract> OwnerContracts { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }
        public DbSet<Contains> Contain { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("USER");
            modelBuilder.Entity<Book>().ToTable("BOOK");
            modelBuilder.Entity<BookCopy>.ToTable("BOOK_COPY");
            modelBuilder.Entity<OwnerContract>.ToTable("OWNER_CONTRACT");
            modelBuilder.Entity<RentalContract>.ToTable("RENTAL_CONTRACT");
            modelBuilder.Entity<Contains>.ToTable("CONTAINS");
        }
    }
}