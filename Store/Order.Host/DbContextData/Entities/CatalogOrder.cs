namespace Order.Host.DbContextData.Entities;

public class CatalogOrder
{
    public int Id { get; set; }
    public string? Date { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? UserId { get; set; }
}