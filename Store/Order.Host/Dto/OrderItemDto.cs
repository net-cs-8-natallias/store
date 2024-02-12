namespace Order.Host.Dto;

public class OrderItemDto
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public decimal SubPrice { get; set; }
    public int Quantity { get; set; }
    
    public override string ToString()
    {
        return $"OrderItemDto: orderId: {OrderId}, itemId: {ItemId}, " +
               $"subPrice: {SubPrice}, quantity: {Quantity}";
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