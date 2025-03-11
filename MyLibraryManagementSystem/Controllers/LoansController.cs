using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibraryManagementSystem.Data;
using MyLibraryManagementSystem.Models;

namespace MyLibraryManagementSystem.Controllers
{
    public class LoansController : Controller
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var loans = _context.Loans.Include(l => l.Book).ThenInclude(b => b.Author)
                                      .Include(l => l.User).Where(l => !l.IsReturned).ToList();
            return View(loans);
        }

        public IActionResult Borrow()
        {
            ViewBag.BookId = new SelectList(_context.Books.Where(b => b.AvailableCopies > 0), "Id", "Title");
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrow(Loan loan)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(loan.BookId);
                if (book != null && book.AvailableCopies > 0)
                {
                    loan.LoanDate = DateTime.Now;
                    loan.IsReturned = false;
                    book.AvailableCopies--;
                    _context.Add(loan);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Book is not available.");
            }
            ViewBag.BookId = new SelectList(_context.Books.Where(b => b.AvailableCopies > 0), "Id", "Title", loan.BookId);
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Name", loan.UserId);
            return View(loan);
        }

        public IActionResult Return(int? id)
        {
            if (id == null) return NotFound();
            var loan = _context.Loans.Find(id);
            if (loan == null) return NotFound();
            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;
            var book = _context.Books.Find(loan.BookId);
            book.AvailableCopies++;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
