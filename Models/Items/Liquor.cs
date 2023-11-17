namespace API.Models.Items;

public class Liquor: Item
{
    public string? LiquorType { get; set; }

    public Liquor(string name, string ean, int quantity, float price, string imageUrl, DateTime expirationDate, string liquorType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.ExpirationDate = expirationDate;
        this.LiquorType = liquorType;
    }
}
