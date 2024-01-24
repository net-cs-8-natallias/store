namespace Catalog.Host.DbContextData.Entities;

public class Item
{
    public int Id { get; set; }
    public int CatalogItemId { get; set; }
    public CatalogItem? CatalogItem { get; set; }
}