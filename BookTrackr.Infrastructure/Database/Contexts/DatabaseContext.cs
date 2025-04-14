using BookTrackr.Domain.Entities;
using BookTrackr.Domain.Entities.Auth;
using BookTrackr.Domain.Entities.BookTrackr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookTrackr.Infrastructure.Database.Contexts
{


    public class DatabaseContext : DbContext
    {

        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=BookTrackr; Encrypt=yes; TrustServerCertificate=true; User Id=user; Password=user");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Book>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserBook>()
                .HasKey(e => new { e.UserId, e.BookId });

            modelBuilder.Entity<UserBook>()
                .HasOne(e => e.User)
                .WithMany(u => u.UserBooks)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(e => e.Book)
                .WithMany(
                    e => e.UserBooks
                )
                .HasForeignKey(e => e.BookId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<UserBook> UserBooks { get; set; }
    }
}