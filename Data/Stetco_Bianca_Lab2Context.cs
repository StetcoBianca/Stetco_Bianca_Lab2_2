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
    }
}
