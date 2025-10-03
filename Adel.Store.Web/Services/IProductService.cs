using Adel.Store.Web.Models;

namespace Adel.Store.Web.Services
{
    public interface IProductService
    {
        List<Category> GetCategories();
        List<Product> GetProducts(int? categoryId, bool? featured, string? query, int page, int pageSize, out int totalCount);
        Product? GetById(int id);
        void ToggleActive(int id);
        void ToggleFeatured(int id);
    }
}


