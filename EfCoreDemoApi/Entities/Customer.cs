namespace EfCoreDemoApi.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Navigation Property: Bir müşterinin birden fazla siparişi olabilir
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
