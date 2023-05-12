using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly EcommerceDbContext _context;

        public ProductsController(EcommerceDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var products = _context.Products.Include(c => c.Category).ToList();
            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories,"CatId","CatName");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product model, IFormFile File)
        {
            if (File != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".jpg";
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Product", ImageName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                model.ProImage = ImageName;
            }
            _context.Products.Add(model);
            _context.SaveChanges();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var products = _context.Products.Find(id);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View(products);
        }


        [HttpPost]
        public IActionResult Edit(int id, Product model, IFormFile File)
        {
            if (File != null)
            {
                string ImageName = Guid.NewGuid().ToString() + ".jpg";
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Product", ImageName);
                using (var stream = System.IO.File.Create(FilePath))
                {
                    File.CopyTo(stream);
                }
                model.ProImage = ImageName;
            }
            else
            {
                model.ProImage = model.ProImage;
            }
            _context.Products.Update(model);
            _context.SaveChanges();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var products = _context.Products.Find(id);
                _context.Products.Remove(products);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
