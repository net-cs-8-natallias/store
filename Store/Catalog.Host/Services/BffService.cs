using Catalog.Host.DbContextData.Entities;
using Catalog.Host.Models;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class BffService: IBffService
{
    private readonly ICatalogRepository<ItemBrand> _brandRepository;
    private readonly ICatalogRepository<ItemType> _typeRepository;
    private readonly ICatalogRepository<ItemCategory> _categoryRepository;
    private readonly ICatalogItemsRepository _itemRepository;
    private readonly ILogger<BffService> _logger;
    

    public BffService(ICatalogRepository<ItemBrand> brandRepository,
        ICatalogRepository<ItemType> typeRepository,
        ICatalogRepository<ItemCategory> categoryRepository,
        ICatalogItemsRepository itemRepository,
        ILogger<BffService> logger)
    {
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _categoryRepository = categoryRepository;
        _itemRepository = itemRepository;
        _logger = logger;
    }
    
    public async Task UpdateItemsStock(List<OrderItem> items)
    {
        await _itemRepository.UpdateItemsStock(items);
        _logger.LogInformation($"stock items was updated with: {items.Count} items");
    }

    public async Task<List<Item>> GetItemsByCatalogItemId(int catalogItemId)
    {
        var items = await _itemRepository.GetItemsByCatalogItemId(catalogItemId);
        _logger.LogInformation($"found: {items.Count} items with catalog item id: {catalogItemId}");
        return items;
    }
    
    public async Task<List<CatalogItem>> GetCatalogItems(CatalogFilter filters)
    {
        var items = await _itemRepository.GetCatalog(filters);
        _logger.LogDebug($"*{GetType().Name}* found {items.Count} items");
        return items;
    }

    public async Task<List<CatalogItem>> GetCatalogItems()
    {
        var items = await _itemRepository.GetCatalog();
        _logger.LogDebug($"*{GetType().Name}* found {items.Count} items");
        return items;

    }

    public async Task<List<ItemBrand>> GetBrands()
    {
        var brands = await _brandRepository.GetCatalog();
        _logger.LogDebug($"*{GetType().Name}* found {brands.Count} brands");
        return brands;
    }

    public async Task<List<ItemType>> GetTypes()
    {
        var types = await _typeRepository.GetCatalog();
        _logger.LogDebug($"*{GetType().Name}* found {types.Count} types");
        return types;
    }

    public async Task<List<ItemCategory>> GetCategories()
    {
        var categories = await _categoryRepository.GetCatalog();
        _logger.LogDebug($"*{GetType().Name}* found {categories.Count} categories");
        return categories;
    }

    public async Task<CatalogItem> GetCatalogItem(int id)
    {
        var item = await _itemRepository.FindById(id);
        _logger.LogDebug($"*{GetType().Name}* found item: {item}");
        return item;
    }

    public async Task<ItemBrand> GetBrand(int id)
    {
        var brand = await _brandRepository.FindById(id);
        _logger.LogDebug($"*{GetType().Name}* found brand: {brand}");
        return brand;
    }

    public async Task<ItemType> GetType(int id)
    {
        var type = await _typeRepository.FindById(id);
        _logger.LogDebug($"*{GetType().Name}* found type: {type}");
        return type;
    }
    
    public async Task<ItemCategory> GetCategory(int id)
    {
        var category = await _categoryRepository.FindById(id);
        _logger.LogDebug($"*{GetType().Name}* found category: {category}");
        return category;
    }
}