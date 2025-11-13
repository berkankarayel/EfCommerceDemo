using EfCoreDemoApi.Entities;

namespace EfCoreDemoApi.Repositories;

// Product'a özel metodlar (filtreleme, sıralama vs.)
public interface IProductRepository : IRepository<Product>
{
    // Filtreleme, sıralama ve kategorileriyle birlikte getir
    Task<IEnumerable<Product>> GetProductsWithFiltersAsync(
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice,
        string? searchName,
        string? sortBy,
        bool isDescending);
    
    // ID'ye göre getir + kategori bilgisiyle
    Task<Product?> GetProductWithCategoryAsync(int id);
}
