using EfCoreDemoApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreDemoApi.Data.Configurations;

// Product entity'si için veritabanı kuralları
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Tablo adı
        builder.ToTable("Products");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Property kuralları
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        // Decimal için precision (18,2) → 9999999999999999.99
        builder.Property(p => p.Price)
            .HasPrecision(18, 2)    // Toplam 18 hane, virgülden sonra 2 hane
            .IsRequired();

        builder.Property(p => p.Stock)
            .IsRequired()
            .HasDefaultValue(0);    // Varsayılan değer 0

        // İlişki: Product -> Category (N-1)
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);  // Kategori silinirse ürün silinmesin (hata versin)

        // Index tanımlama
        builder.HasIndex(p => p.CategoryId)
            .HasDatabaseName("IX_Product_CategoryId");

        builder.HasIndex(p => p.Price)
            .HasDatabaseName("IX_Product_Price");
    }
}
