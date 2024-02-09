namespace Catalog.Host.DbContextData.Entities;

public class ItemType
{
    public int Id { get; set; }
    public string? Type { get; set; }
    
    public override string ToString()
    {
        return $"ItemType: id: {Id}, type: {Type}";
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