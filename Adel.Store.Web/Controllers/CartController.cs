using Adel.Store.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Adel.Store.Web.Services;

namespace Adel.Store.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class CartController : Controller
    {
        private const string CartSessionKey = "cart";
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        private Cart GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
            {
                return new Cart();
            }
            return JsonSerializer.Deserialize<Cart>(json) ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity = 1, string? returnUrl = null)
        {
            var product = _productService.GetById(productId);
            if (product == null) return NotFound();

            var cart = GetCart();
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing == null)
            {
                cart.Items.Add(new CartItem 
                { 
                    ProductId = productId, 
                    Quantity = Math.Max(1, quantity),
                    ProductName = product.NameEn,
                    ProductNameAr = product.NameAr,
                    ProductImageUrl = product.ImageUrl,
                    ProductPrice = product.Price
                });
            }
            else
            {
                existing.Quantity += Math.Max(1, quantity);
            }
            SaveCart(cart);
            
            // Check if it's an AJAX request
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { 
                    success = true, 
                    message = $"تم إضافة {product.NameAr} إلى السلة بنجاح!",
                    messageEn = $"{product.NameEn} added to cart successfully!",
                    cartItemsCount = cart.Items.Sum(i => i.Quantity)
                });
            }
            
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId, string? returnUrl = null)
        {
            var cart = GetCart();
            cart.Items = cart.Items.Where(i => i.ProductId != productId).ToList();
            SaveCart(cart);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
    }
}
