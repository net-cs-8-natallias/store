namespace Basket.Host.Models;

public class Item
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    
    public override string ToString()
    {
        return $"Item: itemId: {ItemId}, quantity: {Quantity}";
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

