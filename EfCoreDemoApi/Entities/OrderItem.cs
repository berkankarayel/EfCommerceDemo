namespace EfCoreDemoApi.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Foreign Key: Bu sipariş detayı hangi siparişe ait
    public int OrderId { get; set; }

    // Foreign Key: Hangi ürün
    public int ProductId { get; set; }

    // Navigation Property: İlişkili sipariş
    public Order Order { get; set; } = null!;

    // Navigation Property: İlişkili ürün
    public Product Product { get; set; } = null!;
}
