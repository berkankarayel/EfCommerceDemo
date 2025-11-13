# 🛒 EfCommerceDemo

**Entity Framework Core** ile modern bir **E-Commerce API** projesi. Bu proje, EF Core'un temel ve orta düzey özelliklerini öğrenmek için geliştirilmiştir.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat&logo=c-sharp)
![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=flat)
![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?style=flat&logo=microsoft-sql-server)

---

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Teknolojiler](#-teknolojiler)
- [Özellikler](#-özellikler)
- [Proje Yapısı](#-proje-yapısı)
- [Kurulum](#-kurulum)
- [API Endpoint'leri](#-api-endpointleri)
- [Öğrenilen Konular](#-öğrenilen-konular)

---

## 🎯 Proje Hakkında

Bu proje, **Entity Framework Core** öğrenmek amacıyla geliştirilmiş kapsamlı bir e-ticaret API'sidir. Temel CRUD işlemlerinden ileri seviye sorgulara, Repository Pattern'den Fluent API'ye kadar birçok EF Core özelliğini içerir.

### Veri Modeli

```
Category (1) ─────── (N) Product (N) ─────── (N) OrderItem
                                                      │
Customer (1) ─────── (N) Order (1) ──────────────────┘
```

---

## 🛠️ Teknolojiler

- **.NET 8.0** - Modern framework
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 8.0** - ORM
- **SQL Server** - Veritabanı
- **Swagger/OpenAPI** - API dokümantasyonu

---

## ✨ Özellikler

### 🔹 CRUD İşlemleri
- Tüm entity'ler için tam CRUD desteği
- DTO kullanımı ile temiz API tasarımı
- Async/Await ile performanslı operasyonlar

### 🔹 İleri Seviye Sorgular
- **AsNoTracking** - Read-only sorgularda performans optimizasyonu
- **Filtering** - Dinamik filtreleme (kategori, fiyat, isim)
- **Sorting** - Sıralama (fiyat, isim - artan/azalan)
- **Include/ThenInclude** - İlişkili verileri eager loading

```csharp
// Örnek: Filtreleme ve sıralama
GET /api/Products?categoryId=1&minPrice=1000&sortBy=price&isDescending=true
```

### 🔹 Repository Pattern
- Generic Repository (`IRepository<T>`)
- Entity'ye özel repository'ler
- Temiz ve test edilebilir kod yapısı

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
```

### 🔹 Fluent API Configuration
- Entity konfigürasyonlarının ayrı sınıflara taşınması
- `IEntityTypeConfiguration<T>` kullanımı
- Temiz entity sınıfları

```csharp
builder.Property(p => p.Name)
    .IsRequired()
    .HasMaxLength(200);

builder.Property(p => p.Price)
    .HasPrecision(18, 2)
    .IsRequired();
```

### 🔹 İlişki Yönetimi
- One-to-Many (1-N) ilişkiler
- Foreign Key yönetimi
- Navigation Property kullanımı
- Cascade/Restrict delete davranışları

---

## 📁 Proje Yapısı

```
EfCoreDemoApi/
├── Controllers/           # API Controller'ları
│   ├── CategoriesController.cs
│   ├── ProductsController.cs
│   ├── CustomersController.cs
│   └── OrdersController.cs
├── Data/                  # EF Core ayarları
│   ├── ApplicationDbContext.cs
│   └── Configurations/    # Fluent API config'leri
│       ├── CategoryConfiguration.cs
│       └── ProductConfiguration.cs
├── DTOs/                  # Data Transfer Objects
│   ├── CategoryDto.cs
│   ├── ProductDto.cs
│   └── ...
├── Entities/              # Domain modeller
│   ├── Category.cs
│   ├── Product.cs
│   ├── Customer.cs
│   ├── Order.cs
│   └── OrderItem.cs
├── Repositories/          # Repository Pattern
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── IProductRepository.cs
│   └── ProductRepository.cs
└── Migrations/            # EF Core migration'ları
```

---

## 🚀 Kurulum

### Gereksinimler
- .NET 8.0 SDK
- SQL Server (LocalDB yeterli)
- Visual Studio 2022 veya VS Code

### Adımlar

1. **Projeyi klonlayın**
```bash
git clone https://github.com/berkankarayel/EfCommerceDemo.git
cd EfCommerceDemo
```

2. **Connection string'i ayarlayın**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. **Veritabanını oluşturun**
```bash
dotnet ef database update
```

4. **Projeyi çalıştırın**
```bash
dotnet run
```

5. **Swagger'ı açın**
```
https://localhost:7040/swagger
```

---

## 📡 API Endpoint'leri

### Categories
```http
GET    /api/Categories          # Tüm kategorileri listele
GET    /api/Categories/{id}     # Belirli kategoriyi getir
POST   /api/Categories          # Yeni kategori ekle
PUT    /api/Categories/{id}     # Kategori güncelle
DELETE /api/Categories/{id}     # Kategori sil
```

### Products
```http
GET    /api/Products                              # Tüm ürünleri listele
GET    /api/Products?categoryId=1                 # Kategoriye göre filtrele
GET    /api/Products?minPrice=100&maxPrice=500    # Fiyat aralığına göre
GET    /api/Products?sortBy=price&isDescending=true  # Sırala
GET    /api/Products/{id}                         # Belirli ürünü getir
POST   /api/Products                              # Yeni ürün ekle
PUT    /api/Products/{id}                         # Ürün güncelle
DELETE /api/Products/{id}                         # Ürün sil
```

### Customers
```http
GET    /api/Customers          # Tüm müşterileri listele
GET    /api/Customers/{id}     # Belirli müşteriyi getir
POST   /api/Customers          # Yeni müşteri ekle
PUT    /api/Customers/{id}     # Müşteri güncelle
DELETE /api/Customers/{id}     # Müşteri sil
```

### Orders
```http
GET    /api/Orders             # Tüm siparişleri listele
GET    /api/Orders/{id}        # Belirli siparişi getir
POST   /api/Orders             # Yeni sipariş oluştur
DELETE /api/Orders/{id}        # Sipariş sil
```

---

## 📚 Öğrenilen Konular

### 🔸 Temel Konular
- ✅ Entity Framework Core kurulumu
- ✅ DbContext oluşturma ve yapılandırma
- ✅ Entity modelleme
- ✅ Migration oluşturma ve güncelleme
- ✅ CRUD işlemleri (Create, Read, Update, Delete)
- ✅ Async/Await kullanımı

### 🔸 İlişkiler
- ✅ One-to-Many (1-N) ilişkiler
- ✅ Foreign Key tanımlama
- ✅ Navigation Property kullanımı
- ✅ Include ve ThenInclude ile eager loading
- ✅ Cascade/Restrict delete davranışları

### 🔸 Performans ve Optimizasyon
- ✅ AsNoTracking kullanımı
- ✅ IQueryable ile deferred execution
- ✅ Select projection ile veri sınırlama
- ✅ Index tanımlama

### 🔸 İleri Seviye Sorgular
- ✅ Dinamik filtreleme
- ✅ OrderBy/OrderByDescending ile sıralama
- ✅ Skip ve Take ile sayfalama
- ✅ Where koşulları zinciri

### 🔸 Mimari ve Design Pattern
- ✅ Repository Pattern
- ✅ Generic Repository
- ✅ Dependency Injection
- ✅ DTO (Data Transfer Object) kullanımı
- ✅ Katmanlı mimari (Controller - Repository - Data)

### 🔸 Fluent API
- ✅ IEntityTypeConfiguration kullanımı
- ✅ Property kuralları (MaxLength, IsRequired, HasPrecision)
- ✅ İlişki konfigürasyonu
- ✅ Index tanımlama
- ✅ Default value belirleme

---

## 💡 Örnek Kullanım

### Ürün Ekleme
```json
POST /api/Products
{
  "name": "iPhone 15 Pro",
  "description": "Apple akıllı telefon",
  "price": 45000,
  "stock": 10,
  "categoryId": 1
}
```

### Filtreleme ve Sıralama
```http
GET /api/Products?categoryId=1&minPrice=1000&maxPrice=50000&sortBy=price
```

### Sipariş Oluşturma
```json
POST /api/Orders
{
  "customerId": 1,
  "orderItems": [
    {
      "productId": 1,
      "quantity": 2
    },
    {
      "productId": 3,
      "quantity": 1
    }
  ]
}
```

---

## 🎓 Notlar

Bu proje **öğrenme amaçlı** geliştirilmiştir. Production ortamı için ek güvenlik, validation ve error handling mekanizmaları eklenmelidir:

- Authentication/Authorization (JWT)
- Global Exception Handling
- Input Validation (FluentValidation)
- Logging (Serilog)
- API Versioning
- Rate Limiting
- Health Checks

---

## 📝 Lisans

Bu proje MIT lisansı altında açık kaynaklıdır.

---

## 👤 Geliştirici

**Berkan Karayel**

GitHub: [@berkankarayel](https://github.com/berkankarayel)

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
