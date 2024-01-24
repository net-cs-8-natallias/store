using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class StockRepository: ICatalogRepository<Stock>
{
    public Task<List<Stock>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<Stock> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(Stock item)
    {
        throw new NotImplementedException();
    }

    public Task<Stock> UpdateInCatalog(Stock item)
    {
        throw new NotImplementedException();
    }

    public Task<Stock> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}