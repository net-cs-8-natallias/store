using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class ItemService: ICatalogService<Item>
{
    public Task<List<Item>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<Item> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Item> UpdateInCatalog(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Item> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}