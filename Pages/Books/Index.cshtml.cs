using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stetco_Bianca_Lab2.Data;
using Stetco_Bianca_Lab2.Models;

namespace Stetco_Bianca_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Stetco_Bianca_Lab2.Data.Stetco_Bianca_Lab2Context _context;

        public IndexModel(Stetco_Bianca_Lab2.Data.Stetco_Bianca_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }




        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            BookD = new BookData();
            CurrentFilter = searchString;


            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            //  if (!String.IsNullOrEmpty(searchString))
            // {
            //    BookD.Books = BookD.Books.Where(s => s.Author.FirstName.Contains(searchString)
            //
            //                   || s.Author.LastName.Contains(searchString)
            //                   || s.Title.Contains(searchString));
            //   }

            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s => (s.Author != null && (s.Author.FirstName.Contains(searchString) 
                                                      || s.Author.LastName.Contains(searchString))) 
                                                      || s.Title.Contains(searchString)).ToList();
            }


            //if (id != null)
            //{
            //    BookID = id.Value;
            //    Book book = BookD.Books
            //    .Where(i => i.ID == id.Value).Single();
            //    BookD.Categories = book.BookCategories.Select(s => s.Category);
            //}

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .Where(i => i.ID == id.Value).SingleOrDefault(); 
                if (book != null) 
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }

            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Title);
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s =>
                   s.Author.FullName);
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s =>
                   s.Author.FullName);
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
            }


        }
    }
}
