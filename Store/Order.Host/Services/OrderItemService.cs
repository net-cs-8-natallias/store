using Order.Host.DbContextData.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderItemService: IOrderService<OrderItem>
{
    private readonly IOrderRepository<OrderItem> _itemRepository;
    private readonly ILogger<OrderItemService> _logger;

    public OrderItemService(IOrderRepository<OrderItem> itemRepository,
        ILogger<OrderItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }
    
    public async Task<List<OrderItem>> GetItems()
    {
        return await _itemRepository.GetItems();
    }

    public async Task<OrderItem> FindById(int id)
    {
        return await _itemRepository.FindById(id);
    }

    public async Task<int?> AddItem(OrderItem item)
    {
        return await _itemRepository.AddItem(item);
    }

    public async Task<OrderItem> UpdateItem(OrderItem item)
    {
        return await _itemRepository.UpdateItem(item);
    }

    public async Task<OrderItem> RemoveItem(int id)
    {
        return await _itemRepository.RemoveItem(id);
    }
}