using EfCoreDemoApi.DTOs;
using EfCoreDemoApi.Entities;
using EfCoreDemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/Products?categoryId=1&minPrice=1000&maxPrice=5000&searchName=iphone&sortBy=price&isDescending=true
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
        [FromQuery] int? categoryId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? searchName,
        [FromQuery] string? sortBy,
        [FromQuery] bool isDescending = false)
    {
        // Repository'den filtrelenmiş ürünleri getir (tüm karmaşık mantık repository'de!)
        var products = await _productRepository.GetProductsWithFiltersAsync(
            categoryId, minPrice, maxPrice, searchName, sortBy, isDescending);

        // DTO'ya dönüştür
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name
        });

        return Ok(productDtos);
    }

    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        // Repository'den kategoriyle birlikte getir
        var product = await _productRepository.GetProductWithCategoryAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };

        return Ok(productDto);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createDto)
    {
        var product = new Product
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Price = createDto.Price,
            Stock = createDto.Stock,
            CategoryId = createDto.CategoryId
        };

        // Repository ile ekle
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        // Eklenen ürünü kategoriyle birlikte getir
        var createdProduct = await _productRepository.GetProductWithCategoryAsync(product.Id);

        var productDto = new ProductDto
        {
            Id = createdProduct!.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            Stock = createdProduct.Stock,
            CategoryId = createdProduct.CategoryId,
            CategoryName = createdProduct.Category.Name
        };

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateDto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = updateDto.Name;
        product.Description = updateDto.Description;
        product.Price = updateDto.Price;
        product.Stock = updateDto.Stock;
        product.CategoryId = updateDto.CategoryId;

        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

        return NoContent();
    }
}
