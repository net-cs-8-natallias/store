using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class CategoryRepository: ICatalogRepository<ItemCategory>
{
    public Task<List<ItemCategory>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<ItemCategory> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(ItemCategory item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemCategory> UpdateInCatalog(ItemCategory item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemCategory> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}