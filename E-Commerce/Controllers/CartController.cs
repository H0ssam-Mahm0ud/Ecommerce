using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly EcommerceDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(EcommerceDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);

            var result = _context.ShoppingCarts.Include(p => p.Product).Where(u => u.UserId == user.Id).ToList();
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(ShoppingCart model)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProId == model.ProId);

            var user = await _userManager.GetUserAsync(User);

            var cart = new ShoppingCart
            {
                UserId = user.Id,
                ProId = product.ProId,
                Quantity = model.Quantity
            };

            var shopcart = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.ProId == model.ProId);

            if (model.Quantity <= 0)
            {
                model.Quantity = 1;
            }
            if (shopcart == null)
            {
                _context.ShoppingCarts.Add(cart);
            }
            else
            {
                shopcart.Quantity += model.Quantity;
            }
            _context.SaveChanges();
            return RedirectToAction("Index" , "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {

            var user = await _userManager.GetUserAsync(User);

            
            var shopcart = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == user.Id && u.CartId == id);

            if (shopcart != null)
            {
                _context.ShoppingCarts.Remove(shopcart);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Cart));
        }
    }
}
