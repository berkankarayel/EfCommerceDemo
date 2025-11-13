using EfCoreDemoApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreDemoApi.Data.Configurations;

// Category entity'si için veritabanı kuralları
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Tablo adı
        builder.ToTable("Categories");

        // Primary Key
        builder.HasKey(c => c.Id);

        // Property kuralları
        builder.Property(c => c.Name)
            .IsRequired()           // NULL olamaz
            .HasMaxLength(100);     // Maksimum 100 karakter

        builder.Property(c => c.Description)
            .HasMaxLength(500);     // Maksimum 500 karakter

        // Index tanımlama (performans için)
        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Category_Name");
    }
}
