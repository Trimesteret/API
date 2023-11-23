namespace API.Models.Items;

public class Liquor: Item
{
    public Liquor(string name, string ean, int quantity, float price, string imageUrl, string description, float mass)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.Description = description;
        this.Mass = mass;
    }
}
