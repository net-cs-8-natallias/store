namespace Catalog.Host.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    
    public override string ToString()
    {
        return $"OrderItem: itemId: {ItemId}, quantity: {Quantity}";
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