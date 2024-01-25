namespace Order.Host.Models;

public class ItemEntityModel
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItemEntityModel? CatalogItem { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
}