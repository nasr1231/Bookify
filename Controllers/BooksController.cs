using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Controllers
{
    public class BooksController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private readonly int _maxAllowedSize = 2097152;


        public BooksController(IMapper mapper, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _context.Books.
                Include(auth => auth.Author).
                Include(b => b.Categories)
                    .ThenInclude(c => c.Category)
                .SingleOrDefault(b => b.Id == id);

            if (book is null)
                return NotFound();

            var viewModel = _mapper.Map<BookViewModel>(book);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View("Form", PopulateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", PopulateViewModel(model));
            }

            var book = _mapper.Map<Book>(model);


            if (model.Image is not null)
            {
                var extension = Path.GetExtension(model.Image.FileName);

                if (!_allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.NotAlowedExtensions);
                    return View("Form", PopulateViewModel(model));
                }

                if (model.Image.Length > _maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.MaxImageSize);
                    return View("Form", PopulateViewModel(model));
                }

                var imageName = $"{Guid.NewGuid()}{extension}";

                var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/Images/Books", imageName);

                using var stream = System.IO.File.Create(path);

                model.Image.CopyTo(stream);
                book.ImageUrl = imageName;
            }

            foreach (var category in model.SelectedCategories)
            {
                book.Categories.Add(new BookCategory { CategoryId = category });
            }

            _context.Add(book);
            _context.SaveChanges();

            // Will be replaced with book details view later
            return RedirectToAction(nameof(Details), new { id= book.Id});
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.Include(e => e.Categories).SingleOrDefault(b => b.Id == id);
            if (book is null)
                return NotFound();

            var model = _mapper.Map<BookFormViewModel>(book);
            var viewModel = PopulateViewModel(model);

            viewModel.SelectedCategories = book.Categories.Select(e => e.CategoryId).ToList();
            return View("Form", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", PopulateViewModel());
            }

            var book = _context.Books.Include(e => e.Categories).SingleOrDefault(b => b.Id == model.Id);
            if (book is null)
                return NotFound();

            if (model.Image is not null)
            {
                /*
                    Describtion: This Validation To check either the user is uploading new Image or replacing the old one
                    with new one or the user did not upload a new Image.

                    Functionality:
                    1- Check if the Image Url is empty or not.
                    2- If it is not empty we store the old path for image.
                    3- The last condition to check whether the the old path is equal to the new and if it is not the same
                    we terminate the old path. - and code will continue to excute the next block of codes.
                 */

                if (!string.IsNullOrEmpty(book.ImageUrl))
                {
                    var OldImagePath = Path.Combine($"{_webHostEnvironment.WebRootPath}/Images/Books", book.ImageUrl);
                    if (System.IO.File.Exists(OldImagePath))
                    {
                        System.IO.File.Delete(OldImagePath);
                    }
                }
                // Here to check if the model is empty - Means the user did not upload any new Image - and the
                // Image url is not empty - Means that there is an exsiting image - so the image still remaining and not to be removed                

                var extension = Path.GetExtension(model.Image.FileName);

                if (!_allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.NotAlowedExtensions);
                    return View("Form", PopulateViewModel());
                }
                if (model.Image.Length > _maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.MaxImageSize);
                    return View("Form", PopulateViewModel());
                }

                var imageName = $"{Guid.NewGuid()}{extension}";

                var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/Images/Books", imageName);

                using var stream = System.IO.File.Create(path);

                model.Image.CopyTo(stream);
                model.ImageUrl = imageName;
            }
            else if (model.Image is null && !string.IsNullOrEmpty(book.ImageUrl))
            {
                model.ImageUrl = book.ImageUrl;
            }

            book = _mapper.Map(model, book);
            book.LastUpdatedOn = DateTime.Now;

            foreach (var category in model.SelectedCategories)
            {
                book.Categories.Add(new BookCategory { CategoryId = category });
            }

            _context.SaveChanges();
            _context.SaveChanges();

            // Will be replaced with book details view later
            return RedirectToAction(nameof(Details), new { id = book.Id });
        }

        private BookFormViewModel PopulateViewModel(BookFormViewModel? model = null)
        {
            /*
                function: To Check if the action is to create a new Book form or To edit an exsisting Book.
                how it works: first, check if the form view is empty then the user is creating a new book. So we
                create a new BookFormViewModel and filling it with authors and categories data and send it to the
                controller to do his function to create new object and save it to database.
                
                second, if the FormViewModel is not empty then the user is already added book but the user is editing
                Book data so we no longer need to send authors and categories data again to the selecteditems list

             */
            BookFormViewModel viewModel = model is null ? new BookFormViewModel() : model;

            var authors = _context.authors.Where(sort => !sort.IsDeleted).OrderBy(a => a.Name).ToList();
            var categories = _context.categories.Where(sort => !sort.IsDeleted).OrderBy(a => a.CategoryName).ToList();

            viewModel.Authors = _mapper.Map<IEnumerable<SelectListItem>>(authors);
            viewModel.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);

            return viewModel;
        }
        public IActionResult UniqueItem(BookFormViewModel model)
        {
            var book = _context.Books.FirstOrDefault(b => b.Title == model.Title && b.AuthorId == model.AuthorId);
            var IsAllowed = book is null || book.Id.Equals(model.Id);
            return Json(IsAllowed);
        }
    }
}
