using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibraryManagementSystem.Data;
using MyLibraryManagementSystem.Models;

namespace MyLibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(LibraryDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToList();
            return View(books);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.PublisherId = new SelectList(_context.Publishers, "Id", "Name");
            ViewBag.AuthorId = new SelectList(_context.Authors, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            _logger.LogInformation("Create POST called with Book: {@Book}", book);
            if (book.AuthorId == null || book.CategoryId == null || book.PublisherId == null)
            {
                ModelState.AddModelError("", "Please select an Author, Category, and Publisher.");
            }
            if (ModelState.IsValid)
            {
                book.AvailableCopies = book.TotalCopies;
                _context.Add(book);
                _context.SaveChanges();
                _logger.LogInformation("Book created successfully: {@Book}", book);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("ModelState invalid. Errors: {@Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            ViewBag.AuthorId = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            ViewBag.AuthorId = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            ViewBag.PublisherId = new SelectList(_context.Publishers, "Id", "Name", book.PublisherId);
            ViewBag.AuthorId = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
