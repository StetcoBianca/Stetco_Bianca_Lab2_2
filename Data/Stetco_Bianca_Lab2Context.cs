using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stetco_Bianca_Lab2.Models;

namespace Stetco_Bianca_Lab2.Data
{
    public class Stetco_Bianca_Lab2Context : DbContext
    {
        public Stetco_Bianca_Lab2Context (DbContextOptions<Stetco_Bianca_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Stetco_Bianca_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Stetco_Bianca_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Stetco_Bianca_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Stetco_Bianca_Lab2.Models.Category> Category { get; set; } = default!;

        public DbSet<Stetco_Bianca_Lab2.Models.Borrowing> Borrowing { get; set; } = default!; // Adaugă DbSet pentru Borrowing


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurarea relației unu-la-unu între Book și Borrowing
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book) // Definirea proprietății de navigare din Borrowing către Book
                .WithOne(b => b.Borrowing) // Definirea proprietății de navigare din Book către Borrowing
                .HasForeignKey<Borrowing>(b => b.BookID); // Asigură-te că BookId este cheia străină
        }
        public DbSet<Stetco_Bianca_Lab2.Models.Member> Member { get; set; } = default!;
    }
}
