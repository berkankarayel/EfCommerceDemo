namespace EfCoreDemoApi.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    // Foreign Key: Bu sipariş hangi müşteriye ait
    public int CustomerId { get; set; }

    // Navigation Property: İlişkili müşteri bilgisi
    public Customer Customer { get; set; } = null!;

    // Navigation Property: Siparişin içindeki ürünler (sipariş detayları)
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
