using EfCoreDemoApi.Data;
using EfCoreDemoApi.Repositories;
using Microsoft.EntityFrameworkCore;


// uygulamanın başlatılmasında gerekli konfigürasyonlar
//dı , logging kestrel ayarları burada yapılır 
var builder = WebApplication.CreateBuilder(args);

// DbContext
//sql kullanacağımız için UseSqlServer metodu ile bağlantı dizesini veriyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories - Dependency Injection
// Scoped: Her HTTP isteği için yeni bir instance oluşturur
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Controllers
//api controllerlarını kullanabilmek için ekliyoruz
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//hazırlanan servisleri kullanarak uygulamayı oluştur

var app = builder.Build();

// HTTP pipeline
//geliştirme ortamında swagger ara yüzünü etkinleştiriyoruz
//production ortamında genellikle kapatılır
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS yönlendirmesi yapar

app.UseHttpsRedirection();

// Route ve httpget vs gibi methodların eşleştirilmesini sağlar
// olmazsa controllerlar çalışmaz
app.MapControllers();


//uygulamayı başlatma komutudur.
app.Run();
