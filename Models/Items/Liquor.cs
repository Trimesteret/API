namespace API.Models.Items;

public class Liquor: Item
{
    public string LiquorType { get; set; }

    public Liquor(string name, string ean, int quantity, float price, string imageUrl, string liquorType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.LiquorType = liquorType;
    }
}
