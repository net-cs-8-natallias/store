namespace Basket.Host.Models;

public class ItemModel
{
    public int Id { get; set; }
    public CatalogItem? CatalogItem { get; set; }
    public string? Size { get; set; }
    public int Quantity { get; set; }
}