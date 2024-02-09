namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService<T, D>
{
    Task<List<T>> GetCatalog();
    Task<T> FindById(int id);
    Task<int?> AddToCatalog(D item);
    Task<T> UpdateInCatalog(T item);
    Task<T> RemoveFromCatalog(int id);
}