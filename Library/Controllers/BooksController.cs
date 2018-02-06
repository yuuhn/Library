using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string title, string author, string isbn,
            string year, string publisher, string country, string sortBy, string asc)
        {
            var books = from m in _context.Book
                         select m;

            if (!String.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.StartsWith(title));
            }

            if (!String.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.StartsWith(author));
            }

            if (!String.IsNullOrEmpty(isbn))
            {
                books = books.Where(b => b.ISBN.StartsWith(isbn));
            }

            if (!String.IsNullOrEmpty(year))
            {
                books = books.Where(b => b.PublicationYear.ToString().StartsWith(year));
            }

            if (!String.IsNullOrEmpty(publisher))
            {
                books = books.Where(b => b.Publisher.StartsWith(publisher));
            }

            if (!String.IsNullOrEmpty(country))
            {
                books = books.Where(b => b.Country.StartsWith(country));
            }

            var library = new LibraryFilterable
            {
                FilterTitle = title,
                FilterAuthor = author,
                FilterISBN = isbn,
                FilterYear = year,
                FilterPublisher = publisher,
                FilterCountry = country,
                OrderByTitleArrow = "↓",
                OrderByAuthorArrow = "↓",
                OrderByISBNArrow = "↓",
                OrderByYearArrow = "↓",
                OrderByPublisherArrow = "↓",
                OrderByCountryArrow = "↓",
            };

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(asc))
            {
                switch(sortBy)
                {
                    case "title":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.Title);
                            library.OrderByTitleArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Title);
                            library.OrderByTitleArrow = "↓";
                        }
                        break;
                    case "author":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.Author);
                            library.OrderByAuthorArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Author);
                            library.OrderByAuthorArrow = "↓";
                        }
                        break;
                    case "isbn":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.ISBN);
                            library.OrderByISBNArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.ISBN);
                            library.OrderByISBNArrow = "↓";
                        }
                        break;
                    case "year":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.PublicationYear);
                            library.OrderByYearArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.PublicationYear);
                            library.OrderByYearArrow = "↓";
                        }
                        break;
                    case "publisher":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.Publisher);
                            library.OrderByPublisherArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Publisher);
                            library.OrderByPublisherArrow = "↓";
                        }
                        break;
                    case "country":
                        if (asc.Equals("true"))
                        {
                            books = books.OrderBy(b => b.Country);
                            library.OrderByCountryArrow = "↑";
                        }
                        else
                        {
                            books = books.OrderByDescending(b => b.Country);
                            library.OrderByCountryArrow = "↓";
                        }
                        break;
                }
            }
            
            library.books = await books.ToListAsync();

            return View(library);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .SingleOrDefaultAsync(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Author,ISBN,PublicationYear,Publisher,Country")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.SingleOrDefaultAsync(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Author,ISBN,PublicationYear,Publisher,Country")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .SingleOrDefaultAsync(b => b.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.SingleOrDefaultAsync(b => b.ID == id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(b => b.ID == id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
