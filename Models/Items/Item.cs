using API.Enums;

namespace API.Models.Items;

public abstract class Item
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Ean { get; protected set; }
    public int Quantity { get; protected set; }
    public double Price { get; protected set; }
    // public string ImageUrl { get; protected set; }
    public string Description { get; protected set; }
    public ItemType? Type { get; protected set; }
    public DateTime? ExpirationDate { get; protected set; }
    public WineType? WineType { get; protected set; }
}


