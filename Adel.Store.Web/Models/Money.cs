using System.Globalization;

namespace Adel.Store.Web.Models
{
    public class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Money() { }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public string Format(CultureInfo? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture;
            var symbol = Currency switch
            {
                Currency.SAR => "﷼", // Saudi Riyal symbol
                Currency.USD => "$",
                Currency.AED => "د.إ",
                _ => string.Empty
            };

            var formatted = string.Format(culture, "{0:N2}", Amount);
            if (culture.TextInfo.IsRightToLeft)
            {
                return symbol + " " + formatted;
            }
            return formatted + " " + symbol;
        }
    }
}


