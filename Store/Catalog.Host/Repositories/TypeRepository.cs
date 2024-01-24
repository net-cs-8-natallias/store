using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class TypeRepository: ICatalogRepository<ItemType>
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