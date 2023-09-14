using Bookify.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.categories.ToList();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
