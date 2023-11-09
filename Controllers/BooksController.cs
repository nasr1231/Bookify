using Bookify.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace Bookify.Controllers
{
    public class BooksController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg"};
        private int _maxAllowedSize = 2097152;


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
        [HttpGet]
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
                return View("Form", PopulateViewModel());
            }

            var book = _mapper.Map<Book>(model);


            if(model.Image is not null)
            {

                var extension = Path.GetExtension(model.Image.FileName);
                if (!_allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.NotAlowedExtensions);
                    return View("Form", PopulateViewModel());
                }
                if(model.Image.Length > _maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), UserErrors.MaxImageSize);
                    return View("Form", PopulateViewModel());
                }

                var imageName = $"{Guid.NewGuid()}{extension}";

                var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/Images/Books",imageName);

                using var stream = System.IO.File.Create(path);

                model.Image.CopyTo(stream);

                book.ImageUrl = imageName;
            }

            foreach(var category in model.SelectedCategories)
            {
                book.Categories.Add(new BookCategory { CategoryId = category});
            }

            _context.Add(book);
            _context.SaveChanges();

            // Will be replaced with book details view later
            return RedirectToAction(nameof(Index));
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
    }
}
