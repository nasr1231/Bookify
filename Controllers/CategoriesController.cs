namespace Bookify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.categories.AsNoTracking().ToList();
            var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(viewModel);

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

            var category = _mapper.Map<Category>(model);

            _context.Add(category);
            _context.SaveChanges();

            var viewModel = _mapper.Map<CategoryViewModel>(model);

            return PartialView("_CategoryRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int id)
        {
            var category = _context.categories.Find(id);

            if (category is null)
                return NotFound();

            var model = _mapper.Map<CategoryFormViewModel>(category);
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

            var viewModel = _mapper.Map<CategoryViewModel>(category);
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

        public IActionResult AllowItems(CategoryFormViewModel model)
        {
            var category = _context.categories.SingleOrDefault(c => c.CategoryName == model.CategoryName);
            var IsAllowed = category is null || category.CategoryId.Equals(model.CategoryId);

            return Json(IsAllowed);
        }
    }
}
