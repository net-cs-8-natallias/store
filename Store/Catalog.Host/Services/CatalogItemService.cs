using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService: ICatalogService<CatalogItem>
{
    private readonly ICatalogItemRepository _itemRepository;
    private readonly ILogger<CatalogItemService> _logger;
    

    public CatalogItemService(ICatalogItemRepository itemRepository,
        ILogger<CatalogItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }
    public async Task<List<CatalogItem>> GetCatalog()
    {
        return await _itemRepository.GetCatalog();
    }

    public async Task<CatalogItem> FindById(int id)
    {
        return await _itemRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(CatalogItem item)
    {
        return await _itemRepository.AddToCatalog(item);
    }

    public async Task<CatalogItem> UpdateInCatalog(CatalogItem item)
    {
        return await _itemRepository.UpdateInCatalog(item);
    }

    public async Task<CatalogItem> RemoveFromCatalog(int id)
    {
        return await _itemRepository.RemoveFromCatalog(id);
    }
}