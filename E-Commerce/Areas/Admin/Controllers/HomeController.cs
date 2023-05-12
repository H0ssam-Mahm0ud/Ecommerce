using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly EcommerceDbContext _context;

        public HomeController(EcommerceDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMessages()
        {
            var message = _context.Contacts.ToList();
            return View(message);
        }
    }
}
