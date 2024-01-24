namespace Catalog.Host.DbContextData.Entities;

public class Stock
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Item? Item { get; set; }
    public int Quantity { get; set; }
    public string? Size { get; set; }
}