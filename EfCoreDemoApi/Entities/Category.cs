namespace EfCoreDemoApi.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigation Property: Bir kategorinin birden fazla ürünü olabilir
    // Başlangıçta boş olmaması için yeni bir liste ile başlattık.
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
