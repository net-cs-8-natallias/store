using Order.Host.DbContextData.Entities;
using Order.Host.Models;

namespace Order.Host.Services.Interfaces;

public interface IOrderBffService
{
    Task<int?> CreateOrder(List<OrderItemModel> i, string userId);
    Task<List<CatalogOrder>> GetOrdersByUserId(string userId);
}