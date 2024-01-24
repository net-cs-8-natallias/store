using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService: ICatalogService<CatalogItem>
{
    public Task<List<CatalogItem>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<CatalogItem> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(CatalogItem item)
    {
        throw new NotImplementedException();
    }

    public Task<CatalogItem> UpdateInCatalog(CatalogItem item)
    {
        throw new NotImplementedException();
    }

    public Task<CatalogItem> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}