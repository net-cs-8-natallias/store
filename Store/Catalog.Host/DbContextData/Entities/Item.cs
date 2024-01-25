namespace Catalog.Host.DbContextData.Entities;

public class Item
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItem? CatalogItem { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
}