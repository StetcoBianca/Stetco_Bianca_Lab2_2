using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stetco_Bianca_Lab2.Data;
using Stetco_Bianca_Lab2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stetco_Bianca_Lab2.Pages.Categories
{
    public class BooksInCategoryModel : PageModel
    {
        private readonly Stetco_Bianca_Lab2Context _context;

        public BooksInCategoryModel(Stetco_Bianca_Lab2Context context)
        {
            _context = context;
        }

        public Category Category { get; set; }
        public List<Book> Books { get; set; }

        public async Task OnGetAsync(int id)
        {
            // Obține categoria selectată împreună cu cărțile sale
            Category = await _context.Category
                .Include(c => c.BookCategories) // Include BookCategories
                .ThenInclude(bc => bc.Book) // Include Books in BookCategories
                .ThenInclude(b => b.Author) // Include Author for each Book
                .FirstOrDefaultAsync(c => c.ID == id);

            // Obține toate cărțile din categoria selectată
            Books = Category?.BookCategories.Select(bc => bc.Book).ToList();
        }
    }
}
