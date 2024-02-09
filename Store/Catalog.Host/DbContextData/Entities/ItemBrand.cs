namespace Catalog.Host.DbContextData.Entities;

public class ItemBrand
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    
    public override string ToString()
    {
        return $"ItemBrand: id: {Id}, brand: {Brand}";
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