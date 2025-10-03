using Adel.Store.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Adel.Store.Web.Services;

namespace Adel.Store.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int? categoryId, bool? featured = null, string? q = null)
        {
            var isArabic = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar";
            ViewBag.IsArabic = isArabic;
            ViewBag.Query = q;
            ViewBag.Featured = featured;
            // Show all products without pagination
            var items = _productService.GetProducts(categoryId, featured, q, 1, int.MaxValue, out var total);
            ViewBag.Categories = _productService.GetCategories();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var isArabic = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar";
            ViewBag.IsArabic = isArabic;
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ToggleActive(int id, string? returnUrl = null)
        {
            _productService.ToggleActive(id);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ToggleFeatured(int id, string? returnUrl = null)
        {
            _productService.ToggleFeatured(id);
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}


