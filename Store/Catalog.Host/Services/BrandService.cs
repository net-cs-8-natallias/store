using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class BrandService: ICatalogService<ItemBrand>
{
    private readonly ICatalogRepository<ItemBrand> _brandRepository;
    private readonly ILogger<BrandService> _logger;
    

    public BrandService(ICatalogRepository<ItemBrand> brandRepository,
        ILogger<BrandService> logger)
    {
        _brandRepository = brandRepository;
        _logger = logger;
    }
    public async Task<List<ItemBrand>> GetCatalog()
    {
        return await _brandRepository.GetCatalog();
    }

    public async Task<ItemBrand> FindById(int id)
    {
        return await _brandRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(ItemBrand item)
    {
        return await _brandRepository.AddToCatalog(item);
    }

    public async Task<ItemBrand> UpdateInCatalog(ItemBrand item)
    {
        return await _brandRepository.UpdateInCatalog(item);
    }

    public async Task<ItemBrand> RemoveFromCatalog(int id)
    {
        return await _brandRepository.RemoveFromCatalog(id);
    }
}