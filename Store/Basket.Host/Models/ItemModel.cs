namespace Basket.Host.Models;

public class ItemModel
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItem? CatalogItem { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
}