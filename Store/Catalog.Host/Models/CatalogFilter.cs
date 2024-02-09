namespace Catalog.Host.Models;

public class CatalogFilter
{
    public int Brand { get; set; }
    public int Type { get; set; }
    public int Category { get; set; }
    
    public override string ToString()
    {
        return $"CatalogFilter: brand: {Brand}, type: {Type}, " +
               $"category: {Category}";
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