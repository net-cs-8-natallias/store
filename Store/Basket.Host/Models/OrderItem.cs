namespace Basket.Host.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int BrandId { get; set; }
    public int TypeId { get; set; }
    public decimal Price { get; set; }
    public string? Size { get; set; }
}