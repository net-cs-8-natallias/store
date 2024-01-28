using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class ItemService: ICatalogService<Item>
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;
    

    public ItemService(IItemRepository itemRepository,
        ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }
    public async Task<List<Item>> GetCatalog()
    {
        return await _itemRepository.GetCatalog();
    }

    public async Task<Item> FindById(int id)
    {
        return await _itemRepository.FindById(id);
    }

    public async Task<int?> AddToCatalog(Item item)
    {
        return await _itemRepository.AddToCatalog(item);
    }

    public async Task<Item> UpdateInCatalog(Item item)
    {
        return await _itemRepository.UpdateInCatalog(item);
    }

    public async Task<Item> RemoveFromCatalog(int id)
    {
        return await _itemRepository.RemoveFromCatalog(id);
    }
}