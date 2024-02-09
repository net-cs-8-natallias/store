namespace Catalog.Host.Dto;

public class BrandDto
{
    public string? Brand { get; set; }
    
    public override string ToString()
    {
        return $"BrandDto: brand: {Brand}";
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