namespace Order.Host.Models;

public class CatalogItemEntityModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ItemBrandId { get; set; }
    public int ItemTypeId { get; set; }
    public decimal Price { get; set; }
}