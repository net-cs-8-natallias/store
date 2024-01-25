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
        CatalogOrder order = new CatalogOrder() { UserId = userId, Date = DateTime.Now.ToShortDateString()};
        int? orderId = await _catalogOrderRepository.AddItem(order);
        List<ItemModel> items = await GetItemsFromBasket();
        decimal totalPrice = await AddOrderItems(items, orderId.Value);
        order.Id = orderId.Value;
        order.TotalPrice = totalPrice;
        await _catalogOrderRepository.UpdateItem(order);
        return orderId.Value;
    }

    private async Task<List<ItemModel>> GetItemsFromBasket()
    {
        var items =  await _httpClient.SendAsync<List<ItemModel>, object>(
            $"http://localhost:5286/basket-bff-controller/items", 
            HttpMethod.Get, null);
        return items;
    }
    
    private async Task<decimal> AddOrderItems(List<ItemModel> orderItemsModel, int orderId)
    {
        decimal totalPrice = 0;
        foreach (var item in orderItemsModel)
        {
            var stockItem = await _httpClient.SendAsync<ItemEntityModel, object>(
                $"http://localhost:5288/catalog-bff-controller/items?catalogItemId={item.ItemId}", 
                HttpMethod.Get, null);
            if (stockItem == null)
            {
                throw new Exception($"item with id: {item.ItemId} does not exist");
            }
            Console.WriteLine($"*** catalog: {stockItem.ToString()}");
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
        return await _catalogOrderRepository.GetOrdersByUserId(userId);
    }
}