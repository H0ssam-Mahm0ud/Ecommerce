using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly EcommerceDbContext _context;


        public CategoriesController(EcommerceDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category model , IFormFile File)
        {
            if (File != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".jpg";
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\category", ImageName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                model.CatPhoto = ImageName;
            }
            _context.Categories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categories = _context.Categories.Find(id);
            return View(categories);
        }


        [HttpPost]
        public IActionResult Edit(int id ,Category model, IFormFile File)
        {
            if (File != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".jpg";
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\category", ImageName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                model.CatPhoto = ImageName;
            }
            else
            {
                model.CatPhoto = model.CatPhoto;
            }
            _context.Categories.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var categories = _context.Categories.Find(id);
                _context.Categories.Remove(categories);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
