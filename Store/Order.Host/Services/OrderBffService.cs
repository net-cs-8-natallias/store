using Namotion.Reflection;
using Order.Host.DbContextData.Entities;
using Order.Host.Models;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderBffService: IOrderBffService
{
    private readonly ICatalogOrderRepository _catalogOrderRepository;
    private readonly IOrderRepository<OrderItem> _orderItemRepository;
    private readonly ILogger<OrderBffService> _logger;
    private readonly IHttpClientService _httpClient;

    public OrderBffService(ICatalogOrderRepository catalogOrderRepository,
        IOrderRepository<OrderItem> orderItemRepository,
        ILogger<OrderBffService> logger, 
        IHttpClientService httpClient)
    {
        _catalogOrderRepository = catalogOrderRepository;
        _orderItemRepository = orderItemRepository;
        _logger = logger;
        _httpClient = httpClient;
    }
    public async Task<int?> CreateOrder(string userId)
    {
        if (!userId.HasValidNullability())
        {
            throw new Exception($"Unknown user");
        }
        CatalogOrder order = new CatalogOrder() { UserId = userId, Date = DateTime.Now.ToShortDateString()};
        int? orderId = await _catalogOrderRepository.AddItem(order);
        if (!orderId.HasValue)
        {
            throw new Exception($"Order was not created");
        }
        List<ItemModel> items = await GetItemsFromBasket(userId);
        decimal totalPrice = await AddOrderItems(items, orderId.Value);
        int totalQuantity = items.Sum(item => item.Quantity);
        order.Id = orderId.Value;
        order.TotalPrice = totalPrice;
        order.TotalQuantity = totalQuantity;
        await _catalogOrderRepository.UpdateItem(order);
        await UpdateBasket(userId);
        await UpdateCatalog(items);
        return orderId.Value;
    }

    private async Task UpdateCatalog(List<ItemModel> items)
    {
        await _httpClient.SendAsync<List<ItemModel>, object>(
            $"http://localhost:5288/catalog-bff-controller/items/stock", 
            HttpMethod.Put, items);
    }
    private async Task UpdateBasket(string userId)
    {
        await _httpClient.SendAsync<List<ItemModel>, object>(
            $"http://localhost:5286/basket-bff-controller/items?userId={userId}", 
            HttpMethod.Delete, null);
    }
    private async Task<List<ItemModel>> GetItemsFromBasket(string userId)
    {
        var items =  await _httpClient.SendAsync<List<ItemModel>, object>(
            $"http://localhost:5286/basket-bff-controller/items?userId={userId}", 
            HttpMethod.Get, null);
        return items;
    }
    
    private async Task<decimal> AddOrderItems(List<ItemModel> orderItemsModel, int orderId)
    {
        decimal totalPrice = 0;
        foreach (var item in orderItemsModel)
        {
            var stockItem = await _httpClient.SendAsync<ItemEntityModel, object>(
                $"http://localhost:5288/catalog-bff-controller/items/{item.ItemId}", 
                HttpMethod.Get, null);
            if (stockItem == null)
            {
                throw new Exception($"item with id: {item.ItemId} does not exist");
            }
            Console.WriteLine($"*** catalog: {stockItem.ToString()}");
            if (!stockItem.CatalogItem.Price.HasValidNullability())
            {
                throw new Exception($"Price for item with id: {stockItem.Id} was not found");
            }
            decimal subtotal = item.Quantity * stockItem.CatalogItem.Price;
            totalPrice += subtotal;
            await _orderItemRepository.AddItem(new()
            {
                OrderId = orderId,
                ItemId = stockItem.Id,
                Quantity = item.Quantity,
                SubPrice = subtotal
            });
        }

        return totalPrice;
    }

    public async Task<List<CatalogOrder>> GetOrdersByUserId(string userId)
    {
        if (!userId.HasValidNullability())
        {
            throw new Exception($"Unknown user");
        }
        return await _catalogOrderRepository.GetOrdersByUserId(userId);
    }
}