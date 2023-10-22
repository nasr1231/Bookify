using Bookify.Filters;
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
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.categories.
                Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    IsDeleted = c.IsDeleted,
                    CreatedOn = c.CreatedOn,
                    LastUpdatedOn = c.LastUpdatedOn,    
                })
                .AsNoTracking()
                .ToList();
            return View(categories);
        }
        [HttpGet]
        [AjaxOnly]
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

            var category = new Category { CategoryName = model.CategoryName };
            _context.Add(category);
            _context.SaveChanges();

            var viewModel = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsDeleted = category.IsDeleted,
                CreatedOn = category.CreatedOn,
                LastUpdatedOn = category.LastUpdatedOn,
            };
    
            return PartialView("_CategoryRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
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
            return PartialView("_Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = _context.categories.Find(model.CategoryId);

            if (category is null)
                return NotFound();

            category.CategoryName = model.CategoryName;
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            var viewModel = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                IsDeleted = category.IsDeleted,
                CreatedOn = category.CreatedOn,
                LastUpdatedOn = category.LastUpdatedOn,
            };

            return PartialView("_CategoryRow", viewModel);

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

        public IActionResult UniqueItems(CategoryFormViewModel model)
        {
            var IsExisted = _context.categories.Any(c => c.CategoryName == model.CategoryName);
            return Json(!IsExisted);
        }
    }
}
