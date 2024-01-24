using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CategoryService: ICatalogService<ItemCategory>
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