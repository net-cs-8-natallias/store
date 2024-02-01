namespace Basket.Host.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ItemBrandId { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
}