namespace Catalog.Host.Dto;

public class CatalogItemDto
{
    public string? Name { get; set; }
    public int ItemBrandId { get; set; }
    public int ItemTypeId { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public int ItemCategoryId { get; set; }
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