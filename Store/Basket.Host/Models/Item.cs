namespace Basket.Host.Models;

public class Item: IItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}