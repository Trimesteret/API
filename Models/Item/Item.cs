namespace API.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? BarCode { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public DateTime ExpirationDate { get; set; }
}
