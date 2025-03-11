using Microsoft.AspNetCore.Mvc;
using MyLibraryManagementSystem.Data;

namespace MyLibraryManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(LibraryDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            _logger.LogInformation("Admin Dashboard accessed.");
            var model = new AdminDashboardViewModel
            {
                BookCount = _context.Books.Count(),
                UserCount = _context.Users.Count(),
                ActiveLoanCount = _context.Loans.Count(l => !l.IsReturned)
            };
            return View(model);
        }
    }

    public class AdminDashboardViewModel
    {
        public int BookCount { get; set; }
        public int UserCount { get; set; }
        public int ActiveLoanCount { get; set; }
    }
}
