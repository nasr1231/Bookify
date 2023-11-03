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
                    Brief = c.Brief,
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
                Brief = model.Brief,
            };
            _context.Add(author);
            _context.SaveChanges();

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Nationality = author.Nationality,
                Brief = author.Brief,
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
                Brief = AuthorView.Brief,
                Nationality = AuthorView.Nationality,
            };
            return PartialView("_AuthorForm", model);

        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int id)
        {
            var AuthorView = _context.authors.Find(id);
            if (AuthorView is null)
            {
                return BadRequest();
            }
            var ViewModel = new AuthorFormViewModel
            {
                Id = id,
                Name = AuthorView.Name,
                Nationality = AuthorView.Nationality,
                Brief = AuthorView.Brief,
            };
            return PartialView("_AuthorForm", ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var item = _context.authors.Find(model.Id);

            if (item is null)
            {
                return NotFound();
            }

            item.Name = model.Name;
            item.Brief = model.Brief;
            item.Nationality = model.Nationality;
            item.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();
            var ViewModel = new AuthorViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Nationality = item.Nationality,
                LastUpdatedOn = item.LastUpdatedOn,
                Brief = item.Brief,
                IsDeleted = item.IsDeleted,
                CreatedOn = item.CreatedOn
            };

            return PartialView("_AuthorRow", ViewModel);
        }
        [HttpPost]
        [AjaxOnly]
        public IActionResult ToggleStatus(int id)
        {
            var AuthorItem = _context.authors.Find(id);
            if (AuthorItem is null)
                return NotFound();

            //Actions Will Be Done
            AuthorItem.LastUpdatedOn = DateTime.Now;
            AuthorItem.IsDeleted = !AuthorItem.IsDeleted;
            _context.SaveChanges();

            return Ok();
        }
        public IActionResult UniqueAuthors(AuthorFormViewModel model)
        {
            var author = _context.authors.FirstOrDefault(c => c.Name == model.Name);
            var IsAllowed = author is null || author.Id.Equals(model.Id);
            return Json(IsAllowed);
        }
    }
}
