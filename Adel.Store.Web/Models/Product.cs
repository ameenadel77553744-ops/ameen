namespace Adel.Store.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Money Price { get; set; } = new Money(0, Currency.SAR);
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
    }
}


