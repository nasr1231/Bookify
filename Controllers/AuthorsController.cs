namespace Bookify.Controllers
{

    public class AuthorsController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;         
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var author = _context.authors.AsNoTracking().ToList();
            var viewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(author);    
            return View(viewModel);
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
            var author = _mapper.Map<Author>(model);
            _context.Add(author);
            _context.SaveChanges();

            var viewModel = _mapper.Map<AuthorViewModel>(author);

            return PartialView("_AuthorRow", viewModel);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Preview(int id)
        {
            var AuthorView = _context.authors.Find(id);

            if (AuthorView is null)
                return NotFound();

            var model = _mapper.Map<AuthorFormViewModel>(AuthorView);
            
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
            var ViewModel = _mapper.Map<AuthorFormViewModel>(AuthorView);
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
            var ViewModel = _mapper.Map<AuthorViewModel>(item);

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
