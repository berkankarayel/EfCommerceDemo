using EfCoreDemoApi.Data;
using EfCoreDemoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreDemoApi.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Filtreleme, sıralama ile ürünleri getir
    public async Task<IEnumerable<Product>> GetProductsWithFiltersAsync(
        int? categoryId,
        decimal? minPrice,
        decimal? maxPrice,
        string? searchName,
        string? sortBy,
        bool isDescending)
    {
        // Tüm filtreleme mantığı buraya taşındı!
        var query = _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .AsQueryable();

        // Filtering
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchName))
        {
            query = query.Where(p => p.Name.Contains(searchName));
        }

        // Sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = sortBy.ToLower() switch
            {
                "price" => isDescending
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),

                "name" => isDescending
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),

                _ => query.OrderBy(p => p.Id)
            };
        }
        else
        {
            query = query.OrderBy(p => p.Id);
        }

        return await query.ToListAsync();
    }

    // ID'ye göre ürün getir + kategori bilgisiyle
    public async Task<Product?> GetProductWithCategoryAsync(int id)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
