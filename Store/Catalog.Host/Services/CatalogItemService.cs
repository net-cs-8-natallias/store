using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService: ICatalogService<CatalogItem>
{
    private readonly ICatalogRepository<CatalogItem> _catalogItemRepository;
    private readonly ILogger<CatalogItemService> _logger;
    

    public CatalogItemService(ICatalogRepository<CatalogItem> catalogItemRepository,
        ILogger<CatalogItemService> logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _logger = logger;
    }
    public async Task<List<CatalogItem>> GetCatalog()
    {
        return await _catalogItemRepository.GetCatalog();
    }

    public async Task<CatalogItem> FindById(int id)
    {
        return await _catalogItemRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(CatalogItem item)
    {
        return await _catalogItemRepository.AddToCatalog(item);
    }

    public async Task<CatalogItem> UpdateInCatalog(CatalogItem item)
    {
        return await _catalogItemRepository.UpdateInCatalog(item);
    }

    public async Task<CatalogItem> RemoveFromCatalog(int id)
    {
        return await _catalogItemRepository.RemoveFromCatalog(id);
    }
}