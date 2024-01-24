namespace Order.Host.DbContextData.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public CatalogOrder? Order { get; set; }
    public int ItemId { get; set; }
    public decimal SubPrice { get; set; }
    public int Quantity { get; set; }
}