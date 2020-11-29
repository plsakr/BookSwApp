using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class DbAppContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("USER");
        }
    }
}