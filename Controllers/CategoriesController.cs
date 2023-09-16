using Bookify.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.categories.AsNoTracking().ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form",model);

            var category = new Category {CategoryName = model.CategoryName};
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Edit(int EditId)
        {
            var category = _context.categories.Find(EditId);

            if (category is null)
                return NotFound();

            var model = new CategoryFormViewModel
            {
                CategoryId = EditId,
                CategoryName = category.CategoryName,
                
            };
            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var category = _context.categories.Find(model.CategoryId);

            if (category is null)
                return NotFound();

            category.CategoryName = model.CategoryName;
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ToggleStatus(int statusid)
        {
            var category = _context.categories.Find(statusid);
            if (category is null)
                return NotFound();

            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }
    }
}
