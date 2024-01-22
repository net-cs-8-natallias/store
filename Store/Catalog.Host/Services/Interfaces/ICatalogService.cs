namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService<T>
{
    Task<List<T>> GetCatalog();
    Task<T> FindById(int id);
    Task<int?> AddToCatalog(T item);
    Task<T> UpdateInCatalog(T item);
    Task<T> RemoveFromCatalog(int id);
}