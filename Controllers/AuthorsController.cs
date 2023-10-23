using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    
    public class AuthorsController : Controller
    {
        public readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var author = _context.authors.AsNoTracking().ToList();
            return View(author);
        }
    }
}
