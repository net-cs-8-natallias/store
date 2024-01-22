using Catalog.Host.Data.Entities;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class TypeService: ICatalogService<ItemType>
{
    public Task<List<ItemType>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<ItemType> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(ItemType item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemType> UpdateInCatalog(ItemType item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemType> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}