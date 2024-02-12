using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NJsonSchema.Annotations;

namespace Catalog.Host.Dto;

public class CatalogItemDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(10)]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Brand id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Brand id must be greater than 0")]
    public int ItemBrandId { get; set; }
    
    [Required(ErrorMessage = "Type id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Type id must be greater than 0")]
    public int ItemTypeId { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(1, 1500, ErrorMessage = "Price must be between 1.00 and 1500.00 $")] 
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Image is required")]
    [StringLength(40)]
    public string? Image { get; set; }
    
    [Required(ErrorMessage = "Category id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Category id must be greater than 0")]
    public int ItemCategoryId { get; set; }
    
    [StringLength(100)]
    public string? Description { get; set; }
    
    public override string ToString()
    {
        return $"CatalogItemDto: name: {Name}, " +
               $"itemBrandId: {ItemBrandId}, itemTypeId: {ItemTypeId}, " +
               $"price: {Price}, image: {Image}, itemCategoryId: {ItemCategoryId}, " +
               $"description: {Description}";
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