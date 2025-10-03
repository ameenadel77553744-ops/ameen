using Adel.Store.Web.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Adel.Store.Web.Controllers
{
    public class CurrencyController : Controller
    {
        [HttpPost]
        public IActionResult Set(string code, string returnUrl)
        {
            // Validate
            if (!Enum.TryParse<Currency>(code, ignoreCase: true, out var parsed))
            {
                parsed = Currency.SAR;
            }

            Response.Cookies.Append(
                "preferred-currency",
                parsed.ToString(),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl ?? "/");
        }

        public static Currency GetPreferredCurrency(HttpRequest request)
        {
            var cookie = request.Cookies["preferred-currency"];
            if (cookie != null && Enum.TryParse<Currency>(cookie, true, out var c))
            {
                return c;
            }
            return Currency.SAR;
        }
    }
}


