using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService: IBasketService
{
    private readonly ILogger<BasketService> _logger;
    private readonly ICacheService _cacheService;

    public BasketService(ILogger<BasketService> logger, 
        ICacheService cacheService)
    {
        _logger = logger;
        _cacheService = cacheService;
    }
    
    public async Task AddItem(string userId, Item item)
    {
        await _cacheService.AddOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task RemoveItem(string userId, Item item)
    {
        await _cacheService.RemoveOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task<List<Item>> GetItems(string userId)
    { 
        var items = await _cacheService.GetAsync(userId);
        List<Item> itemsList = items
           .Select(entry => new Item { ItemId = int.Parse(entry.Name), Quantity = int.Parse(entry.Value) })
           .ToList();
        return itemsList;
       
    }
}