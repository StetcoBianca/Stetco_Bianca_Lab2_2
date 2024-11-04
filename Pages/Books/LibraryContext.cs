using Microsoft.EntityFrameworkCore;
using Stetco_Bianca_Lab2.Models;

namespace Stetco_Bianca_Lab2.Pages.NewFolder
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea explicită a relației între Author și Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)          // Un Book are un singur Author
                .WithMany(a => a.Books)         // Un Author are mai multe Book
                .HasForeignKey(b => b.AuthorID); // Book are cheie străină AuthorId
        }

        // Configurare conexiune la baza de date (exemplu pentru SQL Server)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=your_server_name;Database=your_database_name;Trusted_Connection=True;");
        }
    }
}
