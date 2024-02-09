namespace Catalog.Host.DbContextData.Entities;

public class Item
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItem? CatalogItem { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
    
    public override string ToString()
    {
        return $"Item: id: {Id}, catalogItemId: {CatalogItemId}, " +
               $"quantity: {Quantity}, size: {Size}";
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