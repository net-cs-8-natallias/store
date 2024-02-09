namespace Catalog.Host.DbContextData.Entities;

public class ItemCategory
{
    public int Id { get; set; }
    public string? Category { get; set; }
    
    public override string ToString()
    {
        return $"ItemCategory: id: {Id}, category: {Category}";
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