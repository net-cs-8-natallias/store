using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class StockService: ICatalogService<Stock>
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