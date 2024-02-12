using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Dto;

public class CategoryDto
{
    [Required]
    [StringLength(25)]
    public string? Category { get; set; }
    
    public override string ToString()
    {
        return $"CategoryDto:, category: {Category}";
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