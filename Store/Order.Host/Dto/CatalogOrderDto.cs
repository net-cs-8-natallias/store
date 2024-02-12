namespace Order.Host.Dto;

public class CatalogOrderDto
{
    public string? Date { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? UserId { get; set; }
    
    public override string ToString()
    {
        return $"CatalogOrderDto: date: {Date}, totalQuantity: {TotalQuantity}, " +
               $"totalPrice: {TotalPrice}, userId: {UserId}";
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