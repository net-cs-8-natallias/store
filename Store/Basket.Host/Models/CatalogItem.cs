namespace Basket.Host.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ItemBrandId { get; set; }
    public int ItemTypeId { get; set; }
    public decimal Price { get; set; }
}