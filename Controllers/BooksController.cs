using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace Bookify.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public BooksController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var authors = _context.authors.Where(sort => !sort.IsDeleted).OrderBy(a => a.Name).ToList();
            var categories = _context.categories.Where(sort => !sort.IsDeleted).OrderBy(a => a.CategoryName).ToList();

            var viewModel = new BookFormViewModel
            {
                Authors = _mapper.Map<IEnumerable<SelectListItem>>(authors),
                Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories)
            };
            return View("_Form",viewModel); 
        }
    }
}
