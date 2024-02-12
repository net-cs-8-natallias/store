namespace Order.Host.Services.Interfaces;

public interface IOrderService<T, D>
{
    Task<List<T>> GetItems();
    Task<T> FindById(int id);
    Task<int?> AddItem(D item);
    Task<T> UpdateItem(T item);
    Task<T> RemoveItem(int id);
}