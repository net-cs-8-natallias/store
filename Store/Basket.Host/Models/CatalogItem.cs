namespace Basket.Host.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ItemBrandId { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }

    public override string ToString()
    {
        return $"CatalogItem: id: {Id}, name: {Name}, " +
               $"itemBrandId: {ItemBrandId}, price: {Price}, image: {Image}";
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