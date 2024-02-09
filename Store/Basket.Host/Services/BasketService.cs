using System.Net;
using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using ExceptionHandler;

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
        _logger.LogInformation($"*{GetType().Name}* adding {item.Quantity} item with id: {item.ItemId}");
        var response = await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/decrease", 
            HttpMethod.Put, new List<Item>()
            {
                new()
                {
                    ItemId = item.ItemId, Quantity = item.Quantity
                }
            });
        if (response.Status == TaskStatus.Faulted)
        {
            throw new IllegalArgumentException(response.Exception?.InnerException?.Message ?? "Unknown error");
        }
        await _cacheService.AddOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task RemoveItem(string userId, Item item)
    {
        _logger.LogInformation($"*{GetType().Name}* request to remove {item.Quantity} items: {item.ItemId}");
        var response = await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/increase", 
            HttpMethod.Put, new List<Item>()
            {
                new()
                {
                    ItemId = item.ItemId, Quantity = item.Quantity
                }
            });
        if (response.Status == TaskStatus.Faulted)
        {
            throw new Exception("Unknown error");
        }
        await _cacheService.RemoveOrUpdateAsync(userId, item.ItemId, item.Quantity);
    }

    public async Task<List<OrderItem>> GetItems(string userId)
    {
        var items = await GetBasketItems(userId);
        _logger.LogInformation($"*{GetType().Name}* found {items.Count} items in the basket");
        List<OrderItem> orderItems = new List<OrderItem>();
        if (items.Count == 0)
        {
            return orderItems;
        } 
        
        _logger.LogInformation($"*{GetType().Name}* items in the basket: {orderItems.ToString()}");
        return orderItems;
    }

    private async Task<List<Item>> GetBasketItems(string userId)
    {
        var items = await _cacheService.GetAsync(userId);
        var basketItems =  items
            .Select(entry => new Item
            {
                ItemId = int.Parse(entry.Name), 
                Quantity = int.Parse(entry.Value),
            })
            .ToList();
        _logger.LogInformation($"*{GetType().Name}* total items in the basket: {basketItems.Count}");
        return basketItems;
    }

    public async Task RemoveAll(string userId)
    {
        var items = await GetBasketItems(userId);
        _logger.LogInformation($"*{GetType().Name}* total items to remove: {items.Count}");
        await _cacheService.RemoveAllAsync(userId);
        await _httpClient.SendAsync<Task, object>(
            $"{_catalogBaseUrl}/items/increase", 
            HttpMethod.Post, items);
    }

    public async Task<int> CheckoutBasket(string userId)
    {
        var items = await GetItems(userId);
        _logger.LogInformation($"*{GetType().Name}* total items to check-out: {items.Count}");
        var orderId = await _httpClient.SendAsync<int, object>(
            $"{_orderBaseUrl}/orders/{userId}", 
            HttpMethod.Post, items);
        _logger.LogInformation($"*{GetType().Name}* check-out completed with order id: {orderId}");
        await RemoveAll(userId);
        return orderId;
    }
}