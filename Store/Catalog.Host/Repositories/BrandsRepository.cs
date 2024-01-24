using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class BrandsRepository: ICatalogRepository<ItemBrand>
{
    public Task<List<ItemBrand>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<ItemBrand> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int?> AddToCatalog(ItemBrand item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemBrand> UpdateInCatalog(ItemBrand item)
    {
        throw new NotImplementedException();
    }

    public Task<ItemBrand> RemoveFromCatalog(int id)
    {
        throw new NotImplementedException();
    }
}