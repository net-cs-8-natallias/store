namespace Basket.Host.Models;

public class ItemModel
{
    public int Id { get; set; }
    public CatalogItem? CatalogItem { get; set; }
    public string? Size { get; set; }
    public int Quantity { get; set; }
    
    public override string ToString()
    {
        return $"ItemNodel: id: {Id}, catalogItem: {CatalogItem.ToString()}, " +
               $"size: {Size}, quantity: {Quantity}";
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