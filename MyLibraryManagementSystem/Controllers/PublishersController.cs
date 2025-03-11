using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibraryManagementSystem.Data;
using MyLibraryManagementSystem.Models;

namespace MyLibraryManagementSystem.Controllers
{
    public class PublishersController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(LibraryDbContext context, ILogger<PublishersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Publishers
        public IActionResult Index()
        {
            var publishers = _context.Publishers.ToList();
            return View(publishers);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            _logger.LogInformation("Create POST called with Publisher: {@Publisher}", publisher);
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                _context.SaveChanges();
                _logger.LogInformation("Publisher created successfully: {@Publisher}", publisher);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("ModelState invalid. Errors: {@Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var publisher = _context.Publishers.Find(id);
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Publisher publisher)
        {
            _logger.LogInformation("Edit POST called with Publisher: {@Publisher}", publisher);
            if (id != publisher.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    _context.SaveChanges();
                    _logger.LogInformation("Publisher updated successfully: {@Publisher}", publisher);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Error updating publisher: {@Publisher}", publisher);
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            _logger.LogWarning("ModelState invalid. Errors: {@Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var publisher = _context.Publishers.Find(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
                _logger.LogInformation("Publisher deleted: {@Publisher}", publisher);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
