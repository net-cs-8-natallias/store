using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService: IBasketService
{
    private readonly ILogger<BasketService> _logger;
    private readonly ICacheService _cacheService;
    private readonly IHttpClientService _httpClient;
    private readonly string _catalogBaseUrl = "http://localhost:5288/catalog-bff-controller";
    private readonly string _orderBaseUrl = "http://localhost:5230/order-bff-controller";

    public BasketService(ILogger<BasketService> logger, 
        ICacheService cacheService, IHttpClientService httpClient)
    {
        _logger = logger;
        _cacheService = cacheService;
        _httpClient = httpClient;
    }
    
    public async Task AddItem(string userId, Item item)
    {
        await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/decrease", 
            HttpMethod.Put, new List<Item>()
            {
                new()
                {
                    ItemId = item.ItemId, Quantity = item.Quantity
                }
            });
        await _cacheService.AddOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task RemoveItem(string userId, Item item)
    {
        await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/increase", 
            HttpMethod.Put, new List<Item>()
            {
                new()
                {
                    ItemId = item.ItemId, Quantity = item.Quantity
                }
            });
        await _cacheService.RemoveOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task<List<OrderItem>> GetItems(string userId)
    {
        var items = await GetBasketItems(userId);
        List<OrderItem> orderItems = new List<OrderItem>();
        if (items.Count == 0)
        {
            return orderItems;
        } 
        foreach (var i in items)
        {
            var catalogItem =  await _httpClient.SendAsync<ItemModel, object>(
                $"{_catalogBaseUrl}/items/{i.ItemId}", 
                HttpMethod.Get, null);
           orderItems.Add(new OrderItem()
           {
               ItemId = i.ItemId,
               BrandId = catalogItem.CatalogItem.ItemBrandId,
               TypeId = catalogItem.CatalogItem.ItemTypeId,
               Price = catalogItem.CatalogItem.Price,
               Quantity = i.Quantity,
               Size = catalogItem.Size
           });
        }
        
        return orderItems;
    }

    private async Task<List<Item>> GetBasketItems(string userId)
    {
        var items = await _cacheService.GetAsync(userId);
        return items
            .Select(entry => new Item
            {
                ItemId = int.Parse(entry.Name), 
                Quantity = int.Parse(entry.Value),
            })
            .ToList();
    }

    public async Task RemoveAll(string userId)
    {
        var items = await GetBasketItems(userId);
        await _cacheService.RemoveAllAsync(userId);
        await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/increase", 
            HttpMethod.Post, items);
    }

    public async Task<int> CheckoutBasket(string userId)
    {
        var items = await GetItems(userId);
        
        var orderId = await _httpClient.SendAsync<int, object>(
            $"{_orderBaseUrl}/orders/{userId}", 
            HttpMethod.Post, items);
        await RemoveAll(userId);
        return orderId;
    }
}