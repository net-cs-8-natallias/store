namespace Catalog.Host.Dto;

public class TypeDto
{
    public string? Type { get; set; }
    
    public override string ToString()
    {
        return $"TypeDto: type: {Type}";
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