namespace API.Models.Items;

public abstract class Item
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Ean { get; protected set; }
    public int Quantity { get; protected set; }
    public float Price { get; protected set; }
    public string ImageUrl { get; protected set; }  
    public DateTime? ExpirationDate { get; protected set; }
}


