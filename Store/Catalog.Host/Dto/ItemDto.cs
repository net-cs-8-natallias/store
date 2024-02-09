namespace Catalog.Host.Dto;

public class ItemDto
{
    public int CatalogItemId { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
    
    public override string ToString()
    {
        return $"ItemDto: catalogItemId: {CatalogItemId}, " +
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