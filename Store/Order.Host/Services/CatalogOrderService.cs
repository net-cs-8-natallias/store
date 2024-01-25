using Order.Host.DbContextData.Entities;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class CatalogOrderService: IOrderService<CatalogOrder>
{
    private readonly IOrderRepository<CatalogOrder> _orderRepository;
    private readonly ILogger<CatalogOrderService> _logger;

    public CatalogOrderService(IOrderRepository<CatalogOrder> orderRepository,
        ILogger<CatalogOrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    
    public async Task<List<CatalogOrder>> GetItems()
    {
        return await _orderRepository.GetItems();
    }

    public async Task<CatalogOrder> FindById(int id)
    {
        return await _orderRepository.FindById(id);
    }

    public async Task<int?> AddItem(CatalogOrder item)
    {
        return await _orderRepository.AddItem(item);
    }

    public async Task<CatalogOrder> UpdateItem(CatalogOrder item)
    {
        return await _orderRepository.UpdateItem(item);
    }

    public async Task<CatalogOrder> RemoveItem(int id)
    {
        return await _orderRepository.RemoveItem(id);
    }
}