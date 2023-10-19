namespace API.Models;

public class Order
{
    public int Id { get; set; }
    public Item[]? Items { get; set; }
}
