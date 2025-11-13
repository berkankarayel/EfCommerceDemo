namespace EfCoreDemoApi.Entities;

public class Product
{
    public int Id { get; set; }

    // Boş kalmaması için default değer verdik (string.Empty)
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // Foreign Key: Bu ürün hangi kategoriye ait
    public int CategoryId { get; set; }

    // Navigation Property: İlişkili kategori bilgisi

    // null olmaması için null! kullandık
    public Category Category { get; set; } = null!;

    // Navigation Property: Bu ürünün olduğu sipariş detayları
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
