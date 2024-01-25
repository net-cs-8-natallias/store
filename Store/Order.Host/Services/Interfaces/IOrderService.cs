namespace Order.Host.Services.Interfaces;

public interface IOrderService<T>
{
    Task<List<T>> GetItems();
    Task<T> FindById(int id);
    Task<int?> AddItem(T item);
    Task<T> UpdateItem(T item);
    Task<T> RemoveItem(int id);
}