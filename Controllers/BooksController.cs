using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View(); 
        }
    }
}
