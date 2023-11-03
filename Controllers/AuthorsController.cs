using Bookify.Core.Models;
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
            var author = _context.authors.
                Select(c => new AuthorViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Nationality = c.Nationality,
                    IsDeleted = c.IsDeleted,
                    brief = c.brief,
                    CreatedOn = c.CreatedOn,
                    LastUpdatedOn = c.LastUpdatedOn
                }).
                AsNoTracking()
                .ToList();
            return View(author);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_AuthorForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var author = new Author
            {
                Name = model.Name,
                Nationality = model.Nationality,
                brief = model.brief,
            };
            _context.Add(author);
            _context.SaveChanges();

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Nationality = author.Nationality,
                brief = author.brief,
            };

            return PartialView("_AuthorRow", viewModel);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Preview(int id)
        {
            var AuthorView = _context.authors.Find(id);

            if (AuthorView is null)
                return NotFound();

            var model = new AuthorFormViewModel
            {
                Id = id,
                Name = AuthorView.Name,
                brief = AuthorView.brief,
                Nationality = AuthorView.Nationality,
            };
            return PartialView("_AuthorForm", model);

        }
        public IActionResult UniqueItems(AuthorFormViewModel model)
        {
            var IsExisted = _context.authors.Any(c => c.Name == model.Name);
            return Json(!IsExisted);
        }
    }
}
