namespace Adel.Store.Web.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductNameAr { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
        public Money ProductPrice { get; set; } = new Money(0, Currency.SAR);
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}


