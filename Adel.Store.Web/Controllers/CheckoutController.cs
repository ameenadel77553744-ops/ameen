using Adel.Store.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Adel.Store.Web.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var cartJson = HttpContext.Session.GetString("cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new Cart() : (JsonSerializer.Deserialize<Cart>(cartJson) ?? new Cart());
            return View(cart);
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            // Clear cart for demo
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
