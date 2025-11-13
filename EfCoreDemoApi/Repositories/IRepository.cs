namespace EfCoreDemoApi.Repositories;

// Generic Repository Interface: Tüm entity'ler için ortak CRUD metodları
public interface IRepository<T> where T : class
{
    // Tüm kayıtları getir
    Task<IEnumerable<T>> GetAllAsync();
    
    // ID'ye göre kayıt getir
    Task<T?> GetByIdAsync(int id);
    
    // Yeni kayıt ekle
    Task<T> AddAsync(T entity);
    
    // Kayıt güncelle
    Task UpdateAsync(T entity);
    
    // Kayıt sil
    Task DeleteAsync(T entity);
    
    // Değişiklikleri kaydet
    Task<bool> SaveChangesAsync();
}
