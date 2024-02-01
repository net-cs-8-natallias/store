namespace Basket.Host.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string? Size { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public int StockQuantity { get; set; }
}