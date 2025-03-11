using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibraryManagementSystem.Data;
using MyLibraryManagementSystem.Models;

namespace MyLibraryManagementSystem.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(LibraryDbContext context, ILogger<AuthorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();
            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var author = _context.Authors.Find(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            if (id != author.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View(author);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                if (_context.Books.Any(b => b.AuthorId == id))
                {
                    ModelState.AddModelError("", "Cannot delete author with associated books.");
                    return View(author);
                }
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
