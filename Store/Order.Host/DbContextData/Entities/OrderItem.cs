namespace Order.Host.DbContextData.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public CatalogOrder? Order { get; set; }
    public int ItemId { get; set; }
    public decimal SubPrice { get; set; }
    public int Quantity { get; set; }
    
    public override string ToString()
    {
        return $"OrderItem: id: {Id}, orderId: {OrderId}, itemId: {ItemId}, " +
               $"subPrice: {SubPrice}, quantity: {Quantity}";
    }
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}