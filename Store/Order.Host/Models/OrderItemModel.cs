namespace Order.Host.Models;

public class OrderItemModel
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public int BrandId { get; set; }
    public decimal Price { get; set; }
    public string? Size { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    
    public override string ToString()
    {
        return $"OrderItem: id: {ItemId}, quantity: {Quantity}, " +
               $"brandId: {BrandId}, price: {Price}, size: {Size}, name: {Name}, " +
               $"image: {Image}";
    }
    
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    } 
}