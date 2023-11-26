namespace API.Models.Items;

public class Liquor: Item
{
    public Liquor(string name, string ean, int quantity, double price)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        // this.ImageUrl = imageUrl;
    }
}
