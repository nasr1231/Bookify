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
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = new Category {CategoryName = model.CategoryName};
            _context.Add(category);
            _context.SaveChanges();               
            return PartialView("_CategoryRow", category);
        }
        
        [HttpGet]
        public IActionResult Edit(int EditId)
        {
            var category = _context.categories.Find(EditId);

            if (category is null)
                return BadRequest();

            var model = new CategoryFormViewModel
            {
                CategoryId = EditId,
                CategoryName = category.CategoryName,
                
            };
            return PartialView("_Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("_Form", model);

            var category = _context.categories.Find(model.CategoryId);

            if (category is null)
                return NotFound();

            category.CategoryName = model.CategoryName;
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            TempData["Message"] = "Saved Successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {             
            var category = _context.categories.Find(id);

            if (category is null)
                return NotFound();

            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }
    }
}
